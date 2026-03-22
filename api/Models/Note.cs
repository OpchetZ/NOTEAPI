using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Notes")]
    public class Note
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string? content { get; set; }
        public bool is_pinned { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;

        public string user_id { get; set; }
        public AppUser AppUser {get; set;}
    }
}