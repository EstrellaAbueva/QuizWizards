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
  dropdownTypes,
  iconButtonTypes,
  spinnerSizes,
  textAreaTypes,
  textTypes,
} from "components/constants";

import { useQuiz, useUpdateQuiz, useTopics } from "hooks";

import styles from "./styles.module.scss";

const EditQuiz = () => {
  const { quizId } = useParams();
  const history = useHistory();

  const [isSuccessModalOpen, setIsSuccessModalOpen] = useState(false);

  const { isUpdating, updateQuiz } = useUpdateQuiz();

  const validate = (values) => {
    const errors = {};

    if (!values.name) {
      errors.name = "This field is required.";
    }

    if (!values.description) {
      errors.description = "This field is required.";
    }

    if (isEmpty(addedTopics)) {
      errors.topicId = "Add topics to the quiz.";
    }

    return errors;
  };

  const { quiz, isLoading: isQuizLoading } = useQuiz(quizId);
  const { topics: allTopics, isLoading: isTopicsLoading } = useTopics();

  const [addedTopics, setAddedTopics] = useState([]);

  useEffect(() => {
    setAddedTopics(
      quiz?.topics
        .map(({ id }) => allTopics.find(({ id: topicId }) => topicId === id))
        .map((topic) => ({
          value: topic?.id,
          label: topic?.name,
        }))
    );
  }, [quiz?.id, isTopicsLoading]);

  if (isQuizLoading || isTopicsLoading) {
    return <ScreenLoader />;
  }

  return (
    <Section>
      <Container className={styles.EditQuiz}>
        <div className={styles.EditQuiz_header}>
          <IconButton
            icon="arrow_back"
            type={iconButtonTypes.OUTLINE.LG}
            onClick={() => history.go(-1)}
          />
          <Text
            colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
            type={textTypes.HEADING.XL}
          >
            Edit Quiz
          </Text>
        </div>

        <Formik
          initialValues={{
            name: quiz.name,
            description: quiz.description,
            topicId: quiz.topics
              .map(({ id }) =>
                allTopics.find(({ id: topicId }) => topicId === id)
              )
              .map((topic) => ({
                value: topic?.id,
                label: topic?.name,
              })),
          }}
          onSubmit={async (values, { setErrors }) => {
            const currentFormValues = {
              name: values.name,
              description: values.description,
              topicId: addedTopics
                .map((topic) => topic.value)
                .join(",")
                .split(","),
            };

            const errors = validate(values);
            if (!isEmpty(errors)) {
              setErrors(errors);
              return;
            }

            // Call the API to update the quiz
            const { responseCode: updateQuizResponseCode } = await updateQuiz(
              quizId,
              currentFormValues
            );

            const updateQuizCallBacks = {
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

            switch (updateQuizResponseCode) {
              case 200:
                updateQuizCallBacks.updated();
                break;
              case 400:
                updateQuizCallBacks.invalidFields();
                break;
              case 500:
                updateQuizCallBacks.internalError();
                break;
              default:
                break;
            }
          }}
        >
          {({ errors, values, handleSubmit, setFieldValue }) => {
            // Filter out topics that are already added
            const availableTopics = allTopics.filter(
              (topic) =>
                !addedTopics?.some(
                  (addedTopic) => addedTopic.value === topic.id
                )
            );

            // Add topics to the list of added topics
            const handleAddedTopics = () => {
              const newAddedTopics = values.topicId.filter(
                (topic) =>
                  !addedTopics?.some(
                    (addedTopic) => addedTopic.value === topic.value
                  )
              );

              setAddedTopics([...addedTopics, ...newAddedTopics]);
            };

            // Remove topics from the list of added topics
            const handleDelete = (topicValue) => {
              const newAddedTopics = addedTopics.filter(
                (topic) => topic.value !== topicValue
              );
              setAddedTopics(newAddedTopics);
            };

            return (
              <form onSubmit={handleSubmit} className={styles.EditQuiz_form}>
                <div className={styles.EditQuiz_form_input}>
                  <div className={styles.EditQuiz_form_info}>
                    <Icon className={styles.EditQuiz_form_icon} icon="quiz" />
                    <Text
                      colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
                      type={textTypes.HEADING.MD}
                    >
                      Quiz Name
                    </Text>
                  </div>

                  <ControlledInput
                    placeholder="Add quiz name..."
                    name="name"
                    value={values.name}
                    error={errors.name}
                    onChange={(e) => setFieldValue("name", e.target.value)}
                  />
                </div>

                <div className={styles.EditQuiz_form_textArea}>
                  <div className={styles.EditQuiz_form_info}>
                    <Icon
                      className={styles.EditQuiz_form_icon}
                      icon="description"
                    />
                    <Text
                      colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
                      type={textTypes.HEADING.MD}
                    >
                      Quiz Description
                    </Text>
                  </div>

                  <ControlledTextArea
                    isRequired
                    name="description"
                    error={errors.description}
                    placeholder="Add a brief description about the quiz..."
                    type={textAreaTypes.SLIM}
                    value={values.description}
                    onChange={(e) =>
                      setFieldValue("description", e.target.value)
                    }
                  />
                </div>

                <div className={styles.EditQuiz_form_topics}>
                  <div className={styles.EditQuiz_form_info}>
                    <Icon className={styles.EditQuiz_form_icon} icon="topic" />
                    <Text
                      colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
                      type={textTypes.HEADING.MD}
                    >
                      Quiz Topics
                    </Text>
                  </div>
                  <div className={styles.EditQuiz_form_topics_add}>
                    <ControlledDropdown
                      isMulti
                      isRequired
                      className={styles.EditQuiz_form_topics_dropdown}
                      error={errors.topicId}
                      name="topicId"
                      options={availableTopics.map((topic) => ({
                        value: topic.id,
                        label: topic.name,
                      }))}
                      placeholder="Select topics..."
                      type={dropdownTypes.FORM}
                      value={values.topicId}
                      onChange={(e) => setFieldValue("topicId", e)}
                    />
                    <IconButton icon="add" onClick={handleAddedTopics} />
                  </div>

                  {!isEmpty(addedTopics) && (
                    <div className={styles.EditQuiz_addedTopics}>
                      {addedTopics.map((topic) => (
                        <div
                          className={styles.EditQuiz_addedTopics_topic}
                          key={topic.id}
                        >
                          <div
                            className={styles.EditQuiz_addedTopics_topic_name}
                          >
                            <Icon
                              icon="menu_book"
                              className={styles.EditQuiz_addedTopics_topic_icon}
                            />
                            <Text type={textTypes.HEADING.XS}>
                              {topic.label}
                            </Text>
                          </div>

                          <IconButton
                            icon="delete"
                            onClick={() => handleDelete(topic.value)}
                            colorName={GLOBALS.COLOR_NAMES.RED}
                            type={iconButtonTypes.OUTLINE.MD}
                          />
                        </div>
                      ))}
                    </div>
                  )}
                </div>

                {errors.overall && (
                  <Text
                    className={styles.EditQuiz_form_error}
                    colorClass={GLOBALS.COLOR_CLASSES.RED["200"]}
                  >
                    {errors.overall}
                  </Text>
                )}

                <div className={styles.EditQuiz_form_buttonGroup}>
                  <Button
                    kind={GLOBALS.BUTTON_KINDS.SUBMIT}
                    className={styles.EditQuiz_form_buttonGroup_button}
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
                          className={styles.EditQuiz_form_buttonGroup_spinner}
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
            link={`/admin/quizzes/${quizId}/info`}
            title="Quiz Updated!"
            body="Your quiz has been successfully updated."
          />
        )}
      </Container>
    </Section>
  );
};

export default EditQuiz;
