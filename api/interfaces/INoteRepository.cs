using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;
using api.Models;

namespace api.interfaces
{
    public interface INoteRepository
    {
        Task<List<Note>> GetAllAsync(NoteQueryOb noteQuery,AppUser user);
        Task<Note?> GetByIdAsync(int id);
        Task<Note> CreateAsync(Note noteMo);
        Task<Note?> UpdateAsync(int id,Note noteMo);
        Task<Note?> DeleteAsync(int id);
    }
}