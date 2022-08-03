import React, {useState, useEffect} from "react";
import { deleteAFav, getAUsersListOfFavorites } from "../modules/shoeManager";
import MyFavoriteCard from "./MyFavoriteCard";

const MyFavoritesList = () => {
    const [favorites, setFavorites] = useState([])

    const myFaves = () => {
        getAUsersListOfFavorites().then(setFavorites)
    }

     const unLikeAShoe = (id) => {
        console.log(id)
      deleteAFav(id).then(myFaves())
      }



    useEffect(() => {
        myFaves()
    },[])


    return (
        <>
            <h1>My Favorite Shoes</h1>
        {
            favorites.length > 0 ? favorites.map((fave) => {
                return (
                    <MyFavoriteCard
                    key={fave.id} 
                    myFaves={fave}
                    unlikeShoe ={unLikeAShoe}
                    />
                )
            } ) : 
            <h3>Please Like A Shoe</h3>
        }
        
        </>
    )

}

export default MyFavoritesList