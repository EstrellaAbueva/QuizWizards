import React from "react";

import GLOBALS from "app-globals";

import {
  Container,
  TakerCardLink,
  Section,
  Text,
  ControlledInput,
  ScreenLoader,
  NoResult,
} from "components";
import { textTypes } from "components/constants";
import { useTakers, useSemiPersistentState } from "hooks";

import styles from "./styles.module.scss";

const TakerLists = () => {
  const [searchTaker, setsearchTaker] = useSemiPersistentState(
    "searchTaker",
    ""
  );

  const { takers, isLoading: isTakersLoading } = useTakers();

  const filteredTakers = takers.filter((taker) => {
    return taker.name.toLowerCase().includes(searchTaker.toLowerCase());
  });

  if (isTakersLoading) {
    return <ScreenLoader />;
  }

  return (
    <Section id="takers">
      <Container className={styles.TakerLists}>
        <div className={styles.TakerLists_header}>
          <Text
            colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
            type={textTypes.HEADING.XL}
          >
            Takers
          </Text>

          <ControlledInput
            className={styles.TakerLists_search}
            placeholder="Search a taker..."
            name="taker"
            icon="search"
            value={searchTaker}
            onChange={(e) => setsearchTaker(e.target.value)}
          />
        </div>

        <div className={styles.TakerLists_takers}>
          {filteredTakers.map((taker) => (
            <TakerCardLink
              key={taker.id}
              name={taker.name}
              link={`/admin/takers/${taker.id}/info`}
            />
          ))}
        </div>
        {!filteredTakers.length && (
          <NoResult title="NO TAKERS" message="No takers found" />
        )}
      </Container>
    </Section>
  );
};

export default TakerLists;
