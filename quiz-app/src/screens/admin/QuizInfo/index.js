import React, { useState } from "react";
import { useHistory, useParams } from "react-router-dom";

import GLOBALS from "app-globals";

import {
  ActionsDropdown,
  ConfirmModal,
  Container,
  Icon,
  LeaderboardCard,
  NoResult,
  QuizTab,
  ScreenLoader,
  Section,
  Text,
  TopicCard,
} from "components";
import {
  actionTypes,
  buttonTypes,
  iconButtonTypes,
  textTypes,
} from "components/constants";
import { useQuiz } from "hooks";
import { QuizzesService } from "services";

import styles from "./styles.module.scss";
import { ADMIN_ROUTES } from "screen-wrappers/Admin/constants";
import { useLeaderboard } from "hooks";

const QuizInfo = () => {
  const { quizId } = useParams();
  const history = useHistory();

  const [isDeletingSection, setIsDeletingSection] = useState(false);
  const [isDeletionConfirmationToggled, toggleIsDeletionConfirmation] =
    useState(false);
  const [activeTab, setActiveTab] = useState(GLOBALS.QUIZ_TABS.TOPICS);

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
        history.push(`/admin/quizzes/${quizId}/edit`);
      },
    },
  ].filter((action) => action !== null);

  const { quiz, isLoading: isQuizLoading } = useQuiz(quizId);

  const { leaderboard, isLoading: isLeaderboardLoading } =
    useLeaderboard(quizId);

  if (isQuizLoading || isLeaderboardLoading) {
    return <ScreenLoader />;
  }

  return (
    <>
      <Section id={quizId}>
        <Container className={styles.QuizInfo}>
          <div className={styles.QuizInfo_header}>
            <Icon icon="school" className={styles.QuizInfo_icon} />
            <div className={styles.QuizInfo_details}>
              <Text
                colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["50"]}
                type={textTypes.HEADING.XL}
              >
                {quiz.name}
              </Text>
              <div className={styles.QuizInfo_details_subinfo}>
                <Icon className={styles.QuizInfo_details_icon} icon="info" />
                <Text colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["200"]}>
                  {quiz.description}
                </Text>
              </div>
            </div>

            <ActionsDropdown
              actions={actions}
              className={styles.QuizInfo_elipsis}
              iconButtonType={iconButtonTypes.OUTLINE.LG}
            />
          </div>
          <Container className={styles.QuizInfo_bottom}>
            <QuizTab activeTab={activeTab} setActiveTab={setActiveTab} />

            <div className={styles.QuizInfo_bottom_content}>
              {activeTab === GLOBALS.QUIZ_TABS.TOPICS && (
                <>
                  {quiz.topics.length > 0 && quiz.topics[0].id ? (
                    quiz.topics.map((topic) => (
                      <TopicCard
                        key={topic.id}
                        name={topic.name}
                        link={`/admin/topics/${topic.id}/info`}
                      />
                    ))
                  ) : (
                    <NoResult
                      title="NO TOPICS"
                      message="There are no topics on this Quiz"
                    />
                  )}
                </>
              )}
              {activeTab === GLOBALS.QUIZ_TABS.LEADERBOARD && (
                <>
                  {leaderboard.length ? (
                    leaderboard.map((leaderboardItem, index) => (
                      <LeaderboardCard
                        key={leaderboardItem.id}
                        className={styles.QuizInfo_bottom_leaderboard}
                        takerName={leaderboardItem.takerName}
                        rank={index + 1}
                        isAdmin
                      />
                    ))
                  ) : (
                    <NoResult
                      title="NO TAKERS"
                      message="There are no takers on this quiz yet"
                    />
                  )}
                </>
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

              // Call API to delete the quiz
              await QuizzesService.delete(quizId);

              setIsDeletingSection(false);

              toggleIsDeletionConfirmation(false);
              history.push(ADMIN_ROUTES.QUIZZES);
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
        body="Are you sure to delete this Quiz?"
      />
    </>
  );
};

export default QuizInfo;
