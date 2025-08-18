// See https://aka.ms/new-console-template for more information

using DevIO.EfCore.Dominando.Configuration;
using Microsoft.Extensions.Configuration;

Settings.Configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();


