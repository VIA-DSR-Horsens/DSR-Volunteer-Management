package dk.dsrhorsens.volunteers.data;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import lombok.Getter;
import lombok.Setter;

import java.util.List;

@JsonIgnoreProperties(ignoreUnknown = true)
public class ManagerData {
	@Getter @Setter private String managerId;
	@Getter @Setter private String volunteerId;
	@Getter @Setter private List<String> eventsManaged;
}
