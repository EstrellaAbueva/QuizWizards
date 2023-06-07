import React, { useState } from "react";
import { useHistory, useParams } from "react-router-dom";

import GLOBALS from "app-globals";

import {
  ActionsDropdown,
  ConfirmModal,
  Container,
  Icon,
  ScreenLoader,
  Section,
  Text,
} from "components";
import {
  actionTypes,
  buttonTypes,
  iconButtonTypes,
  textTypes,
} from "components/constants";

import { useQuestion } from "hooks";

import styles from "./styles.module.scss";
import { QuestionsService } from "services";
import { ADMIN_ROUTES } from "screen-wrappers/Admin/constants";

const QuestionInfo = () => {
  const { questionId } = useParams();
  const history = useHistory();

  const [isDeletingSection, setIsDeletingSection] = useState(false);
  const [isDeletionConfirmationToggled, toggleIsDeletionConfirmation] =
    useState(false);

  const actions = [
    {
      type: actionTypes.BUTTON,
      icon: "delete",
      text: "Delete",
      id: "delete",
      action: () => toggleIsDeletionConfirmation(true),
    },
    {
      type: actionTypes.BUTTON,
      icon: "edit",
      text: "Edit",
      id: "edit",
      action: () => {
        history.push(`/admin/questions/${questionId}/edit`);
      },
    },
  ].filter((action) => action !== null);

  const { question, isLoading: isQuestionsLoading } = useQuestion(questionId);

  if (isQuestionsLoading) {
    return <ScreenLoader />;
  }

  return (
    <>
      <Section id={questionId} className={styles.QuestionInfo}>
        <Container className={styles.QuestionInfo_container}>
          <div className={styles.QuestionInfo_header}>
            <Icon icon="help" className={styles.QuestionInfo_icon} />
            <div className={styles.QuestionInfo_details}>
              <Text
                colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["50"]}
                type={textTypes.HEADING.SM}
              >
                {question.question}
              </Text>

              <Text
                className={styles.QuestionInfo_answer}
                colorClass={GLOBALS.COLOR_CLASSES.BLUE["200"]}
                type={textTypes.HEADING.MD}
              >
                Answer: {question.correctAnswer}
              </Text>
            </div>

            <ActionsDropdown
              actions={actions}
              className={styles.QuestionInfo_elipsis}
              iconButtonType={iconButtonTypes.OUTLINE.LG}
            />
          </div>
        </Container>
      </Section>
      <ConfirmModal
        actions={[
          {
            id: "deleteClassConfirmButton",
            text: "Delete",
            type: buttonTypes.PRIMARY.RED,
            onClick: async () => {
              setIsDeletingSection(true);

              // Call API to delete the question
              await QuestionsService.delete(questionId);

              setIsDeletingSection(false);

              toggleIsDeletionConfirmation(false);
              history.push(ADMIN_ROUTES.QUESTIONS);
            },
            disabled: isDeletingSection,
          },
          {
            id: "deleteClassBackButton",
            text: "Back",
            type: buttonTypes.PRIMARY.YELLOW,
            onClick: () => toggleIsDeletionConfirmation(false),
          },
        ]}
        handleClose={() => {
          toggleIsDeletionConfirmation(false);
        }}
        isOpen={isDeletionConfirmationToggled}
        title="Delete?"
        body="Are you sure to delete this Question?"
      />
    </>
  );
};

export default QuestionInfo;
