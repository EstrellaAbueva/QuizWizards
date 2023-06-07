import { isLocal } from "../utils/destinations";

let apiUrl = null;

if (isLocal) {
  apiUrl = "http://localhost:7137";
}

const config = {
  API_URL: apiUrl,
};

export default config;
