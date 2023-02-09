# ASP.NET Web API Demo

### API design for Northwind sql server database

```
.
├── src
|	├── Northwind.Api
|	├── Northwind.Service
|	├── Northwind.Repository
|	└── Northwind.Common
└── sql

```

### Northwind.Api

- Follow RestfulAPI to handle api routes.

### Northwind.Service

- Deal with business logic.
- Using AutoMapper for converting models.

### Northwind.Repository

- Handle connection to Database.
- Implement UnitOfWork Design.

### Northwind.Common

- Place some models and static helpers that's simple.
