import React from "react";

import cn from "classnames";
import PropTypes from "prop-types";

import GLOBALS from "app-globals";

import { Icon, Text } from "components";
import { textTypes } from "components/constants";

import styles from "./styles.module.scss";

const TakerTag = ({ name, className, isAdmin }) => (
  <div className={cn(className, styles.TakerTag)}>
    <Icon
      className={cn(
        styles.TakerTag_icon,
        isAdmin && styles.TakerTag___adminColor
      )}
      icon="person"
    />
    <Text
      colorClass={
        isAdmin
          ? GLOBALS.COLOR_CLASSES.NEUTRAL["50"]
          : GLOBALS.COLOR_CLASSES.NEUTRAL["0"]
      }
      type={textTypes.HEADING.XS}
    >
      {name}
    </Text>
  </div>
);

TakerTag.defaultProps = {
  className: null,
  image: null,
  isAdmin: null,
};

TakerTag.propTypes = {
  className: PropTypes.string,
  name: PropTypes.string.isRequired,
  isAdmin: PropTypes.bool,
};

export default TakerTag;
