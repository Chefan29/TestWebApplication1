using TestWebApplication1;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<INoteService, InMemoryNoteService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



var notes = new List<Note>();

app.MapGet("/api/notes", (INoteService service) =>
    Results.Ok(service.GetAll()));

app.MapGet("/api/notes/{id:int}", (int id, INoteService service) =>
{
    var note = service.GetById(id);
    return note is null ? Results.NotFound() : Results.Ok(note);
});

app.MapPost("/api/notes", (CreateNoteDto dto, INoteService service) =>
{
    var (ok, error, note) = service.Create(dto);

    if (!ok)
        return Results.BadRequest(new { error });

    return Results.Created($"/api/notes/{note!.Id}", note);
});

app.MapPut("/api/notes/{id:int}", (int id, UpdateNoteDto dto, INoteService service) =>
{
    var (ok, error, note) = service.Update(id, dto);

    if (!ok && error is not null)
        return Results.BadRequest(new { error });

    if (note is null)
        return Results.NotFound();

    return Results.Ok(note);
});

app.MapDelete("/api/notes/{id:int}", (int id, INoteService service) =>
{
    var deleted = service.Delete(id);
    return deleted ? Results.NoContent() : Results.NotFound();
});

app.Run();


