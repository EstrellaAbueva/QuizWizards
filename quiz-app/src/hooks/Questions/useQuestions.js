import { useState, useEffect } from "react";

import { QuestionsService } from "services";

const useQuestions = () => {
  const [isLoading, setIsLoading] = useState(true);
  const [questions, setQuestions] = useState([]);

  useEffect(() => {
    const getQuesions = async () => {
      const { data: getQuestionResponse } = await QuestionsService.list();

      if (getQuestionResponse) {
        setQuestions(getQuestionResponse);
      }

      setIsLoading(false);
    };

    getQuesions();
  }, []);

  return { isLoading, questions };
};

export default useQuestions;
