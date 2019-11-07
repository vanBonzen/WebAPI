using System;
using WebAPI.Models;
using WebAPI.Service;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        #region Constructors
        private readonly PeopleContext _context;
        private readonly PeopleService _service;

        public PeopleController(PeopleContext context, PeopleService service)
        {
            _context = context;
            _service = service;
        }
        #endregion

        #region Create
        // POST: api/People
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            Person db = await _service.PostPerson(person);
            return CreatedAtAction("GetPerson", new { id = db.Id }, db);
        }
        #endregion

        #region Read
        // GET: api/People
        [HttpGet]
        public async Task<IEnumerable<Person>> Get()
        {
            return await _service.GetPeople();
        }

        // GET: api/People/805d3892-927d-47b9-a1a3-6fba6f0fe2f4
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(Guid id)
        {
            var result = await _service.Find(id);

            if (result is null) return NotFound();
            return result;
        }
        #endregion

        #region Update
        // PUT: api/People/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(Guid id, Person person)
        {
            var result = await _service.PutPerson(id, person);
            if (result is null) return NotFound();
            return result;
        }
        #endregion

        #region Delete
        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> DeletePerson(Guid id)
        {
            var person = await _service.DeletePerson(id);
            if (person == null) return NotFound();

            return person;
        }
        #endregion

        #region Helper
        private async Task<bool> PersonExists(Guid id)
        {
            return await _service.PersonExists(id);
        }
        #endregion
    }
}
