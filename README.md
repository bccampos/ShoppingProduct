# Shopping Product
I adopted the CQRS (Command Query Responsibility Segregation) and MediatR patterns, while adhering to Clean Architecture and Domain-Driven Design (DDD) principles.

## How to use and run the project

Open the solution and build the application. After the successful build run, you can run the command migration update-database. 
```bash
update-database
```

## :hammer:  API Backend 

![image](https://github.com/bccampos/ShoppingProduct/assets/36283909/555e9f50-3bac-4ced-a88b-d60ce77b8b7f)

- `Application`: Commands e Command Handler / Queries (Using EF)
- `Domain`: Entities / Value Objects / Interfaces / Validation 
- `Infrastructure`: Context EF / Repositories / Mappings / Migrations

## Swagger Shopping / Products API

![image](https://github.com/bccampos/ShoppingProduct/assets/36283909/79cd1c2d-2853-467d-8987-04415b84eaba)

## XUnit - Tests cover all the application (Commands handlers) / Domain (Services / Factories)

![image](https://github.com/bccampos/ShoppingProduct/assets/36283909/9a8f563a-4a46-49b5-b772-f3f94f91c677)

![image](https://github.com/bccampos/ShoppingProduct/assets/36283909/ece25c6c-a36b-43f7-a52f-518af73a79cb)

## Domain - Added a factory to be resposible to calculate by promotion type - following SOLID principles 

![image](https://github.com/bccampos/ShoppingProduct/assets/36283909/ce2875da-24c2-4471-acc8-d5f2273032eb)

* Test Case Coverage of Requirements for promotion 
![image](https://github.com/bccampos/ShoppingProduct/assets/36283909/c5d0bd0a-8ffe-447e-b55b-6529816d1a53)

