# Game Store Tracker

    Idea:
        -Create an API where a store can keep track the curret inventory of the store.

    Summary:
        -The Game Store Tracker will allow workers to check on inventory and track checkouts.

    ES: add some technical termonilgy

        1st
        The Game Store Traker is amied to help those starting a a buniess in which they sale games and consoles. This would allow the ower, user, to make a store asgin workers to the store Once workers are added they can then start adding invetory by inputing the games info, such as Name, Developer, Publisher, Year, and Condition. We can also do the same for Console which use Name, Developer, Year, and Condition. We then can create tags, a one word decription of teh game such as Action, so that we can assign them to games, they can have mutiple tags such as 1st person, action, and shooter. The worker, if cashier, sould be able to procees checkouts of an a item that was sold. As on outside user, not a worker or owner, to view teh current games avliable.

        2nd
        The Game Store Tracker is amied to help those starting a busniess in which they sale games and consoles. This would allow the owner, authenticated user, to make a store enity iin which he can start to add Workers and assagin positions. Once the workes are added we can the use CRUD(Creeat, Read, Updadte, and Delete) to manage the store's inventory in both conolses and games. Consoles recods are captured by using Name, Developer, Release Year, and Conditon, repersented as an enum. The games are captured the same way but we add Publisher. Then the owner can Create tags usch as Action, Shooter, and First Person, to name a few, to assign to games via a many-to-many relationship, mean that games can have mutiple tags and tags can have mutiple gaems. And whenm a worker process a checkout, point of sale, it should record the checkout and updated invintory. And from a public perpsective, unauthecatied user (guest), is allowed to view avilabe invitory of games nad consles.
        

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
