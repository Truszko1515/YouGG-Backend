import { useState } from "react"; 
import styles from "../CSS/Form.module.css";
export default function Form({todos, setTodos}) {
    const [todo, setTodo] = useState("");

    const handleSubmit = (event) => {
        event.preventDefault();
        setTodos([...todos, todo]);
        setTodo("");
        console.log(todos);
    }

    return (
        <form className={styles.todoform} onSubmit={handleSubmit}>
            <div className={styles.inputContainer}>
                <input
                    className={styles.modernInput}
                    onChange={(e) => setTodo(e.target.value)}
                    value={todo}
                    type="text"
                    placeholder="Enter todo item..."
                />
                <button className={styles.modernButton} type="submit">Add</button>
            </div>
        </form>
    )
}