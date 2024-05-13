using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CrystalSharp;
using CrystalSharp.MsSql.Extensions;
using CrystalSharp.MsSql.Migrator;
using CrystalSharp.PostgreSql.Extensions;
using CrystalSharp.PostgreSql.Migrator;
using CSSagaOrchestrationPostgreSqlExample.Application.TripSaga;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string eventStoreConnectionString = builder.Configuration.GetConnectionString("AppEventStoreConnectionString");
string sagaStoreConnectionString = builder.Configuration.GetConnectionString("AppSagaStoreConnectionString");
MsSqlSettings eventStoreDbSettings = new(eventStoreConnectionString);
PostgreSqlSettings sagaStoreDbSettings = new(sagaStoreConnectionString);

IResolver resolver = CrystalSharpAdapter.New(builder.Services)
    .AddCqrs(typeof(PlanTripTransaction))
    .AddMsSqlEventStoreDb<int>(eventStoreDbSettings)
    .AddPostgreSqlSagaStore(sagaStoreDbSettings, typeof(PlanTripTransaction))
    .CreateResolver();

IMsSqlDatabaseMigrator eventStoreDbMigrator = resolver.Resolve<IMsSqlDatabaseMigrator>();

MsSqlEventStoreSetup.Run(eventStoreDbMigrator, eventStoreDbSettings.ConnectionString).Wait();

IPostgreSqlDatabaseMigrator sagaStoreDbMigrator = resolver.Resolve<IPostgreSqlDatabaseMigrator>();

PostgreSqlSagaStoreSetup.Run(sagaStoreDbMigrator, sagaStoreDbSettings.ConnectionString).Wait();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
