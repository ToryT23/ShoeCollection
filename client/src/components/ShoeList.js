import React, {useState, useEffect} from "react";
import { addAFavorite, getAllShoes, getAUsersListOfFavorites } from "../modules/shoeManager";
import ShoeCard from "./ShoeCard";
import "./ShoeCard.css"


export const ShoeList = () => {

    const [shoes, setShoes] = useState([])
    const [favshoes, setFavShoes] = useState([])


    const getShoes = () => {
        getAllShoes().then(setShoes)
        getAUsersListOfFavorites().then(setFavShoes)
    }

    const isFavorite = (id) => {
        if(favshoes.find((fav) => fav.shoeId === id))
        {
            return true
        }
        else {
            return false
        }
    }
    
    const useLikeButton = (shoe) => {
      
            console.log(typeof shoe)
            addAFavorite(shoe).then(() => {getShoes()})
    }
    
    useEffect(() => {
        getShoes()
    },[])

    return(
        <>
        <div className="shoeList">
           {
            shoes.map( (shoe) => (
            <ShoeCard shoe={shoe} key={shoe.id} likeButton={useLikeButton} favored={isFavorite(shoe.id)}  />
            ))
           } 
        </div>
        
        
        
        </>
    )
}

export default ShoeList