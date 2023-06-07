import React from "react";

import PropTypes from "prop-types";

import GLOBALS from "app-globals";

import { ButtonLink, Card, Icon, Text } from "components";
import { textTypes } from "components/constants";

import styles from "./styles.module.scss";

const QuestionCard = ({ question, answer, link }) => {
  return (
    <Card className={styles.QuestionCard}>
      <Icon className={styles.QuestionCard_icon} icon="help" />
      <div className={styles.QuestionCard_text}>
        <Text
          className={styles.QuestionCard_question}
          colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["50"]}
          type={textTypes.HEADING.XXS}
        >
          {question}
        </Text>
        <Text
          className={styles.QuestionCard_answer}
          colorClass={GLOBALS.COLOR_CLASSES.BLUE["200"]}
          type={textTypes.HEADING.SM}
        >
          Answer: {answer}
        </Text>
      </div>

      <ButtonLink className={styles.QuestionCard_button} to={link}>
        <Text
          colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["900"]}
          type={textTypes.HEADING.XXS}
        >
          Open
        </Text>
      </ButtonLink>
    </Card>
  );
};

QuestionCard.propTypes = {
  question: PropTypes.string.isRequired,
  answer: PropTypes.string.isRequired,
  link: PropTypes.string.isRequired,
};

export default QuestionCard;
