Backend Instructions

1. Clone the repository: <br>
   git clone this-repo-url <br>
   cd your-backend-folder-name

2. Update the database connection string:
   - Open appsettings.json
   - Modify the "DefaultConnection" string to match your PostgreSQL setup

3. Open a terminal in the project directory and run the following commands:
   dotnet restore
   dotnet ef database update

4. Build the project:
   dotnet build

5. Run the application:
   dotnet run

The backend API should now be running on http://localhost:5193

API Endpoints:
- GET /api/devices - Retrieve all devices
- GET /api/devices/{id} - Retrieve a specific device
- POST /api/devices - Create a new device
- PUT /api/devices/{id} - Update an existing device
- DELETE /api/devices/{id} - Delete a device

- GET /api/employees - Retrieve all employees
- GET /api/employees/{id} - Retrieve a specific employee
- POST /api/employees - Create a new employee
- PUT /api/employees/{id} - Update an existing employee
- DELETE /api/employees/{id} - Delete an employee

Ensure that PostgreSQL is running before starting the backend application.
