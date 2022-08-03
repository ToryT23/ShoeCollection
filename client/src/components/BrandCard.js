import React from "react";


const BrandCard = ({brand, handleDelete}) => {
    
    
    return(
        <>
        <h3>{brand.brandName}</h3>
        <button onClick={() => handleDelete(brand.id)}>Delete</button>

        </>
    )
}

export default BrandCard