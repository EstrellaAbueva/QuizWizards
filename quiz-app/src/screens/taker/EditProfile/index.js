import React, { useState, useContext } from "react";
import { useHistory } from "react-router-dom";

import isEmpty from "lodash/isEmpty";
import { Formik } from "formik";
import GLOBALS from "app-globals";
import {
  Button,
  Container,
  ControlledInput,
  Icon,
  IconButton,
  Spinner,
  Section,
  Text,
  ConfirmModal,
} from "components";
import {
  buttonTypes,
  iconButtonTypes,
  inputKinds,
  spinnerSizes,
  textTypes,
} from "components/constants";

import { TakerContext } from "context";

import styles from "./styles.module.scss";
import { TAKER_ROUTES } from "screen-wrappers/Taker/constants";
import { useUpdateTaker } from "hooks";

const EditProfile = () => {
  const { taker, loginUpdate } = useContext(TakerContext);
  const history = useHistory();

  const [isSuccessModalOpen, setIsSuccessModalOpen] = useState(false);

  const { isUpdating, updateTaker } = useUpdateTaker();

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

  return (
    <Section>
      <Container className={styles.EditProfile}>
        <div className={styles.EditProfile_header}>
          <IconButton
            icon="arrow_back"
            type={iconButtonTypes.OUTLINE.LG}
            onClick={() => history.go(-1)}
          />
          <Text
            colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
            type={textTypes.HEADING.XL}
          >
            Edit Profile
          </Text>
        </div>

        <Formik
          initialValues={{
            name: taker.name,
            address: taker.address,
            email: taker.email,
            username: taker.username,
            password: taker.password,
            confirmPassword: "",
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

            // Call the API to update the taker
            const {
              responseCode: updateTakerResponseCode,
              errors: updateErrors,
            } = await updateTaker(taker.id, currentFormValues);

            const updateTakerCallBacks = {
              updated: () => {
                loginUpdate({
                  ...taker,
                  ...currentFormValues,
                });

                setIsSuccessModalOpen(true);
              },
              invalidFields: () => {
                setErrors(updateErrors);
              },
              internalError: () => {
                errors.overall = "Internal error.";
                setErrors(errors);
              },
            };

            switch (updateTakerResponseCode) {
              case 200:
                updateTakerCallBacks.updated();
                break;
              case 400:
                updateTakerCallBacks.invalidFields();
                break;
              case 500:
                updateTakerCallBacks.internalError();
                break;
              default:
                break;
            }
          }}
        >
          {({ errors, values, handleSubmit, setFieldValue }) => {
            return (
              <form onSubmit={handleSubmit} className={styles.EditProfile_form}>
                <div className={styles.EditProfile_form_field}>
                  <div className={styles.EditProfile_form_info}>
                    <Icon
                      className={styles.EditProfile_form_icon}
                      icon="person"
                    />
                    <Text type={textTypes.HEADING.MD}>Your Name</Text>
                  </div>

                  <ControlledInput
                    className={styles.EditProfile_form_input}
                    placeholder="Add your name..."
                    name="name"
                    value={values.name}
                    error={errors.name}
                    onChange={(e) => setFieldValue("name", e.target.value)}
                  />
                </div>

                <div className={styles.EditProfile_form_field}>
                  <div className={styles.EditProfile_form_info}>
                    <Icon
                      className={styles.EditProfile_form_icon}
                      icon="location_on"
                    />
                    <Text type={textTypes.HEADING.MD}>Your Address</Text>
                  </div>
                  <ControlledInput
                    className={styles.EditProfile_form_input}
                    placeholder="Add your address..."
                    name="address"
                    value={values.address}
                    error={errors.address}
                    onChange={(e) => setFieldValue("address", e.target.value)}
                  />
                </div>

                <div className={styles.EditProfile_form_field}>
                  <div className={styles.EditProfile_form_info}>
                    <Icon
                      className={styles.EditProfile_form_icon}
                      icon="email"
                    />
                    <Text type={textTypes.HEADING.MD}>Your Email</Text>
                  </div>

                  <ControlledInput
                    className={styles.EditProfile_form_input}
                    placeholder="Add your email..."
                    name="email"
                    value={values.email}
                    error={errors.email}
                    onChange={(e) => setFieldValue("email", e.target.value)}
                  />
                </div>

                <div className={styles.EditProfile_form_field}>
                  <div className={styles.EditProfile_form_info}>
                    <Icon
                      className={styles.EditProfile_form_icon}
                      icon="account_box"
                    />
                    <Text type={textTypes.HEADING.MD}>Your Username</Text>
                  </div>

                  <ControlledInput
                    className={styles.EditProfile_form_input}
                    placeholder="Add your username..."
                    name="username"
                    value={values.username}
                    error={errors.username}
                    onChange={(e) => setFieldValue("username", e.target.value)}
                  />
                </div>

                <div className={styles.EditProfile_form_field}>
                  <div className={styles.EditProfile_form_info}>
                    <Icon
                      className={styles.EditProfile_form_icon}
                      icon="lock"
                    />
                    <Text type={textTypes.HEADING.MD}>Your Password</Text>
                  </div>

                  <ControlledInput
                    className={styles.EditProfile_form_input}
                    placeholder="Add your password..."
                    name="password"
                    kind={inputKinds.PASSWORD}
                    value={values.password}
                    error={errors.password}
                    onChange={(e) => setFieldValue("password", e.target.value)}
                  />
                </div>

                <div className={styles.EditProfile_form_field}>
                  <div className={styles.EditProfile_form_info}>
                    <Icon
                      className={styles.EditProfile_form_icon}
                      icon="vpn_key"
                    />
                    <Text type={textTypes.HEADING.MD}>Confirm Password</Text>
                  </div>

                  <ControlledInput
                    className={styles.EditProfile_form_input}
                    placeholder="Confirm your password..."
                    name="confirmPassword"
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
                    className={styles.EditProfile_form_error}
                    colorClass={GLOBALS.COLOR_CLASSES.RED["200"]}
                  >
                    {errors.overall}
                  </Text>
                )}

                <div className={styles.EditProfile_form_buttonGroup}>
                  <Button
                    kind={GLOBALS.BUTTON_KINDS.SUBMIT}
                    className={styles.EditProfile_form_buttonGroup_button}
                    disabled={isUpdating}
                    type={buttonTypes.PRIMARY.YELLOW}
                    // eslint-disable-next-line @typescript-eslint/no-empty-function
                    onClick={() => {}}
                  >
                    <Text type={textTypes.HEADING.XXS}>
                      Save
                      {isUpdating && (
                        <Spinner
                          size={spinnerSizes.XS}
                          colorName={GLOBALS.COLOR_NAMES.BLACK}
                          className={
                            styles.EditProfile_form_buttonGroup_spinner
                          }
                        />
                      )}
                    </Text>
                  </Button>
                </div>
              </form>
            );
          }}
        </Formik>

        {isSuccessModalOpen && (
          <ConfirmModal
            isOpen={isSuccessModalOpen}
            handleClose={() => setIsSuccessModalOpen(false)}
            link={TAKER_ROUTES.HOME}
            title="Profile Updated!"
            body="Your profile has been successfully updated."
          />
        )}
      </Container>
    </Section>
  );
};

export default EditProfile;
