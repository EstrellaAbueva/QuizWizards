import React from "react";
import GLOBALS from "app-globals";

import {
  ControlledInput,
  Container,
  TopicCardLink,
  Section,
  Text,
  ScreenLoader,
  NoResult,
} from "components";
import { textTypes } from "components/constants";
import { useTopics, useSemiPersistentState } from "hooks";

import styles from "./styles.module.scss";

const TopicLists = () => {
  const [searchTopic, setsearchTopic] = useSemiPersistentState(
    "searchTopic",
    ""
  );

  const { topics, isLoading: isTopicLoading } = useTopics();

  if (isTopicLoading) {
    return <ScreenLoader />;
  }

  const filteredTopics = topics.filter((topic) => {
    return topic.name.toLowerCase().includes(searchTopic.toLowerCase());
  });

  return (
    <Section id="topiclists">
      <Container className={styles.TopicLists}>
        <div className={styles.TopicLists_header}>
          <Text
            colorClass={GLOBALS.COLOR_CLASSES.NEUTRAL["0"]}
            type={textTypes.HEADING.XL}
          >
            Topics
          </Text>

          <ControlledInput
            className={styles.TopicLists_search}
            placeholder="Search a topic..."
            name="topic"
            icon="search"
            value={searchTopic}
            onChange={(e) => setsearchTopic(e.target.value)}
          />
        </div>

        <div className={styles.TopicLists_topics}>
          {filteredTopics.map((topic) => (
            <TopicCardLink
              key={topic.id}
              name={topic.name}
              link={`/admin/topics/${topic.id}/info`}
            />
          ))}
        </div>
        {!filteredTopics.length && (
          <NoResult title="NO TOPICS" message="No topics found" />
        )}
      </Container>
    </Section>
  );
};

export default TopicLists;
