import React from 'react';
import style from './Header.module.css'
import Logo from '../Logo/Logo'
import Navigation from '../Navigation/Navigation'

const Header = () => {
    return (
        <div className={style.header}>
            <Logo />
            <Navigation />
        </div>
    );
};

export default Header;