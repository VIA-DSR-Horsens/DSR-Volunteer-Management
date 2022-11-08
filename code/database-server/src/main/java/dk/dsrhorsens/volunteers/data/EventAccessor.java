package dk.dsrhorsens.volunteers.data;

import dk.dsrhorsens.volunteers.proto.EventId;
import dk.dsrhorsens.volunteers.proto.EventRequest;
import dk.dsrhorsens.volunteers.proto.EventResponse;
import dk.dsrhorsens.volunteers.proto.OperationStatus;

public interface EventAccessor {
	EventId 				createEvent(EventRequest request);
	EventResponse		retrieveEvent(EventId id);
	OperationStatus updateEvent(EventRequest request);
	OperationStatus deleteEvent(EventId id);
}
