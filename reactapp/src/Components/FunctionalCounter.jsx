import { useState, useEffect } from "react";

function FunctionalCounter() {
    const [counter, setCounter] = useState(0);


    const increment = () => {
        setCounter(counter + 1);
    };

    useEffect(() => {
        console.log("XDDD")
    })

    return (
        <div>
            <div>Counter value is: {counter}</div>
                <div>
                    <button onClick={increment}>Increment</button>
                </div>
        </div>
    );
}

export default FunctionalCounter;