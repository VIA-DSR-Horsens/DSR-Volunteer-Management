package dk.dsrhorsens.volunteers.data;

import com.google.protobuf.Empty;
import dk.dsrhorsens.volunteers.DataManager;
import dk.dsrhorsens.volunteers.proto.EventId;
import dk.dsrhorsens.volunteers.proto.EventInfo;
import dk.dsrhorsens.volunteers.proto.IndexedEvent;

import java.util.ArrayList;

public class EventDatabaseAccessor implements EventAccessor {

	@Override public EventId createEvent(EventInfo request) {
		DataManager.getInstance().database.update("INSERT INTO events (name, starttime, endtime) VALUES (?, ?, ?)",
																							request.getEventName(), request.getStartTime(), request.getEndTime());
		return EventId.newBuilder()
				.setId(DataManager.getInstance().database.queryForObject("SELECT currval('events_id_seq')", Integer.class))
				.build();
	}

	@Override public IndexedEvent retrieveEvent(EventId identifier) {
		return DataManager.getInstance().database.queryForObject("SELECT * FROM events WHERE id = ?",
																														 (rs, rowNum) -> IndexedEvent.newBuilder().setEventId(
																																		 EventId.newBuilder().setId(rs.getInt("id")).build())
																																 .setEventInfo(EventInfo.newBuilder()
																																									 .setEventName(rs.getString("name"))
																																									 .setStartTime(
																																											 rs.getString("starttime"))
																																									 .setEndTime(rs.getString("endtime"))
																																									 .build()).build(),
																														 identifier.getId());
	}

	@Override public Empty updateEvent(IndexedEvent request) {
		DataManager.getInstance().database.update("UPDATE events SET name = ?, starttime = ?, endtime = ? WHERE id = ?",
																							request.getEventInfo().getEventName(),
																							request.getEventInfo().getStartTime(),
																							request.getEventInfo().getEndTime(), request.getEventId().getId());
		return Empty.getDefaultInstance();
	}

	@Override public Empty deleteEvent(EventId identifier) {
		DataManager.getInstance().database.update("DELETE FROM events WHERE id = ?", identifier.getId());
		return Empty.getDefaultInstance();
	}

	@Override public IndexedEvent[] listEvents() {
		ArrayList<IndexedEvent> events = new ArrayList<>(DataManager.getInstance().database.query("SELECT * FROM events",
																																															(rs, rowNum) -> IndexedEvent.newBuilder()
																																																	.setEventId(
																																																			EventId.newBuilder()
																																																					.setId(
																																																							rs.getInt(
																																																									"id"))
																																																					.build())
																																																	.setEventInfo(
																																																			EventInfo.newBuilder()
																																																					.setEventName(
																																																							rs.getString(
																																																									"name"))
																																																					.setStartTime(
																																																							rs.getString(
																																																									"starttime"))
																																																					.setEndTime(
																																																							rs.getString(
																																																									"endtime"))
																																																					.build())
																																																	.build()));
		return events.toArray(new IndexedEvent[0]);
	}
}
