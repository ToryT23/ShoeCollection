import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import Login from "./Login";
import Register from "./Register";
import Hello from "./Hello";
import BrandList from "./BrandList";
import BrandForm from "./BrandForm";
import StyleList from "./StyleList";
import StyleForm from "./StyleForm";
import ShoeList from "./ShoeList";
import MyShoeList from "./MyShoeList";
import MyShoeForm from "./MyShoeForm";
import MyFavoritesList from "./MyFavoritesList";
import EditMyShoeForm from "./EditMyShoeForm";


export default function ApplicationViews({ isLoggedIn }) {


    return(
        <main>

        <Routes>
            <Route path="/">
            <Route index element={isLoggedIn ? <ShoeList /> : <Navigate to="login" />}
            />
            <Route path="login" element={<Login />} />
            <Route path="register" element={<Register />} /> 
            <Route path="brand">
            <Route index element={<BrandList />} />
            <Route path="add" element={<BrandForm />} />
            </Route>
            <Route path="style">
            <Route index element={<StyleList />} />
            <Route path="add" element={<StyleForm />} />
            </Route>
            <Route path="shoe">
                <Route index element={<ShoeList />} /> 
                 <Route path="myshoes" element={<MyShoeList />} />
                 <Route path="myshoes/add" element={<MyShoeForm />} />
                 <Route path="myshoes/edit/:shoeId" element={<EditMyShoeForm />} />
            </Route>
            <Route path="myfavorites">
            <Route index element={<MyFavoritesList />} />

            </Route>







            </Route>
        </Routes>
            </main>
    )
}    