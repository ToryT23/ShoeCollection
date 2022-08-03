import React, {useState, useEffect}  from "react";
import {  useNavigate } from "react-router-dom";
import { getAllBrands } from "../modules/brandManager";
import { editMyShoe, getShoeById } from "../modules/shoeManager";
import { getAllStyles } from "../modules/styleManager";
import { useParams } from "react-router-dom";


const EditMyShoeForm = () => {
    const [brands, setBrands] = useState([]);
    const [styles, setStyles] = useState([]);
    const [myShoes, setmyShoes] = useState({
        brandId: 0, styleId: 0, size : 0 , imageUrl: ""
    });

    const { shoeId } = useParams();
    const nav = useNavigate()

    const updateEntry = (event) => {
        if(myShoes.brandId !== 0 && myShoes.styleId !==0 && myShoes.size > 0 && myShoes.imageUrl !== "")
       {
           editMyShoe(myShoes).then(nav("/shoe/myshoes"))
       } else
       {
        alert("Please fill-in all info")
       }
    }

    const handleFormInput = (event) => {
        const newShoe = {...myShoes}
        const inputValue = event.target.value
        const inputId = event.target.id
        newShoe[inputId] = inputValue
        setmyShoes(newShoe)
      }
    
      const handleFormInputForInt = (event) => {
        const newShoe = {...myShoes}
        const inputValue = parseInt(event.target.value)
        const inputId = event.target.id
        newShoe[inputId] = inputValue
        setmyShoes(newShoe)
      }
     const handleSelectInputForBrand = (event) => {
            const newShoe = { ...myShoes }
            const inputValue = parseInt(event.target.value)
            newShoe.brandId = inputValue
            setmyShoes(newShoe)
     }
     const handleSelectInputForStyle = (event) => {
            const newShoe = { ...myShoes}
            const inputValue = parseInt(event.target.value)
            newShoe.styleId = inputValue
            setmyShoes(newShoe)
     }
 
    useEffect(() => {
        getShoeById(shoeId).then(setmyShoes) 
            getAllBrands().then(setBrands)
            getAllStyles().then(setStyles)
        
    },[])

    return (
        <>

        <div>
            <h2>Edit this shoe</h2>
         <label htmlFor="Brand">Brand</label>
        <select  selected id="brandId" onChange={handleSelectInputForBrand} defaultChecked="default"> 
        <option  hidden>{myShoes.brand?.brandName}</option>
        {brands.map( (brand) => (
            <option  defaultValue={myShoes.brandId} value={brand.id} key={brand.id}>{brand.brandName}</option>
            ))}
        </select>

        <label htmlFor="Style">Style</label>
        <select selected id="styleId" onChange={handleSelectInputForStyle} defaultChecked= "default">
            <option hidden>{myShoes.style?.name}</option>
            {styles.map((style) => (
                <option defaultValue={myShoes.styleId} value={style.id} key={style.id}>{style.name}</option>
                )) }
        </select>

        <label htmlFor="Size">Size</label>
        <input id="size" value={myShoes.size}  type="number" onChange={handleFormInputForInt} />

        <label htmlFor="ImageUrl">ImageUrl</label>
        <input id="imageUrl" value={myShoes.imageUrl} type="text" onChange={handleFormInput}/>
        </div>
        

        <button type="submit" onClick={() => updateEntry(myShoes)}>
              Update Shoe
        </button>
        <button type="submit" onClick={() => nav("/shoe/myshoes")}>Cancel</button>
        </>
    )
}

export default EditMyShoeForm