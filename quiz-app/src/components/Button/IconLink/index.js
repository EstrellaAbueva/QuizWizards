import React from "react";

import cn from "classnames";
import PropTypes from "prop-types";

import { Link } from "react-router-dom";

import GLOBALS from "../../../app-globals";

import Icon from "../../Icons";
import iconButtonTypes from "../constants/iconButtonTypes";
import styles from "../icon.module.scss";

const isTypeOutline = (type) => {
  switch (type) {
    case iconButtonTypes.OUTLINE.XS:
    case iconButtonTypes.OUTLINE.SM:
    case iconButtonTypes.OUTLINE.MD:
    case iconButtonTypes.OUTLINE.LG:
      return true;
    default:
      return false;
  }
};

const IconLink = ({
  icon,
  className,
  iconClassName,
  colorName,
  style,
  to,
  type,
  tabIndex,
  disabled,
  onClick,
  id,
}) => {
  const link = (
    <Link
      className={cn(className, styles[`IconButton___${type}`], {
        [styles.IconButton___disabled]: disabled,
        [styles[`IconButton___${colorName}`]]: isTypeOutline(type),
      })}
      id={id}
      tabIndex={tabIndex}
      to={to}
      onClick={onClick}
    >
      <Icon
        className={cn(styles.IconButton_icon, iconClassName)}
        icon={icon}
        style={style}
      />
    </Link>
  );
  return !disabled ? (
    link
  ) : (
    <span className={styles.IconButton_wrapper}>{link}</span>
  );
};

IconLink.defaultProps = {
  id: null,
  className: null,
  style: null,
  to: null,
  iconClassName: null,
  type: iconButtonTypes.SOLID.SM,
  tabIndex: 0,
  disabled: false,
  colorName: GLOBALS.COLOR_NAMES.YELLOW,
  onClick: null,
};

IconLink.propTypes = {
  id: PropTypes.string,
  type: PropTypes.oneOf([
    iconButtonTypes.SOLID.LG,
    iconButtonTypes.SOLID.MD,
    iconButtonTypes.SOLID.SM,
    iconButtonTypes.OUTLINE.LG,
    iconButtonTypes.OUTLINE.MD,
    iconButtonTypes.OUTLINE.SM,
  ]),
  className: PropTypes.string,
  icon: PropTypes.string.isRequired,
  style: PropTypes.object,
  to: PropTypes.string,
  iconClassName: PropTypes.string,
  tabIndex: PropTypes.number,
  disabled: PropTypes.bool,
  colorName: PropTypes.oneOf([
    GLOBALS.COLOR_NAMES.YELLOW,
    GLOBALS.COLOR_NAMES.RED,
    GLOBALS.COLOR_NAMES.GREEN,
  ]),
  onClick: PropTypes.func,
};

export default IconLink;
