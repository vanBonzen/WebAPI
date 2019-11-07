using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Service
{
    public class PeopleService
    {
        #region Constructors
        private PeopleContext _context { get; set; }

        public PeopleService(PeopleContext context)
        {
            _context = context;
        }
        #endregion

        #region Create
        public async Task<Person> PostPerson(Person person)
        {
            _context.Database.EnsureCreated();

            _context.Person.Add(person);
            await _context.SaveChangesAsync();

            return person;
        }
        #endregion

        #region Read
        public async Task<IEnumerable<Person>> GetPeople()
        {
            _context.Database.EnsureCreated();

            var people = await _context.Person.ToListAsync();

            return _context.Person.ToList();
        }
        public async Task<ActionResult<Person>> Find(Guid searchTerm)
        {
            _context.Database.EnsureCreated();

            var person = await _context.Person.FindAsync(searchTerm);

            if (person == null)
            {
                return null;
            }
            return person;
        }
        #endregion

        #region Update
        public async Task<ActionResult> PutPerson(Guid id, Person person)
        {
            _context.Database.EnsureCreated();
            if (id != person.Id) return null;

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PersonExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return null;
        }
        #endregion

        #region Delete
        public async Task<Person> DeletePerson(Guid id)
        {
            _context.Database.EnsureCreated();

            var person = await _context.Person.FindAsync(id);
            _context.Person.Remove(person);

            await _context.SaveChangesAsync();

            return person;
        }
        #endregion

        #region Helpers
        public async Task<bool> PersonExists(Guid id)
        {
            return await _context.Person.AnyAsync(e => e.Id == id);
        }

        public void Add(Person p)
        {
            _context.Person.Add(p);
            _context.SaveChanges();
        }
        #endregion
    }
}
