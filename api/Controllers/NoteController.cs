using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Dtos.Note;
using api.Extensions;
using api.Helpers;
using api.interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/note")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteRepository _noteRepo;
        private readonly UserManager<AppUser> _userManager;

        public NoteController(INoteRepository noteRepo, UserManager<AppUser> userManager)
        {
            _noteRepo = noteRepo;
            _userManager = userManager;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] NoteQueryOb noteQuery)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null)
            {
                return Unauthorized("User not found or token is invalid.");
            }
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var notes = await _noteRepo.GetAllAsync(noteQuery,appUser);
            var noteDTO = notes.Select(s => s.ToNoteDto());

            return Ok(noteDTO);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var username = User.GetUsername();
            var appuser = await _userManager.FindByNameAsync(username);
            if (appuser == null)
            {
                return Unauthorized("User not found or token is invalid.");
            }
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var note =await _noteRepo.GetByIdAsync(id,appuser.Id);

            if(note == null)
            {
                return NotFound("Note Not Found");
            }

            return Ok(note.ToNoteDto());
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateNoteDto noteDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var username = User.GetUsername();
            var AppUser = await _userManager.FindByNameAsync(username);

            var noteModel = noteDto.ToNoteFromCreate();
            noteModel.user_id = AppUser.Id;

            await _noteRepo.CreateAsync(noteModel);
            return CreatedAtAction(nameof(GetById), new {id = noteModel.Id}, noteModel.ToNoteDto());
            
        }
        [HttpPut]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateNoteDto noteDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var note = await _noteRepo.UpdateAsync(id,noteDto.ToNoteFromUpdate());

            if(note == null)
            {
                return NotFound("Note not found");
            }
            return Ok(note.ToNoteDto());
        }
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var notemodel = await _noteRepo.DeleteAsync(id);

            if (notemodel == null)
            {
                return NotFound("Note does not exist");
            }
            return Ok(notemodel);
        }

    }
}