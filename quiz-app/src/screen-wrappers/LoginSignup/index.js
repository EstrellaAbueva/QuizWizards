import React from "react";
import { Switch, Redirect } from "react-router-dom";
import styles from "./styles.module.scss";

import NoAuthRoute from "hocs/NoAuthRoute";

import { Login, Signup } from "screens/public";
import { ScreenLoader } from "components";

const LoginSignupContainer = () => {
  return (
    <div className={styles.LoginSignupContainer}>
      <div className={styles.backgroundImage} />
      <React.Suspense fallback={<ScreenLoader />}>
        <Switch>
          <NoAuthRoute
            path="/login"
            name="Login"
            exact
            render={(props) => <Login {...props} />}
          />

          <NoAuthRoute
            path="/signup"
            name="Sign Up"
            exact
            render={(props) => <Signup {...props} />}
          />

          <Redirect from="*" to="/login" />
        </Switch>
      </React.Suspense>
    </div>
  );
};

export default LoginSignupContainer;
