import { useState } from "react";

import { QuestionsService } from "../../services";

const useUpdateQuestion = () => {
  const [isUpdating, setIsUpdating] = useState(false);

  const updateQuestion = async (questionId, updatedQuestion) => {
    setIsUpdating(true);

    let responseCode;

    try {
      const response = await QuestionsService.update(
        questionId,
        updatedQuestion
      );

      responseCode = response.status;
    } catch (error) {
      responseCode = error.response.status;
    }

    setIsUpdating(false);

    return { responseCode };
  };

  return { isUpdating, updateQuestion };
};

export default useUpdateQuestion;
