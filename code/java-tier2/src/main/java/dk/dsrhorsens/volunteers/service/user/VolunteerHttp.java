package dk.dsrhorsens.volunteers.service.user;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import dk.dsrhorsens.volunteers.DsrVolunteerApplication;
import dk.dsrhorsens.volunteers.service.dto.Shift;
import dk.dsrhorsens.volunteers.service.dto.Volunteer;

import java.io.IOException;
import java.net.URI;
import java.net.URISyntaxException;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public abstract class VolunteerHttp {
    /**
     * Converts a volunteer object to JSON string
     * @param volunteer The administrator object
     * @return The JSON string
     * @throws Exception If the conversion had errors
     */
    private static String convertToJson(Volunteer volunteer) throws Exception {
        ObjectMapper mapper = new ObjectMapper();
        String json;
        try {
            json = mapper.writeValueAsString(volunteer);
        } catch (JsonProcessingException e) {
            System.out.println("Error serializing volunteer object to JSON! "+e.getMessage());
            throw new Exception("500 Error serializing volunteer object to JSON! "+e.getMessage());
        }

        return json;
    }

    /**
     * Get a volunteer object from a http request
     * @param request The request to do
     * @return The returned volunteer object
     * @throws Exception If there's and error during connection or not 200 response
     */
    private static Volunteer getVolunteerResponse(HttpRequest request) throws Exception {
        ObjectMapper mapper = new ObjectMapper();
        var client = HttpClient.newHttpClient();
        var response = client.send(request, HttpResponse.BodyHandlers.ofString());
        if (response.statusCode() >= 400) {
            throw new Exception(response.statusCode()+" "+response.body());
        }

        // parsing successful response
        Volunteer obj = mapper.readValue(response.body(), Volunteer.class);
        return obj;
    }

    /**
     * Create a new volunteer in the database
     * @param volunteer The volunteer data to use, only UUID, full name and email are used
     * @return The volunteer data of the new volunteer
     * @throws Exception If anything went wrong while sending the request
     */
    public static Volunteer createVolunteer(Volunteer volunteer) throws Exception {
        String json = convertToJson(volunteer);

        try {
            // setting up the request
            var uri = new URI(DsrVolunteerApplication.databaseHost +":"+DsrVolunteerApplication.databasePort+"/Volunteer");
            var request = HttpRequest.newBuilder(uri).
                    POST(HttpRequest.BodyPublishers.ofString(json))
                    .header("Content-type", "application/json").
                    build();
            // sending the request
            return getVolunteerResponse(request);
        } catch (URISyntaxException e) {
            // server implementation error
            System.out.println("Invalid POST URL: "+DsrVolunteerApplication.databaseHost+":"+DsrVolunteerApplication.databasePort+"/Volunteer");
            System.out.println("Error message: "+e.getMessage());
            throw new RuntimeException(e);
        } catch (IOException | InterruptedException e) {
            System.out.println("Connection error!");
            System.out.println("Error message: "+e.getMessage());
            throw new Exception("500 Connection error!");
        }
    }

    /**
     * Get volunteer data from the database
     * @param uuid The volunteer's UUID
     * @return The volunteer's data
     * @throws Exception If anything went wrong while sending the request
     */
    public static Volunteer getVolunteer(long uuid) throws Exception {
        try {
            // setting up http client
            var uri = new URI(DsrVolunteerApplication.databaseHost+":"+DsrVolunteerApplication.databasePort+"/Volunteer/"+uuid);
            // setting up request
            var request = HttpRequest.newBuilder(uri)
                    .build();
            // sending the request
            return getVolunteerResponse(request);
        } catch (URISyntaxException e) {
            // server implementation error
            System.out.println("Invalid GET URL: "+DsrVolunteerApplication.databaseHost+":"+DsrVolunteerApplication.databasePort+"/Volunteer/"+uuid);
            System.out.println("Error message: "+e.getMessage());
            throw new RuntimeException(e);
        } catch (IOException | InterruptedException e) {
            System.out.println("Connection error!");
            System.out.println("Error message: "+e.getMessage());
            throw new Exception("500 Connection error!");
        }
    }

    /**
     * Get detailed information about a volunteer's shifts
     * @param uuid The UUID of the volunteer
     * @return A list of detailed information about volunteer's shifts
     * @throws Exception If anything went wrong while sending the request
     */
    public static List<Shift> getShifts(long uuid) throws Exception {
        try {
            // setting up http client
            var uri = new URI(DsrVolunteerApplication.databaseHost+":"+DsrVolunteerApplication.databasePort+"/Volunteer/"+uuid);
            // setting up request
            var request = HttpRequest.newBuilder(uri)
                    .build();
            // sending the request
            ObjectMapper mapper = new ObjectMapper();
            var client = HttpClient.newHttpClient();
            var response = client.send(request, HttpResponse.BodyHandlers.ofString());
            if (response.statusCode() >= 400) {
                throw new Exception(response.statusCode()+" "+response.body());
            }

            // parsing successful response
            Shift[] obj = mapper.readValue(response.body(), Shift[].class);
            List<Shift> shifts = new ArrayList<>();
            Collections.addAll(shifts, obj);
            return shifts;
        } catch (URISyntaxException e) {
            // server implementation error
            System.out.println("Invalid GET URL: "+DsrVolunteerApplication.databaseHost+":"+DsrVolunteerApplication.databasePort+"/Volunteer/"+uuid);
            System.out.println("Error message: "+e.getMessage());
            throw new RuntimeException(e);
        } catch (IOException | InterruptedException e) {
            System.out.println("Connection error!");
            System.out.println("Error message: "+e.getMessage());
            throw new Exception("500 Connection error!");
        }
    }
}
