import TodoItem from "./TodoItem";
export default function TodoList({todos}) {
    return (
        todos.map((item,index) => (
            <li key={index}>{item}</li>
        ))
    )
}