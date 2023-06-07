import React, { useState } from "react";
import { useHistory } from "react-router-dom";

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
} from "components";
import {
  buttonTypes,
  iconButtonTypes,
  spinnerSizes,
  textAreaTypes,
  textTypes,
} from "components/constants";
import { useCreateQuestion, useSemiPersistentState } from "hooks";
import { removeLocalStorageItem } from "utils";

import styles from "./styles.module.scss";

const CreateQuestion = () => {
  const history = useHistory();
  const [isSuccessModalOpen, setIsSuccessModalOpen] = useState(false);

  const [question, setQuestion] = useSemiPersistentState("question", "");
  const [correctAnswer, setCorrectAnswer] = useSemiPersistentState(
    "correctAnswer",
    ""
  );

  const { isCreating, createQuestion } = useCreateQuestion();

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

  return (
    <Section>
      <Container className={styles.CreateQuestion}>
        <div className={styles.CreateQuestion_header}>
          <IconButton
            icon="arrow_back"
            type={iconButtonTypes.OUTLINE.LG}
            onClick={() => history.goBack()}
          />
          <Text
            colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
            type={textTypes.HEADING.XL}
          >
            Create Question
          </Text>
        </div>

        <Formik
          initialValues={{
            question: question || "",
            correctAnswer: correctAnswer || "",
          }}
          onSubmit={async (values, { setErrors, setFieldValue }) => {
            const currentFormValues = {
              question: values.question,
              correctAnswer: values.correctAnswer,
            };

            const errors = validate(values);
            if (!isEmpty(errors)) {
              setErrors(errors);
              return;
            }

            // Call the API to create the question
            const { responseCode: createQuestionResponseCode } =
              await createQuestion(currentFormValues);

            const createQuestionCallBacks = {
              created: () => {
                setIsSuccessModalOpen(true);
                // Remove form values from local storage
                removeLocalStorageItem("question");
                removeLocalStorageItem("correctAnswer");

                // Clear form values
                setFieldValue("question", "");
                setFieldValue("correctAnswer", "");
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

            switch (createQuestionResponseCode) {
              case 201:
                createQuestionCallBacks.created();
                break;
              case 400:
                createQuestionCallBacks.invalidFields();
                break;
              case 500:
                createQuestionCallBacks.internalError();
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
                className={styles.CreateQuestion_form}
              >
                <div className={styles.CreateQuestion_form_textArea}>
                  <div className={styles.CreateQuestion_form_info}>
                    <Icon
                      className={styles.CreateQuestion_form_icon}
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
                    className={styles.CreateQuestion_form_textArea_answerArea}
                    isRequired
                    placeholder="Add question..."
                    name="question"
                    value={values.question}
                    type={textAreaTypes.SLIM}
                    onChange={(e) => {
                      setFieldValue("question", e.target.value);
                      setQuestion(e.target.value);
                    }}
                  />
                </div>

                <div className={styles.CreateQuestion_form_textArea}>
                  <div className={styles.CreateQuestion_form_info}>
                    <Icon
                      className={styles.CreateQuestion_form_icon}
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
                    className={styles.CreateQuestion_form_textArea_answerArea}
                    isRequired
                    name="correctAnswer"
                    error={errors.correctAnswer}
                    placeholder="Add answer..."
                    type={textAreaTypes.SLIM}
                    value={values.correctAnswer}
                    onChange={(e) => {
                      setFieldValue("correctAnswer", e.target.value);
                      setCorrectAnswer(e.target.value);
                    }}
                  />
                </div>

                <div className={styles.CreateQuestion_form_buttonGroup}>
                  <Button
                    kind={GLOBALS.BUTTON_KINDS.SUBMIT}
                    className={styles.CreateQuestion_form_buttonGroup_button}
                    type={buttonTypes.PRIMARY.YELLOW}
                    // eslint-disable-next-line @typescript-eslint/no-empty-function
                    onClick={() => {}}
                  >
                    <Text type={textTypes.HEADING.XXS}>
                      Create
                      {isCreating && (
                        <Spinner
                          size={spinnerSizes.XS}
                          colorName={GLOBALS.COLOR_NAMES.BLACK}
                          className={
                            styles.CreateQuestion_form_buttonGroup_spinner
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
            link="/admin/questions"
            title="Question Created!"
            body="Question has been created successfully."
          />
        )}
      </Container>
    </Section>
  );
};

export default CreateQuestion;
