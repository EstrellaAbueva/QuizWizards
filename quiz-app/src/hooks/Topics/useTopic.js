import { useEffect, useState } from "react";
import { TopicsService } from "services";

const useTopic = (topicId) => {
  const [isLoading, setIsLoading] = useState(true);
  const [topic, setTopic] = useState(null);

  useEffect(() => {
    const getTopic = async () => {
      const { data: getTopicResponse } = await TopicsService.retrieveById(
        topicId
      );

      if (getTopicResponse) {
        setTopic(getTopicResponse);
      }

      setIsLoading(false);
    };

    getTopic();
  }, [topicId]);

  return { isLoading, topic };
};

export default useTopic;
