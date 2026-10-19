using BrasilApiClient.Services;
using Scalar.AspNetCore;
using BrasilApiClient.Handlers;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddHttpClient<BrasilApiService>(client =>
{
  client.BaseAddress = new Uri("https://brasilapi.com.br/api/");
  client.Timeout = TimeSpan.FromSeconds(10);
});

builder.Services.AddExceptionHandler<ApiExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseStaticFiles(); // necessário para servir o config.js

  app.MapOpenApi();

  app.MapScalarApiReference(options =>
  {
    options
          .WithTitle("v1")
          .WithClassicLayout()
          .ExpandAllTags()
          .PreserveSchemaPropertyOrder()
          .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
          .EnablePersistentAuthentication()
          .HideModels()
          .WithTheme(ScalarTheme.DeepSpace)
          .HideDocumentDownload()
          .HideClientButton();
  });
}


app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
