import axios from "axios";
import config from "./config";

const BASE_URL = `${config.API_URL}/api/takers`;

const TakersService = {
  signup: (taker) => axios.post(`${BASE_URL}/signup`, taker),
  login: (taker) => axios.post(`${BASE_URL}/login`, taker),
  logout: () => axios.post(`${BASE_URL}/logout`),
  list: () => axios.get(`${BASE_URL}`),
  retrieveById: (id) => axios.get(`${BASE_URL}/${id}`),
  retrieveByUsername: (username) =>
    axios.get(`${BASE_URL}/${username}/details`),
  update: (id, updatedTaker) => axios.put(`${BASE_URL}/${id}`, updatedTaker),
  delete: (id) => axios.delete(`${BASE_URL}/${id}`),
  assign: (takerId, quizId) =>
    axios.post(`${BASE_URL}/assign`, null, {
      params: {
        takerId: takerId,
        quizId: quizId,
      },
    }),
  quizScore: (takerId, quizId) =>
    axios.get(`${BASE_URL}/quizScore`, {
      params: {
        takerId: takerId,
        quizId: quizId,
      },
    }),
  startQuiz: (takerId, quizId) =>
    axios.put(`${BASE_URL}/startQuiz`, null, {
      params: {
        takerId: takerId,
        quizId: quizId,
      },
    }),
  finishQuiz: (takerId, quizId) =>
    axios.put(`${BASE_URL}/endQuiz`, null, {
      params: {
        takerId: takerId,
        quizId: quizId,
      },
    }),
  updateRetake: (retake, takerId, quizId) =>
    axios.put(`${BASE_URL}/retake`, null, {
      params: {
        retake: retake,
        takerId: takerId,
        quizId: quizId,
      },
    }),
  recordAnswer: (answer) => axios.put(`${BASE_URL}/recordAnswer`, answer),
  takerAnswer: (takerId, quizId, questionId) =>
    axios.get(`${BASE_URL}/answer`, {
      params: {
        takerId: takerId,
        quizId: quizId,
        questionId: questionId,
      },
    }),
};

export default TakersService;
