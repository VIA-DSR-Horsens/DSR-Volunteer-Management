package dk.dsrhorsens.volunteers;

import org.springframework.boot.web.client.RestTemplateBuilder;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.web.client.RestTemplate;

@Configuration
public class DataConfig {
	@Bean public DatabaseAPI databaseAPI() {
		return new DatabaseAPI();
	}
	@Bean public RestTemplate restAPI(RestTemplateBuilder builder) {
		return builder.build();
	}
}
