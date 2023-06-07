import React from "react";
import PropTypes from "prop-types";
import { components } from "react-select";

const { Placeholder } = components;

const CustomPlaceholder = ({ children, ...rest }) => (
  <Placeholder {...rest}>
    <h3>{children}</h3>
  </Placeholder>
);

CustomPlaceholder.propTypes = {
  children: PropTypes.node.isRequired,
  data: PropTypes.shape({
    icon: PropTypes.element,
    label: PropTypes.string.isRequired,
  }).isRequired,
};

export default CustomPlaceholder;
