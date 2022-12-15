package dk.dsrhorsens.volunteers;

import dk.dsrhorsens.volunteers.service.authentication.AuthenticationClientImpl;
import dk.dsrhorsens.volunteers.service.user.UserDatabaseConnection;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

@SpringBootApplication
public class DsrVolunteerApplication {
    public static AuthenticationClientImpl authenticationClient;
    public static UserDatabaseConnection databaseClient;

    public static void main(String[] args) {
        authenticationClient = new AuthenticationClientImpl("localhost", 4567);
        databaseClient = new UserDatabaseConnection("https://localhost", 7190);
        SpringApplication.run(DsrVolunteerApplication.class, args);
    }
}
