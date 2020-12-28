import myListingsReducer from "./myListings-reducer";

const categories = [
    { id: 1, name: "Street"},
    { id: 2, name: "Sports"},
    { id: 3, name: "Festivals"},
    { id: 4, name: "Plays"}
  ];
  
  const image = "https://img.icons8.com/cute-clipart/344/purchase-order.png";
  
  const listingsItems = [
    { image, name: "Listing street #1", price: 1000, categoryId: 1 },
    { image, name: "Listing street #2", price: 800, categoryId: 1 },
    { image, name: "Listing street #3", price: 500, categoryId: 1 },
    { image, name: "Listing street #4", price: 1400, categoryId: 1},
    { image, name: "Listing sport #5", price: 1100, categoryId: 2 },
    { image, name: "Listing sport #6", price: 670, categoryId: 2 },
    { image, name: "Listing sport #7", price: 4500, categoryId: 2 },
    { image, name: "Listing festival #2", price: 800, categoryId: 3 },
    { image, name: "Listing festival #3", price: 500, categoryId: 3 },
    { image, name: "Listing play #4", price: 1400, categoryId: 4 },
    { image, name: "Listing play #5", price: 1100, categoryId: 4 },
    { image, name: "Listing play #6", price: 670, categoryId: 4 },
    { image, name: "Listing play #7", price: 4500, categoryId: 4 }
  ];

function filterListingsItemsByCategories(listingsItems, categories) {
  let result;
  if (categories.length > 0) {
    result = listingsItems.filter((p) => categories.indexOf(p.categoryId) >= 0);
  } else {
    result = listingsItems;
  }
  return result;
}

const store = {
  _state: {
    myListings: {
      categories,
      listingsItems,
      selectedCategories: [],
    },
    myOrders: {},
  },
  _notifyObserver() {
    console.log("State changed");
  },
  getState() {
    return this._state;
  },
  subscribe(observer) {
    this._notifyObserver = observer;
  },
  dispatch(action) {
    this._state.myListings = myListingsReducer(this._state.myListings, action);

    this._state.myListings.listingsItems = filterListingsItemsByCategories(
        listingsItems,
      this._state.myListings.selectedCategories
    ); // TODO: remove me
    this._notifyObserver(this._state);
  },
};

export default store;