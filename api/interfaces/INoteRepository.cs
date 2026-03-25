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
        Task<List<Note?>> GetAllAsync(NoteQueryOb noteQuery,AppUser user);
        Task<Note?> GetByIdAsync(int id,string appUser);
        Task<Note> CreateAsync(Note noteMo);
        Task<Note?> UpdateAsync(int id,Note noteMo,string appUser);
        Task<Note?> DeleteAsync(int id);
    }
}