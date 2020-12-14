import React from 'react';
import style from './MyListings.module.css'
import Categories from "../Categories/Categories";
import Filter from "../Filter/Filter";
import ListingsItems from '../ListingsItems/ListingsItems';

const MyListings = (props) => {
    return (
        <div className={style.mylistings}>
            <div className={style.categories}>
                <Categories categories={props.categories} />
            </div>
            <div className={style.filters}>
                <Filter />
            </div>
            <div className={style.items}>
                <ListingsItems listingsitems={props.listingsitems} />
            </div>
        </div>
    );
}

export default MyListings;