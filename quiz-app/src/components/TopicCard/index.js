import React from "react";

import PropTypes from "prop-types";
import GLOBALS from "app-globals";

import { ButtonLink, Card, Icon, Text } from "components";
import { textTypes } from "components/constants";

import styles from "./styles.module.scss";

const TopicCard = ({ name, link }) => {
  return (
    <Card className={styles.TopicCard}>
      <Icon className={styles.TopicCard_icon} icon="menu_book" />
      <Text
        className={styles.TopicCard_name}
        colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["50"]}
        type={textTypes.HEADING.XS}
      >
        {name}
      </Text>

      <ButtonLink className={styles.TopicCard_button} to={link}>
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

TopicCard.propTypes = {
  name: PropTypes.string.isRequired,
  link: PropTypes.string.isRequired,
};

export default TopicCard;
