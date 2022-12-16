package dk.dsrhorsens.volunteers.service.user;

import dk.dsrhorsens.volunteers.service.dto.Manager;

public interface ManagerRequests {
    Manager createNewManager(Manager manager) throws Exception;
    Manager getManagerById(long managerId) throws Exception;
    Manager getManagerByVolunteer(long uuid) throws Exception;
    void deleteManagerById(long managerId) throws Exception;
    void deleteManagerByVolunteer(long volunteerId) throws Exception;
}
