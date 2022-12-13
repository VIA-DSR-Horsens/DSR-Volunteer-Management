package dk.dsrhorsens.volunteers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.web.client.RestTemplate;

import java.net.*;

/**
 * A RESTful client for the API to access the Database of the Volunteer Manager project.
 */
public class DatabaseAPI {
	/**
	 * The URL of the API.
	 */
	@Value ("${dsr.api.url}") private String baseURL;
	/**
	 * The port used by the API.
	 */
	@Value ("${dsr.api.port}") private String port;

	/**
	 * The RestTemplate used to communicate with the API.
	 */
	@Autowired private RestTemplate restAPI;

	/**
	 * Create a URL object for the resource path.
	 */
	public URI getAPIPath(String resourcePath) {
		try {
			return new URI(baseURL + resourcePath);
		} catch (URISyntaxException e) {
			throw new RuntimeException(e);
		}
	}

	public <U> U get(URI resourceURL, Class<U> responseType) {
		return restAPI.getForObject(resourceURL, responseType);
	}

	public <U> U post(URI resourceURL, Object request, Class<U> responseType) {
		return restAPI.postForObject(resourceURL, request, responseType);
	}
}
