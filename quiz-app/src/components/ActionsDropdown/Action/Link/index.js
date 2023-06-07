import React from "react";

import cn from "classnames";
import PropTypes from "prop-types";
import { Link } from "react-router-dom";

import { Icon } from "components";
import styles from "../styles.module.scss";

const ActionLink = ({ action: { id, action, icon, text, disabled } }) => (
  <Link
    className={cn(styles.Action, {
      [styles.Action___disabled]: disabled,
      [styles.Action___delete]: icon === "delete" || icon === "close",
    })}
    id={id}
    to={action}
    onClick={(e) => {
      e.stopPropagation();
    }}
  >
    <Icon className={styles.Action_icon} icon={icon} />
    {text}
  </Link>
);

ActionLink.propTypes = {
  action: PropTypes.shape({
    action: PropTypes.string.isRequired,
    icon: PropTypes.string.isRequired,
    text: PropTypes.string.isRequired,
    disabled: PropTypes.bool,
    id: PropTypes.string,
  }),
};

export default ActionLink;
