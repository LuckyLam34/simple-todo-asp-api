using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class TodoRepository : ITodoRepository
    {
        private static ConcurrentDictionary<string, TodoItem> _todos = new ConcurrentDictionary<string, TodoItem>();

        public TodoRepository()
        {
            AddItem( new TodoItem { Name = "Item Hello fucking world" } );
        }
        
        public IEnumerable<TodoItem> GetAllItems()
        {
            return _todos.Values;
        }

        public void AddItem( TodoItem item )
        {
            item.Key = Guid.NewGuid().ToString();
            _todos[ item.Key ] = item;
        }

        public TodoItem FindItem( string key )
        {
            TodoItem item;
            _todos.TryGetValue( key, out item );

            return item;
        }

        public TodoItem RemoveItem( string key )
        {
            TodoItem item;
            _todos.TryGetValue( key, out item );
            _todos.TryRemove( key, out item );

            return item;
        }

        public void UpdateItem( TodoItem item )
        {
            _todos[ item.Key ] = item;
        }
    }
}
