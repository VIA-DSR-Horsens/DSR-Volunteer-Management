package dk.dsrhorsens.volunteers.service.user.rest;

/**
 * Administrator DTO class
 */
public class Administrator {
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
