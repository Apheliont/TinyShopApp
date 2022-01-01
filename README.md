# TinyShop

## _Most useless shop ever. You love it!_
  
**TinyShop is an online e-comerce shop written in Blazor C#**

For your convinience there is a docker-compose file in containerization
directory, so you can quickly run app for evaluation.
Do not forget to publish DB schema on freshly instantiated SQL
server (from TinyShopDb\Publications). Publishing also seeds fake data
from file TinyShopDb\dbo\PostDeployment\SeedData.sql for test sake only!
If you don't need this - delete SeedData.sql file.
Also make sure EF core migrations has been done before using login system


## Features

- Database: MsSQL
- Full-Text Search: ElasticSearch
- RDB to Elastic sync: Logstash
- Back+Front: Blazor server (.NET 6)
- Front CSS: Fomantic-UI