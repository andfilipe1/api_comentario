




docker run --name teste-postgres -e "POSTGRES_PASSWORD=Postgres2018!" -p 5432:5432 -d postgres

 

docker run --name teste-pgadmin  -p 15432:80 -e "PGADMIN_DEFAULT_EMAIL=luiz.brandao@ybtgpactual.com" -e "PGADMIN_DEFAULT_PASSWORD=PgAdmin2018!" -d dpage/pgadmin4

 
dotnet ef migrations remove InitialCreate

dotnet ef migrations add InitialCreate

dotnet ef migrations remove

dotnet ef database update InitialCreate

 To undo this action, use 'ef migrations remove'

dotnet ef database update

https://docs.microsoft.com/pt-br/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli

https://docs.microsoft.com/pt-br/ef/core/miscellaneous/cli/dotnet


 

172.17.0.2

5432

User: postgres

Senha : Postgres2018!