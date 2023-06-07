# **`csit327-project-group-6  QUIZ API`**

## `Entity Relationship Diagram`

![Quiz system](https://github.com/CITUCCS/csit341-final-project-group-6-quiz-wizards/assets/111742763/ffe11982-bcdd-48eb-8455-9a172a174786)

## `API Endpoints`: 
### `Takers`
- `POST /api/takers/login`              : <b>Anonymous Access</b> Log in to QuizWizard                                                            
- `POST /api/takers/signup`             : <b>Anonymous Access</b> Creates an Account                                                   
- `POST /api/takers/assign`             : <b>Admin Access</b> Assign a Taker to a Quiz
- `GET /api/takers`                     : <b>Admin Access</b> Get All Takers
- `GET /api/takers/{id}`                : <b>Admin and Taker Access</b> Get Taker by Id 
- `GET /api/takers/{username}/details`  : <b>Anonymous Access</b> Get Taker by Username
- `GET /api/takers/answers`             : <b>Admin Access</b> Get all Answers from a specific quiz, from all takers or a specific taker
- `GET /api/takers/quizScore`           : <b>Admin and Taker Access</b> Get the score of a Taker from a specific Quiz
- `GET /api/takers/answer/{id}`         : <b>Admin Access</b> Gets a Answer from a Quiz of the specific Taker
- `PUT /api/taker/{id}`                 : <b>Admin and Taker Access</b> Updates Taker Information 
- `PUT /api/takers/recordAnswer`        : <b>Taker Access</b> Taker Answer a Quiz
- `PUT /api/takers/startQuiz`           : <b>Taker Access</b> Update the TakenDate of the Taker
- `PUT /api/takers/endQuiz`             : <b>Taker Access</b> Updates the FinishedDate of the Taker
- `PUT /api/takers/retake`              : <b>Admin and Taker Access</b> Allow Taker to Reatake a Quiz
- `DELETE /api/takers/{id}`             : <b>Admin Access</b> Delete Specific Taker
- `DELETE /api/takers/deleteAnswer`     : <b>Admin and Taker Access</b> Delete an Answer from a Taker

### `Quizzes`
- `POST /api/quizzes`                   : <b>Admin Access</b> Creates Quiz
- `GET /api/quizzes`                    : <b>Admin Access</b> Gets all Quizzes
- `GET /api/quizzes/{id}`               : <b>Admin and Taker Access</b> Gets Quiz by Id
- `GET /api/quizzes/leaderboard`        : <b>Admin and Taker Access</b> Gets the leaderboard of the quiz
- `PUT /api/quizzes/{id}`               : <b>Admin Access</b> Updates Quiz
- `DELETE api/quizzes/{id}`             : <b>Admin Access</b> Deletes Quiz

### `Topics`
- `POST /api/topics`                    : <b>Admin Access</b> Creates Topic
- `GET /api/topics`                     : <b>Admin Access</b> Gets all Topics
- `GET /api/topics/{id}`                : <b>Admin Access</b> Gets Topic
- `PUT /api/topics{id}`                 : <b>Admin Access</b> Updates Topic
- `DELETE /api/topics/{id}`             : <b>Admin Access</b> Deletes Topic

### `Questions`
- `POST /api/questions`                 : <b>Admin Access</b> Creates Question
- `GET /api/questions`                  : <b>Admin Access</b> Gets all Questions
- `GET /api/questions/{id}`             : <b>Admin Access</b> Gets Question
- `PUT /api/questions/{id}`             : <b>Admin Access</b> Updates Question
- `DELETE /api/questions/{id}`          : <b>Admin Access</b> Delete a question

# **```THANK YOU SER AMBRAD ❤️❤️❤️❤️```**
