import React, { useContext } from "react";
import { Route, Redirect } from "react-router-dom";
import PropTypes from "prop-types";
import { TakerContext } from "context";
import GLOBALS from "app-globals";

const PrivateRoute = ({ forTakerType, ...rest }) => {
  const { taker } = useContext(TakerContext);
  let takerType = null;

  if (taker?.takerType) {
    takerType = taker.takerType;
  }

  // The page can be accessed by the taker
  if (
    takerType !== null ||
    (forTakerType === GLOBALS.TAKER_TYPES.ADMIN &&
      takerType === GLOBALS.TAKER_TYPES.ADMIN) ||
    (forTakerType === GLOBALS.TAKER_TYPES.TAKER &&
      takerType === GLOBALS.TAKER_TYPES.TAKER)
  ) {
    return <Route {...rest} />;
  }

  // Redirect to /taker/home if taker is not null and the page can be accessed by the admin
  if (
    takerType === GLOBALS.TAKER_TYPES.ADMIN &&
    forTakerType !== GLOBALS.TAKER_TYPES.TAKER
  ) {
    return <Route name="Admin" render={() => <Redirect to="/admin/home" />} />;
  }

  // Redirect to /admin/home if taker is not null and the page can be accessed by the taker
  if (
    takerType === GLOBALS.TAKER_TYPES.TAKER &&
    forTakerType !== GLOBALS.TAKER_TYPES.ADMIN
  ) {
    return <Route name="Taker" render={() => <Redirect to="/taker/home" />} />;
  }

  // Redirect to /login if taker is null
  return <Route name="Login" render={() => <Redirect to="/login" />} />;
};

PrivateRoute.propTypes = {
  forTakerType: PropTypes.string,
};

export default PrivateRoute;
