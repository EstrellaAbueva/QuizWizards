import React, { useState } from "react";
import { useHistory, useParams } from "react-router-dom";

import isEmpty from "lodash/isEmpty";
import { Formik } from "formik";
import GLOBALS from "app-globals";
import {
  Button,
  Container,
  ControlledTextArea,
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
  spinnerSizes,
  textAreaTypes,
  textTypes,
} from "components/constants";

import styles from "./styles.module.scss";
import { useUpdateQuestion, useQuestion } from "hooks";

const EditQuestion = () => {
  const { questionId } = useParams();
  const history = useHistory();

  const [isSuccessModalOpen, setIsSuccessModalOpen] = useState(false);

  const { isUpdating, updateQuestion } = useUpdateQuestion();

  const validate = (values) => {
    const errors = {};

    if (!values.question) {
      errors.question = "This field is required.";
    }

    if (!values.correctAnswer) {
      errors.correctAnswer = "This field is required.";
    }

    return errors;
  };

  const { question, isLoading: isQuestionLoading } = useQuestion(questionId);

  if (isQuestionLoading) {
    return <ScreenLoader />;
  }

  return (
    <Section>
      <Container className={styles.EditQuestion}>
        <div className={styles.EditQuestion_header}>
          <IconButton
            icon="arrow_back"
            type={iconButtonTypes.OUTLINE.LG}
            onClick={() => history.go(-1)}
          />
          <Text
            colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
            type={textTypes.HEADING.XL}
          >
            Edit Question
          </Text>
        </div>

        <Formik
          initialValues={{
            question: question.question,
            correctAnswer: question.correctAnswer,
          }}
          onSubmit={async (values, { setErrors }) => {
            const currentFormValues = {
              question: values.question,
              correctAnswer: values.correctAnswer,
            };

            const errors = validate(values);
            if (!isEmpty(errors)) {
              setErrors(errors);
              return;
            }

            // Call the API to update the question
            const { responseCode: updateQuestionResponseCode } =
              await updateQuestion(questionId, currentFormValues);

            const updateQuestionCallBacks = {
              updated: () => {
                setIsSuccessModalOpen(true);
              },
              invalidFields: () => {
                errors.overall = "Invalid fields.";
                setErrors(errors);
              },
              internalError: () => {
                errors.overall = "Internal error.";
                setErrors(errors);
              },
            };

            switch (updateQuestionResponseCode) {
              case 200:
                updateQuestionCallBacks.updated();
                break;
              case 400:
                updateQuestionCallBacks.invalidFields();
                break;
              case 500:
                updateQuestionCallBacks.internalError();
                break;
              default:
                break;
            }
          }}
        >
          {({ errors, values, handleSubmit, setFieldValue }) => {
            return (
              <form
                onSubmit={handleSubmit}
                className={styles.EditQuestion_form}
              >
                <div className={styles.EditQuestion_form_input}>
                  <div className={styles.EditQuestion_form_info}>
                    <Icon
                      className={styles.EditQuestion_form_icon}
                      icon="help"
                    />
                    <Text
                      colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
                      type={textTypes.HEADING.MD}
                    >
                      Question
                    </Text>
                  </div>

                  <ControlledTextArea
                    className={styles.EditQuestion_form_textArea_answerArea}
                    isRequired
                    placeholder="Add question..."
                    name="question"
                    value={values.question}
                    type={textAreaTypes.SLIM}
                    error={errors.question}
                    onChange={(e) => setFieldValue("question", e.target.value)}
                  />
                </div>

                <div className={styles.EditQuestion_form_textArea}>
                  <div className={styles.EditQuestion_form_info}>
                    <Icon
                      className={styles.EditQuestion_form_icon}
                      icon="description"
                    />
                    <Text
                      colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
                      type={textTypes.HEADING.MD}
                    >
                      Answer
                    </Text>
                  </div>

                  <ControlledTextArea
                    className={styles.EditQuestion_form_textArea_answerArea}
                    isRequired
                    name="correctAnswer"
                    error={errors.correctAnswer}
                    placeholder="Add answer..."
                    type={textAreaTypes.SLIM}
                    value={values.correctAnswer}
                    onChange={(e) =>
                      setFieldValue("correctAnswer", e.target.value)
                    }
                  />
                </div>

                {errors.overall && (
                  <Text
                    className={styles.EditQuestion_form_error}
                    colorClass={GLOBALS.COLOR_CLASSES.RED["200"]}
                  >
                    {errors.overall}
                  </Text>
                )}

                <div className={styles.EditQuestion_form_buttonGroup}>
                  <Button
                    kind={GLOBALS.BUTTON_KINDS.SUBMIT}
                    className={styles.EditQuestion_form_buttonGroup_button}
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
                            styles.EditQuestion_form_buttonGroup_spinner
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
            link={`/admin/questions/${questionId}/info`}
            title="Question Updated!"
            body="Question has been updated successfully."
          />
        )}
      </Container>
    </Section>
  );
};

export default EditQuestion;
