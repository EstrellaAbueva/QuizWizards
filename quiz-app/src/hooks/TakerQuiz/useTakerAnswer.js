import { useEffect, useState } from "react";
import { TakersService } from "services";

const useTakerAnswer = (takerId, quizId, questionId) => {
  const [isLoading, setIsLoading] = useState(true);
  const [takerAnswer, setTakerAnswer] = useState(null);

  useEffect(() => {
    const getTakerAnswer = async () => {
      const { data: getTakerAnswerResponse } = await TakersService.takerAnswer(
        takerId,
        quizId,
        questionId
      );

      if (getTakerAnswerResponse) {
        setTakerAnswer(getTakerAnswerResponse);
      }

      setIsLoading(false);
    };

    getTakerAnswer();
  }, [takerId, quizId, questionId]);

  return { isLoading, takerAnswer };
};

export default useTakerAnswer;
