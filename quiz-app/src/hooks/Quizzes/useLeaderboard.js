import { useState, useEffect } from "react";

import { QuizzesService } from "services";

const useLeaderboard = (quizId) => {
  const [isLoading, setIsLoading] = useState(true);
  const [leaderboard, setLeaderboard] = useState([]);

  useEffect(() => {
    const getLeaderboard = async () => {
      const { data: getLeaderboardResponse } = await QuizzesService.leaderboard(
        quizId
      );

      if (getLeaderboardResponse) {
        setLeaderboard(getLeaderboardResponse);
      }

      setIsLoading(false);
    };

    getLeaderboard();
  }, [quizId]);

  return { isLoading, leaderboard };
};

export default useLeaderboard;
