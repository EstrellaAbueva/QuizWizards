import axios from "axios";
import config from "./config";

const BASE_URL = `${config.API_URL}/api/questions`;

const QuestionsService = {
  create: (question) => axios.post(`${BASE_URL}`, question),
  list: () => axios.get(`${BASE_URL}`),
  retrieveById: (id) => axios.get(`${BASE_URL}/${id}`),
  update: (id, updatedQuestion) =>
    axios.put(`${BASE_URL}/${id}`, updatedQuestion),
  delete: (id) => axios.delete(`${BASE_URL}/${id}`),
};

export default QuestionsService;
