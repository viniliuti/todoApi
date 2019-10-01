using System.Linq;
using todoApi.Models;

namespace todoApi.Service
{
    public class TodoItemService
    {
        private readonly TodoContext _context;

        public TodoItemService(TodoContext context)
        {
            _context = context;
        }
        public TodoItem GetByName(string name)
        {
            var item = _context
                .TodoItems
                .Where(w => w.Name == name)
                .FirstOrDefault();

            return item;
        }
    }
}
