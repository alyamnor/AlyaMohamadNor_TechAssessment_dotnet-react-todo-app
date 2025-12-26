import { useState, useEffect } from "react";
import "./App.css";

function App() {
  const [todos, setTodos] = useState([]);
  const [newTodo, setNewTodo] = useState("");

const API_URL = "http://localhost:5000/api/todos";

  const fetchTodos = async () => {
    const response = await fetch(API_URL);
    const data = await response.json();
    setTodos(data);
  };

  useEffect(() => {
    fetchTodos();
  }, []);

  const addTodo = async () => {
    if (!newTodo.trim()) return alert("Title cannot be empty");
    const response = await fetch(API_URL, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ title: newTodo }),
    });
    const data = await response.json();
    setTodos([...todos, data]);
    setNewTodo("");
  };

  const deleteTodo = async (id) => {
    await fetch(`${API_URL}/${id}`, { method: "DELETE" });
    setTodos(todos.filter((t) => t.id !== id));
  };

  const toggleComplete = async (id) => {
    const todo = todos.find((t) => t.id === id);
    await fetch(`${API_URL}/${id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ ...todo, isCompleted: !todo.isCompleted }),
    });
    fetchTodos();
  };

  return (
    <div className="App">
      <h1>My Todo List</h1>

      <div style={{ display: "flex", marginBottom: "20px" }}>
        <input
          type="text"
          value={newTodo}
          onChange={(e) => setNewTodo(e.target.value)}
          placeholder="Add new todo..."
        />
        <button onClick={addTodo}>Add</button>
      </div>

      <ul>
        {todos.map((todo) => (
          <li
            key={todo.id}
            className={todo.isCompleted ? "completed" : ""}
          >
            <input
              type="checkbox"
              checked={todo.isCompleted}
              onChange={() => toggleComplete(todo.id)}
            />
            <span>{todo.title}</span>
            <button onClick={() => deleteTodo(todo.id)}>Delete</button>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default App;
