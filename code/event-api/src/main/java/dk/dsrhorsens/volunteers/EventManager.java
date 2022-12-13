package dk.dsrhorsens.volunteers;

import com.google.protobuf.Empty;
import dk.dsrhorsens.volunteers.proto.event.*;
import io.grpc.stub.StreamObserver;
import org.jetbrains.annotations.NotNull;
import org.lognet.springboot.grpc.GRpcService;

/**
 * The EventManager class is responsible for handling creation, deletion and modification of events.
 * It is an implementation of a gRPC service. The service is defined in the proto file located in the
 * proto/event module.
 */
@GRpcService
public class EventManager extends EventServiceGrpc.EventServiceImplBase {
	private <U> void respondRequest(@NotNull U response, StreamObserver<U> responseObserver) {
		responseObserver.onNext(response);
		responseObserver.onCompleted();
	}

	@Override public void createEvent(EventInfo request, StreamObserver<EventId> responseObserver) {
		respondRequest(null, responseObserver);
	}

	@Override public void retrieveEvent(EventId request, StreamObserver<IndexedEvent> responseObserver) {
		respondRequest(null, responseObserver);
	}

	@Override public void updateEvent(IndexedEvent request, StreamObserver<Empty> responseObserver) {
		respondRequest(Empty.getDefaultInstance(), responseObserver);
	}

	@Override public void deleteEvent(EventId request, StreamObserver<Empty> responseObserver) {
		respondRequest(Empty.getDefaultInstance(), responseObserver);
	}

	@Override public void listEvents(Empty request, StreamObserver<EventList> responseObserver) {
		respondRequest(null, responseObserver);
	}
}
