import { useState } from "react";

import { QuizzesService } from "../../services";

const useUpdateQuiz = () => {
  const [isUpdating, setIsUpdating] = useState(false);

  const updateQuiz = async (quizId, updatedQuiz) => {
    setIsUpdating(true);

    let responseCode;

    try {
      const response = await QuizzesService.update(quizId, updatedQuiz);

      responseCode = response.status;
    } catch (error) {
      responseCode = error.response.status;
    }

    setIsUpdating(false);

    return { responseCode };
  };

  return { isUpdating, updateQuiz };
};

export default useUpdateQuiz;
