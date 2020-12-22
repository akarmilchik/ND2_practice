import React from 'react';
import style from './MyListings.module.css'
import Categories from "../Categories/Categories";
import Filter from "../Filter/Filter";
import ListingsItems from '../ListingsItems/ListingsItems';
import { changeCategoryActionCreator, clearCategoriesActionCreator} from '../../redux/myListings-reducer';

const MyListings = (props) => {

    const onCategoryChange = (id, value) => {
        props.dispatch(changeCategoryActionCreator(id, value));
    }
    
    const clearSelectedCategories = () => {
        props.dispatch(clearCategoriesActionCreator());
    }
    
    return (
        <div className={style.mylistings}>
            <div className={style.categories}>
                <Categories 
                    categories={props.state.categories} 
                    selectedCategories = {props.state.selectedCategories}
                    onCategoryChange = {onCategoryChange}
                    clearSelectedCategories = {clearSelectedCategories}
                />
            </div>
            <div className={style.filters}>
                <Filter />
            </div>
            <div className={style.items}>
                <ListingsItems listingsItems={props.state.listingsItems} />
            </div>
        </div>
    );
}

export default MyListings;