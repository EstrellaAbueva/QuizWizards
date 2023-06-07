import React from "react";
import { Switch, Redirect, Route } from "react-router-dom";
import styles from "./styles.module.scss";

import { EditProfile, Home, TakeQuiz } from "screens/taker";
import { TAKER_ROUTES } from "./constants";

import Navbar from "screen-wrappers/Navbar";
import { ScreenLoader } from "components";

const TakerContainer = () => {
  return (
    <div className={styles.TakerContainer}>
      <Navbar />

      <React.Suspense fallback={<ScreenLoader />}>
        <Switch>
          <Route
            path={TAKER_ROUTES.HOME}
            name="Home"
            exact
            render={(props) => <Home {...props} />}
          />

          <Route
            path={TAKER_ROUTES.TAKE_QUIZ}
            name="Take Quiz"
            exact
            render={(props) => <TakeQuiz {...props} />}
          />

          <Route
            path={TAKER_ROUTES.EDIT_PROFILE}
            name="Edit Profile"
            exact
            render={(props) => <EditProfile {...props} />}
          />

          <Redirect from="*" to={TAKER_ROUTES.HOME} />
        </Switch>
      </React.Suspense>
    </div>
  );
};

export default TakerContainer;
