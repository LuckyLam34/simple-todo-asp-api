using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public interface ITodoRepository
    {
        void AddItem( TodoItem item );
        IEnumerable<TodoItem> GetAllItems();
        TodoItem FindItem( string key );
        TodoItem RemoveItem( string key );
        void UpdateItem( TodoItem item );

    }
}
