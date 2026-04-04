using GameLibraryApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseSqlite("Data Source=games.db");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint("/openapi/v1.json", "v1");
	});
}

app.MapControllers();

app.UseHttpsRedirection();

app.Run();
