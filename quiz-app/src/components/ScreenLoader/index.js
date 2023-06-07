import React from "react";
import styles from "./styles.module.scss";

import Spinner from "../Spinner";

const ScreenLoader = () => (
  <div className={styles.ScreenLoader}>
    <Spinner />
  </div>
);

export default ScreenLoader;
