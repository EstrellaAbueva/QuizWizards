import React from "react";

import PropTypes from "prop-types";
import { Icon, Modal, LeaderboardCard, Text } from "components";
import { textTypes } from "components/constants";

import styles from "./styles.module.scss";
import GLOBALS from "app-globals";

const LeaderboardModal = ({ isOpen, onClose, leaderboard, takerName }) => {
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

  const currentTakerRank =
    leaderboard.findIndex((item) => item.takerName === takerName) + 1;

  const ordinalSuffix = getOrdinalSuffix(currentTakerRank);

  return (
    <Modal
      className={styles.LeaderboardModal}
      isOpen={isOpen}
      handleClose={onClose}
    >
      <div className={styles.LeaderboardModal_header}>
        <Text type={textTypes.HEADING.LG}>Math Quiz Leaderboard</Text>

        <div className={styles.LeaderboardModal_currentTakerRank}>
          <Text
            colorClass={GLOBALS.COLOR_CLASSES.BLUE["200"]}
            type={textTypes.HEADING.SM}
          >
            Your Rank:
          </Text>
          <Text
            colorClass={GLOBALS.COLOR_CLASSES.BLUE["200"]}
            type={textTypes.HEADING.LG}
          >
            {leaderboard.length ? currentTakerRank + ordinalSuffix : "-"}
          </Text>
        </div>
      </div>

      <div className={styles.LeaderboardModal_content}>
        {leaderboard.map((leaderboardItem, index) => (
          <LeaderboardCard
            key={leaderboardItem.id}
            takerName={leaderboardItem.takerName}
            rank={index + 1}
          />
        ))}
      </div>
      {!leaderboard.length && (
        <div className={styles.LeaderboardModal_noData}>
          <Icon
            className={styles.LeaderboardModal_noData_icon}
            icon="search_off"
          />
          <Text
            colorClass={GLOBALS.COLOR_CLASSES.RED["200"]}
            type={textTypes.HEADING.LG}
          >
            No takers yet.
          </Text>
        </div>
      )}
    </Modal>
  );
};

LeaderboardModal.defaultProps = {
  takerName: null,
};

LeaderboardModal.propTypes = {
  isOpen: PropTypes.bool,
  onClose: PropTypes.func,
  leaderboard: PropTypes.arrayOf(
    PropTypes.shape({
      name: PropTypes.string,
      score: PropTypes.number,
    })
  ),
  takerName: PropTypes.string,
};

export default LeaderboardModal;
