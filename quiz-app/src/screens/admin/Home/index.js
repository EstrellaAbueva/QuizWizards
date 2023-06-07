import React, { useState } from "react";

import isEmpty from "lodash/isEmpty";
import { Formik } from "formik";
import GLOBALS from "app-globals";
import {
  Button,
  Container,
  ControlledDropdown,
  Spinner,
  Section,
  ConfirmModal,
  Text,
  ScreenLoader,
} from "components";
import {
  buttonTypes,
  dropdownTypes,
  spinnerSizes,
  textTypes,
} from "components/constants";

import styles from "./styles.module.scss";
import { useQuizzes, useTakers } from "hooks";
import { TakersService } from "services";

const Home = () => {
  const [isAssigning, setisAssigning] = useState(false);
  const [isSuccessModalOpen, setIsSuccessModalOpen] = useState(false);

  const validate = (values) => {
    const errors = {};

    if (!values.takerId) {
      errors.takerId = "This field is required.";
    }

    if (!values.quizId) {
      errors.quizId = "This field is required.";
    }

    return errors;
  };

  const { takers, isLoading: isTakersLoading } = useTakers();
  const { quizzes, isLoading: isQuizzesLoading } = useQuizzes();

  if (isTakersLoading || isQuizzesLoading) {
    return <ScreenLoader />;
  }

  return (
    <Section>
      <Container className={styles.Home}>
        <div className={styles.Home_header}>
          <Text
            colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
            type={textTypes.HEADING.XL}
          >
            Designatesss a participant to take a quiz
          </Text>
        </div>

        <Formik
          initialValues={{
            takerId: "",
            quizId: "",
          }}
          onSubmit={async (values, { setErrors }) => {
            const currentFormValues = {
              takerId: Number(values.takerId),
              quizId: Number(values.quizId),
            };

            const errors = validate(values);
            if (!isEmpty(errors)) {
              setErrors(errors);
              return;
            }

            // Add functions here calling apis
            try {
              setisAssigning(true);

              await TakersService.assign(
                currentFormValues.takerId,
                currentFormValues.quizId
              );

              setisAssigning(false);
              setIsSuccessModalOpen(true);
            } catch (error) {
              if (error.response.status === 400) {
                if (error.response.data) {
                  // find the taker object with the matching takerId
                  const taker = takers.find(
                    (t) => t.id === currentFormValues.takerId
                  );
                  const takerName = taker?.name;

                  // find the quiz object with the matching quizId
                  const quiz = quizzes.find(
                    (q) => q.id === currentFormValues.quizId
                  );
                  const quizName = quiz?.name;

                  setErrors({
                    overall: `${takerName} is already assigned to quiz ${quizName}.`,
                  });
                }
              }

              setisAssigning(false);
            }
          }}
        >
          {({ errors, values, handleSubmit, setFieldValue }) => {
            // Update taker values of the dropdowns based on the values of the formik form
            const takerValues = takers
              .filter((taker) => taker.id === Number(values.takerId))
              .map((taker) => ({
                value: taker.id,
                name: taker.name,
                label: taker.name,
              }));

            // Update taker displayed value when the user selects a new value
            const onTakerChange = (newtakerValue) => {
              setFieldValue("takerId", newtakerValue?.value || "");
            };

            // Update quiz values of the dropdowns based on the values of the formik form
            const quizValues = quizzes
              .filter((quiz) => quiz.id === Number(values.quizId))
              .map((quiz) => ({
                value: quiz.id,
                name: quiz.name,
                label: quiz.name,
              }));

            // Update quiz displayed value when the user selects a new value
            const onQuizChange = (newQuizValue) => {
              setFieldValue("quizId", newQuizValue?.value || "");
            };

            return (
              <form onSubmit={handleSubmit} className={styles.Home_form}>
                <ControlledDropdown
                  isMulti={false}
                  isRequired
                  className={styles.Home_form_dropdown}
                  error={errors.takerId}
                  name="takerId"
                  options={takers.map((taker) => ({
                    value: taker.id,
                    label: taker.name,
                  }))}
                  placeholder="Select taker..."
                  type={dropdownTypes.FORM}
                  value={takerValues}
                  onChange={onTakerChange}
                />
                <ControlledDropdown
                  isMulti={false}
                  isRequired
                  className={styles.Home_form_dropdown}
                  error={errors.quizId}
                  name="quizId"
                  options={quizzes.map((quiz) => ({
                    value: quiz.id,
                    label: quiz.name,
                  }))}
                  placeholder="Select quiz..."
                  type={dropdownTypes.FORM}
                  value={quizValues}
                  onChange={onQuizChange}
                />
                {errors.overall && (
                  <Text
                    className={styles.Home_form_error}
                    colorClass={GLOBALS.COLOR_CLASSES.RED["200"]}
                  >
                    {errors.overall}
                  </Text>
                )}

                <div className={styles.Home_form_buttonGroup}>
                  <Button
                    kind={GLOBALS.BUTTON_KINDS.SUBMIT}
                    className={styles.Home_form_buttonGroup_button}
                    disabled={isAssigning}
                    type={buttonTypes.PRIMARY.YELLOW}
                    // eslint-disable-next-line @typescript-eslint/no-empty-function
                    onClick={() => {}}
                  >
                    <Text type={textTypes.HEADING.XXS}>
                      Assign
                      {isAssigning && (
                        <Spinner
                          size={spinnerSizes.XS}
                          colorName={GLOBALS.COLOR_NAMES.BLACK}
                          className={styles.Home_form_buttonGroup_spinner}
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
            title="Quiz Assigned!"
            body="You have successfully assigned a quiz to a participant."
          />
        )}
      </Container>
    </Section>
  );
};

export default Home;
