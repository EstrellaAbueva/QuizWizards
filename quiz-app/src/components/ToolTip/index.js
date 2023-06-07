import React from "react";
import PropTypes from "prop-types";
import Tippy from "@tippyjs/react";
import "tippy.js/dist/tippy.css";
import "tippy.js/themes/light.css";
import styles from "./styles.module.scss";

import tooltipPlacement from "./constants/tooltipPlacement";

const Tooltip = ({ className, content, children, placement }) => (
  <Tippy
    allowHTML
    animation="fade"
    className={styles.Tooltip}
    content={content}
    delay={0}
    duration={300}
    placement={placement}
    theme="light"
  >
    <span className={className} tabIndex={-1}>
      {children}
    </span>
  </Tippy>
);

Tooltip.defaultProps = {
  placement: tooltipPlacement.BOTTOM,
  className: null,
};

Tooltip.propTypes = {
  className: PropTypes.string,

  // the text to be displayed in the tooltip
  content: PropTypes.string.isRequired,

  // the element where the tooltip will be toggled
  children: PropTypes.node,

  placement: PropTypes.oneOf([
    tooltipPlacement.BOTTOM,
    tooltipPlacement.LEFT,
    tooltipPlacement.RIGHT,
  ]),
};

export default Tooltip;
