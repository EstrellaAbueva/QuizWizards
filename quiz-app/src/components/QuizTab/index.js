import React from "react";
import PropTypes from "prop-types";
import cn from "classnames";

import GLOBALS from "app-globals";
import { Button, Text } from "components";
import { buttonTypes, textTypes } from "components/constants";

import styles from "./styles.module.scss";

const QuizTab = ({ activeTab, setActiveTab }) => (
  <div className={styles.QuizTab}>
    <Button
      type={buttonTypes.TEXT.GRAY}
      onClick={() => setActiveTab(GLOBALS.QUIZ_TABS.TOPICS)}
      className={cn(styles.QuizTab_button, {
        [styles.QuizTab_button___active]:
          activeTab === GLOBALS.QUIZ_TABS.TOPICS,
      })}
    >
      <Text
        colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["50"]}
        type={textTypes.HEADING.SM}
      >
        Topics
      </Text>
    </Button>
    <Button
      type={buttonTypes.TEXT.GRAY}
      onClick={() => setActiveTab(GLOBALS.QUIZ_TABS.LEADERBOARD)}
      className={cn(styles.QuizTab_button, {
        [styles.QuizTab_button___active]:
          activeTab === GLOBALS.QUIZ_TABS.LEADERBOARD,
      })}
    >
      <Text
        colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["50"]}
        type={textTypes.HEADING.SM}
      >
        Leaderboard
      </Text>
    </Button>
  </div>
);

QuizTab.propTypes = {
  activeTab: PropTypes.string.isRequired,
  setActiveTab: PropTypes.func.isRequired,
};

export default QuizTab;
