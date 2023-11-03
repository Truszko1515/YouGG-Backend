import { useState } from "react";
import UserComponent from "./UserComponent";
import styles from "../CSS/Style.module.css";

export default function Form() {
    const [firstname, setFirstname] = useState("");
    const [lastname, setLastname] = useState("");
    const [userList, setUserList] = useState([]);

    const handleFirstnameChange = (event) => {
        setFirstname(event.target.value);
    }
    const handleLastnameChange = (event) => {
        setLastname(event.target.value);
    }
    const handleSubmit = (event) => {
        event.preventDefault();
      
        let tmpList = userList;
        tmpList.push({ firstname, lastname });

        setUserList(tmpList);
        setFirstname("");
        setLastname("");
        console.log(userList);
    }

    return (
        <div className={style.form}>
            Form
            <form onSubmit={handleSubmit}>
                <input
                    onChange={handleFirstnameChange}
                    type="text"
                    value={firstname} >
                </input>

                <input
                    onChange={handleLastnameChange}
                    type="text"
                    value={lastname} >
                </input>
                <button type="submit">Submit</button>  
            </form>
            {userList.map((user, index) => (
                <UserComponent
                    firstname={user.firstname}
                    lastname={user.lastname}
                    key={index}>
                </UserComponent>
            ))}
        </div>
    );
}