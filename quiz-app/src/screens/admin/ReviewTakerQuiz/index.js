import React, { useState } from "react";
import { useHistory, useParams } from "react-router-dom";

import GLOBALS from "app-globals";
import {
  Container,
  Section,
  Text,
  ScreenLoader,
  IconButton,
  Icon,
  Button,
  ControlledInput,
} from "components";
import { iconButtonTypes, textTypes } from "components/constants";
import { useCookies, useQuiz, useTakerAnswer } from "hooks";

import styles from "./styles.module.scss";

const ReviewTakerQuiz = () => {
  const { takerId, quizId } = useParams();
  const history = useHistory();
  const { getCookie, setCookie, removeCookie } = useCookies();

  const { isLoading: isQuizLoading, quiz } = useQuiz(quizId);

  const [currentTopicIndex, setCurrentTopicIndex] = useState(
    Number(getCookie(`topic-${quizId}`)) || 0
  );
  const [currentQuestionIndex, setCurrentQuestionIndex] = useState(
    Number(getCookie(`question-${quizId}`)) || 0
  );

  const handleNextTopic = () => {
    if (currentTopicIndex < quiz?.topics.length - 1) {
      setCurrentTopicIndex(currentTopicIndex + 1);
      setCookie(`topic-${quizId}`, currentTopicIndex + 1, { path: "/" });
      setCurrentQuestionIndex(0);
      setCookie(`question-${quizId}`, 0, {
        path: "/",
      });
    }
  };

  const handlePrevTopic = () => {
    if (currentTopicIndex > 0) {
      setCurrentTopicIndex(currentTopicIndex - 1);
      setCookie(`topic-${quizId}`, currentTopicIndex - 1, { path: "/" });
      setCurrentQuestionIndex(0);
      setCookie(`question-${quizId}`, 0, {
        path: "/",
      });
    }
  };

  const handleNextQuestion = () => {
    if (currentQuestionIndex < currentTopic?.questions.length - 1) {
      setCurrentQuestionIndex(currentQuestionIndex + 1);
      setCookie(`question-${quizId}`, currentQuestionIndex + 1, {
        path: "/",
      });
    } else {
      handleNextTopic();
    }
  };

  const handlePrevQuestion = () => {
    if (currentQuestionIndex > 0) {
      setCurrentQuestionIndex(currentQuestionIndex - 1);
      setCookie(`question-${quizId}`, currentQuestionIndex - 1, {
        path: "/",
      });
    } else {
      handlePrevTopic();
    }
  };

  const currentTopic = quiz?.topics[currentTopicIndex];
  const currentQuestion = currentTopic?.questions[currentQuestionIndex];

  const { isLoading: isTakerAnswerLoading, takerAnswer } = useTakerAnswer(
    takerId,
    quizId,
    currentQuestion?.id ? currentQuestion.id : 6
  );

  if (isQuizLoading || isTakerAnswerLoading) {
    return <ScreenLoader />;
  }

  return (
    <Section className={styles.ReviewTakerQuiz}>
      <Container>
        <div className={styles.ReviewTakerQuiz_header}>
          <IconButton
            icon="arrow_back"
            type={iconButtonTypes.OUTLINE.LG}
            onClick={() => {
              history.go(-1);

              // Remove all the cookies index for this quiz
              removeCookie(`topic-${quizId}`);
              removeCookie(`question-${quizId}`);
            }}
          />
          <div>
            <Text
              colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
              type={textTypes.HEADING.XL}
            >
              {quiz.name}
            </Text>
            <div className={styles.ReviewTakerQuiz_header_subinfo}>
              <Icon
                className={styles.ReviewTakerQuiz_header_icon}
                icon="info"
              />
              <Text colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["200"]}>
                {quiz.description}
              </Text>
            </div>
          </div>
        </div>
        <div className={styles.ReviewTakerQuiz_content}>
          <div className={styles.ReviewTakerQuiz_content_topic}>
            <Text
              colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
              type={textTypes.HEADING.MD}
            >
              Topic:
            </Text>
            <Text
              colorClass={GLOBALS.COLOR_CLASSES.BLUE["200"]}
              type={textTypes.HEADING.MD}
            >
              {currentTopic.name}
            </Text>
          </div>

          <div className={styles.ReviewTakerQuiz_content_question}>
            <Text
              colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
              className={styles.ReviewTakerQuiz_content_question_info}
              type={textTypes.HEADING.XS}
            >
              {currentQuestionIndex + 1}. {currentQuestion.question}
            </Text>
            <ControlledInput
              disabled
              className={styles.ReviewTakerQuiz_content_question_input}
              placeholder="Enter your answer..."
              name={`answer-${currentTopic.id}-${currentQuestion.id}`}
              value={takerAnswer.answer || ""}
              // eslint-disable-next-line @typescript-eslint/no-empty-function
              onChange={() => {}}
            />

            <Text
              className={styles.ReviewTakerQuiz_content_error}
              colorClass={
                takerAnswer.answer === currentQuestion.correctAnswer
                  ? GLOBALS.COLOR_CLASSES.GREEN["200"]
                  : GLOBALS.COLOR_CLASSES.RED["200"]
              }
            >
              {takerAnswer?.answer === currentQuestion?.correctAnswer
                ? "Correct answer"
                : "Incorrect answer"}
            </Text>
          </div>

          <div className={styles.ReviewTakerQuiz_buttons}>
            <Button
              className={styles.ReviewTakerQuiz_buttons_button}
              disabled={currentTopicIndex === 0 && currentQuestionIndex === 0}
              onClick={handlePrevQuestion}
            >
              <Text type={textTypes.HEADING.XXS}>Back</Text>
            </Button>
            <Button
              className={styles.ReviewTakerQuiz_buttons_button}
              disabled={
                currentTopicIndex === quiz?.topics.length - 1 &&
                currentQuestionIndex === currentTopic?.questions.length - 1
              }
              kind={GLOBALS.BUTTON_KINDS.SUBMIT}
              onClick={handleNextQuestion}
            >
              <Text type={textTypes.HEADING.XXS}>Next</Text>
            </Button>
          </div>
        </div>
      </Container>
    </Section>
  );
};

export default ReviewTakerQuiz;
