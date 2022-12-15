package dk.dsrhorsens.volunteers.service.authentication;

import dk.dsrhorsens.volunteers.proto.authentication.*;
import dk.dsrhorsens.volunteers.service.AuthenticationCache;
import io.grpc.ManagedChannel;
import io.grpc.ManagedChannelBuilder;
import io.grpc.StatusRuntimeException;
import io.grpc.stub.StreamObserver;

import java.util.concurrent.TimeUnit;

/**
 * Implements the java server authentication client to communicate with authentication server
 */
public class AuthenticationClientImpl {
    private final ManagedChannel managedChannel;

    /**
     * Create a new java authentication client
     * @param address The authentication server to connect to
     * @param port The authentication server's port
     */
    public AuthenticationClientImpl(String address, int port) {
        managedChannel = ManagedChannelBuilder
                .forAddress(address, port)
                .usePlaintext()
                .build();

        listenToInvalidatedCookies();
    }

    /**
     * Start listening to authentication server's authentication cookie invalidations
     */
    private void listenToInvalidatedCookies() {
        var authenticationServiceStub = AuthenticationServiceGrpc.newStub(managedChannel);

        // listening to authentication cookie invalidations in the background
        new Thread(() -> {
            // announcement message
            EmptyMessage announceMsg = EmptyMessage.newBuilder()
                    .setImEmpty(true)
                    .build();

            // starts listening to stream of invalidated cookies
            StreamObserver<InvalidatedCookie> invalidatedObserver = new InvalidatedCookieObserver();
            try {
                authenticationServiceStub
                        .withDeadlineAfter(9999, TimeUnit.DAYS)
                        .startListening(announceMsg, invalidatedObserver);
            } catch (StatusRuntimeException e) {
                System.out.println("gRPC listening to invalidated cookies failed: "+e.getStatus());
            }
        }).start();
    }

    /**
     * Verify an authentication cookie
     * @param authenticationCookie The authentication cookie to verify
     * @return UUID of the user if the authentication cookie was valid, null otherwise
     */
    public Long verifyCookie(String authenticationCookie) {
        var existingUuid = AuthenticationCache.getUuidFromAuthenticationCookie(authenticationCookie);
        if (existingUuid != null) {
            return existingUuid;
        }

        // cookie not saved in cache
        var authenticationServiceStub = AuthenticationServiceGrpc.newBlockingStub(managedChannel);

        // the cookie to verify
        CookieVerification message = CookieVerification.newBuilder()
                .setAuthenticationCookie(authenticationCookie)
                .build();
        VerificationResponse response = authenticationServiceStub.verifyCookie(message);

        // saving cookie in cache
        if (!response.getValidCookie()) {
            // invalid cookie
            return null;
        }
        AuthenticationCache.saveAuthenticationCookie(response.getAuthenticationCookie(), response.getUuid());
        return response.getUuid();
    }
}
