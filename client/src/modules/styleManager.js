import React from "react";
import { getToken} from "./authManager";

const api = "/api/style";

export const getAllStyles = () => {
    return getToken().then((token) => {
        return fetch(api ,{
            headers: {
                Authorization: `Bearer ${token}`,
            },
        }).then((res) => res.json())
    });
 }


export const addANewStyle = (style) => {
    return getToken().then((token) => {
    return fetch(api, {
        method: "POST",
        headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
        },
        body: JSON.stringify(style),
    }).then((res) => res.json())
    })
}

export const deleteStyleById = (id) => {
    return getToken().then((token) => {
        return fetch(api + `/${id}`,{
            method: "DELETE",
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
    })
}
      