# ShoppingCartApi

An API for managing a simple shopping cart service.

## Description

The ShoppingCartApi allows consumers to add items to a cart and view items currently in the cart. It supports typical operations such as adding products to the cart and retrieving all cart items. The API ensures that product information such as Name, UnitPrice, and Quantity are correctly managed and validated.

## Getting Started

### Dependencies

- .NET 8.0 SDK
- SQL Server
- Git (for version control)
- IDE (Visual Studio Code)

### Installing and Executing program

- How to run the program
- Clone the repository

```
git clone https://github.com/jeet-patel313/Task-ShoppingCartApi.git
```

- Install dependencies

```
cd ShoppingCartApi
dotnet restore
```

- Set up Environment variables

```
DEFAULT_CONNECTION=<your_connection_string_here>
```

- Run Database Migrations

```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

- Executing Api

```
dotnet run
```

### Executing tests

- Move to Test folder

```
cd ShoppingCartApi.Tests
```

- Running Tests

```
dotnet test
```

## Author

Jeet Patel
[@JeetPatel](https://www.linkedin.com/in/jeetpatel313/)
