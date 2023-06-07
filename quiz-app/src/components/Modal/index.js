import React from "react";

import cn from "classnames";
import PropTypes from "prop-types";
import ReactModal from "react-modal";

import GLOBALS from "../../app-globals";

import ButtonGroup from "../Button/Group";
import buttonTypes from "../Button/constants/buttonTypes";
import Icon from "../Icons";
import Spinner from "../Spinner";

import modalPositions from "./constants/modalPositions";
import modalSizes from "./constants/modalSizes";
import styles from "./styles.module.scss";

import "./styles.scss";

const Modal = ({
  actions,
  children,
  className,
  closeModalId,
  handleClose,
  hasCloseButton,
  id,
  isLoading,
  isOpen,
  overlayClassName,
  buttonGroupClassName,
  hasSeparator,
  parentSelector,
  position,
  size,
}) => (
  <ReactModal
    ariaHideApp
    shouldCloseOnEsc
    shouldCloseOnOverlayClick
    shouldFocusAfterRender
    // second condition is just a temporary workaround for tests
    shouldReturnFocusAfterClose
    bodyOpenClassName={styles.Modal_body___open}
    className={cn(
      className,
      styles[`Modal___${size}`],
      styles[`Modal___${position}`]
    )}
    contentLabel="Modal"
    htmlOpenClassName={styles.Modal_html___open}
    id={id}
    isOpen={isOpen}
    overlayClassName={cn(
      styles[`Modal_overlay___${position}`],
      overlayClassName
    )}
    parentSelector={
      parentSelector && process.env.NODE_ENV !== "test"
        ? () => document.querySelector(parentSelector)
        : () => document.querySelector("body")
    }
    portalClassName={styles.Modal_portal}
    onRequestClose={!isLoading ? handleClose : null}
  >
    {isLoading && (
      <>
        <Spinner className={styles.Modal___loading_spinner} />
        <div className={styles.Modal___loading_background} />
      </>
    )}
    {hasCloseButton && (
      <button
        className={styles.Modal_close}
        id={closeModalId}
        type="button"
        onClick={handleClose}
      >
        <Icon className={styles.Modal_close_icon} icon="close" />
      </button>
    )}
    {children}
    {actions && hasSeparator && <div className={styles.separator} />}
    {actions && (
      <ButtonGroup className={buttonGroupClassName} buttons={actions} />
    )}
  </ReactModal>
);

ReactModal.setAppElement(document.getElementById("root"));

Modal.defaultProps = {
  actions: null,
  className: null,
  closeModalId: null,
  handleClose: null,
  hasCloseButton: true,
  id: null,
  isLoading: false,
  parentSelector: null,
  position: modalPositions.TOP,
  size: modalSizes.MD,
  hasSeparator: false,
};

Modal.propTypes = {
  className: PropTypes.string,
  closeModalId: PropTypes.string,
  id: PropTypes.string,
  isLoading: PropTypes.bool,
  size: PropTypes.oneOf([
    modalSizes.LG,
    modalSizes.MD,
    modalSizes.SM,
    modalSizes.XS,
  ]),
  position: PropTypes.oneOf([modalPositions.CENTER, modalPositions.TOP]),
  children: PropTypes.node.isRequired,
  handleClose: PropTypes.func,
  hasCloseButton: PropTypes.bool,
  isOpen: PropTypes.bool.isRequired,
  overlayClassName: PropTypes.string,
  buttonGroupClassName: PropTypes.string,
  hasSeparator: PropTypes.bool,

  // for mapping the buttons at the bottom of the modal
  actions: PropTypes.arrayOf(
    PropTypes.shape({
      text: PropTypes.oneOfType([PropTypes.string, PropTypes.element])
        .isRequired,
      type: PropTypes.oneOf([
        buttonTypes.PRIMARY.RED,
        buttonTypes.PRIMARY.YELLOW,
        buttonTypes.TEXT.RED,
      ]),
      kind: PropTypes.oneOf([
        GLOBALS.BUTTON_KINDS.BUTTON,
        GLOBALS.BUTTON_KINDS.SUBMIT,
        GLOBALS.BUTTON_KINDS.RESET,
      ]),
      disabled: PropTypes.bool,
      onClick: PropTypes.func.isRequired,
      isLoading: PropTypes.bool,
      dataTestId: PropTypes.string,
    })
  ),

  parentSelector: PropTypes.string,
};

export default Modal;
