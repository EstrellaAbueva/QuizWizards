import React from "react";

import PropTypes from "prop-types";

import GLOBALS from "app-globals";
import { Modal, Text, Button, ButtonGroup, ButtonLink } from "components";
import {
  modalSizes,
  modalPositions,
  textTypes,
  buttonTypes,
  buttonGroupDirections,
} from "components/constants";

import styles from "./styles.module.scss";

const ConfirmModal = ({ isOpen, handleClose, title, body, actions, link }) => {
  return (
    <Modal
      className={styles.ConfirmModal}
      handleClose={handleClose}
      hasCloseButton={false}
      isOpen={isOpen}
      position={modalPositions.CENTER}
      size={modalSizes.XS}
    >
      <div className={styles.ConfirmModal_content}>
        <div className={styles.ConfirmModal_text}>
          <Text
            className={styles.ConfirmModal_text_title}
            type={textTypes.HEADING.XS}
          >
            {title}
          </Text>
          <Text
            className={styles.ConfirmModal_text_info}
            colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["400"]}
            type={textTypes.BODY.SM}
          >
            {body}
          </Text>
        </div>
        {actions && (
          <ButtonGroup
            buttons={actions}
            direction={buttonGroupDirections.VERTICAL}
          />
        )}
        {link && (
          <ButtonLink to={link}>
            <Text>Continue</Text>
          </ButtonLink>
        )}
        {!link && !actions && (
          <Button onClick={handleClose}>
            <Text>Continue</Text>
          </Button>
        )}
      </div>
    </Modal>
  );
};

ConfirmModal.propTypes = {
  isOpen: PropTypes.bool.isRequired,
  handleClose: PropTypes.func.isRequired,
  title: PropTypes.string.isRequired,
  body: PropTypes.string.isRequired,
  actions: PropTypes.arrayOf(
    PropTypes.shape({
      text: PropTypes.oneOfType([PropTypes.string, PropTypes.element])
        .isRequired,
      type: PropTypes.oneOf([
        buttonTypes.PRIMARY.YELLOW,
        buttonTypes.PRIMARY.RED,
        buttonTypes.TEXT.WHITE,
        buttonTypes.TEXT.YELLOW,
        buttonTypes.TEXT.RED,
        buttonTypes.TEXT.GRAY,
      ]),
      disabled: PropTypes.bool,
      onClick: PropTypes.func.isRequired,
    })
  ),
  link: PropTypes.string,
};

export default ConfirmModal;
