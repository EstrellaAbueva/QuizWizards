import React from "react";

import cn from "classnames";
import PropTypes from "prop-types";

import GLOBALS from "../../../app-globals";

import RequiredField from "../../RequiredField";

import Icon from "../../Icons";
import Text from "../../Text";
import textTypes from "../../Text/constants/textTypes";

import inputKinds from "../constants/inputKinds";
import inputTypes from "../constants/inputTypes";
import styles from "../styles.module.scss";

const ControlledInput = ({
  id,
  type,
  kind,
  className,
  inputClassName,
  placeholder,
  isRequired,
  error,
  success,
  name,
  label,
  disabled,
  value,
  icon,
  hasBackground,
  maxLength,
  autoComplete,
  helperText,
  step,
  tabIndex,
  min,
  max,
  onBlur,
  onChange,
}) => {
  return (
    <div className={cn(className, styles.Input_container)}>
      {label && type !== inputTypes.FORM && (
        <Text
          className={styles[`Input___${type}_label`]}
          colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["400"]}
          element="label"
          type={textTypes.BODY.MD}
        >
          {isRequired ? <RequiredField placeholder={label} /> : label}
        </Text>
      )}

      <input
        autoComplete={autoComplete}
        className={cn(styles[`Input___${type}`], inputClassName, {
          [styles[`Input___${type}___icon`]]: icon !== null,
          [styles.Input___success]: success !== null,
          [styles.Input___error]: error !== null,
          [styles.Input___white]: hasBackground,
        })}
        disabled={disabled}
        id={id || name}
        max={max}
        maxLength={maxLength}
        min={min}
        name={name}
        placeholder={
          type !== inputTypes.FORM && placeholder ? placeholder : null
        }
        step={step}
        tabIndex={tabIndex}
        type={kind}
        value={value || ""}
        onBlur={onBlur}
        onChange={onChange}
      />

      {placeholder && type === inputTypes.FORM && (
        <Text
          className={cn(styles.Input___form_label, {
            [styles.Input___form_label___active]: value !== "",
          })}
          colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["500"]}
          type={textTypes.BODY.MD}
        >
          {isRequired ? (
            <RequiredField placeholder={placeholder} />
          ) : (
            placeholder
          )}
        </Text>
      )}

      {icon && <Icon className={styles[`Input___${type}_icon`]} icon={icon} />}

      {(helperText || success || error) && (
        <div className={styles.Input_helperTextContainer}>
          {helperText && (
            <Text
              colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["500"]}
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

          {success && (
            <Text
              colorClass={GLOBALS.COLOR_CLASSES.GREEN["500"]}
              type={textTypes.BODY.XS}
            >
              {success}
            </Text>
          )}
        </div>
      )}
    </div>
  );
};

ControlledInput.defaultProps = {
  id: null,
  type: inputTypes.SLIM,
  kind: inputKinds.TEXT,
  className: null,
  inputClassName: null,
  placeholder: null,
  error: null,
  success: null,
  label: null,
  disabled: false,
  value: "",
  maxLength: null,
  autoComplete: null,
  helperText: null,
  icon: null,
  hasBackground: false,
  step: null,
  tabIndex: null,
  min: null,
  max: null,
  onBlur: null,
};

ControlledInput.propTypes = {
  type: PropTypes.oneOf([inputTypes.FORM, inputTypes.SLIM, inputTypes.CODE]),
  kind: PropTypes.oneOf([
    inputKinds.NUMBER,
    inputKinds.PASSWORD,
    inputKinds.TEXT,
  ]),
  id: PropTypes.string,
  className: PropTypes.string,
  inputClassName: PropTypes.string,
  placeholder: PropTypes.string,
  error: PropTypes.string,
  success: PropTypes.string,
  name: PropTypes.string.isRequired,
  label: PropTypes.string,
  disabled: PropTypes.bool,
  value: PropTypes.string,
  onBlur: PropTypes.func,
  onChange: PropTypes.func.isRequired,
  icon: PropTypes.string,
  maxLength: PropTypes.number,
  autoComplete: PropTypes.string,
  helperText: PropTypes.string,
  step: PropTypes.number,
  tabIndex: PropTypes.number,
  min: PropTypes.number,
  max: PropTypes.number,
  isRequired: PropTypes.bool,
  hasBackground: PropTypes.bool,
};

export default ControlledInput;
