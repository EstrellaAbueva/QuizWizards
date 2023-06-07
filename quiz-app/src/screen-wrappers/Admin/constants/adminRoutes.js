const BASE_ROUTE = "/admin";

const ADMIN_ROUTES = {
  HOME: `${BASE_ROUTE}/home`,
  TAKERS: `${BASE_ROUTE}/takers`,
  QUIZZES: `${BASE_ROUTE}/quizzes`,
  TOPICS: `${BASE_ROUTE}/topics`,
  QUESTIONS: `${BASE_ROUTE}/questions`,
  CREATE_QUIZ: `${BASE_ROUTE}/quizzes/create`,
  CREATE_TOPIC: `${BASE_ROUTE}/topics/create`,
  CREATE_QUESTION: `${BASE_ROUTE}/questions/create`,
  EDIT_TAKER: `${BASE_ROUTE}/takers/:takerId/edit`,
  EDIT_QUIZ: `${BASE_ROUTE}/quizzes/:quizId/edit`,
  EDIT_TOPIC: `${BASE_ROUTE}/topics/:topicId/edit`,
  EDIT_QUESTION: `${BASE_ROUTE}/questions/:questionId/edit`,
  QUIZ_INFO: `${BASE_ROUTE}/quizzes/:quizId/info`,
  QUESTION_INFO: `${BASE_ROUTE}/questions/:questionId/info`,
  REVIEW_QUIZ: `${BASE_ROUTE}/takers/:takerId/quizzes/:quizId/review`,
  TAKER_INFO: `${BASE_ROUTE}/takers/:takerId/info`,
  TOPIC_INFO: `${BASE_ROUTE}/topics/:topicId/info`,
};

export default ADMIN_ROUTES;
