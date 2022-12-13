# API ENDPOINT DOCUMENTATION

---

### VOLUNTEER ENDPOINT
* **POST /Volunteer/:** This endpoint is meant to allow the creation of new volunteers.
It requires a volunteer object which contains *volunteerId*, *fullName* and *email*.
If the volunteer creation is successful, then it returns a volunteer object which contains *volunteerId*, *fullName*, *email*, *shiftsTaken*, list of *shiftId* and *rating*.
It can respond with the following status codes:
  1. *Status code 200*: Successful volunteer creation
  2. *Status code 400*: The data sent in request couldn't be parsed correctly.
  3. *Status code 500*: Either data couldn't be saved to the database or something else went wrong.
* **GET /Volunteer/{volunteerId}:** This endpoint is meant to get volunteer's information from their id.
  If the volunteer exists, then it returns a volunteer object which contains *volunteerId*, *fullName*, *email*, *shiftsTaken*, list of *shiftId* and *rating*.
  It can respond with the following status codes:
  1. *Status code 200*: Successful retrieval of volunteer data
  2. *Status code 404*: The volunteer was not found.
  3. *Status code 500*: Something went wrong while getting data.
* **GET /Volunteer/{volunteerId}/Shifts:** This endpoint is meant to get detailed information about a volunteer's shifts.
  If the volunteer exists, then it returns a list of shift objects which contains *shiftId*, *volunteerId*, *eventId*, *startTime*, *endTime* and *accepted*.
  It can respond with the following status codes:
  1. *Status code 200*: Successful retrieval of volunteer's shifts data
  2. *Status code 404*: The volunteer was not found.
  3. *Status code 500*: Something went wrong while getting data.

---

### MANAGER ENDPOINT
* **POST /Manager/:** This endpoint is meant to allow the creation of new managers.
  It requires a manager object which only needs to contain *volunteerId*.
  If the manager creation is successful, then it returns a manager object which contains *managerId*, *volunteerId* and a list managed *eventId*.
  It can respond with the following status codes:
  1. *Status code 200*: Successful manager creation
  2. *Status code 400*: The data sent in request couldn't be parsed correctly.
  3. *Status code 404*: The volunteer was not found
  4. *Status code 500*: Either data couldn't be saved to the database or something else went wrong.
* **GET /Manager/{managerId}:** This endpoint is meant to get manager's information from their id.
  If the manager exists, then it returns a manager object which contains *managerId*, *volunteerId* and a list managed *eventId*.
  It can respond with the following status codes:
  1. *Status code 200*: Successful retrieval of manager data
  2. *Status code 404*: The manager was not found.
  3. *Status code 500*: Something went wrong while getting data.
* **GET /Manager/Volunteer/{volunteerId}:** This endpoint is meant to get manager's information from their volunteer id.
  If the volunteer is a manager, then it returns a manager object which contains *managerId*, *volunteerId* and a list managed *eventId*.
  It can respond with the following status codes:
  1. *Status code 200*: Successful retrieval of manager data
  2. *Status code 404*: The volunteer was not found or isn't a manager.
  3. *Status code 500*: Something went wrong while getting data.
* **DELETE /Manager/{managerId}:** This endpoint is meant to delete the manager by their id.
  If the manager exists, then it's deleted and if the volunteer was also an administrator, the administrator is also deleted.
  It can respond with the following status codes:
  1. *Status code 200*: Successful deletion of the manager
  2. *Status code 404*: The manager was not found.
  3. *Status code 409*: Manager manages an event which currently has only 1 manager, therefore the manager can't be deleted.
  4. *Status code 500*: Something went wrong while saving data.
* **DELETE /Manager/Volunteer/{volunteerId}:** This endpoint is meant to delete the manager by their volunteer id.
  If the volunteer exists, then it's stripped of their manager role (manager gets deleted) and if the volunteer was also an administrator, the administrator is also deleted.
  It can respond with the following status codes:
  1. *Status code 200*: Successful deletion of the manager
  2. *Status code 404*: The volunteer was not found.
  3. *Status code 409*: Manager manages an event which currently has only 1 manager, therefore the manager can't be deleted.
  4. *Status code 500*: Something went wrong while saving data.

---

### ADMINISTRATOR ENDPOINT
* **POST /Administrator/:** This endpoint is meant to allow the creation of new administrators.
  It requires an administrator object which only needs to contain *volunteerId*.
  If the administrator creation is successful, then it returns an administrator object which contains *administratorId*, *volunteerId* and *managerId*.
  If the volunteer wasn't already a manager, a new manager is made to make the volunteer a manager.
  It can respond with the following status codes:
  1. *Status code 200*: Successful administrator creation
  2. *Status code 400*: The data sent in request couldn't be parsed correctly.
  3. *Status code 404*: The volunteer was not found
  4. *Status code 500*: Either data couldn't be saved to the database or something else went wrong.
* **GET /Administrator/{administratorId}:** This endpoint is meant to get administrator's information from their id.
  If the administrator exists, then it returns an administrator object which contains *administratorId*, *volunteerId* and *managerId*.
  It can respond with the following status codes:
  1. *Status code 200*: Successful retrieval of administrator data
  2. *Status code 404*: The administrator was not found.
  3. *Status code 500*: Something went wrong while getting data.
* **GET /Administrator/Volunteer/{volunteerId}:** This endpoint is meant to get administrator's information from their volunteer id.
  If the volunteer is an administrator, then it returns an administrator object which contains *administratorId*, *volunteerId* and *managerId*.
  It can respond with the following status codes:
  1. *Status code 200*: Successful retrieval of administrator data
  2. *Status code 404*: The volunteer was not found or isn't an administrator.
  3. *Status code 500*: Something went wrong while getting data.
* **DELETE /Administrator/{administratorId}:** This endpoint is meant to delete the administrator by their id.
  If the administrator exists, then it's deleted.
  It can respond with the following status codes:
  1. *Status code 200*: Successful deletion of the administrator
  2. *Status code 404*: The administrator was not found.
  3. *Status code 500*: Something went wrong while saving data.
* **DELETE /Administrator/Volunteer/{volunteerId}:** This endpoint is meant to delete the administrator by their volunteer id.
  If the volunteer exists, then it's stripped of their administrator role (administrator gets deleted).
  It can respond with the following status codes:
  1. *Status code 200*: Successful deletion of the administrator
  2. *Status code 404*: The volunteer was not found.
  3. *Status code 500*: Something went wrong while saving data.

---

### SHIFT ENDPOINT
* **POST /Shift/:** This endpoint is meant to allow the creation of new shifts.
  It requires a shift object which contains *volunteerId*, *eventId*, *startTime*, *endTime* and *accepted*.
  If the shift creation is successful, then it returns a shift object which contains *shiftId*, *volunteerId*, *eventId*, *startTime*, *endTime* and *accepted*.
  It can respond with the following status codes:
  1. *Status code 200*: Successful shift creation
  2. *Status code 400*: The data sent in request couldn't be parsed correctly.
  3. *Status code 404*: The volunteer or event was not found
  4. *Status code 500*: Either data couldn't be saved to the database or something else went wrong.
* **GET /Shift/{shiftId}:** This endpoint is meant to get shift's information from it's id.
  If the shift exists, then it returns a shift object which contains *shiftId*, *volunteerId*, *eventId*, *startTime*, *endTime* and *accepted*.
  It can respond with the following status codes:
  1. *Status code 200*: Successful retrieval of shift data
  2. *Status code 404*: The shift was not found.
  3. *Status code 500*: Something went wrong while getting data.
* **PUT /Shift/{administratorId}:** This endpoint is meant to update the shift with new information.
  It requires a shift object which contains *volunteerId*, *eventId*, *startTime*, *endTime* and *accepted*, although the *volunteerId* and *eventId* can't be changed. 
  If the shift update is successful, then it returns a shift object which contains *shiftId*, *volunteerId*, *eventId*, *startTime*, *endTime* and *accepted*.
  It can respond with the following status codes:
  1. *Status code 200*: Successful update of the shift
  2. *Status code 400*: The data sent in request couldn't be parsed correctly.
  3. *Status code 404*: The shift was not found.
  4. *Status code 500*: Something went wrong while saving data.

---

### EVENT ENDPOINT
* **POST /Event/:** This endpoint is meant to allow the creation of new events.
  It requires an event object which contains *eventName*, *eventDate* and a list of *managerId*.
  If the event creation is successful, then it returns an event object which contains *eventId*, *eventName*, *eventDate*, *startTime*, *endTime*, *location*, a list of *managerId* and a list of *shiftId*.
  It can respond with the following status codes:
  1. *Status code 200*: Successful event creation
  2. *Status code 400*: The data sent in request couldn't be parsed correctly.
  3. *Status code 404*: The manager was not found
  4. *Status code 500*: Either data couldn't be saved to the database or something else went wrong.
* **GET /Event/{eventId}:** This endpoint is meant to get event's information from it's id.
  If the event exists, then it returns an event object which contains *eventId*, *eventName*, *eventDate*, *startTime*, *endTime*, *location*, a list of *managerId* and a list of *shiftId*.
  It can respond with the following status codes:
  1. *Status code 200*: Successful retrieval of event data
  2. *Status code 404*: The event was not found.
  3. *Status code 500*: Something went wrong while getting data.
* **GET /Event/{eventId}/Shifts:** This endpoint is meant to get detailed event's shifts information from it's id.
  If the event exists, then it returns a list of shift objects which contain *shiftId*, *eventId*, *volunteerId*, *startTime*, *endTime*, *accepted*.
  It can respond with the following status codes:
  1. *Status code 200*: Successful retrieval of shift data
  2. *Status code 404*: The shift was not found.
  3. *Status code 500*: Something went wrong while getting data.