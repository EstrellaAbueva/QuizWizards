import { useState } from "react";

import { TopicsService } from "../../services";

const useCreateTopic = () => {
  const [isCreating, setIsCreating] = useState(false);

  const createTopic = async (topic) => {
    setIsCreating(true);

    let responseCode;

    try {
      const response = await TopicsService.create(topic);

      responseCode = response.status;
    } catch (error) {
      responseCode = error.response.status;
    }

    setIsCreating(false);

    return { responseCode };
  };

  return { isCreating, createTopic };
};

export default useCreateTopic;
