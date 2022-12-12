package java.dk.dsrhorsens.shiftapi.service;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.ProtocolException;
import java.net.URL;

public class ShiftService {
    HttpURLConnection conn;
    InputStream inputStream;


    // Create a URL object for the target URL and port number
    public void setUrl() {
        {
            try {
               URL url = new URL("https://localhost:7006/shift");
            } catch (MalformedURLException e) {
                throw new RuntimeException(e);
            }
        }
    }

    // Open a connection to the URL and set the request method to GET
    public void getConnection(URL url) {
        {
            try {
                conn = (HttpURLConnection) url.openConnection();
                conn.connect();
                conn.setRequestMethod("GET");
            } catch (ProtocolException e) {
                throw new RuntimeException(e);
            } catch (IOException e) {
                throw new RuntimeException(e);
            }
        }

    }

    // Read the response from the server
    public void getResponse() {
        BufferedReader reader = new BufferedReader(new InputStreamReader(inputStream));
        String response;
        {
            try {
                inputStream = conn.getInputStream();
                response = reader.readLine();
            } catch (IOException e) {
                throw new RuntimeException(e);
            }
        }
    }

    // Close the connection
    public void disconnect(){
        conn.disconnect();
    }

}

