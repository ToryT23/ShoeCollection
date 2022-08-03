import React, {useState, useEffect} from "react";
import { useNavigate } from "react-router-dom";
import { addANewBrand } from "../modules/brandManager";

const BrandForm = () => {
    const [brand, setBrand] = useState({})
    const navigate = useNavigate();


    const addBrand = (event) => {
        event.preventDefault()
        addANewBrand(brand).then(() => navigate(`/brand`))
    }

    const handleFormInput = (event) => {
        const newBrand = {...brand}
        const inputValue = event.target.value
        const inputId = event.target.id
        newBrand[inputId] = inputValue
        setBrand(newBrand)
      }

     return(
        <>
        <h3>Add a Brand</h3>
        <label htmlFor="brandName">Brand Name</label>
        <input type="text" id="brandName" onChange={handleFormInput} />


        <button type="submit" onClick={addBrand}>Add</button>
        <button onClick={() => navigate("/brand")}>Cancel</button>
        </>
     ) 
}

export default BrandForm;