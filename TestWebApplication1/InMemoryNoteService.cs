namespace TestWebApplication1
{
    internal class InMemoryNoteService: INoteService
    {
        private readonly List<Note> _notes = new();
        private int _nextId = 1;

        public List<Note> GetAll() => _notes;

        public Note? GetById(int id) =>
            _notes.FirstOrDefault(n => n.Id == id);

        public (bool ok, string? error, Note? note) Create(CreateNoteDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                return (false, "Требуется заголовок", null);

            var note = new Note(
                _nextId++,
                dto.Title.Trim(),
                dto.Content ?? "",
                DateTime.UtcNow
            );

            _notes.Add(note);
            return (true, null, note);
        }

        public (bool ok, string? error, Note? note) Update(int id, UpdateNoteDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                return (false, "Требуется заголовок", null);

            var index = _notes.FindIndex(n => n.Id == id);
            if (index == -1)
                return (false, null, null);

            _notes[index] = _notes[index] with
            {
                Title = dto.Title.Trim(),
                Content = dto.Content ?? ""
            };

            return (true, null, _notes[index]);
        }

        public bool Delete(int id)
        {
            var note = GetById(id);
            if (note is null) return false;
            _notes.Remove(note);
            return true;
        }
    }
}
