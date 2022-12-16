package dk.dsrhorsens.volunteers.service.dto;

/**
 * Administrator DTO class
 */
public class Administrator {
    public String getAdministratorId() {
        return administratorId;
    }

    public String getVolunteerId() {
        return volunteerId;
    }

    public String getManagerId() {
        return managerId;
    }

    private String administratorId;
    private String volunteerId;
    private String managerId;

    public void setAdministratorId(String administratorId) {
        this.administratorId = administratorId;
    }

    public void setVolunteerId(String volunteerId) {
        this.volunteerId = volunteerId;
    }

    public void setManagerId(String managerId) {
        this.managerId = managerId;
    }
}
