using TestWebApplication1;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<INoteService, InMemoryNoteService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();


