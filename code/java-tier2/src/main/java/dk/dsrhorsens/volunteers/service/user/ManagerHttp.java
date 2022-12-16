package dk.dsrhorsens.volunteers.service.user;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import dk.dsrhorsens.volunteers.DsrVolunteerApplication;
import dk.dsrhorsens.volunteers.service.dto.Manager;

import java.io.IOException;
import java.net.URI;
import java.net.URISyntaxException;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;

public abstract class ManagerHttp {
    /**
     * Converts an manager object to JSON string
     * @param manager The administrator object
     * @return The JSON string
     * @throws Exception If the conversion had errors
     */
    private static String convertToJson(Manager manager) throws Exception {
        ObjectMapper mapper = new ObjectMapper();
        String json;
        try {
            json = mapper.writeValueAsString(manager);
        } catch (JsonProcessingException e) {
            System.out.println("Error serializing manager object to JSON! "+e.getMessage());
            throw new Exception("500 Error serializing manager object to JSON! "+e.getMessage());
        }

        return json;
    }

    /**
     * Get and manager object from a http request
     * @param request The request to do
     * @return The returned manager object
     * @throws Exception If there's and error during connection or not 200 response
     */
    private static Manager getManagerResponse(HttpRequest request) throws Exception {
        ObjectMapper mapper = new ObjectMapper();
        var client = HttpClient.newHttpClient();
        var response = client.send(request, HttpResponse.BodyHandlers.ofString());
        if (response.statusCode() >= 400) {
            throw new Exception(response.statusCode()+" "+response.body());
        }

        // parsing successful response
        Manager obj = mapper.readValue(response.body(), Manager.class);
        return obj;
    }

    /**
     * Create a new manager in the database
     * @param manager The administrator object to create, manager Id is ignored
     * @return The created manager, null if failed
     */
    public static Manager createNewManager(Manager manager) throws Exception {
        String json = convertToJson(manager);

        try {
            // setting up the request
            var uri = new URI(DsrVolunteerApplication.databaseHost +":"+DsrVolunteerApplication.databasePort+"/Manager");
            var request = HttpRequest.newBuilder(uri).
                    POST(HttpRequest.BodyPublishers.ofString(json))
                    .header("Content-type", "application/json").
                    build();
            // sending the request
            return getManagerResponse(request);
        } catch (URISyntaxException e) {
            // server implementation error
            System.out.println("Invalid POST URL: "+DsrVolunteerApplication.databaseHost+":"+DsrVolunteerApplication.databasePort+"/Manager");
            System.out.println("Error message: "+e.getMessage());
            throw new Exception("500 "+e.getMessage());
        } catch (IOException | InterruptedException e) {
            System.out.println("Connection error!");
            System.out.println("Error message: "+e.getMessage());
            throw new Exception("500 Connection error!");
        }
    }

    public static Manager getManagerById(long managerId) throws Exception {
        try {
            // setting up http client
            var uri = new URI(DsrVolunteerApplication.databaseHost+":"+DsrVolunteerApplication.databasePort+"/Manager/"
                    +managerId);
            // setting up request
            var request = HttpRequest.newBuilder(uri)
                    .build();
            // sending the request
            return getManagerResponse(request);
        } catch (URISyntaxException e) {
            // server implementation error
            System.out.println("Invalid GET URL: "+DsrVolunteerApplication.databaseHost+":"+DsrVolunteerApplication.databasePort+
                    "/Manager/"+managerId);
            System.out.println("Error message: "+e.getMessage());
            throw new Exception("500 "+e.getMessage());
        } catch (IOException | InterruptedException e) {
            System.out.println("Connection error!");
            System.out.println("Error message: "+e.getMessage());
            throw new Exception("500 Connection error!");
        }
    }

    public static Manager getManagerByVolunteer(long managerId) throws Exception {
        try {
            // setting up request
            var uri = new URI(DsrVolunteerApplication.databaseHost+":"+DsrVolunteerApplication.databasePort+
                    "/Manager/Volunteer/"+managerId);
            var request = HttpRequest.newBuilder(uri)
                    .build();
            // sending the request
            return getManagerResponse(request);
        } catch (URISyntaxException e) {
            // server implementation error
            System.out.println("Invalid GET URL: "+DsrVolunteerApplication.databaseHost+":"+DsrVolunteerApplication.databasePort+
                    "/Manager/Volunteer/"+managerId);
            System.out.println("Error message: "+e.getMessage());
            throw new Exception("500 "+e.getMessage());
        } catch (IOException | InterruptedException e) {
            System.out.println("Connection error!");
            System.out.println("Error message: "+e.getMessage());
            throw new Exception("500 Connection error!");
        }
    }

    public static void  deleteManagerById(long managerId) throws Exception {
        try {
            // setting up request
            var uri = new URI(DsrVolunteerApplication.databaseHost+":"+DsrVolunteerApplication.databasePort+
                    "/Manager/"+managerId);
            var request = HttpRequest.newBuilder(uri)
                    .DELETE()
                    .build();
            // sending the request
            getManagerResponse(request);
        } catch (URISyntaxException e) {
            // server implementation error
            System.out.println("Invalid DELETE URL: "+DsrVolunteerApplication.databaseHost+":"+DsrVolunteerApplication.databasePort+
                    "/Manager/"+managerId);
            System.out.println("Error message: "+e.getMessage());
            throw new Exception("500 "+e.getMessage());
        } catch (IOException | InterruptedException e) {
            System.out.println("Connection error!");
            System.out.println("Error message: "+e.getMessage());
            throw new Exception("500 Connection error!");
        }
    }

    public static void  deleteManagerByVolunteer(long managerId) throws Exception {
        try {
            // setting up request
            var uri = new URI(DsrVolunteerApplication.databaseHost+":"+DsrVolunteerApplication.databasePort+
                    "/Manager/Volunteer/"+managerId);
            var request = HttpRequest.newBuilder(uri)
                    .DELETE()
                    .build();
            // sending the request
            getManagerResponse(request);
        } catch (URISyntaxException e) {
            // server implementation error
            System.out.println("Invalid DELETE URL: "+DsrVolunteerApplication.databaseHost+":"+DsrVolunteerApplication.databasePort+
                    "/Manager/Volunteer/"+managerId);
            System.out.println("Error message: "+e.getMessage());
            throw new Exception("500 "+e.getMessage());
        } catch (IOException | InterruptedException e) {
            System.out.println("Connection error!");
            System.out.println("Error message: "+e.getMessage());
            throw new Exception("500 Connection error!");
        }
    }
}
