import styles from "../CSS/todoItem.module.css";

export default function TodoItem({ todo, todos, setTodos }) {
    const handleDelete = (todo) => {
        console.log("Delete button is clicked for item: ", todo.name);
        setTodos(todos.filter(item => item.name !== todo.name));
    }
    const handleClick = (name) => {

        const newTodos = todos.map(todo => todo.name === name ? {...todo, done: !todo.done} : todo);

        setTodos(newTodos);

        console.log(todos);
    }

    const todoIsCompleted = todo.done ? styles.completed : "";

    return (
        <div className={styles.item}>
            <div className={styles.itemName}>
                <span className={todoIsCompleted} onClick={() => handleClick(todo.name)}>
                    {todo.name} {todo.done}
                </span>
                <span>
                    <button className={styles.deleteButton} onClick={() => handleDelete(todo)}>
                        X
                    </button>
                </span>
            </div>
            <hr className={styles.line}></hr>
        </div>
    )
}
