using DevIO.IntroducaoEfCore.App.Configuration;
using Microsoft.Extensions.Configuration;

Settings.Configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();