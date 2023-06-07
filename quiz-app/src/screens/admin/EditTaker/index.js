import React, { useState } from "react";
import { useHistory, useParams } from "react-router-dom";

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
  ScreenLoader,
} from "components";
import {
  buttonTypes,
  iconButtonTypes,
  inputKinds,
  spinnerSizes,
  textTypes,
} from "components/constants";

import { useTaker } from "hooks";

import styles from "./styles.module.scss";
import { useUpdateTaker } from "hooks";

const EditTaker = () => {
  const { takerId } = useParams();
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

    return errors;
  };

  const { taker, isLoading: isTakerLoading } = useTaker(takerId);

  if (isTakerLoading) {
    return <ScreenLoader />;
  }

  return (
    <Section>
      <Container className={styles.EditTaker}>
        <div className={styles.EditTaker_header}>
          <IconButton
            icon="arrow_back"
            type={iconButtonTypes.OUTLINE.LG}
            onClick={() => history.go(-1)}
          />
          <Text
            colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
            type={textTypes.HEADING.XL}
          >
            Edit Taker
          </Text>
        </div>

        <Formik
          initialValues={{
            name: taker.name,
            address: taker.address,
            email: taker.email,
            username: taker.username,
            password: taker.password,
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
            } = await updateTaker(takerId, currentFormValues);

            const updateTakerCallBacks = {
              updated: () => {
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
              <form onSubmit={handleSubmit} className={styles.EditTaker_form}>
                <div className={styles.EditTaker_form_field}>
                  <div className={styles.EditTaker_form_info}>
                    <Icon
                      className={styles.EditTaker_form_icon}
                      icon="person"
                    />
                    <Text
                      colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
                      type={textTypes.HEADING.MD}
                    >
                      Taker Name
                    </Text>
                  </div>

                  <ControlledInput
                    className={styles.EditTaker_form_input}
                    placeholder="Add taker name..."
                    name="name"
                    value={values.name}
                    error={errors.name}
                    onChange={(e) => setFieldValue("name", e.target.value)}
                  />
                </div>

                <div className={styles.EditTaker_form_field}>
                  <div className={styles.EditTaker_form_info}>
                    <Icon
                      className={styles.EditTaker_form_icon}
                      icon="location_on"
                    />
                    <Text
                      colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
                      type={textTypes.HEADING.MD}
                    >
                      Taker Address
                    </Text>
                  </div>
                  <ControlledInput
                    className={styles.EditTaker_form_input}
                    placeholder="Add taker address..."
                    name="address"
                    value={values.address}
                    error={errors.address}
                    onChange={(e) => setFieldValue("address", e.target.value)}
                  />
                </div>

                <div className={styles.EditTaker_form_field}>
                  <div className={styles.EditTaker_form_info}>
                    <Icon className={styles.EditTaker_form_icon} icon="email" />
                    <Text
                      colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
                      type={textTypes.HEADING.MD}
                    >
                      Taker Email
                    </Text>
                  </div>

                  <ControlledInput
                    className={styles.EditTaker_form_input}
                    placeholder="Add taker email..."
                    name="email"
                    value={values.email}
                    error={errors.email}
                    onChange={(e) => setFieldValue("email", e.target.value)}
                  />
                </div>

                <div className={styles.EditTaker_form_field}>
                  <div className={styles.EditTaker_form_info}>
                    <Icon
                      className={styles.EditTaker_form_icon}
                      icon="account_circle"
                    />
                    <Text
                      colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
                      type={textTypes.HEADING.MD}
                    >
                      Taker Username
                    </Text>
                  </div>

                  <ControlledInput
                    className={styles.EditTaker_form_input}
                    placeholder="Add taker username..."
                    name="username"
                    value={values.username}
                    error={errors.username}
                    onChange={(e) => setFieldValue("username", e.target.value)}
                  />
                </div>

                <div className={styles.EditTaker_form_field}>
                  <div className={styles.EditTaker_form_info}>
                    <Icon
                      className={styles.EditTaker_form_icon}
                      icon="vpn_key"
                    />
                    <Text
                      colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
                      type={textTypes.HEADING.MD}
                    >
                      Taker Password
                    </Text>
                  </div>

                  <ControlledInput
                    className={styles.EditTaker_form_input}
                    placeholder="Add taker password..."
                    name="password"
                    kind={inputKinds.PASSWORD}
                    value={values.password}
                    error={errors.password}
                    onChange={(e) => setFieldValue("password", e.target.value)}
                  />
                </div>

                {errors.overall && (
                  <Text
                    className={styles.EditTaker_form_error}
                    colorClass={GLOBALS.COLOR_CLASSES.RED["200"]}
                  >
                    {errors.overall}
                  </Text>
                )}

                <div className={styles.EditTaker_form_buttonGroup}>
                  <Button
                    kind={GLOBALS.BUTTON_KINDS.SUBMIT}
                    className={styles.EditTaker_form_buttonGroup_button}
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
                          className={styles.EditTaker_form_buttonGroup_spinner}
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
            link={`/admin/takers/${takerId}/info`}
            title="Taker Updated!"
            body={`Taker ${taker.name} has been successfully updated.`}
          />
        )}
      </Container>
    </Section>
  );
};

export default EditTaker;
