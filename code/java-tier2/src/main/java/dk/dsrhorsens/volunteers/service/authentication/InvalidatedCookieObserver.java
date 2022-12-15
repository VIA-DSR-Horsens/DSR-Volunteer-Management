package dk.dsrhorsens.volunteers.service.authentication;

import dk.dsrhorsens.volunteers.proto.authentication.InvalidatedCookie;
import dk.dsrhorsens.volunteers.service.AuthenticationCache;
import io.grpc.stub.StreamObserver;

/**
 * Logic implementation that listens to invalidated cookies
 */
public class InvalidatedCookieObserver implements StreamObserver<InvalidatedCookie> {
    @Override
    public void onNext(InvalidatedCookie invalidatedCookie) {
        AuthenticationCache.invalidateAuthenticationCookie(invalidatedCookie.getAuthenticationCookie());
    }

    @Override
    public void onCompleted() {
        return;
    }

    @Override
    public void onError(Throwable throwable) {
        System.out.println("Error while listening to invalidated authentication cookies! " + throwable.getMessage());
    }
}
