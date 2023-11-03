import styles from "../CSS/Style.module.css";
import { nanoid } from 'nanoid';


const FormToDo = ({ todo, setTodo, todoList, setTodoList }) => {
    const handleChange = (event) => {
        setTodo(event.target.value);
    }
    const handleSubmit = (event) => {
        event.preventDefault();
        setTodoList([...todoList, { name: todo, id: nanoid() }]);
        setTodo("");
        console.log(todoList);
    }

    return (
        <div className={styles.Form}>
            <form onSubmit={handleSubmit}>
                <input
                    value={todo}
                    onChange={handleChange}
                    className={styles.todoInput}
                    placeholder="Add Todo Item"
                ></input>
                <button className={styles.todoButton}>Add</button>
            </form>
        </div>
    );
}

export default FormToDo;