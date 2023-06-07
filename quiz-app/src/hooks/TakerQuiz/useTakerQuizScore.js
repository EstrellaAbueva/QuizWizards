import { useEffect, useState } from "react";
import { TakersService } from "services";

const useTakerQuizScore = (takerId, quizId) => {
  const [isLoading, setIsLoading] = useState(true);
  const [quizScore, setQuizScore] = useState(null);

  useEffect(() => {
    const getTakerQuizScore = async () => {
      const { data: getTakerQuizScoreResponse } = await TakersService.quizScore(
        takerId,
        quizId
      );

      if (getTakerQuizScoreResponse) {
        setQuizScore(getTakerQuizScoreResponse);
      }

      setIsLoading(false);
    };

    getTakerQuizScore();
  }, [takerId, quizId]);

  return { isLoading, quizScore };
};

export default useTakerQuizScore;
