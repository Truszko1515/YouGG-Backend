const Fruit = ({name, price, emoji, id, soldout}) => {
    return (  
        <>           
            <li key={id}>{name} {price} {emoji} {soldout ? "Soldout" : ""} </li>
        </>   
    )
}

export default Fruit;