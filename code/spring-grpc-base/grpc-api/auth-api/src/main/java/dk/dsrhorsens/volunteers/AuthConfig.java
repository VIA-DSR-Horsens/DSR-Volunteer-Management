package dk.dsrhorsens.volunteers;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class AuthConfig {
	@Bean public UserManager authAPI() {
		return new UserManager();
	}
}
