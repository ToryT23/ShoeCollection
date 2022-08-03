import React, { useEffect, useState } from "react";
import { deleteStyleById, getAllStyles } from "../modules/styleManager";
import { StyleCard } from "./StyleCard";
import { Link } from "react-router-dom";



const StyleList = () => {

const [styles, setStyles] = useState([])

const getStyles = () => {
    getAllStyles().then(setStyles)
}

const handleDelete = (id) => {
    deleteStyleById(id).then(getStyles())
}

useEffect( () => {
    getStyles()
},[])



return(
    <>
    <Link to={"/style/add"}>Add a Style</Link>
    {
        styles.map((style) => (
        <StyleCard style={style} key={style.id} handleDelete={handleDelete}/>
        ))
    }
    </>
)

}

export default StyleList