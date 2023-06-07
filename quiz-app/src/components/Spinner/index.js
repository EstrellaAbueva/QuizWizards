import React from "react";

import cn from "classnames";
import PropTypes from "prop-types";

import GLOBALS from "../../app-globals";

import spinnerSizes from "./constants/spinnerSizes";
import styles from "./styles.module.scss";

const Spinner = ({ className, colorName, size }) => (
  <div className={cn(styles.Spinner_container, className)}>
    <svg className={styles[`Spinner___${size}`]} viewBox="25 25 50 50">
      <circle
        className={styles[`Spinner_circle___${colorName}`]}
        cx="50"
        cy="50"
        r="20"
      ></circle>
    </svg>
  </div>
);

Spinner.defaultProps = {
  className: null,
  colorName: GLOBALS.COLOR_NAMES.YELLOW,
  size: spinnerSizes.LG,
};

Spinner.propTypes = {
  className: PropTypes.string,
  colorName: PropTypes.oneOf([
    GLOBALS.COLOR_NAMES.WHITE,
    GLOBALS.COLOR_NAMES.BLACK,
    GLOBALS.COLOR_NAMES.YELLOW,
    GLOBALS.COLOR_NAMES.BLUE,
  ]),
  size: PropTypes.oneOf([
    spinnerSizes.LG,
    spinnerSizes.MD,
    spinnerSizes.SM,
    spinnerSizes.XS,
  ]),
};

export default Spinner;
