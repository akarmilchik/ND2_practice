/* eslint-disable jsx-a11y/anchor-is-valid */
import React from "react";
import {
    BrowserRouter as Router,
    Redirect,
    Route,
    Switch,
} from "react-router-dom";
import './App.css';
import Header from './components/Header/Header';
import MyListings from './components/MyListings/MyListings'
import MyOrders from './components/MyOrders/MyOrders';
import { LISTINGS_PATH, ORDERS_PATH } from './constants';

const categories = [
    { id: 1, name: "Street"},
    { id: 2, name: "Sports"},
    { id: 3, name: "Festivals"},
    { id: 4, name: "Plays"}
];

const image = "~/img/order.png";

const listingsitems = [
    { image, name: "Listing street #1", price: 1000, categoryId = 1 },
    { image, name: "Listing street #2", price: 800, categoryId = 1 },
    { image, name: "Listing street #3", price: 500, categoryId = 1 },
    { image, name: "Listing street #4", price: 1400, categoryId = 1 },
    { image, name: "Listing sport #5", price: 1100, categoryId = 2 },
    { image, name: "Listing sport #6", price: 670, categoryId = 2 },
    { image, name: "Listing sport #7", price: 4500, categoryId = 2 },
    { image, name: "Listing festival #2", price: 800, categoryId = 3 },
    { image, name: "Listing festival #3", price: 500, categoryId = 3 },
    { image, name: "Listing play #4", price: 1400, categoryId = 4 },
    { image, name: "Listing play #5", price: 1100, categoryId = 4 },
    { image, name: "Listing play #6", price: 670, categoryId = 4 },
    { image, name: "Listing play #7", price: 4500, categoryId = 4 }
];


const onCategoryChange = (id, value) => {
    debugger;
    if (value) { }
        selectedCategories.push(id);
    }
    else
    {
        selectedCategories.filter((i) => i != id);
    }

}

let selectedCategories = [];

const App = (props) => {
    return (
        <Router basename="ClientApp">
            <div className="App">
                <Header />
                <Switch>
                    <Route path="/" exact render={() => <Redirect to={LISTINGS_PATH} />} />
                    <Route path={LISTINGS_PATH}><MyListings categories={categories} listingsitems={listingsitems} onCategoryChange={onCategoryChange}/></Route>
                    <Route path={ORDERS_PATH}><MyOrders /></Route>
                </Switch>
            </div>
        </Router>
    );
};

export default App;
