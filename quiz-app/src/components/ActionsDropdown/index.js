import React, { useState, useRef } from "react";

import cn from "classnames";
import PropTypes from "prop-types";

import { useOnClickOutside } from "hooks";

import { IconButton } from "components";
import { iconButtonTypes } from "components/constants";

import Dropdown from "./Dropdown";
import actionTypes from "./constants/actionTypes";
import styles from "./styles.module.scss";

const ActionsDropdown = ({
  id,
  icon,
  disabled,
  iconButtonType,
  iconClassName,
  className,
  dropdownClassName,
  iconButtonColorName,
  actions,
}) => {
  const ref = useRef();
  const [isDropdownToggled, toggleDropdown] = useState(false);
  useOnClickOutside(ref, () => toggleDropdown(false));

  if (actions.length === 0) {
    return null;
  }

  return (
    <div ref={ref} className={cn(styles.ActionsDropdown, className)}>
      <IconButton
        className={styles.ActionsDropdown_iconButton}
        colorName={iconButtonColorName}
        disabled={disabled}
        icon={icon}
        iconClassName={iconClassName}
        id={id}
        type={iconButtonType}
        onClick={(e) => {
          e.stopPropagation();
          toggleDropdown(!isDropdownToggled);
        }}
      />
      <Dropdown
        actions={actions}
        className={dropdownClassName}
        isToggled={isDropdownToggled}
      />
    </div>
  );
};

ActionsDropdown.defaultProps = {
  className: null,
  icon: "more_horiz",
  iconButtonType: iconButtonTypes.SOLID.LG,
  id: "ellipseDropdown",
  disabled: false,
};

ActionsDropdown.propTypes = {
  // for dropdown container styling
  className: PropTypes.string,
  iconClassName: PropTypes.string,
  dropdownClassName: PropTypes.string,
  iconButtonClassName: PropTypes.string,
  iconButtonColorName: PropTypes.string,
  disabled: PropTypes.bool,

  // decides which icon to use for dropdown toggle
  icon: PropTypes.string,

  // for mapping dropdown items
  actions: PropTypes.arrayOf(
    PropTypes.shape({
      type: PropTypes.oneOf([actionTypes.BUTTON, actionTypes.LINK]),
      action: PropTypes.oneOfType([PropTypes.func, PropTypes.string]),
      icon: PropTypes.string,
      text: PropTypes.string,
      tooltip: PropTypes.string,
      disabled: PropTypes.bool,
    })
  ).isRequired,

  iconButtonType: PropTypes.oneOf([
    iconButtonTypes.SOLID.LG,
    iconButtonTypes.SOLID.MD,
    iconButtonTypes.SOLID.SM,
    iconButtonTypes.OUTLINE.LG,
    iconButtonTypes.OUTLINE.MD,
    iconButtonTypes.OUTLINE.SM,
  ]),

  id: PropTypes.string,
};

export default ActionsDropdown;
