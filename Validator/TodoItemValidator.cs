using todoApi.Models;
using todoApi.Service;
using FluentValidation;

namespace todoApi.Validator
{
    public class TodoItemValidator : AbstractValidator<TodoItem>
    {
        private readonly TodoItemService _todoItemService;
        private readonly TodoContext _context;

        public TodoItemValidator(TodoContext context)
        {
            _context = context;
            _todoItemService = new TodoItemService(_context);

            RuleFor(r => r.Name)
            .NotNull();

            RuleFor(r => r.Id)
            .Equals(0);

            RuleFor(r => r.IsComplete)
            .NotNull();

            RuleFor(
                r => _todoItemService.GetByName(r.Name))
            .Empty()
            .WithMessage("Já existe esta tarefa");
        }
    }
}