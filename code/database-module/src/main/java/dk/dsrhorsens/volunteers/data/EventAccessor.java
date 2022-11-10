package dk.dsrhorsens.volunteers.data;

import com.google.protobuf.Empty;
import dk.dsrhorsens.volunteers.proto.EventId;
import dk.dsrhorsens.volunteers.proto.EventInfo;
import dk.dsrhorsens.volunteers.proto.IndexedEvent;

public interface EventAccessor {
	EventId createEvent(EventInfo request);
	IndexedEvent retrieveEvent(EventId identifier);
	Empty 					updateEvent(IndexedEvent request);
	Empty 					deleteEvent(EventId identifier);
	IndexedEvent[] 	listEvents();
}
