## Migrations

```
dotnet ef migrations add -c BeanstalkDbContext -p ../Beanstalk.Db/ -s . InitialMigration
dotnet ef migrations script -i -c BeanstalkDbContext -p ../Beanstalk.Db/ -s . -o ../Beanstalk.Db/Migrations/InitialMigration.sql
dotnet ef database update -c BeanstalkDbContext -p ../Beanstalk.Db/ -s . InitialMigration
```
