import "./App.css";
import { useState } from "react";
import Product from "./Components/Product";
import TodoList from "./Components/TodoList";
import Header from "./Components/Header";
import FormToDo from "./Components/FormToDo";
// 
import Hello from "./Components2/Hello";
import Fruits from "./Components2/Fruits";
import Form from "./Components2/Form";
//
import Todo from "./Components3/Todo";
import HeaderTodo from "./Components3/Header";

function App() {
    const [todo, setTodo] = useState("");
    const [todoList, setTodoList] = useState([])
    
    return (
        <div className="App">
            <HeaderTodo />
            <Todo />
        </div>
    );
}

export default App;




/*    
            //Todo app version 1

            const [todo, setTodo] = useState("");
            const [todoList, setTodoList] = useState([])
        
            App.jx body ;)

            <Header />
            <FormToDo
                todo={todo}
                setTodo={setTodo}
                todoList={todoList}
                setTodoList={setTodoList}
            ></FormToDo>
            <TodoList todoList={todoList} setTodoList={setTodoList} />  
*/

/*
            //Todo app version 2            

            <HeaderTodo />
            <Todo />
*/