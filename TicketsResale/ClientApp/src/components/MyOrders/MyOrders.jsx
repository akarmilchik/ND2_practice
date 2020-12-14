import React from 'react';
import style from './MyOrders.module.css'

const MyOrders = () => {
    return (
        <>
            <div className={`${style.item} ${style.odd}`}>
                <h3>Item 1</h3>
                <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. In officia nulla eaque iusto. Cum, ducimus ea quaerat tempora sint modi.</p>
            </div>
            <div className={`${style.item} ${style.even}`}>
            <h3>Item 2</h3>
                <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. In officia nulla eaque iusto. Cum, ducimus ea quaerat tempora sint modi.</p>
            </div>
            <div className={`${style.item} ${style.odd}`}>
            <h3>Item 3</h3>
                <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. In officia nulla eaque iusto. Cum, ducimus ea quaerat tempora sint modi.</p>
            </div> 
        </>
    );
}

export default MyOrders;