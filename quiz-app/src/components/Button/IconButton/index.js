import React from "react";

import cn from "classnames";
import PropTypes from "prop-types";

import GLOBALS from "../../../app-globals";

import { Tooltip } from "../../index";

import { tooltipPlacement } from "../../constants";

import Icon from "../../Icons";
import iconButtonTypes from "../constants/iconButtonTypes";

import styles from "../icon.module.scss";

const isTypeOutline = (type) => {
  switch (type) {
    case iconButtonTypes.OUTLINE.XS:
    case iconButtonTypes.OUTLINE.SM:
    case iconButtonTypes.OUTLINE.MD:
    case iconButtonTypes.OUTLINE.LG:
    case iconButtonTypes.OUTLINE.XL:
      return true;
    default:
      return false;
  }
};

const IconButton = ({
  icon,
  className,
  iconClassName,
  colorName,
  style,
  onClick,
  disabled,
  type,
  tabIndex,
  kind,
  id,
  tooltipContent,
}) => {
  const button = (
    <button
      className={cn(className, styles[`IconButton___${type}`], {
        [styles[`IconButton___${colorName}`]]: isTypeOutline(type),
      })}
      disabled={disabled}
      id={id}
      tabIndex={tabIndex}
      type={kind}
      // eslint-disable-next-line @typescript-eslint/no-empty-function
      onClick={disabled === false ? onClick || (() => {}) : null}
    >
      <Icon
        className={cn(styles.IconButton_icon, iconClassName)}
        icon={icon}
        style={style}
      />
    </button>
  );

  if (tooltipContent) {
    return (
      <Tooltip content={tooltipContent} placement={tooltipPlacement.LEFT}>
        {button}
      </Tooltip>
    );
  }
  return !disabled ? (
    button
  ) : (
    <span className={styles.IconButton_wrapper}>{button}</span>
  );
};

IconButton.defaultProps = {
  className: null,
  style: null,
  onClick: null,
  iconClassName: null,
  disabled: false,
  type: iconButtonTypes.SOLID.SM,
  kind: GLOBALS.BUTTON_KINDS.BUTTON,
  tabIndex: 0,
  id: null,
  colorName: GLOBALS.COLOR_NAMES.YELLOW,
  tooltipContent: null,
};

IconButton.propTypes = {
  type: PropTypes.oneOf([
    iconButtonTypes.SOLID.XL,
    iconButtonTypes.SOLID.LG,
    iconButtonTypes.SOLID.MD,
    iconButtonTypes.SOLID.SM,
    iconButtonTypes.SOLID.XS,
    iconButtonTypes.OUTLINE.XL,
    iconButtonTypes.OUTLINE.LG,
    iconButtonTypes.OUTLINE.MD,
    iconButtonTypes.OUTLINE.SM,
    iconButtonTypes.OUTLINE.XS,
  ]),
  className: PropTypes.string,
  icon: PropTypes.string.isRequired,
  style: PropTypes.object,
  onClick: PropTypes.func,
  iconClassName: PropTypes.string,
  disabled: PropTypes.bool,
  tabIndex: PropTypes.number,
  kind: PropTypes.oneOf([
    GLOBALS.BUTTON_KINDS.BUTTON,
    GLOBALS.BUTTON_KINDS.SUBMIT,
    GLOBALS.BUTTON_KINDS.RESET,
  ]),
  id: PropTypes.string,
  colorName: PropTypes.oneOf([
    GLOBALS.COLOR_NAMES.YELLOW,
    GLOBALS.COLOR_NAMES.RED,
    GLOBALS.COLOR_NAMES.GREEN,
    GLOBALS.COLOR_NAMES.BLUE,
    GLOBALS.COLOR_NAMES.NEUTRAL,
  ]),
  tooltipContent: PropTypes.string,
};

export default IconButton;
