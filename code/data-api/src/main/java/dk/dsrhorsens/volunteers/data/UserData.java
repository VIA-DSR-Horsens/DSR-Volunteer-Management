package dk.dsrhorsens.volunteers.data;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import lombok.Getter;
import lombok.Setter;

import java.util.List;

@JsonIgnoreProperties(ignoreUnknown = true)
public class UserData {
	@Getter @Setter private String volunteerId;
	@Getter @Setter private String fullName;
	@Getter @Setter private String email;
	@Getter @Setter private String shiftsTaken;
	@Getter @Setter private String rating;
	@Getter @Setter private List<String> shifts;
}
