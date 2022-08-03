import React from "react";


export const ShoeCard = ({shoe, likeButton, favored}) => {

return(

        <>
       
        <div className="shoeCard">
        <div>
                <p>These shoes are a size {shoe.size}.</p>
                <p>The shoe brand is {shoe.brand?.brandName}.</p>
                <p>The {shoe.brand.brandName} style is a/an {shoe.style?.name}.</p>
                <p>Added By: {shoe?.user.firstName} {shoe?.user.lastName}</p>
                <p>For More Info Contact: {shoe?.user.email}</p>
        {/* <h3>Shoe Size: {shoe.size}</h3>
        <h3>Brand Name: {shoe.brand?.brandName}</h3>
        <h3>Style Name: {shoe.style?.name}</h3>    */}
        <img src={shoe.imageUrl} alt="none" />  
         
        {!favored ? <button  className="button button-like" onClick={() => likeButton(shoe.id)}  >
                <i className="fa fa-heart"></i>
                <span>Like</span>
        </button>  : null}
        </div>
        </div>
        </>
)
}

export default ShoeCard