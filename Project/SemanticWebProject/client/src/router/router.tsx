import React from "react";
import Home from "@/pages/home.tsx";
import {Route, Routes} from "react-router-dom";
import Details from "@/pages/details.tsx";

export const Router = () => {
    return (
        <>
            <Routes>
                <Route path="/" element={<Home/>}/>
                <Route path="/:wikiId" element={<Details/>}/>
            </Routes>
        </>
    )
}