using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Note
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string user_id { get; set; }
        public string title { get; set; }
        public string content { get; set; } = string.Empty;
        public bool is_pinned { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
    }
}