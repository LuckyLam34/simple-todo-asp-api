﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Produces( "application/json" )]
    [Route( "api/Todo" )]
    public class TodoController : Controller
    {
        public TodoController( ITodoRepository todoItems )
        {
            TodoItems = todoItems;
        }
        public ITodoRepository TodoItems { get; set; }

        public IEnumerable<TodoItem> GetAll()
        {
            return TodoItems.GetAllItems();
        }

        [HttpGet( "{id}", Name = "GetTodo" )]
        public IActionResult GetById( string id )
        {
            var item = TodoItems.FindItem( id );
            if( item == null )
            {
                return NotFound();
            }
            return new ObjectResult( item );
        }

        [HttpPost]
        public IActionResult Create( [FromBody] TodoItem item )
        {
            if( item == null )
            {
                return BadRequest();
            }
            TodoItems.AddItem( item );

            return CreatedAtRoute( "GetTodo", new { id = item.Key }, item );
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] TodoItem item )
        {
            if(id == null || item.Key != id)
            {
                return BadRequest();
            }

            var todo = TodoItems.FindItem( id );
            if(todo == null)
            {
                return NotFound();
            }

            TodoItems.UpdateItem( item );
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            TodoItems.RemoveItem( id );
        }

    }
}