using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Note;
using api.Models;

namespace api.Mappers
{
    public static class NoteMapper
    {
        public static NoteDto ToNoteDto(this Note notemodel)
        {
            return new NoteDto
            {
              Id = notemodel.Id,
              user_id = notemodel.user_id,
              title = notemodel.title,
              content = notemodel.content,
              is_pinned = notemodel.is_pinned,
              created_at = notemodel.created_at

            };
        }
        public static Note ToNoteFromCreate(this CreateNoteDto createNoteDto)
        {
            return new Note
            {
                title = createNoteDto.title,
                content = createNoteDto.content,
                
            };
        }

        public static Note ToNoteFromUpdate(this UpdateNoteDto updateNoteDto)
        {
            return new Note
            {
              title = updateNoteDto.title,
              content = updateNoteDto.content  
            };
        }
    }
}