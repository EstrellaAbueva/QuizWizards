import React from "react";

import { Switch, Redirect, Route } from "react-router-dom";

import {
  CreateQuestion,
  CreateTopic,
  CreateQuiz,
  Home,
  QuizLists,
  QuizInfo,
  TakerLists,
  TakerInfo,
  TopicInfo,
  TopicLists,
  QuestionInfo,
  QuestionLists,
  EditQuiz,
  EditTaker,
  EditTopic,
  EditQuestion,
  ReviewTakerQuiz,
} from "screens/admin";

import { ADMIN_ROUTES } from "./constants";

import Sidebar from "screen-wrappers/Sidebar";
import styles from "./styles.module.scss";
import { ScreenLoader } from "components";

const Admin = () => {
  return (
    <div className={styles.Admin}>
      <Sidebar />

      <React.Suspense fallback={<ScreenLoader />}>
        <div className={styles.Admin_screens}>
          <div className={styles.Admin_logo}></div>
          <Switch>
            <Route
              path={ADMIN_ROUTES.HOME}
              name="Home"
              exact
              render={(props) => <Home {...props} />}
            />

            <Route
              path={ADMIN_ROUTES.REVIEW_QUIZ}
              name="Review Taker Quiz"
              exact
              render={(props) => <ReviewTakerQuiz {...props} />}
            />

            {/* CREATE ROUTES */}

            <Route
              path={ADMIN_ROUTES.CREATE_QUESTION}
              name="Create Question"
              exact
              render={(props) => <CreateQuestion {...props} />}
            />

            <Route
              path={ADMIN_ROUTES.CREATE_QUIZ}
              name="Create Quiz"
              exact
              render={(props) => <CreateQuiz {...props} />}
            />

            <Route
              path={ADMIN_ROUTES.CREATE_TOPIC}
              name="Create Topic"
              exact
              render={(props) => <CreateTopic {...props} />}
            />

            {/* EDIT ROUTES */}

            <Route
              path={ADMIN_ROUTES.EDIT_QUESTION}
              name="Edit Question"
              exact
              render={(props) => <EditQuestion {...props} />}
            />

            <Route
              path={ADMIN_ROUTES.EDIT_QUIZ}
              name="Edit Quiz"
              exact
              render={(props) => <EditQuiz {...props} />}
            />

            <Route
              path={ADMIN_ROUTES.EDIT_TAKER}
              name="Edit Taker"
              exact
              render={(props) => <EditTaker {...props} />}
            />

            <Route
              path={ADMIN_ROUTES.EDIT_TOPIC}
              name="Edit Topic"
              exact
              render={(props) => <EditTopic {...props} />}
            />

            {/* LISTS ROUTES */}

            <Route
              path={ADMIN_ROUTES.QUESTIONS}
              name="Questions"
              exact
              render={(props) => <QuestionLists {...props} />}
            />

            <Route
              path={ADMIN_ROUTES.QUIZZES}
              name="Quizzes"
              exact
              render={(props) => <QuizLists {...props} />}
            />

            <Route
              path={ADMIN_ROUTES.TAKERS}
              name="Takers"
              exact
              render={(props) => <TakerLists {...props} />}
            />

            <Route
              path={ADMIN_ROUTES.TOPICS}
              name="Topics"
              exact
              render={(props) => <TopicLists {...props} />}
            />

            {/* SPECIFIC INFORMATION ROUTES */}

            <Route
              path={ADMIN_ROUTES.QUESTION_INFO}
              name="Question Info"
              exact
              render={(props) => <QuestionInfo {...props} />}
            />

            <Route
              path={ADMIN_ROUTES.QUIZ_INFO}
              name="Quiz Info"
              exact
              render={(props) => <QuizInfo {...props} />}
            />

            <Route
              path={ADMIN_ROUTES.TAKER_INFO}
              name="Taker Profile"
              exact
              render={(props) => <TakerInfo {...props} />}
            />

            <Route
              path={ADMIN_ROUTES.TOPIC_INFO}
              name="Quiz Info"
              exact
              render={(props) => <TopicInfo {...props} />}
            />

            <Redirect from="*" to={ADMIN_ROUTES.HOME} />
          </Switch>
        </div>
      </React.Suspense>
    </div>
  );
};

export default Admin;
