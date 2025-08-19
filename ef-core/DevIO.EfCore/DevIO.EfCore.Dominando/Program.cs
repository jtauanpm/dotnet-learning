// See https://aka.ms/new-console-template for more information

using DevIO.EfCore.Dominando.Configuration;
using DevIO.EfCore.Dominando.Data;
using Microsoft.Extensions.Configuration;

Settings.Configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

EnsureCreatedAndDeleted();
return;

static void EnsureCreatedAndDeleted()
{
    using var db = new ApplicationDbContext();
    db.Database.EnsureCreated();

    db.Database.EnsureDeleted();
}


