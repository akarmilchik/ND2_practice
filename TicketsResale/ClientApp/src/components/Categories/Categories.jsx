import React from "react";
import style from "./Categories.module.css";


const Categories = (props) => {
    const items = props.categories.map((category) => {
        return (
            <Category id={category.id} name={category.name} onCategoryChange={ props.onCategoryChange} />
    });

    return (
        <div>
            <div>
                <span>Clear</span>
            </div>
            {items}
        </div>
    );
};

export default Categories;
