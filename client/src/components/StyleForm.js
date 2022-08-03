import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { addANewStyle } from "../modules/styleManager";

const StyleForm = () => {
const [style, setStyle] = useState()
const nav = useNavigate();

    const addStyle = (event) => {
        event.preventDefault()
        addANewStyle(style).then(() => nav("/style") )
    }

    const handleFormInput = (event) => {
    const newStyle = {...style}
    const inputValue = event.target.value
    const inputId = event.target.id
    newStyle[inputId] = inputValue
    setStyle(newStyle)
    }


    return(
        <>
         <h3>Add a Style</h3>
        <label htmlFor="styleName">Style Name</label>
        <input type="text" id="Name" onChange={handleFormInput} />


        <button type="submit" onClick={addStyle}>Add</button>
        <button onClick={() => nav("/style")}>Cancel</button>
        </>
    )
}

export default StyleForm;