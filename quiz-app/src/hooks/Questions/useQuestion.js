import { useEffect, useState } from "react";
import { QuestionsService } from "services";

const useQuestion = (questionId) => {
  const [isLoading, setIsLoading] = useState(true);
  const [question, setQuestion] = useState(null);

  useEffect(() => {
    const getQuestion = async () => {
      const { data: getQuestionResponse } = await QuestionsService.retrieveById(
        questionId
      );

      if (getQuestionResponse) {
        setQuestion(getQuestionResponse);
      }

      setIsLoading(false);
    };

    getQuestion();
  }, [questionId]);

  return { isLoading, question };
};

export default useQuestion;
