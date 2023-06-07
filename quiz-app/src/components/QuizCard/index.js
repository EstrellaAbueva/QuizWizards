import React, { useState } from "react";

import { useHistory } from "react-router-dom";
import PropTypes from "prop-types";
import cn from "classnames";

import GLOBALS from "app-globals";

import {
  Button,
  ButtonLink,
  Card,
  ConfirmModal,
  Icon,
  IconButton,
  LeaderboardModal,
  Spinner,
  Text,
} from "components";
import {
  buttonTypes,
  iconButtonTypes,
  spinnerSizes,
  textTypes,
} from "components/constants";
import { useLeaderboard, useQuiz, useTakerQuizScore } from "hooks";
import { useAnswerQuiz, useCookies } from "hooks";

import styles from "./styles.module.scss";
import Info from "components/Info";
import Preloader from "components/Preloader";

const QuizCard = ({
  name,
  description,
  isAdmin,
  takerId,
  quizId,
  takerName,
}) => {
  const history = useHistory();
  const { getCookie, setCookie } = useCookies();

  const [isLeaderboardModalOpen, setIsLeaderboardModalOpen] = useState(false);
  const [isSuccessModalOpen, setIsSuccessModalOpen] = useState(false);
  const [isAllowedRetakeLoading, setIsAllowedRetakeLoading] = useState(false);
  const [isRetakeButtonDisabled, setIsRetakeButtonDisabled] = useState(false);

  const [hasRetaken] = useState(getCookie(`hasRetaken-${quizId}`) || false);

  const { startQuiz, updateRetake } = useAnswerQuiz();

  const { leaderboard, isLoading: isLeaderboardLoading } = quizId
    ? useLeaderboard(quizId)
    : { leaderboard: [], isLoading: false };

  const { quizScore, isLoading: isQuizScoreLoading } =
    takerId && quizId
      ? useTakerQuizScore(takerId, quizId)
      : { quizScore: null, isLoading: false };

  const { isLoading: isQuizLoading, quiz } = useQuiz(quizId);

  const handleStartQuiz = () => {
    // Call the api to start the quiz again and redirect to the quiz page
    startQuiz(takerId, quizId);
    history.push(`/taker/take-quiz/${quizId}`);
  };

  if (isLeaderboardLoading || isQuizScoreLoading || isQuizLoading) {
    return <Preloader isAdmin={isAdmin} />;
  }

  return (
    <>
      <Card
        className={cn(styles.QuizCard, {
          [styles.QuizCard___neutral]: isAdmin,
        })}
      >
        <Icon
          className={cn(styles.QuizCard_icon, {
            [styles.QuizCard_icon___neutral]: isAdmin,
          })}
          icon="school"
        />
        <Text
          className={styles.QuizCard_name}
          colorClass={
            isAdmin
              ? GLOBALS.COLOR_CLASSES.NEUTRAL["50"]
              : GLOBALS.COLOR_CLASSES.NEUTRAL["900"]
          }
          type={textTypes.HEADING.MD}
        >
          {name}
        </Text>
        <Text
          colorClass={
            isAdmin
              ? GLOBALS.COLOR_CLASSES.NEUTRAL["50"]
              : GLOBALS.COLOR_CLASSES.NEUTRAL["900"]
          }
          className={styles.QuizCard_description}
        >
          {description}
        </Text>

        {/* Shows only when the taker is done answering the quiz */}
        {!isAdmin && quizScore?.finishedDate && quizScore.canRetake === 0 && (
          <IconButton
            className={styles.QuizCard_leaderboard}
            icon="leaderboard"
            type={iconButtonTypes.OUTLINE.LG}
            onClick={() => setIsLeaderboardModalOpen(true)}
          />
        )}

        <div className={styles.QuizCard_score}>
          <Text
            colorClass={GLOBALS.COLOR_CLASSES.YELLOW["500"]}
            type={textTypes.HEADING.MD}
          >
            {quizScore.finishedDate && quizScore.canRetake === 0
              ? quizScore.score
              : "-"}
          </Text>
          <Text
            colorClass={
              isAdmin
                ? GLOBALS.COLOR_CLASSES.NEUTRAL["50"]
                : GLOBALS.COLOR_CLASSES.NEUTRAL["900"]
            }
            type={textTypes.HEADING.MD}
          >
            /{quiz.maxScore || "-"}
          </Text>
        </div>

        {!isAdmin ? (
          !quizScore?.finishedDate ? (
            <Button
              type={
                quizScore.takenDate
                  ? buttonTypes.PRIMARY.BLUE
                  : buttonTypes.PRIMARY.YELLOW
              }
              className={styles.QuizCard_button}
              onClick={handleStartQuiz}
            >
              <Text
                colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["900"]}
                type={textTypes.HEADING.XXS}
              >
                {!quizScore.takenDate ? "Start Quiz" : "Continue Quiz"}
              </Text>
            </Button>
          ) : (
            <>
              <Button
                className={styles.QuizCard_button}
                disabled={quizScore.canRetake === 0}
                type={
                  hasRetaken
                    ? buttonTypes.PRIMARY.BLUE
                    : buttonTypes.PRIMARY.YELLOW
                }
                onClick={() => {
                  handleStartQuiz();
                  setCookie(`hasRetaken-${quizId}`, true, { path: "/" });
                }}
              >
                <Text
                  colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["900"]}
                  type={textTypes.HEADING.XXS}
                >
                  {hasRetaken ? "Continue Quiz" : "Retake Quiz"}
                </Text>
              </Button>
              {quizScore.canRetake === 0 && (
                <Info
                  tooltip="Request admin to retake"
                  className={styles.QuizCard_tooltip}
                />
              )}
            </>
          )
        ) : (
          <>
            {/* Disable if taker is not yet finished or still retaking the quiz */}
            <div className={styles.QuizCard_buttons}>
              <ButtonLink
                to={`/admin/takers/${takerId}/quizzes/${quizId}/review`}
                disabled={
                  isRetakeButtonDisabled ||
                  quizScore.canRetake === 1 ||
                  !quizScore.finishedDate
                }
                type={buttonTypes.TEXT.YELLOW}
                className={styles.QuizCard_buttons_link}
              >
                <Text
                  colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["900"]}
                  type={textTypes.HEADING.XXS}
                >
                  Review Answer
                </Text>
              </ButtonLink>
              <Button
                disabled={
                  isRetakeButtonDisabled ||
                  quizScore.canRetake === 1 ||
                  !quizScore.finishedDate
                }
                className={styles.QuizCard_button}
                onClick={() => {
                  setIsRetakeButtonDisabled(true);
                  setIsAllowedRetakeLoading(true);

                  // Call the api to update the retake of the taker
                  updateRetake(1, takerId, quizId);

                  setIsAllowedRetakeLoading(false);
                  setIsSuccessModalOpen(true);
                }}
              >
                <Text
                  colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["900"]}
                  type={textTypes.HEADING.XXS}
                >
                  Allow Retake
                </Text>
                {isAllowedRetakeLoading && (
                  <Spinner
                    size={spinnerSizes.XS}
                    colorName={GLOBALS.COLOR_NAMES.BLACK}
                    className={styles.QuizCard_spinner}
                  />
                )}
              </Button>
            </div>

            <Info
              tooltip={
                quizScore.canRetake ||
                !quizScore.finishedDate ||
                isRetakeButtonDisabled
                  ? "Taker is not yet finished"
                  : "You can allow taker to retake the quiz"
              }
              className={styles.QuizCard_tooltip}
            />
          </>
        )}
      </Card>

      {isLeaderboardModalOpen && (
        <LeaderboardModal
          isOpen={isLeaderboardModalOpen}
          onClose={() => setIsLeaderboardModalOpen(false)}
          leaderboard={leaderboard}
          takerName={takerName}
        />
      )}

      {isSuccessModalOpen && (
        <ConfirmModal
          isOpen={isSuccessModalOpen}
          handleClose={() => setIsSuccessModalOpen(false)}
          title="Allowed Retake!"
          body="Allowed taker to retake the quiz successfully."
        />
      )}
    </>
  );
};

QuizCard.defaultProps = {
  isAdmin: false,
  quizId: null,
  takerId: null,
  takerName: null,
};

QuizCard.propTypes = {
  isAdmin: PropTypes.bool,
  name: PropTypes.string.isRequired,
  description: PropTypes.string.isRequired,
  quizId: PropTypes.number,
  takerId: PropTypes.number,
  takerName: PropTypes.string,
};

export default QuizCard;
