var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var sqlServer = builder.AddSqlServer("sqlServer")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithContainerName("SqlServerContainer");

var db = sqlServer.AddDatabase("weatherDb");

var apiService = builder.AddProject<Projects.AspireAppNet9_ApiService>("apiservice")
    .WithReference(db)
    .WaitFor(db);

builder.AddProject<Projects.AspireAppNet9_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
