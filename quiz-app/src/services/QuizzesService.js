import axios from "axios";
import config from "./config";

const BASE_URL = `${config.API_URL}/api/quizzes`;

const QuizzesService = {
  create: (quiz) => axios.post(`${BASE_URL}`, quiz),
  list: () => axios.get(`${BASE_URL}`),
  retrieveById: (id) => axios.get(`${BASE_URL}/${id}`),
  update: (id, updatedQuiz) => axios.put(`${BASE_URL}/${id}`, updatedQuiz),
  delete: (id) => axios.delete(`${BASE_URL}/${id}`),
  leaderboard: (id) =>
    axios.get(`${BASE_URL}/leaderboard`, {
      params: {
        id,
      },
    }),
};

export default QuizzesService;
