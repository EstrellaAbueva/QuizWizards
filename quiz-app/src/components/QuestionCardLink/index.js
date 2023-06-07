import React from "react";

import PropTypes from "prop-types";
import GLOBALS from "app-globals";

import { CardLink, Icon, Text } from "components";
import { textTypes } from "components/constants";

import styles from "./styles.module.scss";

const QuestionCardLink = ({ question, link }) => {
  return (
    <CardLink className={styles.QuestionCardLink} to={link}>
      <div className={styles.QuestionCardLink_title}>
        <Icon className={styles.QuestionCardLink_icon} icon="help" />
        <Text
          className={styles.QuestionCardLink_name}
          colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["50"]}
          type={textTypes.HEADING.XXXS}
        >
          {question}
        </Text>
      </div>
    </CardLink>
  );
};

QuestionCardLink.propTypes = {
  question: PropTypes.string.isRequired,
  link: PropTypes.string.isRequired,
};

export default QuestionCardLink;
