# Lyra.Task.API

This API handles CRUD operation on 'Tasks' used by the [Lyra web application](https://lyra.jjeffr.in/). 

## Links

Swagger can be accessed [here](https://task.lyra.jjeffr.in/swagger/index.html)

API [Health](https://task.lyra.jjeffr.in/health) can also be checked. 

## How it works?

This is a .NET Core Web API created using .NET 7. It interacts with an Azure SQL Database using Entity Framework Core. Only requests with valid JWT access token will be authorized.
JWT token can be with obatined the [auth API](https://auth.lyra.jjeffr.in/swagger/index.html)
