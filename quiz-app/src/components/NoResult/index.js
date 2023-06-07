import React from "react";

import PropTypes from "prop-types";
import cn from "classnames";

import GLOBALS from "app-globals";

import Text from "components/Text";
import { textTypes } from "components/constants";
import noResult from "../../static/images/no-result.png";

import styles from "./styles.module.scss";

const NoResult = ({ className, title, message }) => {
  return (
    <div className={cn(styles.NoResult, className)}>
      <div className={styles.NoResult_title}>
        <img
          src={noResult}
          alt="No Result"
          width={200}
          className={styles.NoResult_title_img}
        />
        <Text
          colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["300"]}
          type={textTypes.HEADING.XXL}
          className={styles.NoResult_title_text}
        >
          {title}
        </Text>
      </div>

      <Text
        colorClass={GLOBALS.COLOR_CLASSES.RED["200"]}
        type={textTypes.HEADING.XXL}
      >
        {message}
      </Text>
    </div>
  );
};

NoResult.propTypes = {
  className: PropTypes.string,
  title: PropTypes.string,
  message: PropTypes.string,
};

export default NoResult;
