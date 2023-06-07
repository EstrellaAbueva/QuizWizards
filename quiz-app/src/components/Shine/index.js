import React from "react";
import cn from "classnames";
import PropTypes from "prop-types";
import styles from "./styles.module.scss";

import SHINE_TYPES from "app-globals/shineTypes";

const Shine = ({ className, type, isAdmin }) => (
  <div
    className={cn(
      styles.Shine,
      className,
      { [styles.Shine_admin]: isAdmin },
      styles[`Shine___${type}`]
    )}
  />
);

Shine.defaultProps = {
  className: null,
  type: SHINE_TYPES.STRAIGHT,
};

Shine.propTypes = {
  className: PropTypes.string,
  type: PropTypes.oneOf(Object.values(SHINE_TYPES)),
  isAdmin: PropTypes.bool,
};

export default Shine;
