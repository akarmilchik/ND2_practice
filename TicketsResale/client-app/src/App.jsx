/* eslint-disable jsx-a11y/anchor-is-valid */
import './App.css';
import Header from './components/Header/Header';
import MyListings from './components/MyListings/MyListings'
import MyOrders from './components/MyOrders/MyOrders';
import {BrowserRouter as Router,  Switch, Route, Redirect } from 'react-router-dom';
import {LISTINGS_PATH,  ORDERS_PATH} from './constants'

function App() {
  return (
    <Router>
    <div className="App">
      <Header />
      <Switch>
        <Route path="/" exact render={() => {
          return (
              <Redirect to={LISTINGS_PATH}/>
          )
        }}/>
        <Route path={LISTINGS_PATH}><MyListings /></Route>
        <Route path={ORDERS_PATH}><MyOrders /></Route>
      </Switch>
    </div>
    </Router>
  );
}

export default App;
