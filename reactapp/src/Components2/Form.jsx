import { useState } from "react";

const Form = () => {
    const [name, setName] = useState({ firstname: "", lastname: "" });
    const [names, setNames] = useState([]);

    const handleSubmit = (e) => {
        e.preventDefault();
        setNames([...names, { firstname: name.firstname, lastname: name.lastname }]);
        setName({ firstname: "", lastname: "" });
        console.log(names);
    }

    return (
        <div>
            {name.firstname} {name.lastname}
            <form onSubmit={handleSubmit}>
                <input
                    type="text"
                    value={name.firstname}
                    onChange={(e) => setName({...name, firstname: e.target.value})} 
                />
                <input
                    type="text"
                    value={name.lastname}
                    onChange={(e) => setName({...name, lastname: e.target.value})}
                />
                <button type="submit">Submit</button>
            </form>
            {names.map((name,index) => (
                <h3 key={index}>{name.firstname} {name.lastname}</h3>
            ))}
        </div>
    )
}

export default Form;