import React from 'react';
import style from './MyListings.module.css'

const MyListings = () => {
    return (
        <div className={style.MyListings}>
            <div className={style.categories}>
                Categories
            </div>
            <div className={style.filters}>
                Filters
            </div>
            <div className={style.items}>
                Items
            </div>
        </div>
    );
}

export default MyListings;