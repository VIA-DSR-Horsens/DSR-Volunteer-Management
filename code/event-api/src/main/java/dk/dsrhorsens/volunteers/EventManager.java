package dk.dsrhorsens.volunteers;

import com.google.protobuf.Empty;
import dk.dsrhorsens.volunteers.proto.*;
import io.grpc.stub.StreamObserver;
import org.lognet.springboot.grpc.GRpcService;

@GRpcService
public class EventManager extends EventServiceGrpc.EventServiceImplBase {
	private <U> void respondRequest(U response, StreamObserver<U> responseObserver) {
		responseObserver.onNext(response);
		responseObserver.onCompleted();
	}
	@Override public void createEvent(EventInfo request, StreamObserver<EventId> responseObserver) {
	}

	@Override public void retrieveEvent(EventId request, StreamObserver<IndexedEvent> responseObserver) {
		super.retrieveEvent(request, responseObserver);
	}

	@Override public void updateEvent(IndexedEvent request, StreamObserver<Empty> responseObserver) {
		super.updateEvent(request, responseObserver);
	}

	@Override public void deleteEvent(EventId request, StreamObserver<Empty> responseObserver) {
		super.deleteEvent(request, responseObserver);
	}

	@Override public void listEvents(Empty request, StreamObserver<EventList> responseObserver) {
		super.listEvents(request, responseObserver);
	}
}
