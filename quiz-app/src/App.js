import React, { useState, useEffect } from "react";
import Cookies from "universal-cookie";
import {
  BrowserRouter as Router,
  Switch,
  Redirect,
  Route,
} from "react-router-dom";

import { Admin, LoginSignup, Taker } from "./screen-wrappers";
import { Logout } from "./screens/public";
import { TakerContext } from "context";
import PrivateRoute from "./hocs/PrivateRoute";

import "./styles/App.scss";
import TAKER_TYPES from "app-globals/takerTypes";
import { ScreenLoader } from "components";

const cookies = new Cookies();

function App() {
  const [taker, setTaker] = useState(cookies.get("taker"));

  useEffect(() => {
    document.title = "Quiz Wizard";
  }, []);

  const loginUpdate = (takerData) => {
    cookies.set("taker", takerData, {
      path: "/",
    });

    setTaker(takerData);
  };

  const loginRestart = () => {
    cookies.remove("taker", {
      path: "/",
    });
    cookies.remove("accessToken", {
      path: "/",
    });

    setTaker(null);
  };

  return (
    <React.Suspense fallback={<ScreenLoader />}>
      <TakerContext.Provider value={{ taker, loginUpdate, loginRestart }}>
        <Router>
          <Switch>
            <PrivateRoute
              path="/logout"
              name="Logout"
              render={(props) => <Logout {...props} />}
            />

            {taker?.takerType === TAKER_TYPES.TAKER && (
              <PrivateRoute
                forTakerType={TAKER_TYPES.TAKER}
                path="/taker"
                name="Taker"
                render={(props) => <Taker {...props} />}
              />
            )}

            {taker?.takerType === TAKER_TYPES.ADMIN && (
              <PrivateRoute
                forTakerType={TAKER_TYPES.ADMIN}
                path="/admin"
                name="Admin"
                render={(props) => <Admin {...props} />}
              />
            )}

            <Route
              path="/"
              name="Login/Sign Up"
              render={(props) => <LoginSignup {...props} />}
            />

            <Redirect from="/" to="/login" />
            <Redirect to="/page-not-found" />
          </Switch>
        </Router>
      </TakerContext.Provider>
    </React.Suspense>
  );
}

export default App;
