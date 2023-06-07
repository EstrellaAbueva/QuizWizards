import React, { useEffect, useState } from "react";
import { useHistory, useParams } from "react-router-dom";

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

import { useUpdateTopic, useTopic, useQuestions } from "hooks";

import styles from "./styles.module.scss";

const EditTopic = () => {
  const { topicId } = useParams();
  const history = useHistory();

  const [isSuccessModalOpen, setIsSuccessModalOpen] = useState(false);

  const { isUpdating, updateTopic } = useUpdateTopic();

  const validate = (values) => {
    const errors = {};

    if (!values.name) {
      errors.name = "This field is required.";
    }

    if (isEmpty(addedQuestions)) {
      errors.topicIds = "Add topics to the quiz.";
    }

    return errors;
  };

  const { topic, isLoading: isTopicLoading } = useTopic(topicId);
  const { questions: allQuestions, isLoading: isQuestionsLoading } =
    useQuestions();

  const [addedQuestions, setAddedQuestions] = useState([]);

  useEffect(() => {
    setAddedQuestions(
      topic?.questions
        .map(({ id }) =>
          allQuestions.find(({ id: questionId }) => questionId === id)
        )
        .map((question) => ({
          value: question?.id,
          label: question?.question,
          correctcorrectAnswer: question?.correctAnswer,
        }))
    );
  }, [topic?.id, isQuestionsLoading]);

  if (isTopicLoading || isQuestionsLoading) {
    return <ScreenLoader />;
  }

  return (
    <Section>
      <Container className={styles.EditTopic}>
        <div className={styles.EditTopic_header}>
          <IconButton
            icon="arrow_back"
            type={iconButtonTypes.OUTLINE.LG}
            onClick={() => history.go(-1)}
          />
          <Text
            colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
            type={textTypes.HEADING.XL}
          >
            Edit Topic
          </Text>
        </div>

        <Formik
          initialValues={{
            name: topic.name,
            questionId: topic.questions
              .map(({ id }) =>
                allQuestions.find(({ id: questionId }) => questionId === id)
              )
              .map((question) => ({
                value: question?.id,
                label: question?.question,
                correctAnswer: question?.correctAnswer,
              })),
          }}
          onSubmit={async (values, { setErrors }) => {
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

            // Call the API to update the topic
            const { responseCode: updateTopicResponseCode } = await updateTopic(
              topicId,
              currentFormValues
            );

            const updateTopicCallBacks = {
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

            switch (updateTopicResponseCode) {
              case 200:
                updateTopicCallBacks.updated();
                break;
              case 400:
                updateTopicCallBacks.invalidFields();
                break;
              case 500:
                updateTopicCallBacks.internalError();
                break;
              default:
                break;
            }
          }}
        >
          {({ errors, values, handleSubmit, setFieldValue }) => {
            // Filter out questions that are already in topic
            const availableQuestions = allQuestions.filter(
              (question) =>
                !addedQuestions?.some(
                  (addedQuestion) => addedQuestion.value === question.id
                )
            );

            // Gets the answer of the question
            const getAnswerByQuestionId = (questionId) => {
              const question = allQuestions.find(
                (question) => question.id === questionId
              );
              return question ? question.correctAnswer : "";
            };

            // Add questions to the list of added questions
            const handleAddedQuestions = () => {
              const newAddedQuestions = values.questionId.filter(
                (question) =>
                  !addedQuestions.some(
                    (addedQuestion) => addedQuestion.value === question.value
                  )
              );

              setAddedQuestions([...addedQuestions, ...newAddedQuestions]);
            };

            // Remove question from list of added questions
            const handleDelete = (questionValue) => {
              const newAddedQuestions = addedQuestions.filter(
                (addedQuestion) => addedQuestion.value !== questionValue
              );
              setAddedQuestions(newAddedQuestions);
            };

            return (
              <form onSubmit={handleSubmit} className={styles.EditTopic_form}>
                <div className={styles.EditTopic_form_input}>
                  <div className={styles.EditTopic_form_info}>
                    <Icon className={styles.EditTopic_form_icon} icon="quiz" />
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
                    onChange={(e) => setFieldValue("name", e.target.value)}
                  />
                </div>

                <div className={styles.EditTopic_form_questions}>
                  <div className={styles.EditTopic_form_info}>
                    <Icon className={styles.EditTopic_form_icon} icon="help" />
                    <Text
                      colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
                      type={textTypes.HEADING.MD}
                    >
                      Questions
                    </Text>
                  </div>
                  <div className={styles.EditTopic_form_questions_add}>
                    <ControlledDropdown
                      isMulti
                      isRequired
                      className={styles.EditTopic_form_questions_dropdown}
                      error={errors.questionId}
                      options={availableQuestions.map((question) => ({
                        value: question.id,
                        label: question.question,
                        correctAnswer: question.correctAnswer,
                      }))}
                      placeholder="Select questions..."
                      type={dropdownTypes.FORM}
                      value={values.questionId}
                      onChange={(e) => setFieldValue("questionId", e)}
                    />
                    <IconButton icon="add" onClick={handleAddedQuestions} />
                  </div>

                  {!isEmpty(addedQuestions) && (
                    <div className={styles.EditTopic_addedQuestions}>
                      {addedQuestions.map((question) => (
                        <div
                          className={styles.EditTopic_addedQuestions_question}
                          key={question.value}
                        >
                          <div
                            className={
                              styles.EditTopic_addedQuestions_question_name
                            }
                          >
                            <Icon
                              icon="help"
                              className={
                                styles.EditTopic_addedQuestions_question_icon
                              }
                            />
                            <div
                              className={
                                styles.EditTopic_addedQuestions_questionAnswer
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
                    className={styles.EditTopic_form_error}
                    colorClass={GLOBALS.COLOR_CLASSES.RED["200"]}
                  >
                    {errors.overall}
                  </Text>
                )}

                <div className={styles.EditTopic_form_buttonGroup}>
                  <Button
                    kind={GLOBALS.BUTTON_KINDS.SUBMIT}
                    className={styles.EditTopic_form_buttonGroup_button}
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
                          className={styles.EditTopic_form_buttonGroup_spinner}
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
            link={`/admin/topics/${topicId}/info`}
            title="Topic Updated!"
            body="Your topic has been updated successfully."
          />
        )}
      </Container>
    </Section>
  );
};

export default EditTopic;
