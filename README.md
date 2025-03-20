# Products Web API

This project was developed to fulfill the goals outlined in each user story listed in the User Stories section below.

## Implementiation Details

### Application Architecture
The Product API follows a design inspired by Clean Architecture principles. For a more detailed explanation of this architectural approach, please refer to the link provided below.

Documentation link: [Click Here](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures#clean-architecture)

I chose to adopt this architecture due to its many advantages, including making the application easier to maintain, test, and extend.

### Problem Details
I used the ```ProblemDetails``` standard for the HTTP responses that represent application errors (400 and above status codes). They are great for conveying specific issues that were encountered by a REST API. 

Microsoft's ProblemDetails documentation: [Click Here](https://learn.microsoft.com/en-us/aspnet/core/web-api/handle-errors?view=aspnetcore-9.0#client-error-response)

#### Example
Below is an example of the request and response for a scenario where a caller attempts to create a product that already exists in the system.

##### Request
The intent is to create a product that already exists in the system.

```
{
  "id": 2,
  "description": "The best soda ever!",
  "price": 5.50,
  "quantity": 20
}
```

##### Response
The response clearly conveys that the product already exists in the system.

```
{
  "type": "https://localhost:44349/errors/invalid-product-id",
  "title": "Product with specified Id already exists.",
  "status": 400,
  "detail": "The product with Id:(2) already exists in the system.",
  "instance": "/Product",
  "traceId": "00-2922a2bae029d2f80aac936b4c2dea01-d4e422175a6cc2b9-00"
}
```

## Testing Notes
I manually validated that the application meets its objectives and provided unit tests for each layer, except for the Infrastructure.SqlServer layer.

### Running the app
To run the app and validate it against a live data source, please perform the following:

1. Run the script that is provided in the **Database Notes** section, so that the table is created within a database.
2. Provide the connectionstring of your SQL Server instance in the appsettings.json file. In particular update the ```ProductDatabaseConnection:ConnectionString``` configuration section.
3. If the app can successfully connect to a db instance, then it should work as expected.

Ideally, it would be much better if the app and a SQL Server instance can be containerized together using Docker. It would fascilitate local testing for developers. This approach is convenient because we won't have an external dependency, like having Microsoft SQL Server installed. Below I provide a link to this topic.

Microsoft's documentation: [Click Here](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/multi-container-microservice-net-applications/database-server-container#sql-server-running-as-a-container-with-a-microservice-related-database)

## Database Notes
This data storage provider that this application integrates with is Microsoft SQL Server.

### Table Creation Script

Below is the script to create the ```[dbo].[Product]``` table within an existing database. The schema below is designed to store the product data managed by the Web API.


```
USE [YOUR-DB-NAME-HERE]
GO

/****** Object:  Table [dbo].[Product]  ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Product](
	[Id] [bigint] NOT NULL,
	[Description] [text] NULL,
	[Price] [decimal](18, 4) NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
```

***

## User Stories

### User Story #1: Ability to view all of the products I am tracking in the system

#### Value

As a Store Owner, I want to retrieve all of the products I have entered into the software.

#### Acceptance Criteria

* I should be able to retrieve all of the properties for each Product (product Id, description, price, quantity).
* I should be able to see the field names and their associated values for each product.

### User Story #2: Add new products to the system

#### Value

As a Store Owner, I want to be add additional products to the software.

#### Acceptance Criteria

* I should be able to submit the information about that product (product Id, description, price, quantity).
* I should be made aware of invalid values that may have been submitted.
* After I successfully create the new product, it should be available in the system.

### User Story #3 Update an existing product in the system

#### Value

As a Store Owner, I want to be able to modify any of the products I've already entered into the software.

#### Acceptance Criteria

* For a given product, I should be able to submit updated information about that product (product Id, description, price, quantity).
* I should be made aware of invalid values have been submitted.
* After I successfully create the new product, it should be available in the system.

### User Story #4 Delete a product from the system

#### Value

As a Store Owner, I want to be able to delete any of the products I've already added into the software.

#### Acceptance Criteria
* I should be able to delete any of the existing products in the system.
* After I successfully delete that product, it should no longer be available in the system.