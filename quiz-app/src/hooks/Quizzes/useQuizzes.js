import { useState, useEffect } from "react";

import { QuizzesService } from "services";

const useQuizzes = () => {
  const [isLoading, setIsLoading] = useState(true);
  const [quizzes, setQuizzes] = useState([]);

  useEffect(() => {
    const getQuizzes = async () => {
      const { data: getQuizResponse } = await QuizzesService.list();

      if (getQuizResponse) {
        setQuizzes(getQuizResponse);
      }

      setIsLoading(false);
    };

    getQuizzes();
  }, []);

  return { isLoading, quizzes };
};

export default useQuizzes;
