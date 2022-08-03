import React from "react";


 const MyFavoriteCard = ({myFaves, unlikeShoe}) => {
    return(
        <>
         <img src={myFaves.shoe?.imageUrl} alt="none" />
         <button onClick={() => unlikeShoe(myFaves.shoeId)}>Unlike</button>
        </>
    )
}

export default MyFavoriteCard