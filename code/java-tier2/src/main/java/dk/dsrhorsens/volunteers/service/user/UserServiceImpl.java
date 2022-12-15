package dk.dsrhorsens.volunteers.service.user;

import dk.dsrhorsens.volunteers.DsrVolunteerApplication;
import dk.dsrhorsens.volunteers.proto.user.AuthenticationRequest;
import dk.dsrhorsens.volunteers.proto.user.LoggedInUserInfo;
import dk.dsrhorsens.volunteers.proto.user.NewUser;
import dk.dsrhorsens.volunteers.proto.user.UserServiceGrpc;
import dk.dsrhorsens.volunteers.service.user.dto.Volunteer;
import io.grpc.stub.StreamObserver;
import org.lognet.springboot.grpc.GRpcService;

@GRpcService
public class UserServiceImpl extends UserServiceGrpc.UserServiceImplBase {
    @Override
    public void authenticate(AuthenticationRequest request, StreamObserver<LoggedInUserInfo> responseObserver) {
        System.out.println("Received authentication request!" + request.toString());
        try {
            Long userUuid = DsrVolunteerApplication.authenticationClient.verifyCookie(request.getAuthenticationCookie());
            if (userUuid == null) {
                responseObserver.onError(new Throwable("401"));
                return;
            }

            try {
                Volunteer volunteer = VolunteerHttp.getVolunteer(userUuid);

                var userBuilder = LoggedInUserInfo.newBuilder()
                        .setShiftsTaken(Long.parseLong(volunteer.getShiftsTaken()))
                        .setEmail(volunteer.getEmail())
                        .setFullName(volunteer.getFullName())
                        .setRating(Long.parseLong(volunteer.getRating()))
                        .setIsAdministrator(false)
                        .setIsManager(false);

                // parsing shift ids and adding them to volunteer
                for (String shift: volunteer.getShifts()) {
                    userBuilder.addShifts(Long.parseLong(shift));
                }

                // sending data
                var userInfo = userBuilder.build();
                responseObserver.onNext(userInfo);
                responseObserver.onCompleted();
            } catch (Exception e) {
                System.out.println("Error while getting user data!");
                responseObserver.onError(new Throwable(e.getMessage()));
            }
        } catch (Exception e) {
            System.out.println("Error while getting user's UUID from authentication cookie! "+e.getMessage());
            responseObserver.onError(new Throwable("500 Error while getting user's UUID from authentication cookie! "+e.getMessage()));
        }
    }

    @Override
    public void signUp(NewUser request, StreamObserver<LoggedInUserInfo> responseObserver) {
        System.out.println("Received sign up request!" + request.toString());
        try {
            Long userUuid = DsrVolunteerApplication.authenticationClient.verifyCookie(request.getAuthenticationCookie());
            if (userUuid == null) {
                responseObserver.onError(new Throwable("401"));
                return;
            }

            try {
                var parsedRequest = new Volunteer();
                parsedRequest.setEmail(request.getEmail());
                parsedRequest.setVolunteerId(""+userUuid);
                parsedRequest.setFullName(request.getFullName());
                Volunteer volunteer = VolunteerHttp.createVolunteer(parsedRequest);

                var userBuilder = LoggedInUserInfo.newBuilder()
                        .setShiftsTaken(Long.parseLong(volunteer.getShiftsTaken()))
                        .setEmail(volunteer.getEmail())
                        .setFullName(volunteer.getFullName())
                        .setRating(Long.parseLong(volunteer.getRating()))
                        .setIsAdministrator(false)
                        .setIsManager(false);

                // parsing shift ids and adding them to volunteer
                for (String shift: volunteer.getShifts()) {
                    userBuilder.addShifts(Long.parseLong(shift));
                }

                // sending data
                var userInfo = userBuilder.build();
                responseObserver.onNext(userInfo);
                responseObserver.onCompleted();
            } catch (Exception e) {
                System.out.println("Error while getting user data!");
                responseObserver.onError(new Throwable(e.getMessage()));
            }
        } catch (Exception e) {
            System.out.println("Error while getting user's UUID from authentication cookie! "+e.getMessage());
            responseObserver.onError(new Throwable("500 Error while getting user's UUID from authentication cookie! "+e.getMessage()));
        }

        super.signUp(request, responseObserver);
    }
}
