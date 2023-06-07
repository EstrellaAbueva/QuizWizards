import React, { useContext } from "react";

import GLOBALS from "app-globals";

import { TakerContext } from "context";

import { Container, NoResult, QuizCard, Section, Text } from "components";
import { textTypes } from "components/constants";

import styles from "./styles.module.scss";

const Home = () => {
  const { taker } = useContext(TakerContext);

  return (
    <Section id="home" className={styles.Home}>
      <Container>
        <div className={styles.Home_header}>
          <Text
            colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
            type={textTypes.HEADING.XL}
          >
            Assigned Quizzes
          </Text>
        </div>

        {taker.quizzes.length > 0 ? (
          <>
            <div className={styles.Home_quizzes}>
              {taker.quizzes.map((quiz) => (
                <QuizCard
                  key={quiz.id}
                  quizId={quiz.id}
                  takerId={taker.id}
                  takerName={taker.name}
                  name={quiz.name}
                  description={quiz.description}
                  maxScore={quiz.maxScore}
                />
              ))}
            </div>
          </>
        ) : (
          <NoResult title="NO QUIZZES" message=" No quizzes assigned yet" />
        )}
      </Container>
    </Section>
  );
};

export default Home;
