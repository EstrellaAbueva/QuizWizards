import axios from "axios";
import config from "./config";

const BASE_URL = `${config.API_URL}/api/topics`;

const TopicsService = {
  create: (topic) => axios.post(`${BASE_URL}`, topic),
  list: () => axios.get(`${BASE_URL}`),
  retrieveById: (id) => axios.get(`${BASE_URL}/${id}`),
  update: (id, updatedTopic) => axios.put(`${BASE_URL}/${id}`, updatedTopic),
  delete: (id) => axios.delete(`${BASE_URL}/${id}`),
};

export default TopicsService;
