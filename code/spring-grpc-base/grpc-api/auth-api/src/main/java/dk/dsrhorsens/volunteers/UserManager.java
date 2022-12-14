package dk.dsrhorsens.volunteers;

import dk.dsrhorsens.volunteers.data.AdminData;
import dk.dsrhorsens.volunteers.data.ManagerData;
import dk.dsrhorsens.volunteers.data.ShiftData;
import dk.dsrhorsens.volunteers.data.UserData;
import dk.dsrhorsens.volunteers.proto.authentication.CookieVerification;
import dk.dsrhorsens.volunteers.proto.authentication.VerificationResponse;
import dk.dsrhorsens.volunteers.proto.user.AuthenticationRequest;
import dk.dsrhorsens.volunteers.proto.user.LoggedInUserInfo;
import dk.dsrhorsens.volunteers.proto.user.NewUser;
import dk.dsrhorsens.volunteers.proto.user.UserServiceGrpc;
import io.grpc.Status;
import io.grpc.stub.StreamObserver;
import org.jetbrains.annotations.NotNull;
import org.lognet.springboot.grpc.GRpcService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.client.HttpClientErrorException;

/**
 * The UserManager class is responsible for handling users sessions. It is an implementation of a gRPC service. The
 * service is defined in the proto file located in the proto/user module.
 */
@GRpcService
public class UserManager extends UserServiceGrpc.UserServiceImplBase {
	@Autowired DatabaseAPI databaseAPI;

	private <U> void respondRequest(@NotNull U response, StreamObserver<U> responseObserver) {
		responseObserver.onNext(response);
		responseObserver.onCompleted();
	}

	/**
	 * The login method is responsible for handling login requests. It takes a LoginRequest as input and returns the info
	 * of the corresponding user.
	 *
	 * @param request          the LoginRequest containing the username and password of the user.
	 * @param responseObserver the StreamObserver that will be used to send the response.
	 */
	@Override public void authenticate(AuthenticationRequest request, StreamObserver<LoggedInUserInfo> responseObserver) {
		try (AuthClient authClient = new AuthClient()) {
			String authCookie = request.getAuthenticationCookie();
			VerificationResponse verificationResponse = authClient.verifyCookie(
					CookieVerification.newBuilder().setAuthenticationCookie(authCookie).build());
			if (verificationResponse.getValidCookie()) {
				LoggedInUserInfo response = getUserInfo(verificationResponse.getUuid());
				respondRequest(response, responseObserver);
			}
		}
	}

	/**
	 * The signup method is responsible for requesting
	 *
	 * @param request the new user requested
	 * @param responseObserver the StreamObserver that will be used to send the response.
	 */
	@Override public void signUp(NewUser request, StreamObserver<LoggedInUserInfo> responseObserver) {
		responseObserver.onError(Status.UNIMPLEMENTED.asRuntimeException());
		/*try (AuthClient authClient = new AuthClient()) {
			String full_name = request.getFullName();
			String email = request.getEmail();
			String cookie = request.getAuthenticationCookie();
		}*/
	}

	private LoggedInUserInfo getUserInfo(long uuid) {
		UserData data = databaseAPI.get("/Volunteer/" + uuid, UserData.class);
		return transform(data);
	}

	private LoggedInUserInfo transform(UserData data) {
		LoggedInUserInfo.Builder builder = LoggedInUserInfo.newBuilder();
		builder.setEmail(data.getEmail());
		builder.setFullName(data.getFullName());
		builder.setShiftsTaken(Long.parseLong(data.getShiftsTaken()));
		builder.setRating(Long.parseLong(data.getRating()));
		try {
			ManagerData managerData = databaseAPI.get("/Manager/Volunteer/" + data.getVolunteerId(), ManagerData.class);
			if (!managerData.getManagerId().equals("")) {
				builder.setIsManager(true);
			}
			AdminData adminData = databaseAPI.get("/Administrator/Volunteer/" + data.getVolunteerId(), AdminData.class);
			if (!adminData.getAdministratorId().equals("")) {
				builder.setIsAdministrator(true);
			}
		} catch (HttpClientErrorException ignored) {}
		ShiftData[] shifts = databaseAPI.get("/Volunteer/" + data.getVolunteerId() + "/Shifts", ShiftData[].class);
		for (int i = 0; i < shifts.length; i++) {
			builder.setShifts(i, Long.parseLong(shifts[i].getShiftId()));
		}
		//TODO: No way to set managed events for now
		/*
		for (int i = 0; i < events.length; i++) {
			builder.setShifts(i, Long.parseLong(shifts[i].getShiftId()));
		}
		*/
		return builder.build();
	}
}

