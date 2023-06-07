import ADMIN_ROUTES from "./adminRoutes";

const ADMIN_LINKS = [
  {
    id: "home",
    name: "Home",
    icon: "home",
    link: ADMIN_ROUTES.HOME,
  },
  {
    id: "takers",
    name: "Takers",
    icon: "people",
    link: ADMIN_ROUTES.TAKERS,
  },
  {
    id: "quizzes",
    name: "Quizzes",
    icon: "assignment",
    link: ADMIN_ROUTES.QUIZZES,
  },
  {
    id: "topics",
    name: "Topics",
    icon: "category",
    link: ADMIN_ROUTES.TOPICS,
  },
  {
    id: "questions",
    name: "Questions",
    icon: "help",
    link: ADMIN_ROUTES.QUESTIONS,
  },
];

export default ADMIN_LINKS;
