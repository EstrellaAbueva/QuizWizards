import React from "react";

import PropTypes from "prop-types";
import { components } from "react-select";

import GLOBALS from "../../../../app-globals";

import Text from "../../../Text";
import textTypes from "../../../Text/constants/textTypes";

import dropdownTypes from "../../constants/dropdownTypes";

import styles from "./styles.module.scss";

const { ValueContainer } = components;

const CustomValueContainer = (props) => {
  const { children, type, selectProps, hasValue } = props;
  const shouldFloatLabel = selectProps.menuIsOpen || hasValue;

  return (
    <ValueContainer {...props}>
      <div className={styles.CustomValueContainer}>
        {type === dropdownTypes.FORM && shouldFloatLabel && (
          <Text
            className={styles.CustomValueContainer_label}
            colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["500"]}
            type={textTypes.BODY.MD}
          >
            {selectProps.placeholder}
          </Text>
        )}
        {children}
      </div>
    </ValueContainer>
  );
};

CustomValueContainer.propTypes = {
  // the default selectProps from `react-select`
  selectProps: PropTypes.object.isRequired,

  // true if the parent <Select /> has a value
  hasValue: PropTypes.bool.isRequired,

  // the type of the parent <Select />
  type: PropTypes.oneOf([
    dropdownTypes.FORM,
    dropdownTypes.SLIM,
    dropdownTypes.PLAYGROUND,
    dropdownTypes.LARGE,
  ]),

  children: PropTypes.any.isRequired,
};

export default CustomValueContainer;
