import { useState } from "react";

import { TopicsService } from "../../services";

const useUpdateTopic = () => {
  const [isUpdating, setIsUpdating] = useState(false);

  const updateTopic = async (topicId, updatedTopic) => {
    setIsUpdating(true);

    let responseCode;

    try {
      const response = await TopicsService.update(topicId, updatedTopic);

      responseCode = response.status;
    } catch (error) {
      responseCode = error.response.status;
    }

    setIsUpdating(false);

    return { responseCode };
  };

  return { isUpdating, updateTopic };
};

export default useUpdateTopic;
