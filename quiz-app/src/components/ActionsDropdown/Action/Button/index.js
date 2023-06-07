import React from "react";

import cn from "classnames";
import PropTypes from "prop-types";

import { Icon } from "components";

import styles from "../styles.module.scss";

const ActionButton = ({
  action: {
    id,
    action,
    applyColorStyles = true,
    className,
    iconClassName,
    icon,
    text,
    disabled,
  },
}) => (
  <button
    className={cn(className, styles.Action, {
      [styles.Action___disabled]: disabled,
      [styles.Action___delete]:
        (icon === "delete" || icon === "close") && applyColorStyles,
    })}
    data-test="button"
    disabled={disabled}
    id={id}
    type="button"
    onClick={action}
  >
    <Icon className={cn(iconClassName, styles.Action_icon)} icon={icon} />
    {text}
  </button>
);

ActionButton.propTypes = {
  action: PropTypes.shape({
    action: PropTypes.func,
    applyColorStyles: PropTypes.bool,
    className: PropTypes.string,
    iconClassName: PropTypes.string,
    icon: PropTypes.string.isRequired,
    text: PropTypes.string.isRequired,
    disabled: PropTypes.bool,
    id: PropTypes.string,
  }).isRequired,
};

export default ActionButton;
