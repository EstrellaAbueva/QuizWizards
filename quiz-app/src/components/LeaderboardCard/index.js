import React from "react";

import PropTypes from "prop-types";
import cn from "classnames";

import GLOBALS from "app-globals";
import { Card, TakerTag, Text } from "components";
import { textTypes } from "components/constants";

import styles from "./styles.module.scss";

// Get the ordinal suffix of a number
const getOrdinalSuffix = (rank) => {
  const lastDigit = rank % 10;

  if (rank % 100 >= 11 && rank % 100 <= 13) {
    return "th";
  }

  switch (lastDigit) {
    case 1:
      return "st";
    case 2:
      return "nd";
    case 3:
      return "rd";
    default:
      return "th";
  }
};

const LeaderboardCard = ({ rank, isAdmin, takerName }) => {
  const ordinalSuffix = getOrdinalSuffix(rank);
  return (
    <>
      <Card
        className={cn(styles.LeaderboardCard, {
          [styles.LeaderboardCard___neutral]: isAdmin,
        })}
      >
        <TakerTag name={takerName} isAdmin={isAdmin} />

        {rank && (
          <Text
            colorClass={(() => {
              if (rank === 1 || rank === 2 || rank === 3) {
                return GLOBALS.COLOR_CLASSES.YELLOW["500"];
              } else {
                return GLOBALS.COLOR_CLASSES.BLUE["200"];
              }
            })()}
            type={textTypes.HEADING.LG}
          >
            {rank + ordinalSuffix}
          </Text>
        )}
      </Card>
    </>
  );
};

LeaderboardCard.defaultProps = {
  rank: null,
  isAdmin: false,
};

LeaderboardCard.propTypes = {
  rank: PropTypes.number,
  isAdmin: PropTypes.bool,
  takerName: PropTypes.string.isRequired,
};

export default LeaderboardCard;
