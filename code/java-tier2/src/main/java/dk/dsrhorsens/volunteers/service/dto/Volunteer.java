package dk.dsrhorsens.volunteers.service.dto;

import java.util.List;

public class Volunteer {
    private String volunteerId;
    private String fullName;
    private String email;
    private String shiftsTaken;
    private List<String> shifts;
    private String rating;

    public String getVolunteerId() {
        return volunteerId;
    }

    public void setVolunteerId(String volunteerId) {
        this.volunteerId = volunteerId;
    }

    public String getFullName() {
        return fullName;
    }

    public void setFullName(String fullName) {
        this.fullName = fullName;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getShiftsTaken() {
        return shiftsTaken;
    }

    public void setShiftsTaken(String shiftsTaken) {
        this.shiftsTaken = shiftsTaken;
    }

    public List<String> getShifts() {
        return shifts;
    }

    public void setShifts(List<String> shifts) {
        this.shifts = shifts;
    }

    public String getRating() {
        return rating;
    }

    public void setRating(String rating) {
        this.rating = rating;
    }
}
