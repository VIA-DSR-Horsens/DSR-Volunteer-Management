## Authentication ReST API documentation

---

### Login API

* **POST /Login/**: This endpoint is meant to attempt to log-in the user via their email and password.
The request needs a User object which contains *email* and *password* of the user.
If the log-in was successful the response will be authentication cookie to use on the Java server.
Response status codes:
  1. *Status code 200*: The user successfully logged in
  2. *Status code 401*: The user's email or password were incorrect
  3. *Status code 500*: Something went wrong while logging in

---

### Logout API

* **POST /Logout/**: This endpoint is meant to attempt to log-out the user via their email and password.
  The request needs a User object which contains *email* and *password* of the user.
  If the log-out was successful the response will be a true boolean.
  Response status codes:
  1. *Status code 200*: The user successfully logged out
  2. *Status code 401*: The user's email or password were incorrect
  3. *Status code 500*: Something went wrong while logging out

---

###  User API
* **POST /User/**: This endpoint is meant to create a new user in the authentication server database.
  The request needs a User object which contains *email* and *password* of the user.
  If the creation was successful the response will be authentication cookie to use on the Java server.
  Response status codes:
  1. *Status code 200*: The user account was created successfully
  2. *Status code 401*: There was a problem while creating a proper authentication for the user
  3. *Status code 500*: Something went wrong while creating the user account