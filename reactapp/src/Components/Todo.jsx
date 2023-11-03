import styles from "../CSS/Style.module.css";

const Todo = ({ todo ,  todoList ,  setTodoList }) => {
    const deleteTodo = () => {
        setTodoList(todoList.filter((item) => item.id !== todo.id));
    };

    return (
        <>
            <div className={styles.todoItem}>
                <h3 className={styles.todoName}>{todo.name} && {todo.id}</h3>
                <button className={styles.deleteButton} onClick={deleteTodo}>Done</button>
            </div>
        </>
    )
}

export default Todo;