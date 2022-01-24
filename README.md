# IsuReservation Test Project

Technical Test Description

The items that will be evaluated are:

1. N-tier application development.
2. Aptitude to learn and to use new components.
3. Clarity and quality of your source code.
4. Source code documentation (in the code files)
5. Data validation: both in UI and backend.
6. Make your app available as a Git repository (Github, Gitlab, Bitbucket, etc.) and have one initial commit and at least a  second one with proper comments explaining what was committed.

Technology:

1. Latest version of ASP .net Core MVC
2. .net Core OOP knowledge.
3. Web site development using latest version Angular
4. Implementing internationalization using Angular i18n
5. HTML5 / CSS
6. ASP.NET Core with Entity Framework Core Code-First
7. My SQL, Postgres or SQL Server for the database
   * Knowledge: primary keys and foreign keys must be in place.
   * Creation and utilization of stored procedures.
   
Specifications:

This is a very small reservation system. When the application loads, it needs to take you to the reservation list, from there the user can click on the Create reservation button that will take them to the create / edit contact and create reservation form.

Create a Contact and Reservation form that looks as close as possible as the attached images. Please note that you also have access to the JPG files.

The forms must be made using the latest front-end technologies including responsive layout (see the responsive design attached) and Angular 4 or latter / html5.

From there you need to have a button that takes you to a reservation list with all the reservations (see above) Please note that one contact can have multiple reservations.

When the user is entering a contact name, the system has to search for that contact in the database, if not found then it will create a new one, if found it will auto fill the contact type, phone number and birthdate.

Also add two buttons to the Create Contact Form banner:  List and Edit. When the user clicks on list, create a list page that starts after the banner, the list grid must contain the following fields:

Contact name
Phone number
Birth date
Contact type (look up description from other table)

The list must be sortable by clicking on each of the grid header. Also the list must have pagination implemented.

User must be able to add, edit and delete contacts. Add and edit must be done in a separate page. Mandatory fields are contact name, birth date and contact type. For contact type use a dropdown
For the date you must use a calendar control but also allow users to type in the date in the field. All field need to have proper tab other.

For the description field on the Add and Edit Form be sure to research and add a rich text editor component that looks as close as possible to the image provided.

How to run it?

Requirements:
1. NetFramework 6.0
2. Net Core SDK 6.0
3. Angular 13.1.3
4. Node 16.13.1
5. DBMS: Microsoft SQL Server (ver. 15.00.4188)
   Driver: Microsoft JDBC Driver 9.4 for SQL Server (ver. 9.4.0.0, JDBC4.2)

Steps:

1. Clone project from Github repository (https://github.com/rrbauta/IsuReservation.git)
2. Install project dependencies (angular and net core)
3. Create a database and update app settings file with correct connection string
4. Run migrations. Only exist one migration (InitialCreate). Run it to create database, insert contact types and destinations sample data and create CreateContact store procedure.
5. Run project and wait....
