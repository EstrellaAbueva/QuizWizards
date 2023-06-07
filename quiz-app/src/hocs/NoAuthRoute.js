import React, { useContext } from "react";
import { Route, Redirect } from "react-router-dom";

import GLOBALS from "app-globals";
import { TakerContext } from "context";

const NoAuthRoute = ({ ...rest }) => {
  const { taker } = useContext(TakerContext);

  // The page can be accessed by the taker
  if (taker) {
    // Redirect to /taker/home if taker is not null
    if (taker.takerType === GLOBALS.TAKER_TYPES.TAKER) {
      return (
        <Route name="Taker" render={() => <Redirect to="/taker/home" />} />
      );
    }

    // Redirect to /admin/home if taker is not null
    if (taker.takerType === GLOBALS.TAKER_TYPES.ADMIN) {
      return (
        <Route name="Admin" render={() => <Redirect to="/admin/home" />} />
      );
    }
  }

  // the page can be accessed by the taker
  return <Route {...rest} />;
};

export default NoAuthRoute;
