package dk.dsrhorsens.volunteers.service.user;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import dk.dsrhorsens.volunteers.service.user.rest.Administrator;

import java.io.IOException;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLConnection;
import java.net.http.HttpClient;
import java.nio.charset.StandardCharsets;

/**
 * Provides methods to retrieve and send data to the database related to /Volunteer/, /Manager/ and /Administrator/ API endpoints
 */
public class UserDatabaseConnection {
    private final String host;
    private final int port;

    /**
     * Create a new user database connection
     * @param host The host server
     * @param port The host server's port
     */
    public UserDatabaseConnection(String host, int port) {
        this.host = host;
        this.port = port;
    }

    /**
     * Create a new administrator in the database
     * @param administrator The administrator object to create, administrator Id is ignored
     * @return The created administrator
     */
    public Administrator createNewAdministrator(Administrator administrator) {
        ObjectMapper mapper = new ObjectMapper();
        String json;
        try {
            json = mapper.writeValueAsString(administrator);
        } catch (JsonProcessingException e) {
            System.out.println("Error serializing administrator object to JSON! "+e.getMessage());
            return null;
        }


    }
}
