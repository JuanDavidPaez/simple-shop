# SimpleShop

A simple shop application for learning purposes!

### Entity framework migrations

#### add-migration
````
add-migration ***__NameOfThemigration__*** -Project src\SimpleShop.Infrastructure -StartupProject src\SimpleShop.Infrastructure -OutputDir Persistence\Migrations
````

#### update-database
````
update-database -Project src\SimpleShop.Infrastructure -StartupProject src\SimpleShop.Infrastructure
````


