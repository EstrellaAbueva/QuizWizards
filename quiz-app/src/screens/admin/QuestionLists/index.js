import React from "react";
import GLOBALS from "app-globals";

import {
  ControlledInput,
  Container,
  QuestionCardLink,
  Section,
  Text,
  ScreenLoader,
  NoResult,
} from "components";
import { textTypes } from "components/constants";
import { useQuestions, useSemiPersistentState } from "hooks";

import styles from "./styles.module.scss";

const QuestionLists = () => {
  const [searchQuestion, setsearchQuestion] = useSemiPersistentState(
    "searchQuestion",
    ""
  );

  const { questions, isLoading: isQuestionsLoading } = useQuestions();

  const filteredQuestions = questions.filter((question) => {
    return question.question
      .toLowerCase()
      .includes(searchQuestion.toLowerCase());
  });

  if (isQuestionsLoading) {
    return <ScreenLoader />;
  }
  return (
    <Section id="questionLists">
      <Container className={styles.QuestionLists}>
        <div className={styles.QuestionLists_header}>
          <Text
            colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
            type={textTypes.HEADING.XL}
          >
            Questions
          </Text>

          <ControlledInput
            className={styles.QuestionLists_search}
            placeholder="Search a question..."
            name="question"
            icon="search"
            value={searchQuestion}
            onChange={(e) => setsearchQuestion(e.target.value)}
          />
        </div>

        <div className={styles.QuestionLists_questions}>
          {filteredQuestions.map((question) => (
            <QuestionCardLink
              key={question.id}
              link={`/admin/questions/${question.id}/info`}
              question={question.question}
            />
          ))}
        </div>
        {!filteredQuestions.length && (
          <NoResult title="NO QUESTIONS" message="No questions found" />
        )}
      </Container>
    </Section>
  );
};

export default QuestionLists;
