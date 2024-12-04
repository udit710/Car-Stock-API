# Car-Stock-API

## Table of Contents
- [Description](#description)
- [Architecture](#architecture)
- [Database](#database)
- [Project Structure](#project-structure)
- [Pre-requisites](#pre-requisites)
- [Tech Stack](#tech-stack)
- [Features](#features)
- [How to run](#how-to-run)
- [API Endpoints](#api-endpoints)
- [Frontend](#frontend)

## Description
This is a simple API that allows you to manage a car stock. You can add, remove, update stock, list and search cars.

## Architecture
The API is built using ASP.NET Core and FastEndpoints. The frontend is built using Svelte. The API is secured using JWT Tokens and uses a simple username and password for authentication. Queries are executed using Dapper and SQLite. Validation is done using FluentValidation.
Frontend is built using Svelte and uses Axios to make requests to the API.

## Database
The database is a simple SQLite database with two tables, Dealers and Cars.

- Dealers: 
    - DealerId (Primary Key) : INT
    - Username : TEXT
    - Password : TEXT

- Cars:
    - CarId (Primary Key) : INT
    - DealerId (Foreign Key) : INT
    - Make : TEXT
    - Model : TEXT
    - Year : INT
    - StockCount : INT

## Project Structure

- CarStockAPI /
    - Endpoints /
        - Auth /
            - LoginEndpoint.cs
            - RegisterEndpoint.cs
        - Cars /
            - AddCarEndpoint.cs
            - RemoveCarEndpoint.cs
            - ListCarsEndpoint.cs
            - SearchCarsEndpoint.cs
            - UpdateStockEndpoint.cs
    - Data /
        - DbContext.cs
        - PopulateDb.cs
    - Models /
        - Car.cs
        - Dealer.cs
        - Requests /
            - AddCarRequest.cs
            - AuthRequest.cs
            - AuthResponse.cs
            - UpdateStockRequest.cs
    - Helpers /
        - JwtHelper.cs
    - appsettings.json
    - Program.cs
    - cars.db

- car-stock-frontend /
    - public /
    - src /
        - stores /
            - auth.js
        - routes /
            - AddCar.svelte
            - Login.svelte
            - Register.svelte
            - SearchCar.svelte
            - Navbar.svelte
        - App.svelte
        - main.js
    - package.json
    - rollup.config.js

## Pre-requisites
- .NET 8.0
- NPM 8.19.4

## Tech Stack
- ASP.NET Core 8.0
- FastEndpoints 5.32.0
- Svelte 3.59.2

## Features
- Register
- Login
- List all cars
- Add a car
- Search cars
- Update stock count
- Delete a car

## How to run
1. Clone the repository

```bash
git clone https://github.com/udit710/Car-Stock-API.git
```

2. Navigate to the backend directory

```bash
cd CarStockAPI
dotnet restore
dotnet run
```

The API will be running on `http://localhost:5000`

3. Navigate to the frontend directory

```bash
cd car-stock-frontend
npm install
npm run dev
```

The frontend will be running on `http://localhost:8080`

## API Endpoints
- ### Register
    - POST /auth/register
    - Request Body
    ```json
    {
        "username": "string",
        "password": "string"
    }
    ```
- ### Login
    - POST /auth/login
    - Request Body
    ```json
    {
        "username": "string",
        "password": "string"
    }
    ```
    - Response: Jwt Token (To be used as Authorization Bearer for all requests)
    ```json
    {
        "token": "string"
    }
    ```
    - 
- ### List all cars
    - GET /cars
    - Authorization: Bearer Token
    - Response: list of cars
    ```json
    [
        {
            "carId": "int",
            "dealerId": "int",
            "make": "string",
            "model": "string",
            "year": "string",
            "stockCount": "int"
        }
    ]
    ```

- ### Add a car
    - POST /cars
    - Authorization: Bearer Token
    - Request Body
    ```json
    {
        "make": "string",
        "model": "string",
        "year": "string",
        "stockCount": "string"
    }
    ```

    - Success Response
    ```json
    {
        "message": "string",
        "carId": "int"
    }
    ```

    - Error Response
    ```json
    "statusCode":"int",
    "message":"string",
    "errors":
        {
            "generalErrors":["string"]
        }
    ```

- ### Search cars
    - GET /cars/search?make=string&model=string
        - Search cars by make and model
    - Authorization: Bearer Token
    - Parameters: 
        ```json
        {
            "make": "string",
            "model": "string"
        }
        ```

    - Response: list of cars
    ```json
    [
        {
            "carId": "int",
            "dealerId": "int",
            "make": "string",
            "model": "string",
            "year": "string",
            "stockCount": "int"
        }
    ]
    ```

- ### Update stock count
    - PUT /cars/{carId}/stock
    - Authorization Bearer Token
    - Request Body
    ```json
    {
        "stockCount": "int"
    }
    ```

    - Success Response
    ```json
    {
        "message": "string"
    }
    ```

    - Error Response
     ```json
    "statusCode":"int",
    "message":"string",
    "errors":
        {
            "generalErrors":["string"]
        }
    ```

- ### Delete a car
    - DELETE /cars/{carId}
    - Authorization: Bearer Token
    - Success Response
    ```json
    {
        "message": "string"
    }
    ```

    - Error Response
     ```json
    "statusCode":"int",
    "message":"string",
    "errors":
        {
            "generalErrors":["string"]
        }
    ```

## Frontend

The frontend is built using Svelte. It allows you to register, login, list cars, add cars, search cars, update stock count and delete cars.
