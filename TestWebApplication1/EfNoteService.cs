using TestWebApplication1.Data;
using Microsoft.EntityFrameworkCore;
namespace TestWebApplication1
{
    public class EfNoteService: INoteService
    {
        private readonly AppDbContext _db;

        public EfNoteService(AppDbContext db) => _db = db;

        public List<Note> GetAll() =>
            _db.Notes.AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new Note(x.Id, x.Title, x.Content, x.CreatedAt))
                .ToList();

        public Note? GetById(int id) =>
            _db.Notes.AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new Note(x.Id, x.Title, x.Content, x.CreatedAt))
                .FirstOrDefault();

        public (bool ok, string? error, Note? note) Create(CreateNoteDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                return (false, "Title is required", null);

            var e = new NoteEntity
            {
                Title = dto.Title.Trim(),
                Content = dto.Content ?? "",
                CreatedAt = DateTime.UtcNow
            };

            _db.Notes.Add(e);
            _db.SaveChanges();

            return (true, null, new Note(e.Id, e.Title, e.Content, e.CreatedAt));
        }

        public (bool ok, string? error, Note? note) Update(int id, UpdateNoteDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                return (false, "Title is required", null);

            var e = _db.Notes.FirstOrDefault(x => x.Id == id);
            if (e is null) return (false, null, null);

            e.Title = dto.Title.Trim();
            e.Content = dto.Content ?? "";
            _db.SaveChanges();

            return (true, null, new Note(e.Id, e.Title, e.Content, e.CreatedAt));
        }

        public bool Delete(int id)
        {
            var e = _db.Notes.FirstOrDefault(x => x.Id == id);
            if (e is null) return false;

            _db.Notes.Remove(e);
            _db.SaveChanges();
            return true;
        }
    }
}
