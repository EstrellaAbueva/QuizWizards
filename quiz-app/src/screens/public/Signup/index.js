import React, { useState, useContext } from "react";

import Cookies from "universal-cookie";
import { Link } from "react-router-dom";

import { Formik } from "formik";
import isEmpty from "lodash/isEmpty";

import GLOBALS from "app-globals";

import { ControlledInput, Button, Spinner, Text, Container } from "components";

import {
  buttonTypes,
  inputKinds,
  spinnerSizes,
  textTypes,
} from "components/constants";

import BUTTON_KINDS from "app-globals/buttonKinds";
import Logo from "../../../static/images/logo-name.svg";

import { TakerContext } from "context";
import { TakersService } from "services";

import styles from "./styles.module.scss";

const validate = (values) => {
  const errors = {};

  if (!values.name) {
    errors.name = "This field is required.";
  }

  if (!values.address) {
    errors.address = "This field is required.";
  }

  if (!values.email) {
    errors.email = "This field is required.";
  }

  if (!values.username) {
    errors.username = "This field is required.";
  }

  if (!values.password) {
    errors.password = "This field is required.";
  }

  if (!values.confirmPassword) {
    errors.confirmPassword = "This field is required.";
  } else if (values.password && values.password !== values.confirmPassword) {
    errors.confirmPassword = "This must match with your password.";
  }

  return errors;
};

const Signup = () => {
  const takerContext = useContext(TakerContext);
  const cookies = new Cookies();
  const [isSigningUp, setIsSigningUp] = useState(false);

  return (
    <>
      <Container className={styles.Signup}>
        <div className={styles.Signup_header}>
          <img src={Logo} width={400} alt="Quiz Wizard" />
        </div>

        <div className={styles.Signup_content}>
          <Formik
            initialValues={{
              name: "",
              address: "",
              email: "",
              username: "",
              password: "",
            }}
            onSubmit={async (values, { setErrors }) => {
              const currentFormValues = {
                name: values.name,
                address: values.address,
                email: values.email,
                username: values.username,
                password: values.password,
              };

              const errors = validate(values);
              if (!isEmpty(errors)) {
                setErrors(errors);
                return;
              }
              setIsSigningUp(true);
              try {
                // Call POST /users/ endpoint
                const { data: signupResponse } = await TakersService.signup(
                  currentFormValues
                );
                // Then we log the user in
                const { data: loginResponse } = await TakersService.login({
                  username: signupResponse.username,
                  password: currentFormValues.password,
                });
                const { data: retrievedUser } =
                  await TakersService.retrieveByUsername(
                    signupResponse.username
                  );
                cookies.set("accessToken", loginResponse, {
                  path: "/",
                });
                // Update login
                takerContext.loginUpdate(retrievedUser);
              } catch (error) {
                console.log(error);
              } finally {
                setIsSigningUp(false);
              }
            }}
          >
            {({ errors, values, handleSubmit, setFieldValue }) => (
              <form onSubmit={handleSubmit} className={styles.Signup_form}>
                <div className={styles.Signup_form_field}>
                  <Text
                    colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
                    type={textTypes.HEADING.XS}
                  >
                    Your Name
                  </Text>
                  <ControlledInput
                    className={styles.Signup_content_input}
                    placeholder="Enter Name..."
                    name="name"
                    icon="badge"
                    value={values.name}
                    error={errors.name}
                    onChange={(e) => setFieldValue("name", e.target.value)}
                  />
                </div>

                <div className={styles.Signup_form_field}>
                  <Text
                    colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
                    type={textTypes.HEADING.XS}
                  >
                    Your Address
                  </Text>

                  <ControlledInput
                    className={styles.Signup_content_input}
                    placeholder="Enter Address..."
                    name="address"
                    icon="location_on"
                    value={values.address}
                    error={errors.address}
                    onChange={(e) => setFieldValue("address", e.target.value)}
                  />
                </div>

                <div className={styles.Signup_form_field}>
                  <Text
                    colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
                    type={textTypes.HEADING.XS}
                  >
                    Your Email
                  </Text>
                  <ControlledInput
                    className={styles.Signup_content_input}
                    placeholder="Enter Email..."
                    name="email"
                    icon="email"
                    value={values.email}
                    error={errors.email}
                    onChange={(e) => setFieldValue("email", e.target.value)}
                  />
                </div>

                <div className={styles.Signup_form_field}>
                  <Text
                    colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
                    type={textTypes.HEADING.XS}
                  >
                    Your Username
                  </Text>

                  <ControlledInput
                    className={styles.Signup_content_input}
                    placeholder="Enter Username..."
                    name="username"
                    icon="person"
                    value={values.username}
                    error={errors.username}
                    onChange={(e) => setFieldValue("username", e.target.value)}
                  />
                </div>
                <div className={styles.Signup_form_field}>
                  <Text
                    colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
                    type={textTypes.HEADING.XS}
                  >
                    Your Password
                  </Text>
                  <ControlledInput
                    className={styles.Signup_content_input}
                    placeholder="Enter Password..."
                    name="password"
                    icon="vpn_key"
                    kind={inputKinds.PASSWORD}
                    value={values.password}
                    error={errors.password}
                    onChange={(e) => setFieldValue("password", e.target.value)}
                  />
                </div>

                <div className={styles.Signup_form_field}>
                  <Text
                    colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
                    type={textTypes.HEADING.XS}
                  >
                    Confirm Password
                  </Text>
                  <ControlledInput
                    className={styles.Signup_content_input}
                    placeholder="Confirm Password..."
                    name="confirmPassword"
                    icon="lock"
                    kind={inputKinds.PASSWORD}
                    value={values.confirmPassword}
                    error={errors.confirmPassword}
                    onChange={(e) =>
                      setFieldValue("confirmPassword", e.target.value)
                    }
                  />
                </div>

                {errors.overall && (
                  <Text
                    className={styles.Signup_content_input_errorMessage}
                    colorClass={GLOBALS.COLOR_CLASSES.RED["200"]}
                  >
                    {errors.overall}
                  </Text>
                )}

                <div className={styles.Signup_content_buttonGroup}>
                  <Button
                    kind={BUTTON_KINDS.SUBMIT}
                    className={styles.Signup_button}
                    disabled={isSigningUp}
                    type={buttonTypes.PRIMARY.YELLOW}
                    // eslint-disable-next-line @typescript-eslint/no-empty-function
                    onClick={() => {}}
                  >
                    <span
                      className={styles.Signup_content_buttonGroup_buttonText}
                    >
                      Sign Up
                      {isSigningUp && (
                        <Spinner
                          size={spinnerSizes.XS}
                          colorName={GLOBALS.COLOR_NAMES.BLACK}
                          className={styles.Signup_content_buttonGroup_spinner}
                        />
                      )}
                    </span>
                  </Button>
                  <Container className={styles.Signup_content_group}>
                    <Text colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}>
                      Already have an account?
                    </Text>
                    <Link className={styles.Signup_link} to="/login">
                      Sign In
                    </Link>
                  </Container>
                </div>
              </form>
            )}
          </Formik>
        </div>
      </Container>
    </>
  );
};

export default Signup;
