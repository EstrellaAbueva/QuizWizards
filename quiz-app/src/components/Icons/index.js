import React from "react";

import cn from "classnames";
import PropTypes from "prop-types";

import styles from "./styles.module.scss";

const Icon = React.forwardRef(
  ({ id, icon, className, style, isPulsating, ...rest }, ref) => (
    <i
      className={cn(styles.Icon, className, {
        [styles.Icon___pulsating]: isPulsating === true,
      })}
      id={id}
      style={style}
      {...rest}
      ref={ref}
    >
      {icon}
    </i>
  )
);

Icon.displayName = "Icon";

Icon.defaultProps = {
  id: null,
  className: null,
  style: null,
  isPulsating: false,
};

Icon.propTypes = {
  id: PropTypes.string,
  className: PropTypes.string,
  icon: PropTypes.string.isRequired,
  style: PropTypes.object,
  isPulsating: PropTypes.bool,
};

export default Icon;
