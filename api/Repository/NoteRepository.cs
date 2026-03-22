using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Note;
using api.Helpers;
using api.interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class NoteRepository : INoteRepository
    {
        private readonly ApplicationDBContext _context;
        public NoteRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Note> CreateAsync(Note noteMo)
        {
            await _context.Notes.AddAsync(noteMo);
            await _context.SaveChangesAsync();
            return noteMo;
        }

        public async Task<Note?> DeleteAsync(int id)
        {
            var notemodel = await _context.Notes.FirstOrDefaultAsync(x => x.Id ==id);

            if(notemodel == null)
            {
                return null;
            }
            _context.Notes.Remove(notemodel);
            await _context.SaveChangesAsync();
            return notemodel;
        }

        public async Task<List<Note>> GetAllAsync(NoteQueryOb noteQuery, AppUser user)
        {
            var notes = _context.Notes.Where(a => a.user_id == user.Id).AsQueryable();
            if(noteQuery.IsDecsending == true)
            {

                notes = notes
                    .OrderByDescending(p => p.is_pinned)    
                    .ThenByDescending(c => c.created_at);   
            }
            else
            {
            
                notes = notes
                    .OrderByDescending(p => p.is_pinned)    
                    .ThenBy(c => c.created_at);            
            }

            var skip = (noteQuery.PageNum - 1) * noteQuery.PageSize;
            return await notes.Skip(skip).Take(noteQuery.PageSize).ToListAsync();
        }

        public async Task<Note?> GetByIdAsync(int id)
        {
            return await _context.Notes.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Note?> UpdateAsync(int id, Note noteMo)
        {
            var checkNote = await _context.Notes.FindAsync(id);

            if(checkNote == null)
            {
                return null;
            }

            checkNote.title = noteMo.title;
            checkNote.content = noteMo.content;
            checkNote.is_pinned = noteMo.is_pinned;

            await _context.SaveChangesAsync();
            return checkNote;
        }
    }
}