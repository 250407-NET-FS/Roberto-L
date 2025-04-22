# Game Store Tracker

    Idea:
        -Create an API where a store can keep track the curret inventory of the store.

    Summary:
        -The Game Store Tracker is aimed at helping those starting a business in which they sell games and consoles. This would allow the owner, authenticated user, to make a store entity in which he can start to add Workers and assign positions. We will also be able to manage the store's inventory, so that when a worker process a sale, it should record the checkout and update the inventory. This project required the use of many .NET technologies, including Entity Framework, unit testing with xUnit, and much more.
        

    Create an API thet does the follwoing:
        -Keep inventory of games and condition they are in.
        -Keep inventory of consoles and cindition they are in.
        -Keep track of checkots and update inventory.
        -Search for games via store, console, publisher, developer, name, and year.
        -Search for consoles via store, developer, name, and year.

    Models: 
        -Stores
            Id (A Key), name, Worker Id(FK)
        -Workers
            Id (A Key), store Id (FK), name, user (unique), pass, positoin
        -Consoles:
            Id (A Key), Store Id(FK), inventory, Price, condition, developer, name, and year
        -Games:
            Id (A Key), Store Id(FK), inventory, Price, condition, publisher, developer, name, and year
        -Tags
            Tag Id and name (1st, 2d, 3d, Action, Puzzle, etc.)
        -Game Tags
            Game Id (want so that a tag can have many games), Tag id (want it so game can can have many tags)
        -Store Managers
            Store Id, Worker Id
        -Checkout
            Workers Id (A Key), and consoles/games inventory, Price
        

    Strech Goals:
        -Make a simple website that allows for us to look for what games are available.
        -If website is possible try to have a image for items

## Overview (Due: 04/18/2025)

For this project, you'll create an ASP.NET Core MinimalAPI project, where the application will be interacting with users via HTTP calls. The project will conclude with a presentation of working software to trainers and colleagues.

## Hard Requirments (from trainers)

    Project hosted on GitHub
    README that describes the application and its functionalities
    ERD of your models and the relationships between them
    The application should be ASP.NET Core Minimal API application
    The application should build and run
    The application should have unit tests and at least 20% coverage
    The application should communicate via HTTP(s) (Must have POST, GET, DELETE)
    The application should be RESTful API
    Have 2 or more models
    Have at least one many to many relationship between the two models.

## Strech Goals (from trainers)

    Persisting data to a SQL Server DB
    The application should communicate to DB via EF Core (Entity Framework Core)

## Presentation

    You can either demo just API via Swagger or have a frontend (either a C# Console App, or web front end, your choice)
    Powerpoint (optional)
    Please keep the length of presentation to 5-10 minutes

## P1 Check-in (04/15/2024)

    Repo setup
    Minimal API and Xunit project setup and connected (atleast one test written)
    Model(s) and Repo(s) layer file structure setup
    At least 1 endpoint tie to a model
    README
    ERD
