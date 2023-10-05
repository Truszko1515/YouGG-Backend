import "./App.css";
import Hello from "./Components/Hello";
import Profile from "./Components/Profile";


function App() {
    return (
        <div className="App">
            <Hello />
            <Profile name="Bartosz"/>
        </div>
    );
}

export default App;