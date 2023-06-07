import React, { useState } from "react";
import { useHistory, useParams } from "react-router-dom";

import GLOBALS from "app-globals";

import {
  ActionsDropdown,
  ConfirmModal,
  Container,
  Icon,
  NoResult,
  QuestionCard,
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

import { useTopic } from "hooks";
import { TopicsService } from "services";

import styles from "./styles.module.scss";
import { ADMIN_ROUTES } from "screen-wrappers/Admin/constants";

const TopicInfo = () => {
  const { topicId } = useParams();
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
        history.push(`/admin/topics/${topicId}/edit`);
      },
    },
  ].filter((action) => action !== null);

  const { topic, isLoading: isTopicLoading } = useTopic(topicId);

  if (isTopicLoading) {
    return <ScreenLoader />;
  }

  return (
    <>
      <Section id={topicId}>
        <Container className={styles.TopicInfo}>
          <div className={styles.TopicInfo_header}>
            <Icon icon="menu_book" className={styles.TopicInfo_icon} />

            <Text
              className={styles.TopicInfo_heading}
              colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
              type={textTypes.HEADING.XL}
            >
              {topic.name}
            </Text>

            <ActionsDropdown
              actions={actions}
              className={styles.TopicInfo_elipsis}
              iconButtonType={iconButtonTypes.OUTLINE.LG}
            />
          </div>
          <Container className={styles.TopicInfo_bottom}>
            <Text
              className={styles.TopicInfo_bottom_heading}
              type={textTypes.HEADING.MD}
              colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["50"]}
            >
              Questions
            </Text>

            <div className={styles.TopicInfo_bottom_questions}>
              {topic.questions.length > 0 && topic.questions[0].id ? (
                topic.questions.map((question) => (
                  <QuestionCard
                    key={question.id}
                    question={question.question}
                    answer={question.correctAnswer}
                    link={`/admin/questions/${question.id}/info`}
                  />
                ))
              ) : (
                <NoResult
                  title="NO QUESTIONS"
                  message="There are no questions on this topic"
                />
              )}
            </div>
          </Container>
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

              // Call API to delete the topic
              await TopicsService.delete(topicId);

              setIsDeletingSection(false);

              toggleIsDeletionConfirmation(false);
              history.push(ADMIN_ROUTES.TOPICS);
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
        body="Are you sure to delete this Topic?"
      />
    </>
  );
};

export default TopicInfo;
