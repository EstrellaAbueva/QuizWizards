/* eslint-disable @typescript-eslint/no-empty-function */
import { createContext } from "react";

const TakerContext = createContext({
  taker: {},
  loginUpdate: () => {},
  loginRestart: () => {},
});

export default TakerContext;
