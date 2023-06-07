import React from "react";
import PropTypes from "prop-types";
import styles from "./styles.module.scss";

const RequiredField = ({ placeholder }) => (
  <>
    {placeholder}
    <span className={styles.RequiredField}>*</span>
  </>
);

RequiredField.defaultProps = {
  placeholder: null,
};

RequiredField.propTypes = {
  placeholder: PropTypes.string,
};

export default RequiredField;
