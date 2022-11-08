package dk.dsrhorsens.volunteers;

import dk.dsrhorsens.volunteers.proto.EventId;
import dk.dsrhorsens.volunteers.proto.EventResponse;
import org.slf4j.Logger;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.jdbc.core.JdbcTemplate;

import java.util.ArrayList;

@SpringBootApplication
public class DatabaseServer {
	public JdbcTemplate database;

	public DatabaseServer(JdbcTemplate database) {
		this.database = database;
		database.execute("SET SCHEMA 'events'");
		System.out.println("Database schema selected");
		database.execute("INSERT INTO dsr.volunteermanager.events (name, starttime, endtime) VALUES ('Test', '11:00', '13:00')");
		System.out.println("Test event inserted");
		ArrayList<EventResponse> events = new ArrayList<>();
		database.query("SELECT * FROM dsr.volunteermanager.events", (rs, rowNum) ->
				EventResponse.newBuilder()
						.setEventId(EventId.newBuilder().setId(rs.getInt("id")).build())
						.setEventName(rs.getString("name"))
						.setEventTimestamp(rs.getString("starttime")).build());
	}

	public static void main(String[] args) {
		SpringApplication.run(DatabaseServer.class, args);
	}
}
