# iidentifiiForum
API and datastore backend of a web forum for a small number of users. Written as a C# .NET 8 application with a test database for local development and testing.

---

## Getting Started

These instructions will get a copy of the project up and running on your local machine for assessment and development purposes.

### Prerequisites

Before you begin, ensure you have the following software installed on your machine.
* [**Git**](https://git-scm.com/downloads)

* [**.NET 8 SDK**](https://dotnet.microsoft.com/download/dotnet/8.0)

  * The SDK is required to build and run the C# project.

* [**SQL Server LocalDB**](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb) or another supported database engine

  * A local instance of SQL Server (e.g., SQL Server Express or LocalDB) is required to host the project's database.

* [**SqlPackage CLI**](https://learn.microsoft.com/en-us/sql/tools/sqlpackage/sqlpackage-download?view=sql-server-ver16)

  * This command-line tool is used to deploy the database schema from a DACPAC file. It's often included with SQL Server Management Studio (SSMS) but can also be installed as a standalone tool.

* [**Visual Studio 2022**](https://visualstudio.microsoft.com/) or [**Visual Studio Code**](https://code.visualstudio.com/)
  
* SQL Server Data Tools (SSDT)
  * Required to build the `Forum.Database` SQL Server project (`.sqlproj`) if you choose to use the command line, as it relies on Visual Studio targets that are not available to `dotnet build`.
  * Go to [Visual Studio Build Tools](https://visualstudio.microsoft.com/visual-cpp-build-tools/)
  * During installation, select SQL Server Data Tools under Desktop & Database Build Tools.

  *The Recommended approach is to build the solution in the **Visual Studio 2022** IDE  

### Installation

Follow these steps to set up the project and its database locally.

1.  **Clone the repository**

    ```
    git clone https://github.com/renesh007/iidentifiiForum.git
    ```

2.  **Navigate into the project directory**

    ```
    cd iidentifiiForum\Forum
    ```

3.  **Restore project dependencies**

    ```
    dotnet restore
    ```

    This command downloads and installs all the necessary NuGet packages for the solution.

---

## Usage

### Build Project 
Open the `.sln` file in `iidentifiiForum\Forum\Forum.sln` from **Visual Studio 2022** IDE and build the solution

**Note:** The Forum.Database project may fail to build with `dotnet build` alone due to missing SSDT targets.
To build the database project, open the solution in Visual Studio 2022 (with SSDT installed) and build it there.

**Database Setup**
After the solution has been built you can publish the databse locally. The project uses a DACPAC (`.dacpac` file) to manage its database schema.
To publish the database, use the `sqlpackage` tool:

```
sqlpackage /Action:Publish /SourceFile:"Forum.Database/bin/Debug/Forum.Database.dacpac" /TargetServerName:"(localdb)\MSSQLLocalDB" /TargetDatabaseName:"Forum"
```

* Replace `Forum.Database/bin/Debug/Forum.Database.dacpac` with the actual path to the `.dacpac` file within the solution. It can be changed to `Forum.Database/bin/Release` or `Forum.Database/bin/Debug` depending on your build profile

* Update the `/TargetServerName` if you are not using SQL Server LocalDB.

* The name of the database is `Forum`.

After running this command, ensure your project's `appsettings.json` file has the correct connection string to your newly created local database.
The default is: 
```
"ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=Forum;Integrated Security=true;"
}
```
## Run Application
To run the project from the IDE, select the `http` run profile and ensure you have the `Forum` project selected

This will start the web server. Open your web browser and navigate to `http://localhost:5520` (or the specific port listed in the console) to view the application.

## Testing

This project includes unit tests written using NUnit and Nsubstitute.  

To run the tests, navigate to the project root directory where the `.sln` file is located and use the .NET CLI:
```
dotnet test

```

### Postman Collections are avaliable in the project root directory

* `Forum API Integration Tests.postman_collection.json` can be used to with pre-populated data that exists in the seeded database. It also makes use of Pre-Request Scripts and Test Scripts (described below). 
* `Forum.postman_collection.json` is the standard collection with no pre-populated request data.   

#### How Pre-Request and Test Scripts Work

##### Pre-Request Scripts
Pre-request scripts in this collection are used to **automatically set the Authorization header** for each request based on the role required:

- A mapping is maintained between endpoint paths and token variables:
  - Admin-only endpoints (e.g., `/api/User/role`) use `adminToken`
  - Moderator-only endpoints (e.g., `/api/Tag`) use `moderatorToken`
  - Regular user endpoints (e.g., `/api/Post/create`, `/api/Comment`, `/api/Like`) use `userToken`

- Before the request is sent, the pre-request script checks the mapping:
  1. Finds the appropriate token variable.
  2. Retrieves the token from Postman collection variables.
  3. Sets the `Authorization: Bearer <token>` header automatically.

This ensures that the request always uses a valid token for the correct user role without manually adding headers.

---

##### Test Scripts
Test scripts are executed **after the response is received**:

- They verify the **HTTP status code** (e.g., 200 OK) to ensure the request succeeded.
- They **extract data from the response** and store it in collection variables for later use:
  - Example: storing `userToken` after login
  - Example: storing `postId` after creating a post

This allows subsequent requests to reuse values like tokens or IDs dynamically during integration testing.

---

#### Forum API Endpoints

Integration tests for forum API covering posts, comments, likes, moderation, filtering, and user roles.

---

##### User Registration and Login

- **Register New Regular User**  
  `POST /api/User/register`  
  Registers a new regular user with email, name, and password.

- **Login Regular User**  
  `POST /api/User/login`  
  Authenticates a regular user and returns a JWT token. (Seed data)

- **Login Moderator**  
  `POST /api/User/login`  
  Authenticates a moderator user and returns a JWT token.(Seed data)

- **Login Admin**  
  `POST /api/User/login`  
  Authenticates an admin user and returns a JWT token. (Seed data)

- **Change User Role to Moderator**  
  `PATCH /api/User/role`  
  Admin-only endpoint to promote a regular user to a moderator role.

---

##### Posts

- **Create Post**  
  `POST /api/Post/create`  
  Creates a new post with title and content. Requires authentication.

- **Get Posts (Paginated)**  
  `GET /api/Post/posts`  
  Retrieves a paginated list of posts. Supports filtering by author, date range, tags, and sorting (e.g., by likes).

- **Get Post Details**  
  `GET /api/Post/{postId}`  
  Retrieves detailed information for a specific post by its ID.

---

###### Comments

- **Add Comment**  
  `POST /api/Comment`  
  Adds a comment to a specific post. Requires authentication.

---

##### Likes

- **Like Post**  
  `POST /api/Like`  
  Add or Remove Like for a specific post. Requires authentication.

---

##### Moderation

- **Tag Post as Misleading**  
  `POST /api/Tag`  
  Moderator-only endpoint to mark a post as misleading.

---

### Seed Data

The following tables represent the initial seed data used to populate the database on a fresh installation.

#### `User Types`

This table contains the predefined user roles for the application.

| Id | Description | 
 | ----- | ----- | 
| 1 | Admin | 
| 2 | Moderator | 
| 3 | User | 

#### `Users`

This table contains the initial users, including an Admin, Moderator, and a regular User, for testing purposes.

| Id | Name | Email | UserTypeId | Password |
 | ----- | ----- | ----- | ----- | ----- |
| 11111111-1111-1111-1111-111111111111 | admin | admin@example.com | 1 | admin123 |
| 22222222-2222-2222-2222-222222222222 | mod | moderator@example.com | 2 | mod123 |
| 33333333-3333-3333-3333-333333333333 | user | user@example.com | 3 | user123 |

#### `Tags`

This table contains predefined tags that can be used to tag posts within the application.

| Name | Description | 
 | ----- | ----- | 
| Misleading | Posts that contain information that is misleading or deceptive. | 
| False Information | Posts that contain information proven to be false. | 

#### `Posts`

This table contains initial Posts that are used to populate the database on a fresh installation.

| Title | Content | UserId |
| ----- | ------- | ------ | 
| Welcome Post | This is the first post in the system. Welcome to our platform! | 11111111-1111-1111-1111-111111111111 (Admin) |
| Moderator Guidelines | This post outlines the guidelines for moderators to follow. | 22222222-2222-2222-2222-222222222222 (Moderator) |
| User Tips | Some helpful tips for regular users on how to navigate the platform. | 33333333-3333-3333-3333-333333333333 (User) |
