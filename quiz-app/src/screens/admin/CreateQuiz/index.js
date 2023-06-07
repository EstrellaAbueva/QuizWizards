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
import {
  useSemiPersistentState,
  useSemiPersistentStateForObjects,
} from "hooks";
import { removeLocalStorageItem } from "utils";
import { useCreateQuiz, useTopics } from "hooks";

import styles from "./styles.module.scss";

const CreateQuiz = () => {
  const history = useHistory();

  const [isSuccessModalOpen, setIsSuccessModalOpen] = useState(false);

  const [quizName, setQuizName] = useSemiPersistentState("quizName", "");
  const [quizDescription, setQuizDescription] = useSemiPersistentState(
    "quizDescription",
    ""
  );
  const [addedTopics, setAddedTopics] = useSemiPersistentStateForObjects(
    "addedTopics",
    []
  );

  const { isCreating, createQuiz } = useCreateQuiz();

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

  const { topics, isLoading: isTopicsLoading } = useTopics();

  if (isTopicsLoading) {
    return <ScreenLoader />;
  }

  return (
    <Section>
      <Container className={styles.CreateQuiz}>
        <div className={styles.CreateQuiz_header}>
          <IconButton
            icon="arrow_back"
            type={iconButtonTypes.OUTLINE.LG}
            onClick={() => history.go(-1)}
          />
          <Text
            colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
            type={textTypes.HEADING.XL}
          >
            Create Quiz
          </Text>
        </div>

        <Formik
          initialValues={{
            name: quizName || "",
            description: quizDescription || "",
            topicId: [],
          }}
          onSubmit={async (values, { setErrors, setFieldValue }) => {
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

            // Call the API to create the quiz
            const { responseCode: createQuizResponseCode } = await createQuiz(
              currentFormValues
            );

            const createQuizCallBacks = {
              created: () => {
                setIsSuccessModalOpen(true);
                // Remove form values from local storage
                removeLocalStorageItem("quizName");
                removeLocalStorageItem("quizDescription");
                removeLocalStorageItem("addedTopics");

                // Clear form values
                setFieldValue("name", "");
                setFieldValue("description", "");
                setFieldValue("topicId", []);
                setAddedTopics([]);
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

            switch (createQuizResponseCode) {
              case 201:
                createQuizCallBacks.created();
                break;
              case 400:
                createQuizCallBacks.invalidFields();
                break;
              case 500:
                createQuizCallBacks.internalError();
                break;
              default:
                break;
            }
          }}
        >
          {({ errors, values, handleSubmit, setFieldValue }) => {
            // Filter out topics that are already added
            const availableTopics = topics.filter(
              (topic) =>
                !addedTopics.some((addedTopic) => addedTopic.value === topic.id)
            );

            // Convert topic values to dropdown options
            const topicValues = values.topicId
              .map((topicId) => topics.find((topic) => topic?.id === topicId))
              .map((topic) => ({
                value: topic?.id,
                label: topic?.name,
              }));

            // Update topic values on change
            const onTopicChange = (newTopicValues) => {
              setFieldValue(
                "topicId",
                newTopicValues?.map((option) => option.value) || []
              );
            };

            // Add topics to the list of added topics
            const handleAddedTopics = () => {
              const newAddedTopics = topicValues.filter(
                (topic) =>
                  !addedTopics.some(
                    (addedTopic) => addedTopic.value === topic.value
                  )
              );
              setAddedTopics(addedTopics.concat(newAddedTopics));
            };

            // Remove topics from the list of added topics
            const handleDelete = (topicValue) => {
              const newAddedTopics = addedTopics.filter(
                (topic) => topic.value !== topicValue
              );
              setAddedTopics(newAddedTopics);
            };

            return (
              <form onSubmit={handleSubmit} className={styles.CreateQuiz_form}>
                <div className={styles.CreateQuiz_form_input}>
                  <div className={styles.CreateQuiz_form_info}>
                    <Icon className={styles.CreateQuiz_form_icon} icon="quiz" />
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
                    onChange={(e) => {
                      setFieldValue("name", e.target.value);
                      setQuizName(e.target.value);
                    }}
                  />
                </div>

                <div className={styles.CreateQuiz_form_textArea}>
                  <div className={styles.CreateQuiz_form_info}>
                    <Icon
                      className={styles.CreateQuiz_form_icon}
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
                    onChange={(e) => {
                      setFieldValue("description", e.target.value);
                      setQuizDescription(e.target.value);
                    }}
                  />
                </div>

                <div className={styles.CreateQuiz_form_topics}>
                  <div className={styles.CreateQuiz_form_info}>
                    <Icon
                      className={styles.CreateQuiz_form_icon}
                      icon="topic"
                    />
                    <Text
                      colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
                      type={textTypes.HEADING.MD}
                    >
                      Quiz Topics
                    </Text>
                  </div>
                  <div className={styles.CreateQuiz_form_topics_add}>
                    <ControlledDropdown
                      isMulti
                      isRequired
                      className={styles.CreateQuiz_form_topics_dropdown}
                      error={errors.topicId}
                      name="topicId"
                      options={availableTopics.map((topic) => ({
                        value: topic.id,
                        label: topic.name,
                      }))}
                      placeholder="Select topics..."
                      type={dropdownTypes.FORM}
                      value={topicValues}
                      onChange={onTopicChange}
                    />
                    <IconButton
                      key={addedTopics}
                      icon="add"
                      onClick={handleAddedTopics}
                    />
                  </div>

                  {!isEmpty(addedTopics) && (
                    <div className={styles.CreateQuiz_addedTopics}>
                      {addedTopics.map((topic) => (
                        <div
                          className={styles.CreateQuiz_addedTopics_topic}
                          key={topic.id}
                        >
                          <div
                            className={styles.CreateQuiz_addedTopics_topic_name}
                          >
                            <Icon
                              icon="menu_book"
                              className={
                                styles.CreateQuiz_addedTopics_topic_icon
                              }
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
                    className={styles.CreateQuiz_form_error}
                    colorClass={GLOBALS.COLOR_CLASSES.RED["200"]}
                  >
                    {errors.overall}
                  </Text>
                )}

                <div className={styles.CreateQuiz_form_buttonGroup}>
                  <Button
                    kind={GLOBALS.BUTTON_KINDS.SUBMIT}
                    className={styles.CreateQuiz_form_buttonGroup_button}
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
                          className={styles.CreateQuiz_form_buttonGroup_spinner}
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
            link="/admin/quizzes"
            title="Quiz Created!"
            body="Your quiz has been successfully created."
          />
        )}
      </Container>
    </Section>
  );
};

export default CreateQuiz;
