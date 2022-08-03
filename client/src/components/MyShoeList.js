import React, {useEffect, useState} from "react";
import { useNavigate } from "react-router-dom";
import { deleteAShoe, getAllOfMyShoes } from "../modules/shoeManager";
import MyShoeCard from "./MyShoeCard";
import "./MyShoeCard.css"

export const MyShoeList = () => {
    const [myShoes, setmyShoes] = useState([])

    const nav = useNavigate()

    const getMyShoes = () => {
        getAllOfMyShoes().then(setmyShoes)
    }

    const handleDeleteForShoes = (id) => {
        deleteAShoe(id).then(getMyShoes())
    }

    useEffect(() => {
        getMyShoes()
    },[])


    
    return(
        <>
        <button onClick={() => nav("/shoe/myshoes/add")}>New Shoe</button>
        <div>
           {
            myShoes.map((myShoe) => (
            <MyShoeCard myShoe={myShoe} key={myShoe.id} handleDelete={handleDeleteForShoes}  />
            ))
           } 
        </div>
        </>
    )
}

export default MyShoeList