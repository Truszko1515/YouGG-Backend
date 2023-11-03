import { nanoid } from 'nanoid';
import Fruit from "./Fruit"; 

const Fruits = () => {

    const fruits = [
        { name: "Apple", price: 10, emoji: "🍎", soldout:false , id: nanoid()},
        { name: "Pizza", price: 7, emoji: "🍕", soldout: false, id: nanoid()},
        { name: "Cake", price: 2, emoji: "🥧", soldout: true, id: nanoid()},
        { name: "Cucumber", price: 5, emoji: "🥒", soldout: false, id: nanoid()},
        { name: "Fries", price: 8, emoji: "🍟", soldout: true, id: nanoid()},
    ]
    
    return (
        <div>
            <ul>
                {fruits.map(fruit => (
                    <Fruit
                        name={fruit.name}
                        price={fruit.price}
                        emoji={fruit.emoji}
                        id={fruit.id}
                        soldout={fruit.soldout}
                    />
                ))}
            </ul>
        </div>
    );
}

export default Fruits;