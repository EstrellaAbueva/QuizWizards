import React from "react";

import cn from "classnames";
import PropTypes from "prop-types";

import Card from "../../Card";

import ActionButton from "../Action/Button";
import ActionLink from "../Action/Link";
import actionTypes from "../constants/actionTypes";

import styles from "./styles.module.scss";

const Dropdown = ({ className, isToggled, actions }) => (
  <Card
    className={cn(className, {
      [styles.Dropdown]: !isToggled,
      [styles.Dropdown___toggled]: isToggled,
    })}
  >
    {actions?.map((action, index) =>
      action.type === actionTypes.LINK ? (
        <ActionLink key={`Action-${index}`} action={action} />
      ) : (
        <ActionButton key={`Action-${index}`} action={action} />
      )
    )}
  </Card>
);

Dropdown.defaultProps = {
  className: null,
};

Dropdown.propTypes = {
  className: PropTypes.string,
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
  isToggled: PropTypes.bool.isRequired,
};

export default Dropdown;
