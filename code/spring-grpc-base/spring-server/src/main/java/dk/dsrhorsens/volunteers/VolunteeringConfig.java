package dk.dsrhorsens.volunteers;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.context.annotation.Import;

@Configuration
@Import ({AuthConfig.class, DataConfig.class})
public class VolunteeringConfig {
	@Bean public EventManager eventBean() {
		return new EventManager();
	}
}
