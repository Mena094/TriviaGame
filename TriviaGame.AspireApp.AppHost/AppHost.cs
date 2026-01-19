var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.TriviaGame_Api>("triviagame-api");

builder.Build().Run();
