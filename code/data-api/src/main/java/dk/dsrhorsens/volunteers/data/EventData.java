package dk.dsrhorsens.volunteers.data;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import lombok.Getter;
import lombok.Setter;

import java.util.List;

/**
 * The EventData class is responsible for representing an event in the database. It is used to serialize and deserialize
 * events to and from the database. It mirrors the Event.cs class in the EFC database.
 */
@JsonIgnoreProperties (ignoreUnknown = true)
public class EventData {
	@Getter @Setter
	private long id;
	@Getter @Setter
	private String name;
	@Getter @Setter
	private String date;
	@Getter @Setter
	private String startTime;
	@Getter @Setter
	private String endTime;
	@Getter @Setter
	private String location;
}
