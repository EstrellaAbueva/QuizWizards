import React from "react";
import cn from "classnames";
import PropTypes from "prop-types";
import styles from "./styles.module.scss";

const Container = React.forwardRef(({ children, className }, ref) => (
  <div ref={ref} className={cn(styles.Container, className)}>
    {children}
  </div>
));

Container.displayName = "Container";

Container.defaultProps = {
  className: null,
  children: null,
};

Container.propTypes = {
  className: PropTypes.string,
  children: PropTypes.any,
};

export default Container;
