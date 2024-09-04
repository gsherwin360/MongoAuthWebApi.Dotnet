# MongoAuthWebApi.Dotnet
In this demo API, I've implemented secure user authentication and management using MongoDB as the database. The API is built on `AspNetCore.Identity.MongoDbCore` and introduces key features such as user registration, JWT-based authentication, and user lockout.

## Key Features
- **Secure User Registration and Login**: Users can securely register and log in to their accounts.
- **JWT-Based Authentication**: Enhanced security with JSON Web Token (JWT) authentication.
- **User Lockout**: Enhanced security by implementing user lockout features. In this API, a user's account is temporarily locked after five consecutive failed login attempts. The account remains locked for five minutes, after which the user can attempt to log in again. This feature helps protect against unauthorized access and brute force attacks.

## Development Prerequisites
Before diving into development with this project, ensure you have the following prerequisites:

- **Development Environment**: Either Visual Studio 2022 (IDE) or Visual Studio Code (source-code editor)
- **.NET 8**: Required framework version for building and running the project
- **Docker Desktop**: Required for running both MongoDB and Mongo Express (web-based administrative interface for MongoDB) within containers 

## Getting Started
To run the API locally, follow these steps:

### Part 1: Running MongoDB and Mongo Express Containers
1. Clone this repository to your local machine.
2. Ensure you have Docker installed and running.
3. Open a shell and navigate to the tools folder within the cloned repository.
4. Run the following command to start MongoDB and Mongo Express containers in detached mode: 
   ```bash
   docker-compose up -d
5. Once the containers are running, you can connect to Mongo Express by visiting http://localhost:8081 in your web browser. Please note that the default basicAuth credentials in Mongo Express are "admin:pass". It is highly recommended to change these credentials to ensure the security of your application.

### Part 2: Running the Application
1. Ensure that MongoDB and Mongo Express containers are running by following the steps in Part 1.
2. Open the project in your preferred development environment.
3. Build and run the project using the appropriate commands or methods for your environment.

## Screenshots

### API:
- Overview: ![API Overview](https://github.com/gsherwin360/MongoAuthWebApi.DotnetCore/assets/17651320/083d982c-d3b9-4575-8eab-8bc7caae38ac)
- JWT Authentication: ![JWT Authentication](https://github.com/user-attachments/assets/98f4cf6d-911f-4215-aacf-42446fec802c)
- User Lockout: ![User Lockout](https://github.com/user-attachments/assets/305a035b-94a4-426f-af48-015e53591f8d)
- Fetch User List (Authenticated Request): ![Fetch_User_List_Authenticated_Request](https://github.com/user-attachments/assets/5eba30bc-be4f-4e5b-89f6-7eece66e13a7)
  
---
### Mongo Express:
- Overview: ![Mongo Express Overview](https://github.com/user-attachments/assets/bf4a1ab8-4c24-4b18-a6bb-b72770284437)
  
---
### Docker:
- Overview: ![Docker Overview](https://github.com/gsherwin360/MongoAuthWebApi.DotnetCore/assets/17651320/3400d7bb-2502-4d94-8a46-f54dfbc3517e)




