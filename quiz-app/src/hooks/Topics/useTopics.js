import { useState, useEffect } from "react";

import { TopicsService } from "services";

const useTopics = () => {
  const [isLoading, setIsLoading] = useState(true);
  const [topics, setTopics] = useState([]);

  useEffect(() => {
    const getTopics = async () => {
      const { data: getTopicResponse } = await TopicsService.list();

      if (getTopicResponse) {
        setTopics(getTopicResponse);
      }

      setIsLoading(false);
    };

    getTopics();
  }, []);

  return { isLoading, topics };
};

export default useTopics;
