using AutoMapper;
using DevCommands.Data;
using DevCommands.DTOs;
using DevCommands.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DevCommands.Controllers
{
    [Route("api/commands")] // Class name not being used for route
    [ApiController]
    public class CommandsController : ControllerBase
    {
        //private readonly MockCommanderRepo _repo = new MockCommanderRepo();
        private readonly ICommanderRepo _repo;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        
        [HttpGet] // Get api/commands
        public ActionResult<IEnumerable<CommandReadDTO>> GetAllCommands() // Abstract Class
        {
            IEnumerable<Command> commandItems = _repo.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDTO>>(commandItems));
        }

        [HttpGet("{id}", Name = "GetCommandById")] // Appending to route api/commands/{id}
        public ActionResult<CommandReadDTO> GetCommandById(int id)
        {
            // Checks to see if command is found
            Command commandItem = _repo.GetCommandById(id);
            if(commandItem == null) return NotFound();

            return Ok(_mapper.Map<CommandReadDTO>(commandItem));
        }

        [HttpPost] // Post api/command
        public ActionResult<CommandCreateDTO> CreateCommand(CommandCreateDTO commandCreateDTO)
        {
            Command commandModel = _mapper.Map<Command>(commandCreateDTO);
            _repo.CreateCommand(commandModel);
            _repo.SaveChanges();

            CommandReadDTO commandReadDTO = _mapper.Map<CommandReadDTO>(commandModel);
            
            return CreatedAtRoute("GetCommandById", new { commandReadDTO.Id }, commandReadDTO); // Using HttpGet route name
        }

        [HttpPut("{id}")] //Put api/command/{id}
        public ActionResult<CommandUpdateDTO> UpdateCommand(int id, CommandUpdateDTO commandUpdateDTO)
        {
            // Checks to see if command is found
            Command commandModelRepo = _repo.GetCommandById(id);
            if (commandModelRepo == null) return NotFound();

            _mapper.Map(commandUpdateDTO, commandModelRepo);
            _repo.UpdateCommand(commandModelRepo);
            _repo.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")] //Patch api/commands/{id}
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDTO> jsonPatchDoc)
        {
            Command commandModelRepo = _repo.GetCommandById(id);
            if (commandModelRepo == null) return NotFound();

            CommandUpdateDTO commandToPatch = _mapper.Map<CommandUpdateDTO>(commandModelRepo); // new C.U.DTO to our command repo and into commandPatch

            jsonPatchDoc.ApplyTo(commandToPatch, ModelState);
            if (!TryValidateModel(commandToPatch)) return ValidationProblem(ModelState); // Validation

            _mapper.Map(commandToPatch, commandModelRepo); // update command patch to our repo
            _repo.UpdateCommand(commandModelRepo);
            _repo.SaveChanges();

            return NoContent();
        }
  
        [HttpDelete("{id}")] // Delete api/command/{id}
        public ActionResult DeleteCommand(int id)
        {
            Command commandModelRepo = _repo.GetCommandById(id);
            if (commandModelRepo == null) return NotFound();

            _repo.DeleteCommand(commandModelRepo);
            _repo.SaveChanges();

            return NoContent();
        }
    }
}
