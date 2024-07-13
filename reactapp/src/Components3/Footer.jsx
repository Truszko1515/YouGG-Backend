import styles from "../CSS/Footer.module.css";
export default function Footer({ completedTodos, totalTodos }) {
    return (
        <div className={styles.footer}>
            <span className={styles.item}>Completed Tasks: {completedTodos}</span>
            <span className={styles.item}>Total Todos: {totalTodos}</span>
        </div>
    )
}