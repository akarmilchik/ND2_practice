/* eslint-disable jsx-a11y/anchor-is-valid */
import React from 'react';
import { LISTINGS_PATH, ORDERS_PATH } from '../../constants';
import style from './Navigation.module.css'
import {NavLink} from 'react-router-dom';

const Navigation = () => {
    return (
        <nav className={style.navigation}>
          <NavLink to={LISTINGS_PATH}>My listings</NavLink>
          <NavLink to={ORDERS_PATH}>My orders</NavLink>
          <NavLink to="#">Login</NavLink>
        </nav>
    );

}

export default Navigation;