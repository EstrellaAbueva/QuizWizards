import { TakersService } from "services";

const useAnswerQuiz = () => {
  const startQuiz = async (takerId, quizId) => {
    await TakersService.startQuiz(takerId, quizId);
  };

  const finishQuiz = async (takerId, quizId) => {
    await TakersService.finishQuiz(takerId, quizId);
  };

  const updateRetake = async (retake, takerId, quizId) => {
    await TakersService.updateRetake(retake, takerId, quizId);
  };

  return { startQuiz, finishQuiz, updateRetake };
};

export default useAnswerQuiz;
