package dk.dsrhorsens.volunteers.service.user;

import dk.dsrhorsens.volunteers.DsrVolunteerApplication;
import dk.dsrhorsens.volunteers.proto.user.AuthenticationRequest;
import dk.dsrhorsens.volunteers.proto.user.LoggedInUserInfo;
import dk.dsrhorsens.volunteers.proto.user.UserServiceGrpc;
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
                responseObserver.onError(new Throwable("Invalid authentication cookie!"));
            }

        } catch (Exception e) {
            System.out.println("Error while getting user's UUID from authentication cookie! "+e.getMessage());
            responseObserver.onError(new Throwable("Error while getting user's UUID from authentication cookie! "+e.getMessage()));
        }
    }
}
