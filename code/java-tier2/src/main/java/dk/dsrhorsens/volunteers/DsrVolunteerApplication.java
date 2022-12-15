package dk.dsrhorsens.volunteers;

import dk.dsrhorsens.volunteers.service.authentication.AuthenticationClientImpl;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

@SpringBootApplication
public class DsrVolunteerApplication {
    public static AuthenticationClientImpl authenticationClient;
    public static final String databaseHost = "https://localhost";
    public static final int databasePort = 7190;

    public static void main(String[] args) {
        authenticationClient = new AuthenticationClientImpl("localhost", 4567);
        SpringApplication.run(DsrVolunteerApplication.class, args);
    }
}
