package dk.dsrhorsens.volunteers.data;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import lombok.Getter;
import lombok.Setter;

@JsonIgnoreProperties(ignoreUnknown = true)
public class AdminData {
	@Getter @Setter private String administratorId;
	@Getter @Setter private String volunteerId;
	@Getter @Setter private String managerId;
}
