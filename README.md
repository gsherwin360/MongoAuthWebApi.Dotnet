# MongoAuthWebApi.DotnetCore
In this demo API, I've implemented secure user authentication and management using MongoDB as the database. The API is built on the `AspNetCore.Identity.MongoDbCore` and introduces key features such as user registration, JWT-based authentication, and user lockout capabilities.

## Key Features
- **Secure User Registration and Login**: Users can securely register and log in to their accounts.
- **JWT-Based Authentication**: Enhanced security with JSON Web Token (JWT) authentication.
- **User Lockout Functionality**: Enhanced security by implementing user lockout features. In this API, a user's account is temporarily locked after five consecutive failed login attempts. The account remains locked for five minutes, after which the user can attempt to log in again. This feature helps against unauthorized access and brute force attacks.

## Development Prerequisites
Before diving into development with this project, ensure you have the following prerequisites:

- **Development Environment**: Either Visual Studio 2022 (IDE) or Visual Studio Code (Source-code editor)
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
1. Ensure MongoDB and Mongo Express containers are running by following the steps above.
2. Open the project in your preferred development environment.
3. Build and run the project.
4. Access the API documentation via [Swagger](http://localhost:5281/swagger/index.html) in your web browser while the API and MongoDB Containers are running.

## Screenshots

### API:
- Overview: ![API Overview](https://github.com/gsherwin360/MongoAuthWebApi.DotnetCore/assets/17651320/083d982c-d3b9-4575-8eab-8bc7caae38ac)
- JWT Authentication: ![JWT Authentication](https://github.com/gsherwin360/MongoAuthWebApi.DotnetCore/assets/17651320/651e075e-6438-4f38-aec5-6650db598966)
- User Lockout Functionality: ![User Lockout](![image](https://github.com/gsherwin360/MongoAuthWebApi.DotnetCore/assets/17651320/cde7df59-42a2-4195-95c9-0edbd7688def)
- Fetch User List (Authenticated Request): ![image](https://github.com/gsherwin360/MongoAuthWebApi.DotnetCore/assets/17651320/9191ae13-3bdd-43bd-9b6c-a751d678f686)
  
---
### Mongo Express:
- Overview: ![Mongo Express Overview](https://github.com/gsherwin360/MongoAuthWebApi.DotnetCore/assets/17651320/21e9d375-797a-47a0-bce8-898c6aeae73a)

---
### Docker:
- Overview: ![Docker Overview](https://github.com/gsherwin360/MongoAuthWebApi.DotnetCore/assets/17651320/3400d7bb-2502-4d94-8a46-f54dfbc3517e)




