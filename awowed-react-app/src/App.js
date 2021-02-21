import {MyNavbar} from "./components/MyNavbar"
import {BrowserRouter, Switch, Route} from "react-router-dom"
import {HomePage} from "./Pages/HomePage/HomePage"
import {AboutPage} from "./Pages/AboutPage/AboutPage"

function App() {
  return (
    <BrowserRouter>
      <MyNavbar />
      <Switch>
          <Route component={HomePage} path="/" exact />
          <Route component={AboutPage} path="/about" exact />
      </Switch>
    </BrowserRouter>
  );
}

export default App;
