import React from 'react';
import style from "./Category.module.css";


const Category = (props) => {
    const handleChange = (e) => {
        props.onCategoryChange(props.id, e.target.checked);
    };

    return (
        <div className={style.item}>
            <label>
                <span>{s.name}</span>
                <input type="checkbox" onChange={handleChange} />
            </label>
        </div>);
};

export default Category;