import React from "react";
import { Link } from "react-router-dom";


export const MyShoeCard = ({myShoe, handleDelete, }) => {

return(

        <>
        <div className="myShoeCard">

        <h3>Shoe Size: {myShoe.size}</h3>
        <h3>Brand Name: {myShoe.brand?.brandName}</h3>
        <h3>Style Name: {myShoe.style?.name}</h3>   
        <img src={myShoe.imageUrl} alt="none" />  
        <div>
        <button onClick={() => handleDelete(myShoe.id)}>Delete</button> 
        
        <Link to={`/shoe/myshoes/edit/${myShoe.id}`} >
        <button>Edit</button> 
        </Link>
                </div> 
        </div>

        </>
)
}

export default MyShoeCard