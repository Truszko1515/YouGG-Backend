import { useState } from "react";
import FunctionalCounter from "./Counter";

export default function ConditionalComponent() {
    const [display, setDissplay] = useState(true);

    return display ? (
        <div>
            <FunctionalCounter />
        </div>
    ) : (
        <div>
            <h3>Nothing to see here</h3>
        </div>
    );
    

} 