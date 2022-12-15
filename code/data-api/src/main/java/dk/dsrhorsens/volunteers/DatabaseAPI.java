package dk.dsrhorsens.volunteers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.web.client.HttpClientErrorException;
import org.springframework.web.client.RestTemplate;

import java.net.URI;
import java.net.URISyntaxException;

/**
 * A RESTful client for the API to access the Database of the Volunteer Manager project.
 */
public class DatabaseAPI {
	/**
	 * The URL of the API.
	 */
	@Value ("${dsr.data.host}") private String databaseHost;
	/**
	 * The port used by the API.
	 */
	@Value ("${dsr.data.port}") private String databasePort;

	/**
	 * The RestTemplate used to communicate with the API.
	 */
	@Autowired private RestTemplate restAPI;

	/**
	 * Create a URL object for the resource path.
	 */
	public URI getDatabasePath(String resourcePath) {
		try {
			return new URI(databaseHost + ':' + databasePort + resourcePath);
		} catch (URISyntaxException e) {
			throw new RuntimeException(e);
		}
	}

	public <U> U get(URI resourceURL, Class<U> responseType) throws HttpClientErrorException {
		return restAPI.getForObject(resourceURL, responseType);
	}

	public <U> U post(URI resourceURL, Object request, Class<U> responseType) throws HttpClientErrorException {
		return restAPI.postForObject(resourceURL, request, responseType);
	}

	public void delete(URI resourceURI) throws HttpClientErrorException {
		restAPI.delete(resourceURI);
	}

	public <U> U get(String resourcePath, Class<U> responseType) throws HttpClientErrorException {
		return get(getDatabasePath(resourcePath), responseType);
	}

	public <U> U post(String resourcePath, Object request, Class<U> responseType) throws HttpClientErrorException {
		return post(getDatabasePath(resourcePath), request, responseType);
	}

	public void delete(String resourcePath) throws HttpClientErrorException {
		delete(getDatabasePath(resourcePath));
	}
}
