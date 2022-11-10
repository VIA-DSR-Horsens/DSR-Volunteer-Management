package dk.dsrhorsens.volunteers;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.jdbc.core.JdbcTemplate;

@SpringBootApplication
public class DatabaseServer {

	public DatabaseServer(JdbcTemplate database) {
		DataManager.initialize(database);
	}

	public static void main(String[] args) {
		SpringApplication.run(DatabaseServer.class, args);
	}
}
