import React from "react";

import PropTypes from "prop-types";
import GLOBALS from "app-globals";

import { CardLink, Icon, Text } from "components";
import { textTypes } from "components/constants";

import styles from "./styles.module.scss";

const QuizCardLink = ({ name, link }) => {
  return (
    <CardLink className={styles.QuizCardLink} to={link}>
      <div className={styles.QuizCardLink_title}>
        <Icon className={styles.QuizCardLink_icon} icon="school" />
        <Text
          className={styles.QuizCardLink_name}
          colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["50"]}
          type={textTypes.HEADING.XS}
        >
          {name}
        </Text>
      </div>
    </CardLink>
  );
};

QuizCardLink.propTypes = {
  name: PropTypes.string.isRequired,
  link: PropTypes.string.isRequired,
};

export default QuizCardLink;
