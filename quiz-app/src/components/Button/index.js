import React from "react";

import cn from "classnames";
import PropTypes from "prop-types";

import GLOBALS from "app-globals";
import { Icon, Spinner } from "../index";
import { buttonTypes, spinnerSizes } from "../constants";
import styles from "./styles.module.scss";

const Button = ({
  children,
  className,
  disabled,
  icon,
  iconClassName,
  iconPosition,
  id,
  innerRef,
  isLoading,
  kind,
  tabIndex,
  type,
  onClick,
}) => {
  const button = (
    <button
      ref={(e) => {
        if (innerRef) {
          innerRef.current = e;
        }
      }}
      className={cn(className, styles[`Button___${type}`], {
        [styles.Button___withIcon]: icon !== null,
        [styles.Button___disabled]: isLoading,
      })}
      disabled={disabled || isLoading}
      id={id}
      tabIndex={tabIndex}
      type={kind}
      onClick={onClick}
    >
      {isLoading ? (
        <Spinner
          colorName={GLOBALS.COLOR_NAMES.WHITE}
          data-test="loadingSpinner"
          size={spinnerSizes.SM}
        />
      ) : (
        <>
          {icon && iconPosition === "left" && (
            <Icon
              className={cn(styles.Button___withIcon_iconLeft, iconClassName)}
              icon={icon}
            />
          )}
          {children}
          {icon && iconPosition === "right" && (
            <Icon
              className={cn(styles.Button___withIcon_iconRight, iconClassName)}
              icon={icon}
            />
          )}
        </>
      )}
    </button>
  );

  return !disabled ? button : <span>{button}</span>;
};

Button.defaultProps = {
  className: null,
  disabled: false,
  icon: null,
  iconPosition: "left",
  id: null,
  isLoading: false,
  kind: GLOBALS.BUTTON_KINDS.BUTTON,
  lockedIconClassName: null,
  tabIndex: 0,
  type: buttonTypes.PRIMARY.YELLOW,
  wrapperClassName: null,
  // eslint-disable-next-line @typescript-eslint/no-empty-function
  onClick: () => {},
};

Button.propTypes = {
  kind: PropTypes.oneOf([
    GLOBALS.BUTTON_KINDS.BUTTON,
    GLOBALS.BUTTON_KINDS.SUBMIT,
    GLOBALS.BUTTON_KINDS.RESET,
  ]),
  type: PropTypes.oneOf([
    buttonTypes.PRIMARY.RED,
    buttonTypes.PRIMARY.YELLOW,
    buttonTypes.PRIMARY.BLUE,
    buttonTypes.TEXT.RED,
    buttonTypes.TEXT.YELLOW,
    buttonTypes.TEXT.GRAY,
  ]),
  innerRef: PropTypes.any,
  children: PropTypes.any.isRequired,
  className: PropTypes.string,
  onClick: PropTypes.func,
  disabled: PropTypes.bool,
  id: PropTypes.string,
  icon: PropTypes.string,
  tabIndex: PropTypes.number,
  iconClassName: PropTypes.string,
  iconPosition: PropTypes.oneOf(["left", "right"]),

  // these can only have values if `disalbed` is true

  wrapperClassName: PropTypes.string,
  lockedIconClassName: PropTypes.string,
  isLoading: PropTypes.bool,
};

export default Button;
