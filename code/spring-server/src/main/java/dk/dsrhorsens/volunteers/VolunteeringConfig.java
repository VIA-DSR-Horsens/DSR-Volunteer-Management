package dk.dsrhorsens.volunteers;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class VolunteeringConfig {
	@Bean
	public EventManager eventBean() {
		return new EventManager();
	}
}
