using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using todoApi.Models;
using todoApi.Validator;

namespace todoApi.Service
{
    public class TodoService
    {
        private readonly TodoContext _context;
        public TodoService(TodoContext context)
        {
            _context = context;
        }

        public async Task<TodoItem> AddTodoItem(TodoItem item)
        {
            TodoItemValidator todoItemValidator = new TodoItemValidator(_context);

            ValidationResult result = todoItemValidator.Validate(item);

            if (result.IsValid)
            {
                var itemReturn = await _context.AddAsync(item);
                await _context.SaveChangesAsync();
                
                return item;
            }
            else if (result.Errors.Select(w =>
                w.ErrorMessage.Contains("existe") &&
                w.ErrorMessage.Contains("tarefa")) != null)
                return new TodoItem { Name = result.Errors.FirstOrDefault().ErrorMessage };

            return null;
        }
    }
}