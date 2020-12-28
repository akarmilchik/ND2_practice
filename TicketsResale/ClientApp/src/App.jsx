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

const App = (props) => {
    return (
        <Router basename="ClientApp">
            <div className="App">
                <Header />
                <Switch>
                    <Route path="/" exact render={() => <Redirect to={LISTINGS_PATH} />} />
                    <Route path={LISTINGS_PATH}>
                        <MyListings 
                            state={props.state.myListings} 
                            dispatch = {props.dispatch}
                        />
                    </Route>
                    <Route path={ORDERS_PATH}><MyOrders state={props.state.myOrders}/></Route>
                </Switch>
            </div>
        </Router>
    );
};
export default App;