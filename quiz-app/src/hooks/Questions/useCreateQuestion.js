import { useState } from "react";

import { QuestionsService } from "../../services";

const useCreateQuestion = () => {
  const [isCreating, setIsCreating] = useState(false);

  const createQuestion = async (question) => {
    setIsCreating(true);

    let responseCode;

    try {
      const response = await QuestionsService.create(question);

      responseCode = response.status;
    } catch (error) {
      responseCode = error.response.status;
    }

    setIsCreating(false);

    return { responseCode };
  };

  return { isCreating, createQuestion };
};

export default useCreateQuestion;
