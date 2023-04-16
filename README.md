# Address Book Application
This is a web application that provides a public address book functionality. Users can view, create, update, and delete contacts. Contacts contain name, date of birth, address, and multiple telephone numbers, and each phone number can only be associated with one contact. Contacts are unique by name and address.

The application is built using .NET Core Web API and PostgreSQL database. SignalR is used to provide real-time updates to connected clients.

## Technologies Used
- .NET Core Web API
- PostgreSQL
- SignalR
## Prerequisites
- [.NET 5 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)
- [PostgreSQL](https://www.postgresql.org/download/)
## Getting Started
- Clone the repository
- Navigate to the project directory
- Run **'dotnet restore'** to install the project dependencies
- Update the connection string in the **'appsettings.json'** file to point to your PostgreSQL database.
- Run the following command to create the database tables: **'dotnet ef database update'**
- Run the application using **'dotnet run'**
- The application should now be running

## Endpoints
### Get Paged Contacts
GET /api/contacts

Query parameters:

- pageNumber (integer)
- pageSize (integer)

Returns a paged list of contacts.

### Get Contact By Id
GET /api/contacts/{id}

Returns a contact by ID.

### Create Contact
POST /api/contacts

Body:
```json

{
    "name": "John Doe",
    "dateOfBirth": "1990-01-01",
    "address": "123 Main St",
    "telephoneNumbers": ["123-456-7890", "234-567-8901"]
}

```
Creates a new contact.


### Update Contact
PUT /api/contacts/{id}

Body:
```json

{
    "name": "John Doe",
    "dateOfBirth": "1990-01-01",
    "address": "123 Main St",
    "telephoneNumbers": ["123-456-7890", "234-567-8901"]
}

```
Updates an existing contact.

### Delete Contact
DELETE /api/contacts/{id}

Deletes a contact by ID.

## Real-Time Updates
The application uses SignalR to provide real-time updates to connected clients. When a contact is created, updated, or deleted, a message is sent to all connected clients with the contact's name.

To test the real-time updates, open the application in multiple browser windows and perform create, update, or delete operations. You should see the updates appear in real-time on all connected clients.

## Conclusion
This application provides a basic address book functionality, allowing users to view, create, update, and delete contacts. Real-time updates are provided using SignalR. The application can be extended to add more features and functionality as needed.
 