using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todoApi.Models;
using todoApi.Service;
using todoApi.Validator;

namespace todoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
            if (_context.TodoItems.Count() == 0)
            {
                _context.TodoItems.Add(new TodoItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<TodoItem>> GetAll()
        {
            return _context.TodoItems.ToList();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public ActionResult<TodoItem> GetById(long id)
        {
            var item = _context.TodoItems.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpGet("first")]
        public ActionResult<TodoItem> GetFirst()
        {
            var item = _context.TodoItems.FirstOrDefault();

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpGet("last")]
        public ActionResult<TodoItem> GetLast()
        {
            var item = _context.TodoItems.LastOrDefault();

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost]
        public async Task<StatusCodeResult> Post([FromBody]TodoItem body)
        {
            try
            {
                // TodoItem newItem = new TodoItem();
                TodoService todoService = new TodoService(_context);

                TodoItem todoReturn = await todoService.AddTodoItem(body);
                if (todoReturn.Name.Contains("existe"))
                    return Conflict();
                else if (todoReturn.Equals(body))
                    return Ok();
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}