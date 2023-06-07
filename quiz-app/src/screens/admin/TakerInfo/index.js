import React, { useState } from "react";
import { useHistory, useParams } from "react-router-dom";

import GLOBALS from "app-globals";

import {
  ActionsDropdown,
  ConfirmModal,
  Container,
  Icon,
  NoResult,
  QuizCard,
  ScreenLoader,
  Section,
  Text,
} from "components";
import {
  actionTypes,
  buttonTypes,
  iconButtonTypes,
  textTypes,
} from "components/constants";
import { useTaker } from "hooks";

import styles from "./styles.module.scss";
import { TakersService } from "services";
import { ADMIN_ROUTES } from "screen-wrappers/Admin/constants";

const TakerInfo = () => {
  const { takerId } = useParams();
  const history = useHistory();

  const actions = [
    {
      type: actionTypes.BUTTON,
      icon: "delete",
      text: "Delete",
      id: "delete",
      action: () => toggleIsDeletionConfirmation(true),
    },
    {
      type: actionTypes.BUTTON,
      icon: "edit",
      text: "Edit",
      id: "edit",
      action: () => {
        history.push(`/admin/takers/${takerId}/edit`);
      },
    },
  ].filter((action) => action !== null);

  const [isDeletingSection, setIsDeletingSection] = useState(false);
  const [isDeletionConfirmationToggled, toggleIsDeletionConfirmation] =
    useState(false);

  const { taker, isLoading: isTakerLoading } = useTaker(takerId);

  if (isTakerLoading) {
    return <ScreenLoader />;
  }

  return (
    <>
      <Section id={takerId}>
        <Container className={styles.TakerInfo}>
          <div className={styles.TakerInfo_header}>
            <Icon icon="person" className={styles.TakerInfo_icon} />
            <div className={styles.TakerInfo_details}>
              <Text
                colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
                type={textTypes.HEADING.XL}
              >
                {taker.name}
              </Text>
              <div className={styles.TakerInfo_details_subInfo}>
                <div className={styles.TakerInfo_details_info}>
                  <Text
                    colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
                    type={textTypes.HEADING.XXS}
                  >
                    Address:
                  </Text>
                  <Text colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}>
                    {taker.address}
                  </Text>
                </div>

                <div className={styles.TakerInfo_details_info}>
                  <Text
                    colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
                    type={textTypes.HEADING.XXS}
                  >
                    Email:
                  </Text>
                  <Text colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}>
                    {taker.email}
                  </Text>
                </div>
              </div>
            </div>

            <ActionsDropdown
              actions={actions}
              className={styles.TakerInfo_elipsis}
              iconButtonType={iconButtonTypes.OUTLINE.LG}
            />
          </div>
          <Container className={styles.TakerInfo_bottom}>
            <Text
              className={styles.TakerInfo_bottom_heading}
              type={textTypes.HEADING.MD}
              colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["50"]}
            >
              Quizzes
            </Text>

            <div className={styles.TakerInfo_bottom_quizzes}>
              {taker.quizzes.length > 0 && taker.quizzes[0].id ? (
                taker.quizzes.map((quiz) => (
                  <QuizCard
                    key={quiz.id}
                    {...quiz}
                    quizId={quiz.id}
                    takerId={Number(takerId)}
                    isAdmin
                  />
                ))
              ) : (
                <NoResult
                  title="NO QUIZZES"
                  message="Taker has not taken any quizzes"
                />
              )}
            </div>
          </Container>
        </Container>
      </Section>

      <ConfirmModal
        actions={[
          {
            id: "deleteClassConfirmButton",
            text: "Delete",
            type: buttonTypes.PRIMARY.RED,
            onClick: async () => {
              setIsDeletingSection(true);

              // Call API to delete the taker
              await TakersService.delete(takerId);

              setIsDeletingSection(false);

              toggleIsDeletionConfirmation(false);
              history.push(ADMIN_ROUTES.TAKERS);
            },
            disabled: isDeletingSection,
          },
          {
            id: "deleteClassBackButton",
            text: "Back",
            type: buttonTypes.PRIMARY.YELLOW,
            onClick: () => toggleIsDeletionConfirmation(false),
          },
        ]}
        handleClose={() => {
          toggleIsDeletionConfirmation(false);
        }}
        isOpen={isDeletionConfirmationToggled}
        title="Delete?"
        body="Are you sure to delete this Taker?"
      />
    </>
  );
};

export default TakerInfo;
