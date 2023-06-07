import React, { useState, useContext } from "react";
import { useHistory, useParams } from "react-router-dom";

import { Formik } from "formik";
import isEmpty from "lodash/isEmpty";

import GLOBALS from "app-globals";
import {
  Container,
  Section,
  Text,
  ScreenLoader,
  IconButton,
  Icon,
  Button,
  Spinner,
  ControlledInput,
  ConfirmModal,
} from "components";
import { iconButtonTypes, spinnerSizes, textTypes } from "components/constants";
import { useQuiz } from "hooks";
import { TakerContext } from "context";

import { useAnswerQuiz, useCookies, useRecordAnswer } from "hooks";

import styles from "./styles.module.scss";

const TakeQuiz = () => {
  const { quizId } = useParams();
  const history = useHistory();
  const { taker } = useContext(TakerContext);
  const { getCookie, setCookie, removeCookie } = useCookies();

  const { isLoading: isQuizLoading, quiz } = useQuiz(quizId);
  const { finishQuiz, updateRetake } = useAnswerQuiz();
  const { isRecording: isSubmitting, recordAnswer } = useRecordAnswer();

  const [isSuccessModalOpen, setIsSuccessModalOpen] = useState(false);
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

  if (isQuizLoading) {
    return <ScreenLoader />;
  }

  return (
    <Section className={styles.TakeQuiz}>
      <Container>
        <div className={styles.TakeQuiz_header}>
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
            <div className={styles.TakeQuiz_header_subinfo}>
              <Icon className={styles.TakeQuiz_header_icon} icon="info" />
              <Text colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["200"]}>
                {quiz.description}
              </Text>
            </div>
          </div>
        </div>
        <div className={styles.TakeQuiz_content}>
          <div className={styles.TakeQuiz_content_topic}>
            <Text
              colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["900"]}
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

          <Formik
            initialValues={{
              // Get the answer from the cookies for state persistence
              answer:
                getCookie(
                  `answer-${quizId}-${currentTopic?.id}-${currentQuestion?.id}`
                ) || "",
            }}
            onSubmit={async ({ setErrors }) => {
              const currentFormValues = {
                answer: getCookie(
                  `answer-${quizId}-${currentTopic?.id}-${currentQuestion?.id}`
                ),
              };

              const errors = {};
              if (!isEmpty(errors)) {
                setErrors(errors);
                return;
              }

              // Create the answer object to be recorded
              const currentAnswer = {
                takerId: taker.id,
                quizId: quizId,
                questionId: currentQuestion.id,
                answer: currentFormValues.answer || "",
              };

              // Call the API to record the answer
              const { responseCode: recordAnswerResponseCode } =
                await recordAnswer(currentAnswer);

              const recordAnswerCallBacks = {
                recorded: () => {
                  // If this is the last question in the last topic, finish the quiz
                  if (
                    currentTopicIndex === quiz?.topics.length - 1 &&
                    currentQuestionIndex === currentTopic?.questions.length - 1
                  ) {
                    // Call finish quiz API
                    finishQuiz(taker.id, quizId);

                    // Call update retake status API
                    updateRetake(0, taker.id, quizId);

                    // Remove all the cookies index for this quiz
                    removeCookie(`topic-${quizId}`);
                    removeCookie(`question-${quizId}`);
                    removeCookie(`hasRetaken-${quizId}`);

                    // Iterate through all to remove all the answer cookies
                    for (let i = 0; i < quiz?.topics.length; i++) {
                      for (
                        let j = 0;
                        j < quiz?.topics[i]?.questions.length;
                        j++
                      ) {
                        removeCookie(
                          `answer-${quizId}-${quiz?.topics[i]?.id}-${quiz?.topics[i]?.questions[j]?.id}`
                        );
                      }
                    }

                    setIsSuccessModalOpen(true);
                  }

                  handleNextQuestion();
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

              switch (recordAnswerResponseCode) {
                case 200:
                  recordAnswerCallBacks.recorded();
                  break;
                case 400:
                  recordAnswerCallBacks.invalidFields();
                  break;
                case 500:
                  recordAnswerCallBacks.internalError();
                  break;
                default:
                  break;
              }
            }}
          >
            {({ errors, handleSubmit, setFieldValue }) => (
              <form onSubmit={handleSubmit}>
                <div className={styles.TakeQuiz_content_question}>
                  <Text
                    className={styles.TakeQuiz_content_question_info}
                    type={textTypes.HEADING.XS}
                  >
                    {currentQuestionIndex + 1}. {currentQuestion.question}
                  </Text>
                  <ControlledInput
                    className={styles.TakeQuiz_content_question_input}
                    placeholder="Enter your answer..."
                    name={`answer-${currentTopic.id}-${currentQuestion.id}`}
                    value={getCookie(
                      `answer-${quizId}-${currentTopic?.id}-${currentQuestion?.id}`
                    )}
                    error={errors.answer}
                    onChange={(e) => {
                      setFieldValue(
                        `answer-${currentTopic.id}-${currentQuestion.id}`,
                        e.target.value
                      );

                      const answerKey = `answer-${quizId}-${currentTopic.id}-${currentQuestion.id}`;
                      setCookie(answerKey, e.target.value, { path: "/" });
                    }}
                  />
                  {errors.overall && (
                    <Text
                      className={styles.TakeQuiz_content_error}
                      colorClass={GLOBALS.COLOR_CLASSES.RED["200"]}
                    >
                      {errors.overall}
                    </Text>
                  )}
                </div>

                <div className={styles.TakeQuiz_buttons}>
                  <Button
                    className={styles.TakeQuiz_buttons_button}
                    disabled={
                      (currentTopicIndex === 0 && currentQuestionIndex === 0) ||
                      isSubmitting
                    }
                    onClick={handlePrevQuestion}
                  >
                    <Text type={textTypes.HEADING.XXS}>Back</Text>
                  </Button>
                  <Button
                    className={styles.TakeQuiz_buttons_button}
                    disabled={isSubmitting}
                    kind={GLOBALS.BUTTON_KINDS.SUBMIT}
                  >
                    <Text type={textTypes.HEADING.XXS}>
                      {currentTopicIndex === quiz?.topics.length - 1 &&
                      currentQuestionIndex ===
                        currentTopic?.questions.length - 1
                        ? "Submit"
                        : currentQuestionIndex ===
                          currentTopic?.questions.length - 1
                        ? "Proceed to Next Topic"
                        : "Next"}
                    </Text>
                    {isSubmitting && (
                      <Spinner
                        size={spinnerSizes.XS}
                        colorName={GLOBALS.COLOR_NAMES.BLACK}
                        className={styles.TakeQuiz_buttons_spinner}
                      />
                    )}
                  </Button>
                </div>
              </form>
            )}
          </Formik>
          {isSuccessModalOpen && (
            <ConfirmModal
              isOpen={isSuccessModalOpen}
              handleClose={() => setIsSuccessModalOpen(false)}
              title="Quiz Completed!"
              body="You have successfully completed the quiz."
              link="/taker/home"
            />
          )}
        </div>
      </Container>
    </Section>
  );
};

export default TakeQuiz;
