package dk.dsrhorsens.volunteers.data;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import lombok.Getter;
import lombok.Setter;

@JsonIgnoreProperties
public class ShiftData {
	@Getter @Setter private String shiftId;
	@Getter @Setter private String volunteerId;
	@Getter @Setter private String eventId;
	@Getter @Setter private String startTime;
	@Getter @Setter private String endTime;
	@Getter @Setter private boolean accepted;
}
