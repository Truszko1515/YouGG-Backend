import { useState } from "react"; 
import styles from "../CSS/Form.module.css";
export default function Form({todos, setTodos}) {
    const [todo, setTodo] = useState({name: "", done: false});


    const handleSubmit = (event) => {
        event.preventDefault();
        setTodos([...todos, todo]);
        setTodo({name: "", done: false});
    }

    return (
        <form className={styles.todoform} onSubmit={handleSubmit}>
            <div className={styles.inputContainer}>
                <input
                    className={styles.modernInput}
                    onChange={(e) => setTodo({name: e.target.value, done: false})}
                    value={todo.name}
                    type="text"
                    placeholder="Enter todo item..."
                />
                <button className={styles.modernButton} type="submit">Add</button>
            </div>
        </form>
    )
}