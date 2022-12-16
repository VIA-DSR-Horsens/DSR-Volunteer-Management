package dk.dsrhorsens.volunteers.service.user;

import dk.dsrhorsens.volunteers.service.dto.Administrator;

/**
 * Mehtods to work with administrators on the database
 */
public interface AdministratorRequests {
    /**
     * Create a new administrator in the database
     * @param administrator The administrator data, administrator id is ignored
     * @return The newly created administrator, null if not successful
     * @throws Exception Exception if something went wrong, first 3 characters are HTTP error code
     */
    Administrator createNewAdministrator(Administrator administrator) throws Exception;

    /**
     * Get information about an administrator by administrator id
     * @param administratorId The administrator's id
     * @return The administrator object
     * @throws Exception Exception if something went wrong, first 3 characters are HTTP error code
     */
    Administrator getAdministratorById(long administratorId) throws Exception;

    /**
     * Get information about an administrator by their volunteer id
     * @param volunteerId The volunteer's id
     * @return The administrator object
     * @throws Exception Exception if something went wrong, first 3 characters are HTTP error code
     */
    Administrator getAdministratorByVolunteer(long volunteerId) throws Exception;

    /**
     * Delete an administrator by their id
     * @param administratorId The administrator's id
     * @throws Exception Exception if something went wrong, first 3 characters are HTTP error code
     */
    void deleteAdministratorById(long administratorId) throws Exception;

    /**
     * Delete an administrator by their volunteer id
     * @param volunteerId The volunteer's id
     * @throws Exception Exception if something went wrong, first 3 characters are HTTP error code
     */
    void deleteAdministratorByVolunteer(long volunteerId) throws Exception;
}
