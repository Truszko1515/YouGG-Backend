import Todo from "./Todo";

const TodoList = ({ todoList, setTodoList }) => {
    return (
        <div>
            {todoList.map((todoItem, index) => (
                <Todo
                    key={todoItem.id}
                    todo={todoItem}
                    todoList={todoList}
                    setTodoList={setTodoList}
                ></Todo>
            ))}
        </div>
    )
};

export default TodoList;