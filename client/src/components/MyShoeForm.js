import React, {useEffect, useState} from "react";
import { useNavigate } from "react-router-dom";
import { getAllBrands } from "../modules/brandManager";
import { createAShoe } from "../modules/shoeManager";
import { getAllStyles } from "../modules/styleManager";

export const MyShoeForm = () => {
    const [shoe, setShoe] = useState({})
    const [brands, setBrands] = useState([])
    const [styles, setStyles] = useState([])
    const nav = useNavigate()

    const addAShoe = (event) => {
        event.preventDefault()
        if(shoe.brandId !== 0 && shoe.styleId !== 0 && shoe.size > 0 && shoe.imageUrl !== "")
        {
            createAShoe(shoe).then(() => nav("/shoe/myShoes"))
        }else
        {
            alert("Please choose/enter a value for every field.")
        }
    }

    const handleFormInput = (event) => {
        const newShoe = {...shoe}
        const inputValue = event.target.value
        const inputId = event.target.id
        newShoe[inputId] = inputValue
        setShoe(newShoe)
      }
    
      const handleFormInputForInt = (event) => {
        const newShoe = {...shoe}
        const inputValue = parseInt(event.target.value)
        const inputId = event.target.id
        newShoe[inputId] = inputValue
        setShoe(newShoe)
      }
     const handleSelectInputForBrand = (event) => {
            const newShoe = { ...shoe }
            const inputValue = parseInt(event.target.value)
            newShoe.brandId = inputValue
            setShoe(newShoe)
     }
     const handleSelectInputForStyle = (event) => {
            const newShoe = { ...shoe }
            const inputValue = parseInt(event.target.value)
            newShoe.styleId = inputValue
            setShoe(newShoe)
     }
    
    useEffect(() => {
        getAllBrands().then(setBrands)
        getAllStyles().then(setStyles)
    },[])

    return(
        <>
        <h1>Add A Shoe</h1>

        <div>

        <label htmlFor="Brand">Brand</label>
        <select selected id="brandId" onChange={handleSelectInputForBrand} defaultChecked="default"> 
        <option hidden> Select One</option>
        {brands.map( (brand) => (
        
            <option value={brand.id} key={brand.id}>{brand.brandName}</option>
        ))}
        </select>

        <label htmlFor="Style">Style</label>
        <select selected id="styleId" onChange={handleSelectInputForStyle} defaultChecked= "default">
        <option hidden> Select One</option>
            {styles.map((style) => (
                <option value={style.id} key={style.id}>{style.name}</option>
                )) }
        </select>

        <label htmlFor="Size">Size</label>
        <input id="size"  type="number" onChange={handleFormInputForInt} />

        <label htmlFor="ImageUrl">ImageUrl</label>
        <input id="imageUrl" type="text" onChange={handleFormInput}/>
        </div>

        <button type="submit" onClick={addAShoe}>
              Add Shoe
        </button>
        <button type="submit" onClick={() => nav("/shoe/myshoes")}>Cancel</button>
        </>
    )

}

export default MyShoeForm