package dk.dsrhorsens.volunteers.data;

import dk.dsrhorsens.volunteers.DatabaseServer;
import dk.dsrhorsens.volunteers.proto.EventId;
import dk.dsrhorsens.volunteers.proto.EventRequest;
import dk.dsrhorsens.volunteers.proto.EventResponse;
import dk.dsrhorsens.volunteers.proto.OperationStatus;
import org.springframework.jdbc.core.JdbcTemplate;

public class EventDatabaseAccessor implements EventAccessor {
	@Override public EventId createEvent(EventRequest request) {
		return null;
	}

	@Override public EventResponse retrieveEvent(EventId id) {
		return null;
	}

	@Override public OperationStatus updateEvent(EventRequest request) {
		return null;
	}

	@Override public OperationStatus deleteEvent(EventId id) {
		return null;
	}
}
