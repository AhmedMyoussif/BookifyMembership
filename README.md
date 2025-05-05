# Bookify User Membership API

## Overview
This is a simple User Membership API for Bookify with registration, login, and membership tiers.

## Endpoints
- **POST /api/members/register**: Register a new user with username, email, and password.
- **POST /api/members/login**: Login with username or email and password.
- **GET /api/members/{userId}:
- **Retrieve user details.

## How to test
1. Run the project locally using Visual Studio or VS Code.
2. Use **Postman** or **Swagger** to test the endpoints.
3. For registration, send a POST request to `/api/members/register` with JSON data containing username, email, and password.
4. For login, send a POST request to `/api/members/login` with username/email and password.

## Example:
- Register: 
  ```json
  {
    "username": "john_doe",
    "email": "john@example.com",
    "password": "strongpassword"
  }
