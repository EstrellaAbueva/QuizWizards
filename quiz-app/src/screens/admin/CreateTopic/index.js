import React, { useState } from "react";
import { useHistory } from "react-router-dom";

import isEmpty from "lodash/isEmpty";
import { Formik } from "formik";
import GLOBALS from "app-globals";
import {
  Button,
  Container,
  ControlledDropdown,
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
  dropdownTypes,
  iconButtonTypes,
  spinnerSizes,
  textTypes,
} from "components/constants";
import {
  useSemiPersistentState,
  useSemiPersistentStateForObjects,
} from "hooks";
import { removeLocalStorageItem } from "utils";
import { useCreateTopic, useQuestions } from "hooks";

import styles from "./styles.module.scss";

const CreateTopic = () => {
  const history = useHistory();
  const [isSuccessModalOpen, setIsSuccessModalOpen] = useState(false);

  const [topicName, setTopicName] = useSemiPersistentState("topicName", "");
  const [addedQuestions, setAddedQuestions] = useSemiPersistentStateForObjects(
    "addedQuestions",
    []
  );

  const { isCreating, createTopic } = useCreateTopic();

  const validate = (values) => {
    const errors = {};

    if (!values.name) {
      errors.name = "This field is required.";
    }

    if (isEmpty(addedQuestions)) {
      errors.questionIds = "Add questions to the topic.";
    }

    return errors;
  };

  const { questions, isLoading: isQuestionsLoading } = useQuestions();

  if (isQuestionsLoading) {
    return <ScreenLoader />;
  }

  return (
    <Section>
      <Container className={styles.CreateTopic}>
        <div className={styles.CreateTopic_header}>
          <IconButton
            icon="arrow_back"
            type={iconButtonTypes.OUTLINE.LG}
            onClick={() => history.go(-1)}
          />
          <Text
            colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
            type={textTypes.HEADING.XL}
          >
            Create Topic
          </Text>
        </div>

        <Formik
          initialValues={{
            name: topicName || "",
            questionId: [],
          }}
          onSubmit={async (values, { setErrors, setFieldValue }) => {
            const currentFormValues = {
              name: values.name,
              questionId: addedQuestions
                .map((question) => question.value)
                .join(",")
                .split(","),
            };

            const errors = validate(values);
            if (!isEmpty(errors)) {
              setErrors(errors);
              return;
            }

            // Call the API to create the topic
            const { responseCode: createTopicResponseCode } = await createTopic(
              currentFormValues
            );

            const createTopicCallBacks = {
              created: () => {
                setIsSuccessModalOpen(true);
                // Remove form values from local storage
                removeLocalStorageItem("topicName");
                removeLocalStorageItem("addedQuestions");

                // Reset form values
                setFieldValue("name", "");
                setFieldValue("questionId", []);
                setAddedQuestions([]);
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

            switch (createTopicResponseCode) {
              case 201:
                createTopicCallBacks.created();
                break;
              case 400:
                createTopicCallBacks.invalidFields();
                break;
              case 500:
                createTopicCallBacks.internalError();
                break;
              default:
                break;
            }
          }}
        >
          {({ errors, values, handleSubmit, setFieldValue }) => {
            // Filter out questions that are already in topic
            const availableQuestions = questions.filter(
              (question) =>
                !addedQuestions.some(
                  (addedQuestions) => addedQuestions.value === question.id
                )
            );

            // Convert question values to dropdown options
            const questionValues = values.questionId
              .map((questionId) =>
                questions.find((question) => question?.id === questionId)
              )
              .map((question) => ({
                value: question?.id,
                label: question?.question,
                correctAnswer: question?.correctAnswer,
              }));

            // Gets the answer of the question
            const getAnswerByQuestionId = (questionId) => {
              const question = questions.find(
                (question) => question.id === questionId
              );
              return question ? question.correctAnswer : null;
            };

            // Update question values on change
            const onQuestionChange = (newQuestionValues) => {
              setFieldValue(
                "questionId",
                newQuestionValues?.map((question) => question.value) || []
              );
            };

            // Add question to the list of added questions
            const handleAddedQuestions = () => {
              const newAddedQuestions = questionValues.filter(
                (question) =>
                  !addedQuestions.some(
                    (addedQuestion) => addedQuestion.value === question.value
                  )
              );
              setAddedQuestions(addedQuestions.concat(newAddedQuestions));
            };

            // Remove question from the list of added questions
            const handleDelete = (questionValue) => {
              const newAddedQuestions = addedQuestions.filter(
                (question) => question.value !== questionValue
              );
              setAddedQuestions(newAddedQuestions);
            };

            return (
              <form onSubmit={handleSubmit} className={styles.CreateTopic_form}>
                <div className={styles.CreateTopic_form_input}>
                  <div className={styles.CreateTopic_form_info}>
                    <Icon
                      className={styles.CreateTopic_form_icon}
                      icon="quiz"
                    />
                    <Text
                      colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
                      type={textTypes.HEADING.MD}
                    >
                      Topic Name
                    </Text>
                  </div>

                  <ControlledInput
                    placeholder="Add topic name..."
                    name="name"
                    value={values.name}
                    error={errors.name}
                    onChange={(e) => {
                      setFieldValue("name", e.target.value);
                      setTopicName(e.target.value);
                    }}
                  />
                </div>

                <div className={styles.CreateTopic_form_questions}>
                  <div className={styles.CreateTopic_form_info}>
                    <Icon
                      className={styles.CreateTopic_form_icon}
                      icon="help"
                    />
                    <Text
                      colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
                      type={textTypes.HEADING.MD}
                    >
                      Questions
                    </Text>
                  </div>
                  <div className={styles.CreateTopic_form_questions_add}>
                    <ControlledDropdown
                      isMulti
                      isRequired
                      className={styles.CreateTopic_form_questions_dropdown}
                      error={errors.questionId}
                      name="questionId"
                      options={availableQuestions.map((question) => ({
                        value: question.id,
                        label: question.question,
                        correctAnswer: question.correctAnswer,
                      }))}
                      placeholder="Select questions..."
                      type={dropdownTypes.FORM}
                      value={questionValues}
                      onChange={onQuestionChange}
                    />
                    <IconButton icon="add" onClick={handleAddedQuestions} />
                  </div>

                  {!isEmpty(addedQuestions) && (
                    <div className={styles.CreateTopic_addedQuestions}>
                      {addedQuestions.map((question) => (
                        <div
                          className={styles.CreateTopic_addedQuestions_question}
                          key={question.id}
                        >
                          <div
                            className={
                              styles.CreateTopic_addedQuestions_question_name
                            }
                          >
                            <Icon
                              icon="help"
                              className={
                                styles.CreateTopic_addedQuestions_question_icon
                              }
                            />
                            <div
                              className={
                                styles.CreateTopic_addedQuestions_questionAnswer
                              }
                            >
                              <Text type={textTypes.HEADING.XS}>
                                {question.label}
                              </Text>

                              <Text
                                type={textTypes.HEADING.XS}
                                colorClass={GLOBALS.COLOR_CLASSES.BLUE["200"]}
                              >
                                Answer: {getAnswerByQuestionId(question.value)}
                              </Text>
                            </div>
                          </div>
                          <div
                            className={styles.CreateTopic_addedQuestions_delete}
                          >
                            <IconButton
                              icon="delete"
                              onClick={() => handleDelete(question.value)}
                              colorName={GLOBALS.COLOR_NAMES.RED}
                              type={iconButtonTypes.OUTLINE.MD}
                            />
                          </div>
                        </div>
                      ))}
                    </div>
                  )}
                </div>
                {errors.overall && (
                  <Text
                    className={styles.CreateTopic_form_error}
                    colorClass={GLOBALS.COLOR_CLASSES.RED["200"]}
                  >
                    {errors.overall}
                  </Text>
                )}

                <div className={styles.CreateTopic_form_buttonGroup}>
                  <Button
                    kind={GLOBALS.BUTTON_KINDS.SUBMIT}
                    className={styles.CreateTopic_form_buttonGroup_button}
                    disabled={isCreating}
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
                            styles.CreateTopic_form_buttonGroup_spinner
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
            link="/admin/topics"
            title="Topic Created!"
            body="Topic has been created successfully."
          />
        )}
      </Container>
    </Section>
  );
};

export default CreateTopic;
