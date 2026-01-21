namespace TestWebApplication1
{
    internal interface INoteService
    {
        List<Note> GetAll();
        Note? GetById(int id);
        (bool ok, string? error, Note? note) Create(CreateNoteDto dto);
        (bool ok, string? error, Note? note) Update(int id, UpdateNoteDto dto);
        bool Delete(int id);
    }
}
