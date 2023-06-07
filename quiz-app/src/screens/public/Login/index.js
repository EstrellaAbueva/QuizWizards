import React, { useState, useContext } from "react";

import Cookies from "universal-cookie";

import { Link } from "react-router-dom";
import { Formik } from "formik";
import isEmpty from "lodash/isEmpty";

import GLOBALS from "app-globals";

import { ControlledInput, Button, Spinner, Text, Container } from "components";

import { TakerContext } from "context";
import { inputKinds, spinnerSizes } from "components/constants";
import buttonTypes from "components/Button/constants/buttonTypes";
import BUTTON_KINDS from "app-globals/buttonKinds";
import Logo from "../../../static/images/logo-name.svg";

import styles from "./styles.module.scss";
import { TakersService } from "services";

const validate = (values) => {
  const errors = {};

  if (!values.username) {
    errors.username = "This field is required.";
  }

  if (!values.password) {
    errors.password = "This field is required.";
  }

  return errors;
};

const Login = () => {
  const takerContext = useContext(TakerContext);
  const cookies = new Cookies();
  const [isLoggingIn, setIsLoggingIn] = useState(false);

  return (
    <>
      <Container className={styles.Login}>
        <div className={styles.Login_header}>
          <div className={styles.Login_header_headingTextWrapper}>
            <img src={Logo} width={400} alt="Quiz Wizard" />
          </div>
        </div>

        <div className={styles.Login_content}>
          <Formik
            initialValues={{
              username: "",
              password: "",
            }}
            onSubmit={async (values, { setErrors }) => {
              const currentFormValues = {
                username: values.username,
                password: values.password,
              };

              const errors = validate(values);
              if (!isEmpty(errors)) {
                setErrors(errors);
                return;
              }

              setIsLoggingIn(true);

              try {
                const { data: loginResponse } = await TakersService.login(
                  currentFormValues
                );

                const { data: retrievedUser } =
                  await TakersService.retrieveByUsername(
                    currentFormValues.username
                  );

                cookies.set("accessToken", loginResponse, {
                  path: "/",
                });

                // Update login
                takerContext.loginUpdate({
                  ...retrievedUser,
                });
              } catch (error) {
                setErrors({
                  overall: "Invalid username and/or password.",
                });
              }

              setIsLoggingIn(false);
            }}
          >
            {({ errors, values, handleSubmit, setFieldValue }) => (
              <form onSubmit={handleSubmit}>
                <ControlledInput
                  className={styles.Login_content_input}
                  placeholder="Username"
                  name="username"
                  icon="email"
                  value={values.username}
                  error={errors.username}
                  onChange={(e) => setFieldValue("username", e.target.value)}
                />
                <ControlledInput
                  className={styles.Login_content_input}
                  placeholder="Password"
                  name="password"
                  icon="vpn_key"
                  kind={inputKinds.PASSWORD}
                  value={values.password}
                  error={errors.password}
                  onChange={(e) => setFieldValue("password", e.target.value)}
                />
                {errors.overall && (
                  <Text
                    className={styles.Login_content_input_errorMessage}
                    colorClass={GLOBALS.COLOR_CLASSES.RED["200"]}
                  >
                    {errors.overall}
                  </Text>
                )}

                <div className={styles.Login_content_buttonGroup}>
                  <Button
                    kind={BUTTON_KINDS.SUBMIT}
                    className={styles.Login_button}
                    disabled={isLoggingIn}
                    type={buttonTypes.PRIMARY.YELLOW}
                    // eslint-disable-next-line @typescript-eslint/no-empty-function
                    onClick={() => {}}
                  >
                    <span
                      className={styles.Login_content_buttonGroup_buttonText}
                    >
                      Sign In
                      {isLoggingIn && (
                        <Spinner
                          size={spinnerSizes.XS}
                          colorName={GLOBALS.COLOR_NAMES.BLACK}
                          className={styles.Login_content_buttonGroup_spinner}
                        />
                      )}
                    </span>
                  </Button>

                  <Container className={styles.Signup_content_group}>
                    <Text colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}>
                      No account yet?
                    </Text>
                    <Link className={styles.Signup_link} to="/signup">
                      Sign Up
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

export default Login;
