using Microsoft.AspNetCore.Mvc;

namespace TodoApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    // In-memory list of todos
    private static List<TodoItem> todos = new List<TodoItem>();
    private static int nextId = 1;

    // GET: api/todos
    [HttpGet]
    public IEnumerable<TodoItem> Get()
    {
        return todos;
    }

    // POST: api/todos
    [HttpPost]
    public IActionResult Post([FromBody] TodoItem item)
    {
        if (string.IsNullOrWhiteSpace(item.Title))
            return BadRequest("Title cannot be empty");

        item.Id = nextId++;
        todos.Add(item);
        return Ok(item);
    }

    // PUT: api/todos/{id}
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] TodoItem updatedItem)
    {
        var todo = todos.FirstOrDefault(t => t.Id == id);
        if (todo == null) return NotFound();

        if (string.IsNullOrWhiteSpace(updatedItem.Title))
            return BadRequest("Title cannot be empty");

        todo.Title = updatedItem.Title;
        todo.IsCompleted = updatedItem.IsCompleted;

        return Ok(todo);
    }

    // DELETE: api/todos/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var todo = todos.FirstOrDefault(t => t.Id == id);
        if (todo == null) return NotFound();

        todos.Remove(todo);
        return Ok();
    }

    // PATCH: api/todos/{id}/complete
    [HttpPatch("{id}/complete")]
    public IActionResult Complete(int id)
    {
        var todo = todos.FirstOrDefault(t => t.Id == id);
        if (todo == null) return NotFound();

        todo.IsCompleted = true;
        return Ok(todo);
    }
}

// Todo model
public class TodoItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; } = false;
}
