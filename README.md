# TinyShop

## _Most useless shop ever. You'll love it!_
  
**TinyShop is an online e-comerce shop written in .NET Blazor Server**

This is test app where I play around with some technologies and architecture.
 The original idea was to implement some sort of online shop.
For your convinience there is a docker-compose file in containerization
directory, so you can quickly spin up app for evaluation.
Do not forget to publish DB schema on freshly instantiated SQL
server (from TinyShopDb\Publications). Publishing also seeds fake data
from file TinyShopDb\dbo\PostDeployment\SeedData.sql for test sake only.
If you don't need this - delete SeedData.sql file before publishing.
Also make sure EF core migrations has been done before using login system


## Features

- Database: MsSQL
- Message broker: RabbitMq
- Full-Text Search: ElasticSearch
- RDB to Elastic sync: Logstash
- Back+Front: Blazor server
- Front CSS: Fomantic-UI