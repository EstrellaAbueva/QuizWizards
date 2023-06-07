using AutoMapper;
using Moq;
using QuizApi.Dtos.Question;
using QuizApi.Dtos.QuizD;
using QuizApi.Dtos.Topic;
using QuizApi.Models;
using QuizApi.Repositories.Quizzes;
using QuizApi.Services.Quizzes;

namespace QuizAPITests.Services
{
    public class IQuizServiceTests
    {
        private readonly IQuizService _QuizService;
        private readonly Mock<IQuizRepository> _fakeQuizRepository;
        private readonly Mock<IMapper> _fakeMapper;

        public IQuizServiceTests()
        {
            _fakeQuizRepository = new Mock<IQuizRepository>();
            _fakeMapper = new Mock<IMapper>();
            _QuizService = new QuizService(_fakeQuizRepository.Object, _fakeMapper.Object);
        }

        //CreateQuiz returns quiz
        [Fact]
        public async Task CreateQuiz_WithValidQuiz_ReturnsQuiz()
        {
            //Arrange
            var id = It.IsAny<int>();
            var quizToCreate = new QuizCreationDto
            {
                Name = "Test Name",
                Description = "Test Description",
                TopicId = new List<string>
                {
                    "1","2"
                }
            };

            var model = new Quiz
            {
                Id = id,
                Name = "Test Name",
                Description = "Test Description",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                TopicId = new List<string>
                {
                    "1"
                },
                MaxScore = 1,
                Topics = new List<TopicDto>
                {
                    new TopicDto
                    {
                        Id = id,
                        Name = "Test Name",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        NumberOfQuestions = 1,
                        Questions = new List<Problem>
                        {
                            new Problem
                            {
                                Id = id,
                                Question = "Sample Question",
                                CorrectAnswer = "Correct Answer",
                                CreatedDate = DateTime.Now,
                                UpdatedDate = DateTime.Now,
                            }
                        }
                    }
                }
            };

            var quiz = new QuizDto
            {
                Id = id,
                Name = "Test Name",
                Description = "Test Description",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                MaxScore = 1,
                Topics = new List<TopicDto>
                {
                    new TopicDto
                    {
                        Id = id,
                        Name = "Test Name",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        NumberOfQuestions = 1,
                        Questions = new List<Problem>
                        {
                            new Problem
                            {
                                Id = id,
                                Question = "Sample Question",
                                CorrectAnswer = "Correct Answer",
                                CreatedDate = DateTime.Now,
                                UpdatedDate = DateTime.Now,
                            }
                        }
                    }
                }
            };

            _fakeMapper.Setup(m => m.Map<Quiz>(quizToCreate)).Returns(model);
            _fakeQuizRepository.Setup(m => m.CreateQuiz(model)).ReturnsAsync(1);
            _fakeMapper.Setup(m => m.Map<QuizDto>(model)).Returns(quiz);

            //Act
            var result = await _QuizService.CreateQuiz(quizToCreate);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(quiz, result);
        }

        //CreateQuiz returns null
        [Fact]
        public async Task CreateQuiz_WhenCalled_ReturnsNull()
        {
            //Arrange
            var id = It.IsAny<int>();
            var quizToCreate = new QuizCreationDto
            {
                Name = "Test Name",
                Description = "Test Description",
                TopicId = new List<string>
                {
                    "1","2"
                }
            };

            var model = new Quiz
            {
                Id = id,
                Name = "Test Name",
                Description = "Test Description",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                TopicId = new List<string>
                {
                    "1"
                },
                MaxScore = 1,
                Topics = new List<TopicDto>
                {
                    new TopicDto
                    {
                        Id = id,
                        Name = "Test Name",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        NumberOfQuestions = 1,
                        Questions = new List<Problem>
                        {
                            new Problem
                            {
                                Id = id,
                                Question = "Sample Question",
                                CorrectAnswer = "Correct Answer",
                                CreatedDate = DateTime.Now,
                                UpdatedDate = DateTime.Now,
                            }
                        }
                    }
                }
            };

            var quiz = new QuizDto
            {
                Id = id,
                Name = "Test Name",
                Description = "Test Description",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                MaxScore = 1,
                Topics = new List<TopicDto>
                {
                    new TopicDto
                    {
                        Id = id,
                        Name = "Test Name",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        NumberOfQuestions = 1,
                        Questions = new List<Problem>
                        {
                            new Problem
                            {
                                Id = id,
                                Question = "Sample Question",
                                CorrectAnswer = "Correct Answer",
                                CreatedDate = DateTime.Now,
                                UpdatedDate = DateTime.Now,
                            }
                        }
                    }
                }
            };

            _fakeMapper.Setup(m => m.Map<Quiz>(quizToCreate)).Returns(model);
            _fakeQuizRepository.Setup(m => m.CreateQuiz(model)).ReturnsAsync(-2);

            //Act
            var result = await _QuizService.CreateQuiz(quizToCreate);

            //Assert
            Assert.Null(result);
        }

        //CreateQuiz ThrowsException
        [Fact]
        public async Task CreateQuiz_WhenCalled_ThrowsException()
        {
            //Arrange
            var id = It.IsAny<int>();
            var quizToCreate = new QuizCreationDto
            {
                Name = "Test Name",
                Description = "Test Description",
                TopicId = new List<string>
                {
                    "1","2"
                }
            };

            var model = new Quiz
            {
                Id = id,
                Name = "Test Name",
                Description = "Test Description",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                TopicId = new List<string>
                    {
                        "1"
                    },
                MaxScore = 1,
                Topics = new List<TopicDto>
                    {
                        new TopicDto
                        {
                            Id = id,
                            Name = "Test Name",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now,
                            NumberOfQuestions = 1,
                            Questions = new List<Problem>
                            {
                                new Problem
                                {
                                    Id = id,
                                    Question = "Sample Question",
                                    CorrectAnswer = "Correct Answer",
                                    CreatedDate = DateTime.Now,
                                    UpdatedDate = DateTime.Now,
                                }
                            }
                        }
                    }
            };

            var quiz = new QuizDto
            {
                Id = id,
                Name = "Test Name",
                Description = "Test Description",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                MaxScore = 1,
                Topics = new List<TopicDto>
                    {
                        new TopicDto
                        {
                            Id = id,
                            Name = "Test Name",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now,
                            NumberOfQuestions = 1,
                            Questions = new List<Problem>
                            {
                                new Problem
                                {
                                    Id = id,
                                    Question = "Sample Question",
                                    CorrectAnswer = "Correct Answer",
                                    CreatedDate = DateTime.Now,
                                    UpdatedDate = DateTime.Now,
                                }
                            }
                        }
                }
            };

            _fakeMapper.Setup(m => m.Map<Quiz>(quizToCreate)).Returns(model);
            _fakeQuizRepository.Setup(m => m.CreateQuiz(model))
                               .Throws(new Exception("Database connection error"));

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _QuizService.CreateQuiz(quizToCreate));

            // Assert
            Assert.Equal("Database connection error", exception.Message);
        }

        //GetAllQuizzes returns AllQuizzes
        [Fact]
        public async Task GetAllQuizzes_WithValidQuiz_ReturnsAllQuizzes()
        {
            // Arrange
            var id = It.IsAny<int>();
            var quizzes = new List<QuizDto>
            {
                new QuizDto
                {
                    Id = id,
                    Name = "Test Name",
                    Description = "Test Description",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    MaxScore = 1,
                    Topics = new List<TopicDto>
                        {
                            new TopicDto
                            {
                                Id = id,
                                Name = "Test Name",
                                CreatedDate = DateTime.Now,
                                UpdatedDate = DateTime.Now,
                                NumberOfQuestions = 1,
                                Questions = new List<Problem>
                                {
                                    new Problem
                                    {
                                        Id = id,
                                        Question = "Sample Question",
                                        CorrectAnswer = "Correct Answer",
                                        CreatedDate = DateTime.Now,
                                        UpdatedDate = DateTime.Now,
                                    }
                                }
                            }
                        }
                }
            };

            var quiz2 = new List<Quiz>
            {
                new Quiz
                {
                    Id = id,
                    Name = "Test Name",
                    Description = "Test Description",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    TopicId = new List<string>
                        {
                            "1"
                        },
                    MaxScore = 1,
                    Topics = new List<TopicDto>
                        {
                            new TopicDto
                            {
                                Id = id,
                                Name = "Test Name",
                                CreatedDate = DateTime.Now,
                                UpdatedDate = DateTime.Now,
                                NumberOfQuestions = 1,
                                Questions = new List<Problem>
                                {
                                    new Problem
                                    {
                                        Id = id,
                                        Question = "Sample Question",
                                        CorrectAnswer = "Correct Answer",
                                        CreatedDate = DateTime.Now,
                                        UpdatedDate = DateTime.Now,
                                    }
                                }
                            }
                        }
                }
            };

            _fakeQuizRepository.Setup(m => m.GetAllQuiz()).ReturnsAsync(quiz2);
            _fakeMapper.Setup(m => m.Map<IEnumerable<QuizDto>>(quiz2)).Returns(quizzes);

            // Act
            var result = await _QuizService.GetAllQuizzes();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<QuizDto>>(result);
            Assert.Equal(quizzes.Count(), result.Count());
        }

        //GetAllQuiz returns Empty
        [Fact]
        public async Task GetAllQuiz_WhenCalled_ReturnsEmpty()
        {
            //Arrange
            var model = new List<Quiz>();
            var quizzes = new List<QuizDto>();

            _fakeQuizRepository.Setup(m => m.GetAllQuiz()).ReturnsAsync(model);
            _fakeMapper.Setup(m => m.Map<IEnumerable<QuizDto>>(model)).Returns(quizzes);

            //Act
            var result = await _QuizService.GetAllQuizzes();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<QuizDto>>(result);
            Assert.Empty(result);
        }

        //GetAllQuiz ThrowsException
        [Fact]
        public async Task GetAllQuiz_WhenCalled_ThrowsException()
        {
            // Arrange
            _fakeQuizRepository.Setup(m => m.GetAllQuiz())
                                   .Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _QuizService.GetAllQuizzes());

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //GetQuizById returns quiz
        [Fact]
        public async Task GetQuizById_WithValidQuiz_ReturnsQuizById()
        {
            // Arrange
            var id = It.IsAny<int>();
            var quiz = new QuizDto
            {
                Id = id,
                Name = "Test Name",
                Description = "Test Description",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                MaxScore = 1,
                Topics = new List<TopicDto>
                        {
                            new TopicDto
                            {
                                Id = id,
                                Name = "Test Name",
                                CreatedDate = DateTime.Now,
                                UpdatedDate = DateTime.Now,
                                NumberOfQuestions = 1,
                                Questions = new List<Problem>
                                {
                                    new Problem
                                    {
                                        Id = id,
                                        Question = "Sample Question",
                                        CorrectAnswer = "Correct Answer",
                                        CreatedDate = DateTime.Now,
                                        UpdatedDate = DateTime.Now,
                                    }
                                }
                            }
                        }
            };

            var quiz2 = new Quiz
            {
                Id = id,
                Name = "Test Name",
                Description = "Test Description",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                TopicId = new List<string>
                        {
                            "1"
                        },
                MaxScore = 1,
                Topics = new List<TopicDto>
                        {
                            new TopicDto
                            {
                                Id = id,
                                Name = "Test Name",
                                CreatedDate = DateTime.Now,
                                UpdatedDate = DateTime.Now,
                                NumberOfQuestions = 1,
                                Questions = new List<Problem>
                                {
                                    new Problem
                                    {
                                        Id = id,
                                        Question = "Sample Question",
                                        CorrectAnswer = "Correct Answer",
                                        CreatedDate = DateTime.Now,
                                        UpdatedDate = DateTime.Now,
                                    }
                                }
                            }
                        }
            };

            _fakeMapper.Setup(m => m.Map<Quiz>(quiz)).Returns(quiz2);
            _fakeQuizRepository.Setup(m => m.GetQuizById(id)).ReturnsAsync(quiz2);
            _fakeMapper.Setup(m => m.Map<QuizDto>(quiz2)).Returns(quiz);

            // Act
            var result = await _QuizService.GetQuizById(id);

            // Assert
            Assert.Equal(quiz, result);
            Assert.NotNull(result);
            Assert.IsType<QuizDto>(result);
        }

        //GetQuizById returns null
        [Fact]
        public async Task GetQuizById_WhenCalled_ReturnsNull()
        {
            // Arrange
            var id = It.IsAny<int>();
            var quiz = new QuizDto();
            var quiz2 = new Quiz();

            _fakeMapper.Setup(m => m.Map<Quiz>(quiz)).Returns(quiz2);
            _fakeQuizRepository.Setup(m => m.GetQuizById(id)).ReturnsAsync(quiz2);
            _fakeMapper.Setup(m => m.Map<QuizDto>(quiz2)).Returns(quiz);

            // Act
            var result = await _QuizService.GetQuizById(id);

            // Assert
            Assert.Equal(quiz, result);
        }

        //GetQuizById ThrowsException
        [Fact]
        public async Task GetQuizById_WhenCalled_ThrowsException()
        {
            // Arrange
            var quizId = It.IsAny<int>();
            _fakeQuizRepository.Setup(m => m.GetQuizById(quizId))
                                   .Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _QuizService.GetQuizById(quizId));

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //GetQuizLeaderboardById returns leaderboard
        [Fact]
        public async Task GetQuizLeaderboardById_WithValidDetails_ReturnsLeaderboard()
        {
            // Arrange
            var quizId = It.IsAny<int>();
            var leaderboard = new List<QuizLeaderboard>
            {
                new QuizLeaderboard
                {
                    TakerName = "TakerTest Name",
                    QuizName = "TestQuiz Name",
                    Score = 1
                }
            };

            _fakeQuizRepository.Setup(m => m.GetQuizLeaderboardById(quizId)).ReturnsAsync(leaderboard);

            // Act
            var result = await _QuizService.GetQuizLeaderboardById(quizId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<QuizLeaderboard>>(result);
            Assert.Equal(leaderboard.Count(), result.Count());
        }

        //GetQuizLeaderboardById returns empty
        [Fact]
        public async Task GetQuizLeaderboardById_WithValidDetails_ReturnsEmpty()
        {
            // Arrange
            var quizId = It.IsAny<int>();
            var leaderboard = new List<QuizLeaderboard>();

            _fakeQuizRepository.Setup(m => m.GetQuizLeaderboardById(quizId)).ReturnsAsync(leaderboard);

            // Act
            var result = await _QuizService.GetQuizLeaderboardById(quizId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<QuizLeaderboard>>(result);
            Assert.Empty(result);
        }

        //GetQuizLeaderboardById ThrowsException
        [Fact]
        public async Task GetQuizLeaderboardById_WithValidDetails_ThrowsException()
        {
            // Arrange
            var id = It.IsAny<int>();
            var leaderboard = new List<QuizLeaderboard>();

            _fakeQuizRepository.Setup(m => m.GetQuizLeaderboardById(id))
                               .Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _QuizService.GetQuizLeaderboardById(id));

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //UpdateQuiz
        [Fact]
        public async Task UpdateQuiz_WithValidQuiz_ReturnsQuiz()
        {
            // Arrange
            var id = It.IsAny<int>();
            var quiz = new QuizDto
            {
                Id = id,
                Name = "Test Name",
                Description = "Test Description",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                MaxScore = 1,
                Topics = new List<TopicDto>
                        {
                            new TopicDto
                            {
                                Id = id,
                                Name = "Test Name",
                                CreatedDate = DateTime.Now,
                                UpdatedDate = DateTime.Now,
                                NumberOfQuestions = 1,
                                Questions = new List<Problem>
                                {
                                    new Problem
                                    {
                                        Id = id,
                                        Question = "Sample Question",
                                        CorrectAnswer = "Correct Answer",
                                        CreatedDate = DateTime.Now,
                                        UpdatedDate = DateTime.Now,
                                    }
                                }
                            }
                        }
            };

            var model = new Quiz
            {
                Id = id,
                Name = "Test Name",
                Description = "Test Description",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                TopicId = new List<string>
                        {
                            "1"
                        },
                MaxScore = 1,
                Topics = new List<TopicDto>
                        {
                            new TopicDto
                            {
                                Id = id,
                                Name = "Test Name",
                                CreatedDate = DateTime.Now,
                                UpdatedDate = DateTime.Now,
                                NumberOfQuestions = 1,
                                Questions = new List<Problem>
                                {
                                    new Problem
                                    {
                                        Id = id,
                                        Question = "Sample Question",
                                        CorrectAnswer = "Correct Answer",
                                        CreatedDate = DateTime.Now,
                                        UpdatedDate = DateTime.Now,
                                    }
                                }
                            }
                        }
            };

            var updateQuiz = new QuizCreationDto
            {
                Name = "Test Name",
                Description = "Test Description",
                TopicId = new List<string>
                        {
                            "1"
                        }
            };

            _fakeMapper.Setup(m => m.Map<Quiz>(updateQuiz)).Returns(model);
            _fakeQuizRepository.Setup(m => m.UpdateQuiz(model)).ReturnsAsync(true);
            _fakeMapper.Setup(m => m.Map<QuizDto>(model)).Returns(quiz);

            // Act
            var result = await _QuizService.UpdateQuiz(id, updateQuiz);

            // Assert
            Assert.Equal(quiz, result);
            Assert.NotNull(result);
            Assert.IsType<QuizDto>(result);
        }

        //UpdateQuiz returns null
        [Fact]
        public async Task UpdateQuiz_WhenCalled_ReturnsNull()
        {
            // Arrange
            var quizId = It.IsAny<int>();
            var quiz = new QuizDto();
            var quiz2 = new Quiz();
            var updateQuiz = new QuizCreationDto();

            _fakeMapper.Setup(m => m.Map<Quiz>(updateQuiz)).Returns(quiz2);
            _fakeQuizRepository.Setup(m => m.UpdateQuiz(quiz2)).ReturnsAsync(true);
            _fakeMapper.Setup(m => m.Map<QuizDto>(quiz2)).Returns(quiz);

            // Act
            var result = await _QuizService.UpdateQuiz(quizId, updateQuiz);

            // Assert
            Assert.Equal(quiz, result);
        }

        //UpdateQuestion ThrowsException
        [Fact]
        public async Task UpdateQuiz_WhenCalled_ThrowsException()
        {
            // Arrange
            var quizId = It.IsAny<int>();
            var quiz = new QuizDto();
            var quiz2 = new Quiz();
            var updateQuiz = new QuizCreationDto();

            _fakeMapper.Setup(m => m.Map<Quiz>(updateQuiz)).Returns(quiz2);
            _fakeQuizRepository.Setup(m => m.UpdateQuiz(quiz2))
                               .Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _QuizService.UpdateQuiz(quizId, updateQuiz));

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //DeleteQuiz returns True
        [Fact]
        public async Task DeleteQuiz_WithValidQuiz_ReturnsTrue()
        {
            // Arrange
            var quizId = It.IsAny<int>();

            _fakeQuizRepository.Setup(m => m.DeleteQuiz(quizId)).ReturnsAsync(true);

            // Act
            var result = await _QuizService.DeleteQuiz(quizId);

            // Assert
            Assert.True(result);
        }

        //DeleteQuiz returns False
        [Fact]
        public async Task DeleteQuiz_WithValidQuiz_ReturnsFalse()
        {
            // Arrange
            var quizId = It.IsAny<int>();

            _fakeQuizRepository.Setup(m => m.DeleteQuiz(quizId)).ReturnsAsync(false);

            // Act
            var result = await _QuizService.DeleteQuiz(quizId);

            // Assert
            Assert.False(result);
        }

        //DeleteQuestion ThrowsException
        [Fact]
        public async Task DeleteQuiz_WhenCalled_ThrowsException()
        {
            // Arrange
            var quizId = It.IsAny<int>();
            _fakeQuizRepository.Setup(m => m.DeleteQuiz(quizId))
                                   .Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _QuizService.DeleteQuiz(quizId));

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //CheckQuizById returns true
        [Fact]
        public async Task CheckQuizById_WithValidQuiz_ReturnsTrue()
        {
            // Arrange
            var quizId = It.IsAny<int>();
            _fakeQuizRepository.Setup(m => m.CheckQuizId(quizId)).ReturnsAsync(true);

            // Act
            var result = await _QuizService.CheckQuizById(quizId);
            
            // Assert
            Assert.True(result);
        }

        //CheckQuizById returns false
        [Fact]
        public async Task CheckQuizById_WithValidQuiz_ReturnsFalse()
        {
            // Arrange
            var quizId = It.IsAny<int>();
            _fakeQuizRepository.Setup(m => m.CheckQuizId(quizId)).ReturnsAsync(false);

            // Act
            var result = await _QuizService.CheckQuizById(quizId);

            // Assert
            Assert.False(result);
        }

        //CheckQuizById returns false
        [Fact]
        public async Task CheckQuizById_WhenCallse_ThrowsException()
        {
            // Arrange
            var quizId = It.IsAny<int>();
            _fakeQuizRepository.Setup(m => m.CheckQuizId(quizId))
                               .Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _QuizService.CheckQuizById(quizId));

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //CheckQuizValidation returns True
        [Fact]
        public async Task CheckQuizValidation_WithValidQuiz_ReturnsTrue()
        {
            // Arrange
            var quizId = It.IsAny<int>();
            var quizToCreate = new QuizCreationDto
            {
                Name = "Test Name",
                Description = "Test Description",
                TopicId = new List<string>
                        {
                            "1"
                        }
            };

            _fakeQuizRepository.Setup(m => m.CheckQuizValidation(quizToCreate)).ReturnsAsync(true);

            // Act
            var result = await _QuizService.CheckQuizValidation(quizToCreate);

            // Assert
            Assert.True(result);
        }

        //CheckQuizValidation returns False
        [Fact]
        public async Task CheckQuizValidation_WithValidQuiz_ReturnsFalse()
        {
            // Arrange
            var quizId = It.IsAny<int>();
            var quizToCreate = new QuizCreationDto
            {
                Name = "Test Name",
                Description = "Test Description",
                TopicId = new List<string>
                        {
                            "1"
                        }
            };

            _fakeQuizRepository.Setup(m => m.CheckQuizValidation(quizToCreate)).ReturnsAsync(false);

            // Act
            var result = await _QuizService.CheckQuizValidation(quizToCreate);

            // Assert
            Assert.False(result);
        }

        //CheckQuizValidation ThrowsException
        [Fact]
        public async Task CheckQuizValidation_WithValidQuiz_ThrowsException()
        {
            // Arrange
            var quizId = It.IsAny<int>();
            var quizToCreate = new QuizCreationDto
            {
                Name = "Test Name",
                Description = "Test Description",
                TopicId = new List<string>
                        {
                            "1"
                        }
            };

            _fakeQuizRepository.Setup(m => m.CheckQuizValidation(quizToCreate))
                               .Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _QuizService.CheckQuizValidation(quizToCreate));

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }
    }
}
