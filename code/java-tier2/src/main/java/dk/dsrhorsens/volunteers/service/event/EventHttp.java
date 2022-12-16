package dk.dsrhorsens.volunteers.service.event;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import dk.dsrhorsens.volunteers.DsrVolunteerApplication;
import dk.dsrhorsens.volunteers.service.dto.Event;
import dk.dsrhorsens.volunteers.service.dto.Volunteer;

import java.io.IOException;
import java.net.URI;
import java.net.URISyntaxException;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;

public abstract class EventHttp {
    /**
     * Converts a event object to JSON string
     * @param event The administrator object
     * @return The JSON string
     * @throws Exception If the conversion had errors
     */
    private static String convertToJson(Event event) throws Exception {
        ObjectMapper mapper = new ObjectMapper();
        String json;
        try {
            json = mapper.writeValueAsString(event);
        } catch (JsonProcessingException e) {
            System.out.println("Error serializing event object to JSON! "+e.getMessage());
            throw new Exception("500 Error serializing event object to JSON! "+e.getMessage());
        }

        return json;
    }

    /**
     * Get a volunteer object from a http request
     * @param request The request to do
     * @return The returned volunteer object
     * @throws Exception If there's and error during connection or not 200 response
     */
    private static Event getEventResponse(HttpRequest request) throws Exception {
        ObjectMapper mapper = new ObjectMapper();
        var client = HttpClient.newHttpClient();
        var response = client.send(request, HttpResponse.BodyHandlers.ofString());
        if (response.statusCode() >= 400) {
            throw new Exception(response.statusCode()+" "+response.body());
        }

        // parsing successful response
        Event obj = mapper.readValue(response.body(), Event.class);
        return obj;
    }

    public static Event createNewEvent(Event event) throws Exception {
        String json = convertToJson(event);

        try {
            // setting up the request
            var uri = new URI(DsrVolunteerApplication.databaseHost +":"+DsrVolunteerApplication.databasePort+"/Manager");
            var request = HttpRequest.newBuilder(uri).
                    POST(HttpRequest.BodyPublishers.ofString(json))
                    .header("Content-type", "application/json").
                    build();
            // sending the request
            return getEventResponse(request);
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
}
