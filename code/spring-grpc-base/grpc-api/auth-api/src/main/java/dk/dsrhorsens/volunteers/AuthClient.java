package dk.dsrhorsens.volunteers;

import dk.dsrhorsens.volunteers.proto.authentication.*;
import io.grpc.ManagedChannel;
import io.grpc.ManagedChannelBuilder;
import io.grpc.internal.Stream;
import org.springframework.beans.factory.annotation.Value;

import java.io.Closeable;
import java.io.IOException;
import java.util.Iterator;

public class AuthClient implements AutoCloseable {
	/**
	 * Closes this stream and releases any system resources associated with it.
	 */
	@Override public void close() {
		authenticationService = null;
		gRPCConnection.shutdown();
	}

	private final ManagedChannel gRPCConnection;
	private AuthenticationServiceGrpc.AuthenticationServiceBlockingStub authenticationService;
	@Value("${dsr.auth.host}") private String host;
	@Value("${dsr.auth.port}") private int port;

	public AuthClient() {
		gRPCConnection = ManagedChannelBuilder.forAddress(host, port).usePlaintext().build();
		authenticationService = AuthenticationServiceGrpc.newBlockingStub(gRPCConnection);
	}

	public VerificationResponse verifyCookie(CookieVerification unverified) {
		return authenticationService.verifyCookie(unverified);
	}

	public Iterator<InvalidatedCookie> listen() {
				return authenticationService.startListening(EmptyMessage.newBuilder().build());
	}
}
