import React from "react";
import PropTypes from "prop-types";
import styles from "./styles.module.scss";

const CustomSingleValue = ({ data: { icon, label } }) => (
  <div className={styles.CustomSingleValue}>
    {icon && <span className={styles.CustomSingleValue_icon}>{icon}</span>}
    <span>{label}</span>
  </div>
);

CustomSingleValue.propTypes = {
  data: PropTypes.shape({
    icon: PropTypes.element,
    label: PropTypes.string.isRequired,
  }).isRequired,
};

export default CustomSingleValue;
