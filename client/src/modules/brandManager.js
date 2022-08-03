import { getToken } from "./authManager";

const api = "/api/brand";

export const getAllBrands = () => {
    return getToken().then((token) => {
        return fetch(api, {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        }).then((res) => res.json())
    });
};


export const addANewBrand = (brand) => {
    return getToken().then((token) => {
        return fetch(api, {
            method: "POST",
            headers: { 
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
            },
            body: JSON.stringify(brand),
        }).then((res) => res.json())
        
    })
}


export const deleteABrand = (id) => {
    return getToken().then((token) => {
        return fetch(api +`/${id}`, {
            method: "DELETE",
            headers: { 
            Authorization: `Bearer ${token}`,
            },
        })

    }
    )
}
