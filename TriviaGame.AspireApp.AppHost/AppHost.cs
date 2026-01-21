var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.TriviaGame_Api>("triviagame-api");

builder.AddProject<Projects.TriviaGame_AppWeb>("triviagame-appweb");

builder.Build().Run();
