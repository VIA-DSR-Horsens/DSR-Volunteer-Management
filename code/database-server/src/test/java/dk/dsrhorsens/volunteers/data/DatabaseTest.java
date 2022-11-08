package dk.dsrhorsens.volunteers.data;

import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class DatabaseTest {
	@Test void testGetDataSource() {
		Database.Credentials creds = Database.Credentials.getCredentials();
		System.out.println(creds.toString());
		System.out.println(creds.getURL());
	}
}