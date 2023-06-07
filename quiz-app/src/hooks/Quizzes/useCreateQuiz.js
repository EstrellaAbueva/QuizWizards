import { useState } from "react";

import { QuizzesService } from "../../services";

const useCreateQuiz = () => {
  const [isCreating, setIsCreating] = useState(false);

  const createQuiz = async (quiz) => {
    setIsCreating(true);

    let responseCode;

    try {
      const response = await QuizzesService.create(quiz);

      responseCode = response.status;
    } catch (error) {
      responseCode = error.response.status;
    }

    setIsCreating(false);

    return { responseCode };
  };

  return { isCreating, createQuiz };
};

export default useCreateQuiz;
