import React, {useEffect, useState} from "react";
import { getAllBrands } from "../modules/brandManager";
import BrandCard from "./BrandCard";
import { Link } from "react-router-dom";
import { deleteABrand } from "../modules/brandManager";

const BrandList = () => {
    const [brands, setBrands] = useState([])

    const handleDeleteForBrand = (id) => {
        deleteABrand(id).then(getBrands())
    }

    const getBrands = () => {
        getAllBrands()
        .then(setBrands)
    }

    useEffect( () => {
        getBrands()
    },[])

    return(
        <>
        <Link to="/brand/add">Add a post</Link>
        {brands.map((brand) => (
          <BrandCard brand={brand} key={brand.id} handleDelete={handleDeleteForBrand} />
        ))}

        </>
    )
}

export default BrandList

