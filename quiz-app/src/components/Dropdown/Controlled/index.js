import React from "react";

import cn from "classnames";
import PropTypes from "prop-types";
import Select from "react-select";

import GLOBALS from "../../../app-globals";

import RequiredField from "../../../components/RequiredField";

import Text from "../../Text";
import textTypes from "../../Text/constants/textTypes";

import dropdownTypes from "../constants/dropdownTypes";

import CustomOption from "../custom/Option";
import CustomSingleValue from "../custom/SingleValue";
import CustomValueContainer from "../custom/ValueContainer";
import styles from "../styles.module.scss";
import determineStyles from "../styles/determineStyles";

const ControlledDropdown = ({
  className,
  disabled,
  error,
  helperText,
  id,
  isClearable,
  isMulti,
  isRequired,
  isSearchable,
  label,
  name,
  onBlur,
  onChange,
  options,
  placeholder,
  stylesOverride,
  type,
  value,
  hasBackground,
}) => (
  <div className={cn(className, styles.Dropdown)} id={name}>
    {label && type === dropdownTypes.SLIM && (
      <Text
        className={styles.Dropdown___slim_label}
        colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["400"]}
        element="label"
        id="dropdownLabel"
        type={textTypes.BODY.MD}
      >
        {isRequired ? <RequiredField placeholder={label} /> : label}
      </Text>
    )}
    <Select
      components={{
        Option: CustomOption,
        SingleValue: CustomSingleValue,
        ValueContainer: (valueContainerProps) => (
          <CustomValueContainer type={type} {...valueContainerProps} />
        ),
        IndicatorSeparator: null,
      }}
      id={id}
      isClearable={isClearable}
      isDisabled={disabled}
      isMulti={isMulti}
      isSearchable={isSearchable}
      name={name}
      options={options}
      placeholder={
        isRequired ? <RequiredField placeholder={placeholder} /> : placeholder
      }
      styles={{
        ...determineStyles(type, hasBackground),
        ...stylesOverride,
      }}
      value={value}
      onBlur={onBlur}
      onChange={onChange}
    />
    {(helperText || error) && (
      <div className={styles.Dropdown_helperTextContainer}>
        {helperText && (
          <Text
            className={styles.Dropdown_helperText}
            colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["400"]}
            type={textTypes.BODY.XS}
          >
            {helperText}
          </Text>
        )}
        {error && (
          <Text
            colorClass={GLOBALS.COLOR_CLASSES.RED["200"]}
            type={textTypes.BODY.XS}
          >
            {error}
          </Text>
        )}
      </div>
    )}
  </div>
);

ControlledDropdown.defaultProps = {
  className: null,
  disabled: false,
  error: null,
  helperText: null,
  id: null,
  isClearable: false,
  isMulti: false,
  isSearchable: false,
  label: null,
  onBlur: null,
  placeholder: null,
  stylesOverride: null,
  type: dropdownTypes.FORM,
  value: null,
  hasBackground: false,
};

ControlledDropdown.propTypes = {
  id: PropTypes.string,
  className: PropTypes.string,
  label: PropTypes.string,
  error: PropTypes.string,
  type: PropTypes.oneOf([
    dropdownTypes.FORM,
    dropdownTypes.SLIM,
    dropdownTypes.PLAYGROUND,
    dropdownTypes.LARGE,
  ]),
  onBlur: PropTypes.func,
  onChange: PropTypes.func.isRequired,
  name: PropTypes.string,
  options: PropTypes.oneOfType([
    // Proptype for the non-grouped options
    PropTypes.arrayOf(
      PropTypes.shape({
        value: PropTypes.oneOfType([PropTypes.string, PropTypes.number])
          .isRequired,
        label: PropTypes.string.isRequired,

        // custom icon in each option (a custom component)
        icon: PropTypes.element,
      })
    ),

    // Proptype for the grouped options
    PropTypes.arrayOf(
      PropTypes.shape({
        label: PropTypes.string.isRequired,
        options: PropTypes.arrayOf(
          PropTypes.shape({
            value: PropTypes.oneOfType([PropTypes.string, PropTypes.number])
              .isRequired,
            label: PropTypes.string.isRequired,

            // custom icon in each option (a custom component)
            icon: PropTypes.element,
          })
        ),
      })
    ),
  ]).isRequired,
  isMulti: PropTypes.bool,
  value: PropTypes.oneOfType([
    PropTypes.shape({
      value: PropTypes.oneOfType([PropTypes.string, PropTypes.number])
        .isRequired,
      label: PropTypes.string.isRequired,

      // custom icon in each option (a custom component)
      icon: PropTypes.element,
    }),
    PropTypes.arrayOf(
      PropTypes.shape({
        value: PropTypes.oneOfType([PropTypes.string, PropTypes.number])
          .isRequired,
        label: PropTypes.string.isRequired,

        // custom icon in each option (a custom component)
        icon: PropTypes.element,
      })
    ),
  ]),
  placeholder: PropTypes.string,
  helperText: PropTypes.string,
  isNightMode: PropTypes.bool,
  isClearable: PropTypes.bool,
  isRequired: PropTypes.bool,
  isSearchable: PropTypes.bool,
  disabled: PropTypes.bool,
  stylesOverride: PropTypes.object,
  hasBackground: PropTypes.bool,
};

export default ControlledDropdown;
