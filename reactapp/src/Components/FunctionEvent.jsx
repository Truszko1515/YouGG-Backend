function FunctionEvent() {

    const handleClick = () => {
        console.log("function event happened");
    }

    return (
        <div>
            Functional Component
            <button onClick={ handleClick }>Przycisk</button>
        </div>
    );

}

export default FunctionEvent;