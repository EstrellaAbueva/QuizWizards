import { useEffect, useState } from "react";
import { QuizzesService } from "services";

const useQuiz = (quizId) => {
  const [isLoading, setIsLoading] = useState(true);
  const [quiz, setQuiz] = useState(null);

  useEffect(() => {
    const getQuiz = async () => {
      const { data: getQuizResponse } = await QuizzesService.retrieveById(
        quizId
      );

      if (getQuizResponse) {
        setQuiz(getQuizResponse);
      }

      setIsLoading(false);
    };

    getQuiz();
  }, [quizId]);

  return { isLoading, quiz };
};

export default useQuiz;
