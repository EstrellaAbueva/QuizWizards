import React from "react";

import PropTypes from "prop-types";
import GLOBALS from "app-globals";

import { CardLink, Icon, Text } from "components";
import { textTypes } from "components/constants";

import styles from "./styles.module.scss";

const TopicCardLink = ({ name, link }) => {
  return (
    <CardLink className={styles.TopicCardLink} to={link}>
      <div className={styles.TopicCardLink_title}>
        <Icon className={styles.TopicCardLink_icon} icon="menu_book" />
        <Text
          className={styles.TopicCardLink_name}
          colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["50"]}
          type={textTypes.HEADING.XS}
        >
          {name}
        </Text>
      </div>
    </CardLink>
  );
};

TopicCardLink.propTypes = {
  name: PropTypes.string.isRequired,
  link: PropTypes.string.isRequired,
};

export default TopicCardLink;
