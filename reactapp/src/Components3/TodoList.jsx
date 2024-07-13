import TodoItem from "./TodoItem";
import styles from "../CSS/todoList.module.css";
export default function TodoList({ todos, setTodos }) {
    const sortedTodos = todos.sort((a, b) => Number(a.done) - Number(b.done));

    return (
        <div className={styles.list}>
            {sortedTodos.map((item, index) => (
                <TodoItem todo={item} todos={todos} setTodos={setTodos} key={item.name} />
            ))}
        </div>
    )
}