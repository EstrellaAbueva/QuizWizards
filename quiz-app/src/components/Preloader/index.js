import React from "react";
import styles from "./styles.module.scss";
import PropTypes from "prop-types";

import Card from "components/Card";
import Shine from "components/Shine";

const Preloader = ({ isAdmin }) => (
  <Card className={styles.Preloader}>
    <Shine className={styles.Preloader_body_shine} isAdmin={isAdmin} />
    <Shine className={styles.Preloader_body_shine} isAdmin={isAdmin} />
    <Shine className={styles.Preloader_body_shine} isAdmin={isAdmin} />
  </Card>
);

Preloader.defaultProps = {
  isAdmin: false,
};

Preloader.propTypes = {
  isAdmin: PropTypes.bool,
};

export default Preloader;
