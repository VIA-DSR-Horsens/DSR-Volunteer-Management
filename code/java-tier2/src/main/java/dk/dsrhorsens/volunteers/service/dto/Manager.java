package dk.dsrhorsens.volunteers.service.dto;

import java.util.List;

/**
 * Manager DTO class
 */
public class Manager {
    private String managerId;
    private String volunteerId;
    private List<String> eventsManaged;

    public String getManagerId() {
        return managerId;
    }

    public void setManagerId(String managerId) {
        this.managerId = managerId;
    }

    public String getVolunteerId() {
        return volunteerId;
    }

    public void setVolunteerId(String volunteerId) {
        this.volunteerId = volunteerId;
    }

    public List<String> getEventsManaged() {
        return eventsManaged;
    }

    public void setEventsManaged(List<String> eventsManaged) {
        this.eventsManaged = eventsManaged;
    }
}
