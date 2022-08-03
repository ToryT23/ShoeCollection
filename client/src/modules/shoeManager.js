import { getToken } from "./authManager";

const api = "/api/shoe"
const myshoe = "/api/shoe/myShoes"
const myListOfFavorites = "/api/shoe/myFavorites"
const favoriteShoe = "/api/shoe/favorite"
const unlikeShoe = "/api/shoe/unlikeshoe"
const editUrl = "/api/Shoe/myshoes/edit/"


export const getAllShoes = () => {
 return getToken().then((token) => {
    return fetch( api , {
        headers: {
            Authorization : `Bearer ${token}`
        },
    }).then((res) => res.json())
 })
}

export const getAllOfMyShoes = () => {
   return getToken().then((token) => {
        return fetch(myshoe ,{
            headers: {
                Authorization: `Bearer ${token}`
            },
        } ).then((res) => res.json())
    })
}

export const getAUsersListOfFavorites = () => {
    return getToken().then((token) => {
        return fetch(myListOfFavorites, {
            headers: {
                Authorization: `Bearer ${token}`
            },
        }).then((res) => res.json())
    })
}

export const addAFavorite = (favorite) => {
    console.log(typeof favorite)
    return getToken().then((token) => {
        return fetch(favoriteShoe , {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json",
            },
            body: JSON.stringify({shoeId: favorite})
        })
        .then((res) => res.json())
    })
}

export const createAShoe = (shoe) => {
    return getToken().then((token) => {
    return fetch(api, {
        method: "POST",
        headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
        },
        body: JSON.stringify(shoe),
    }).then((res) => res.json())
    })
}

export const deleteAShoe = (id) => {
    return getToken().then((token) => {
        return fetch(myshoe +`/${id}`, {
            method: "DELETE",
            headers: { 
            Authorization: `Bearer ${token}`,
            },
        })

    }
    )
}

export const getShoeById = (id) => {
    return getToken().then((token) => {
        return fetch(myshoe + `/${id}`, {
            headers : {
                Authorization: `Bearer ${token}`,
            },
        }).then((res) => res.json())
    })
}

export const editMyShoe = (shoe) => {
    return getToken().then((token) => {
        return fetch(editUrl + `${shoe.id}`, {
            method: "PUT",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(shoe)
        })
    })
}

export const deleteAFav = (id) => {
    return getToken().then((token) => {
    return fetch(unlikeShoe + `/${id}`, {
        method: "DELETE",
        headers: {
            Authorization: `Bearer ${token}`,
        },
    })
    })
}