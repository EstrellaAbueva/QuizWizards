[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-24ddc0f5d75046c5622901739e7c5dd533143b0c8e959d652212380cedb1ea36.svg)](https://classroom.github.com/a/JrvVU50A)

# Quiz Wizard

The Quiz Wizard project revolutionizes quiz management by providing efficient features for handling takers, quizzes, topics, questions, answers, and results. The platform is designed to streamline the process and enhance the user experience. Here are the key aspects of its functionality:

#### 1. Taker Management:

- Administrators can easily manage and organize takers, including registration, authentication, and tracking of their progress.
- Personalized profiles enable takers to access their quiz history, results, and customized recommendations.

#### 2. Quiz Creation and Management:

- Administrators have the flexibility to create quizzes with various topics and questions.
- Topics within quizzes allow for categorization and organization of questions based on themes or subjects.

#### 3. Question and Answer Management:

- Administrators can add, edit, and organize questions within topics.

![LoginDesign](Documents/LogInPage.png)

# Starting App Through Docker

1. If the selected End of Line Sequence is `CRLF` change to `LF` in `quiz-api -> QuizDb -> import-quiz-db.sh` file
2. Run `docker-compose up -d --build`
3. If still first time cloning the repo, publish first the QuizDb together with the containers

   a. Edit the target database connection and click browse

   b. Enter `localhost,1433` as the Server Name

   c. Select `SQL Server Authentication`

   d. Enter `SA` as the User Name

   e. Enter the password specified in the `.env` file and then click Ok

   f. Enter `QuizDb` as the Database name and then click publish

   g. Populate the QuizDb through the `PopulateQuiz` script and connect with the same connection as publishing the QuizDb

4. Wait for the QuizDb container to be connected (max attempt in importing QuizDb is 50 attempts) you can check the Logs
5. After all of these, you're good to go to use the application

## Authors

- Abueva, Estrella
- Garcia, Victor Emmanuel F.
- Sinoy, Stephine N.
- Uy, Andre Lennard S.

## Acknowledgements

Thank you, Sir Ambrad! <3
