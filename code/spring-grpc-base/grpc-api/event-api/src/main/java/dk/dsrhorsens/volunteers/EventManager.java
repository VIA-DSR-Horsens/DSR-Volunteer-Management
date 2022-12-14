package dk.dsrhorsens.volunteers;

import com.google.protobuf.Empty;
import dk.dsrhorsens.volunteers.data.EventData;
import dk.dsrhorsens.volunteers.proto.event.*;
import io.grpc.Status;
import io.grpc.stub.StreamObserver;
import org.jetbrains.annotations.NotNull;
import org.lognet.springboot.grpc.GRpcService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatusCode;
import org.springframework.web.client.HttpClientErrorException;

import java.util.ArrayList;
import java.util.List;

/**
 * The EventManager class is responsible for handling creation, deletion and modification of events. It is an
 * implementation of a gRPC service. The service is defined in the proto file located in the proto/event module.
 */
@GRpcService
public class EventManager extends EventServiceGrpc.EventServiceImplBase {
	@Autowired DatabaseAPI databaseAPI;

	private <U> void respondRequest(@NotNull U response, StreamObserver<U> responseObserver) {
		responseObserver.onNext(response);
		responseObserver.onCompleted();
	}

	@Override public void createEvent(EventInfo gRPCRequest, StreamObserver<EventId> responseObserver) {
		EventData restRequest = transform(gRPCRequest);
		//Send the REST request to the database and store the expected response.
		EventData restResponse = null;
		try {
			restResponse = databaseAPI.post("/Event/", restRequest, EventData.class);
		} catch (HttpClientErrorException restError) {
			HttpStatusCode statusCode = restError.getStatusCode();
			switch (statusCode.value()) {
				case 400 ->
						responseObserver.onError(Status.INTERNAL.withDescription("Failed to parse data").asRuntimeException());
				case 404 -> responseObserver.onError(
						Status.NOT_FOUND.withDescription("The requested resource was not found").asRuntimeException());
				case 500 -> responseObserver.onError(
						Status.INTERNAL.withDescription("An internal error occurred fetching the data").asRuntimeException());
				default -> responseObserver.onError(
						Status.UNKNOWN.withDescription("An unexpected error occurred").asRuntimeException());
			}
			return;
		}
		long eventID = restResponse != null ? Long.parseLong(restResponse.getId()) : -1;
		EventId gRPCResponse;
		if (eventID == -1) {
			responseObserver.onError(Status.INTERNAL.withDescription("Failed to create event").asRuntimeException());
		}
		gRPCResponse = EventId.newBuilder().setEventId(eventID).build();
		respondRequest(gRPCResponse, responseObserver);
	}

	@Override public void retrieveEvent(EventId request, StreamObserver<IndexedEvent> responseObserver) {
		EventData restResponse = null;
		try {
			restResponse = databaseAPI.get("/Event/" + request.getEventId(), EventData.class);
		} catch (HttpClientErrorException restError) {
			HttpStatusCode statusCode = restError.getStatusCode();
			switch (statusCode.value()) {
				case 404 -> responseObserver.onError(
						Status.NOT_FOUND.withDescription("The requested resource was not found").asRuntimeException());
				case 500 -> responseObserver.onError(
						Status.INTERNAL.withDescription("An internal error occurred fetching the data").asRuntimeException());
				default -> responseObserver.onError(
						Status.UNKNOWN.withDescription("An unexpected error occurred").asRuntimeException());
			}
			return;
		}
		IndexedEvent gRPCResponse = transform(restResponse);
		respondRequest(gRPCResponse, responseObserver);
	}

	@Override public void updateEvent(IndexedEvent request, StreamObserver<Empty> responseObserver) {
		responseObserver.onError(Status.UNIMPLEMENTED.withDescription("Unable to modify data").asRuntimeException());
//		respondRequest(Empty.getDefaultInstance(), responseObserver);
	}

	@Override public void deleteEvent(EventId request, StreamObserver<Empty> responseObserver) {
		try {
			databaseAPI.delete("/Event/" + request.getEventId());
		} catch (HttpClientErrorException restError) {
			HttpStatusCode statusCode = restError.getStatusCode();
			switch (statusCode.value()) {
				case 404 -> responseObserver.onError(
						Status.NOT_FOUND.withDescription("The requested resource was not found").asRuntimeException());
				case 500 -> responseObserver.onError(
						Status.INTERNAL.withDescription("An internal error occurred fetching the data").asRuntimeException());
				default -> responseObserver.onError(
						Status.UNKNOWN.withDescription("An unexpected error occurred").asRuntimeException());
			}
			return;
		}
		respondRequest(Empty.getDefaultInstance(), responseObserver);
	}

	@Override public void listEvents(Empty request, StreamObserver<EventList> responseObserver) {
		responseObserver.onError(
				Status.UNIMPLEMENTED.withDescription("Unable to fetch a list from database").asRuntimeException());
		respondRequest(null, responseObserver);
	}

	private IndexedEvent transform(EventData data) {
		if (data == null) {
			return null;
		}
		IndexedEvent.Builder builder = IndexedEvent.newBuilder();
		long id = Long.parseLong(data.getId());
		EventId.Builder eventIdBuilder = EventId.newBuilder();
		eventIdBuilder.setEventId(id);
		EventInfo.Builder eventInfoBuilder = EventInfo.newBuilder();
		eventInfoBuilder.setEventName(data.getName());
		eventInfoBuilder.setEventDate(data.getDate());
		eventInfoBuilder.setStartTime(data.getStartTime());
		eventInfoBuilder.setEndTime(data.getEndTime());
		eventInfoBuilder.setLocation(data.getLocation());
		builder.setEventId(eventIdBuilder.build());
		builder.setEventInfo(eventInfoBuilder.build());
		return builder.build();
	}

	private EventData transform(IndexedEvent data) {
		if (data == null) {
			return null;
		}
		EventData builder = new EventData();
		builder.setId(String.valueOf(data.getEventId().getEventId()));
		builder.setName(data.getEventInfo().getEventName());
		builder.setDate(data.getEventInfo().getEventDate());
		builder.setStartTime(data.getEventInfo().getStartTime());
		builder.setEndTime(data.getEventInfo().getEndTime());
		builder.setLocation(data.getEventInfo().getLocation());
		return builder;
	}

	private EventData transform(EventInfo data) {
		if (data == null) {
			return null;
		}
		EventData builder = new EventData();
		builder.setId(null);
		builder.setName(data.getEventName());
		builder.setDate(data.getEventDate());
		builder.setStartTime(data.getStartTime());
		builder.setEndTime(data.getEndTime());
		builder.setLocation(data.getLocation());
		List<String> managers = new ArrayList<>();
		managers.add(String.valueOf(data.getMainManager().getManagerId()));
		builder.setManagers(managers);
		return builder;
	}
}
