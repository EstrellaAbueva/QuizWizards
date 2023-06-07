import React from "react";
import GLOBALS from "app-globals";

import {
  Container,
  ControlledInput,
  NoResult,
  QuizCardLink,
  ScreenLoader,
  Section,
  Text,
} from "components";
import { textTypes } from "components/constants";
import { useQuizzes, useSemiPersistentState } from "hooks";

import styles from "./styles.module.scss";

const QuizLists = () => {
  const [searchQuiz, setsearchQuiz] = useSemiPersistentState("searchQuiz", "");
  const { quizzes, isLoading: isQuizzesLoading } = useQuizzes();

  const filteredQuizzes = quizzes.filter((quiz) => {
    return quiz.name.toLowerCase().includes(searchQuiz.toLowerCase());
  });

  if (isQuizzesLoading) {
    return <ScreenLoader />;
  }

  return (
    <Section id="quizlists">
      <Container className={styles.QuizLists}>
        <div className={styles.QuizLists_header}>
          <Text
            colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
            type={textTypes.HEADING.XL}
          >
            Quizzes
          </Text>
          <ControlledInput
            className={styles.QuizLists_search}
            placeholder="Search a quiz..."
            name="quiz"
            icon="search"
            value={searchQuiz}
            onChange={(e) => setsearchQuiz(e.target.value)}
          />
        </div>

        <div className={styles.QuizLists_quizzes}>
          {filteredQuizzes.map((quiz) => (
            <QuizCardLink
              key={quiz.id}
              name={quiz.name}
              description={quiz.description}
              link={`/admin/quizzes/${quiz.id}/info`}
            />
          ))}
        </div>
        {!filteredQuizzes.length && (
          <NoResult title="NO QUIZZES" message="No quizzes found" />
        )}
      </Container>
    </Section>
  );
};

export default QuizLists;
