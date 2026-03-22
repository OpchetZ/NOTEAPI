using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Note
{
    public class UpdateNoteDto
    {
        [Required]
        public string title { get; set; } = string.Empty;
        [Required]
        [MaxLength(500, ErrorMessage ="It's over 500 character")]
        public string content { get; set; } = string.Empty;
        [Required]
        public bool is_pinned { get; set; }
    }
}