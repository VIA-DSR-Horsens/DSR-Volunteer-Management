PROJECT STRUCTURE:
==================

* **./** This directory is the root of the app. It contains some settings and the main files.
    * _Imports.razor: Includes common Razor directives to include the components for the app (@using directives for
      namespaces).
    * App.razor: The root component of the app that sets up client-side routing using the Router component. The Router
      component intercepts browser navigation and renders the page that matches the requested address.
    * appsettings.json: Contains the settings. 
    * Program.cs: The entry point of the app. It creates the host builder and configures the app.
* **./Properties/** Contains the project properties.
* **./Components/** Contains the Blazor components used in this app.
* **./Pages/** Contains the Razor pages that use the directive @page and are thus navigable.
* **./Source/** Contains the program's code.
* **./wwwroot/** Contains the static files that are served by the app.
* **./styles/** Contains the CSS sources in scss. This files are compiled to the web root before changes are visible.