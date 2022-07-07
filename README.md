# TinyShop

## _Most useless shop ever. You'll love it!_
  
**TinyShop is an online e-comerce shop written in .NET Blazor Server**

This is test app where I play around with some technologies and architecture.
Original idea was to implement some sort of online shop in microservice manner.
For your convinience there is a docker-compose file in containerization
directory, so you can quickly spin up app for evaluation.
Do not forget to apply migration DB(Update-Database command) and seed fake data
from file TinyShop.Catalog\SeedDataPostgres.sql for test sake only.


## Features

- Database: Postgres
- Message broker: RabbitMq
- Full-Text Search: ElasticSearch
- RDB to Elastic sync: Logstash
- AAA: Duende IdentityServer
- Back+Front: Blazor server
- Front CSS: Fomantic-UI