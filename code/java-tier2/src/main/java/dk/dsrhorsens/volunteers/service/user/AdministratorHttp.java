package dk.dsrhorsens.volunteers.service.user;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import dk.dsrhorsens.volunteers.service.user.AdministratorRequests;
import dk.dsrhorsens.volunteers.service.user.dto.Administrator;

import java.io.IOException;
import java.net.URI;
import java.net.URISyntaxException;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;

public abstract class AdministratorHttp implements AdministratorRequests {
    private String host;
    private int port;

    /**
     * Set the host of the database server
     * @param host The host server
     */
    public void setHost(String host) {
        this.host = host;
    }

    /**
     * Set the host of the database server
     * @param port The host server
     */
    public void setPort(int port) {
        this.port = port;
    }

    /**
     * Converts an administrator object to JSON string
     * @param administrator The administrator object
     * @return The JSON string
     * @throws Exception If the conversion had errors
     */
    private String convertToJson(Administrator administrator) throws Exception {
        ObjectMapper mapper = new ObjectMapper();
        String json;
        try {
            json = mapper.writeValueAsString(administrator);
        } catch (JsonProcessingException e) {
            System.out.println("Error serializing administrator object to JSON! "+e.getMessage());
            throw new Exception("500 Error serializing administrator object to JSON! "+e.getMessage());
        }

        return json;
    }

    /**
     * Get and administrator object from a http request
     * @param request The request to do
     * @return The returned administrator object
     * @throws Exception If there's and error during connection or not 200 response
     */
    private Administrator getAdministratorResponse(HttpRequest request) throws Exception {
        ObjectMapper mapper = new ObjectMapper();
        var client = HttpClient.newHttpClient();
        var response = client.send(request, HttpResponse.BodyHandlers.ofString());
        if (response.statusCode() >= 400) {
            throw new Exception(response.statusCode()+" "+response.body());
        }

        // parsing successful response
        Administrator obj = mapper.readValue(response.body(), Administrator.class);
        return obj;
    }

    /**
     * Create a new administrator in the database
     * @param administrator The administrator object to create, administrator Id is ignored
     * @return The created administrator, null if failed
     */
    public Administrator createNewAdministrator(Administrator administrator) throws Exception {
        String json = convertToJson(administrator);

        try {
            // setting up the request
            var uri = new URI(host+":"+port+"/Administrator");
            var request = HttpRequest.newBuilder(uri).
                    POST(HttpRequest.BodyPublishers.ofString(json))
                    .header("Content-type", "application/json").
                    build();
            // sending the request
            return getAdministratorResponse(request);
        } catch (URISyntaxException e) {
            // server implementation error
            System.out.println("Invalid POST URL: "+host+":"+port+"/Administrator");
            System.out.println("Error message: "+e.getMessage());
            throw new RuntimeException(e);
        } catch (IOException | InterruptedException e) {
            System.out.println("Connection error!");
            System.out.println("Error message: "+e.getMessage());
            throw new Exception("500 Connection error!");
        }
    }

    public Administrator getAdministratorById(long administratorId) throws Exception {
        try {
            // setting up http client
            var uri = new URI(host+":"+port+"/Administrator/"+administratorId);
            // setting up request
            var request = HttpRequest.newBuilder(uri)
                    .build();
            // sending the request
            return getAdministratorResponse(request);
        } catch (URISyntaxException e) {
            // server implementation error
            System.out.println("Invalid GET URL: "+host+":"+port+"/Administrator/"+administratorId);
            System.out.println("Error message: "+e.getMessage());
            throw new RuntimeException(e);
        } catch (IOException | InterruptedException e) {
            System.out.println("Connection error!");
            System.out.println("Error message: "+e.getMessage());
            throw new Exception("500 Connection error!");
        }
    }

    public Administrator getAdministratorByVolunteer(long volunteerId) throws Exception {
        try {
            // setting up request
            var uri = new URI(host+":"+port+"/Administrator/Volunteer/"+volunteerId);
            var request = HttpRequest.newBuilder(uri)
                    .build();
            // sending the request
            return getAdministratorResponse(request);
        } catch (URISyntaxException e) {
            // server implementation error
            System.out.println("Invalid GET URL: "+host+":"+port+"/Administrator/Volunteer/"+volunteerId);
            System.out.println("Error message: "+e.getMessage());
            throw new RuntimeException(e);
        } catch (IOException | InterruptedException e) {
            System.out.println("Connection error!");
            System.out.println("Error message: "+e.getMessage());
            throw new Exception("500 Connection error!");
        }
    }

    public void  deleteAdministratorById(long administratorId) throws Exception {
        try {
            // setting up request
            var uri = new URI(host+":"+port+"/Administrator/"+administratorId);
            var request = HttpRequest.newBuilder(uri)
                    .DELETE()
                    .build();
            // sending the request
            getAdministratorResponse(request);
        } catch (URISyntaxException e) {
            // server implementation error
            System.out.println("Invalid DELETE URL: "+host+":"+port+"/Administrator/"+administratorId);
            System.out.println("Error message: "+e.getMessage());
            throw new RuntimeException(e);
        } catch (IOException | InterruptedException e) {
            System.out.println("Connection error!");
            System.out.println("Error message: "+e.getMessage());
            throw new Exception("500 Connection error!");
        }
    }

    public void  deleteAdministratorByVolunteer(long volunteerId) throws Exception {
        try {
            // setting up request
            var uri = new URI(host+":"+port+"/Administrator/Volunteer/"+volunteerId);
            var request = HttpRequest.newBuilder(uri)
                    .DELETE()
                    .build();
            // sending the request
            getAdministratorResponse(request);
        } catch (URISyntaxException e) {
            // server implementation error
            System.out.println("Invalid DELETE URL: "+host+":"+port+"/Administrator/Volunteer/"+volunteerId);
            System.out.println("Error message: "+e.getMessage());
            throw new RuntimeException(e);
        } catch (IOException | InterruptedException e) {
            System.out.println("Connection error!");
            System.out.println("Error message: "+e.getMessage());
            throw new Exception("500 Connection error!");
        }
    }
}
