using AutoMapper;
using Microsoft.Extensions.Configuration;
using Moq;
using QuizApi.Dtos.QuizD;
using QuizApi.Dtos.Taker;
using QuizApi.Dtos.Topic;
using QuizApi.Models;
using QuizApi.Repositories.Takers;
using QuizApi.Services.Takers;

namespace QuizAPITests.Services
{
#pragma warning disable
    public class ITakerServiceTests
    {
        private readonly ITakerService _TakerService;
        private readonly Mock<ITakerRepository> _fakeTakerRepository;
        private readonly Mock<IMapper> _fakeMapper;
        private readonly Mock<IConfiguration> _fakeConfiguration;

        public ITakerServiceTests()
        {
            _fakeTakerRepository = new Mock<ITakerRepository>();
            _fakeMapper = new Mock<IMapper>();
            _fakeConfiguration = new Mock<IConfiguration>();
            _TakerService = new TakerService(_fakeTakerRepository.Object, _fakeConfiguration.Object, _fakeMapper.Object);
        }

        //CreateTaker returns TakerDto
        [Fact]
        public async Task CreateTaker_WithValidTaker_ReturnsTakerDto()
        {
            //Arrange
            var id = It.IsAny<int>();
            var takerToCreate = new TakerCreationDto
            {
                Name = "Test Name",
                Address = "Test Address",
                Email = "test@gmail.com",
                Username = "testUsername",
                Password = "testPassword"
            };

            var model = new Taker
            {
                Id = id,
                Name = "Test Name",
                Address = "Test Address",
                Email = "test@gmail.com",
                Username = "testUsername",
                Password = "testPassword",
                TakerType = "T",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Quizzes = new List<QuizDto>
                {
                    new QuizDto
                    {
                        Id = id,
                        Name = "Test Name",
                        Description = "Test Desciption",
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
                                        Question = "Test Question",
                                        CorrectAnswer = "Test Answer",
                                        CreatedDate = DateTime.Now,
                                        UpdatedDate = DateTime.Now
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var taker = new TakerDto
            {
                Id = id,
                Name = "Test Name",
                Address = "Test Address",
                Email = "test@gmail.com",
                Username = "testUsername",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };

            _fakeMapper.Setup(m => m.Map<Taker>(takerToCreate)).Returns(model);
            _fakeTakerRepository.Setup(m => m.CreateTaker(model)).ReturnsAsync(id);
            _fakeMapper.Setup(m => m.Map<TakerDto>(model)).Returns(taker);

            //Act
            var result = await _TakerService.CreateTaker(takerToCreate);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(taker, result);
        }

        //CreateTaker returns Null
        [Fact]
        public async Task CreateTaker_WhenCalled_ReturnsNull()
        {
            //Arrange
            var id = It.IsAny<int>();
            var takerToCreate = new TakerCreationDto
            {
                Name = "Test Name",
                Address = "Test Address",
                Email = "test@gmail.com",
                Username = "testUsername",
                Password = "testPassword"
            };

            var model = new Taker
            {
                Id = id,
                Name = "Test Name",
                Address = "Test Address",
                Email = "test@gmail.com",
                Username = "testUsername",
                Password = "testPassword",
                TakerType = "T",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Quizzes = new List<QuizDto>
                {
                    new QuizDto
                    {
                        Id = id,
                        Name = "Test Name",
                        Description = "Test Desciption",
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
                                        Question = "Test Question",
                                        CorrectAnswer = "Test Answer",
                                        CreatedDate = DateTime.Now,
                                        UpdatedDate = DateTime.Now
                                    }
                                }
                            }
                        }
                    } 
                }
            };

            var taker = new TakerDto
            {
                Id = id,
                Name = "Test Name",
                Address = "Test Address",
                Email = "test@gmail.com",
                Username = "testUsername",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };

            _fakeMapper.Setup(m => m.Map<Taker>(takerToCreate)).Returns(model);
            _fakeTakerRepository.Setup(m => m.CreateTaker(model)).ReturnsAsync(-2);

            //Act
            var result = await _TakerService.CreateTaker(takerToCreate);

            //Assert
            Assert.Null(result);
        }

        //CreateTaker ThrowsException
        [Fact]
        public async Task CreateTaker_WhenCalled_ThrowsException()
        {
            //Arrange
            var id = It.IsAny<int>();
            var takerToCreate = new TakerCreationDto
            {
                Name = "Test Name",
                Address = "Test Address",
                Email = "test@gmail.com",
                Username = "testUsername",
                Password = "testPassword"
            };

            var model = new Taker
            {
                Id = id,
                Name = "Test Name",
                Address = "Test Address",
                Email = "test@gmail.com",
                Username = "testUsername",
                Password = "testPassword",
                TakerType = "T",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Quizzes = new List<QuizDto>
                {
                    new QuizDto
                    {
                        Id = id,
                        Name = "Test Name",
                        Description = "Test Desciption",
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
                                        Question = "Test Question",
                                        CorrectAnswer = "Test Answer",
                                        CreatedDate = DateTime.Now,
                                        UpdatedDate = DateTime.Now
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var taker = new TakerDto
            {
                Id = id,
                Name = "Test Name",
                Address = "Test Address",
                Email = "test@gmail.com",
                Username = "testUsername",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };

            _fakeMapper.Setup(m => m.Map<Taker>(takerToCreate)).Returns(model);
            _fakeTakerRepository.Setup(m => m.CreateTaker(model))
                                .Throws(new Exception("Database connection error"));

            //Act
            var result = await Assert.ThrowsAsync<Exception>(() => _TakerService.CreateTaker(takerToCreate));

            //Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //GetAllTakers returns TopicDto
        [Fact]
        public async Task GetAllTakers_WithValidTaker_ReturnsTopicDto()
        {
            // Arrange
            var id = It.IsAny<int>();
            var takers = new List<TakerDto>
            {
                new TakerDto
                {
                    Id = id,
                    Name = "Test Name",
                    Address = "Test Address",
                    Email = "test@gmail.com",
                    Username = "testUsername",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                }
            };

            var taker2 = new List<Taker>
            {
                new Taker
                {
                    Id = id,
                    Name = "Test Name",
                    Address = "Test Address",
                    Email = "test@gmail.com",
                    Username = "testUsername",
                    Password = "testPassword",
                    TakerType = "T",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Quizzes = new List<QuizDto>
                    {
                        new QuizDto
                        {
                            Id = id,
                            Name = "Test Name",
                            Description = "Test Desciption",
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
                                            Question = "Test Question",
                                            CorrectAnswer = "Test Answer",
                                            CreatedDate = DateTime.Now,
                                            UpdatedDate = DateTime.Now
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            _fakeTakerRepository.Setup(m => m.GetAll()).ReturnsAsync(taker2);
            _fakeMapper.Setup(m => m.Map<IEnumerable<TakerDto>>(taker2)).Returns(takers);

            //Act
            var result = await _TakerService.GetAllTakers();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<TakerDto>>(result);
            Assert.Equal(takers.Count(), result.Count());
        }

        //GetAllTakers returns Empty
        [Fact]
        public async Task GetAllTakers_WhenCalled_ReturnsEmpty()
        {
            // Arrange
            var takers = new List<TakerDto>();
            var taker2 = new List<Taker>();

            _fakeTakerRepository.Setup(m => m.GetAll()).ReturnsAsync(taker2);
            _fakeMapper.Setup(m => m.Map<IEnumerable<TakerDto>>(taker2)).Returns(takers);

            //Act
            var result = await _TakerService.GetAllTakers();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<TakerDto>>(result);
            Assert.Empty(result);
        }

        //GetAllTakers ThrowsException
        [Fact]
        public async Task GetAllTakers_WhenCalled_ThrowsException()
        {
            // Arrange
            _fakeTakerRepository.Setup(m => m.GetAll()).Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _TakerService.GetAllTakers());

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //GetTakerByQuizId returns TakerQuizDetailsDto
        [Fact]
        public async Task GetTakerByQuizId_WhenCalled_ReturnsTakerQuizDetailsDto()
        {
            // Arrange
            var id = It.IsAny<int>();
            var model = new Taker
            {
                Id = id,
                Name = "Test Name",
                Address = "Test Address",
                Email = "test@gmail.com",
                Username = "testUsername",
                Password = "testPassword",
                TakerType = "T",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Quizzes = new List<QuizDto>
                {
                    new QuizDto
                    {
                        Id = id,
                        Name = "Test Name",
                        Description = "Test Desciption",
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
                                        Question = "Test Question",
                                        CorrectAnswer = "Test Answer",
                                        CreatedDate = DateTime.Now,
                                        UpdatedDate = DateTime.Now
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var taker = new TakerQuizzesDto
            {
                Id = id,
                Name = "Test Name",
                Address = "Test Address",
                Email = "test@gmail.com",
                Username = "testUsername",
                TakerType = "T",
                Quizzes = new List<QuizDto>
                {
                    new QuizDto
                    {
                        Id = id,
                        Name = "Test Name",
                        Description = "Test Desciption",
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
                                        Question = "Test Question",
                                        CorrectAnswer = "Test Answer",
                                        CreatedDate = DateTime.Now,
                                        UpdatedDate = DateTime.Now
                                    }
                                }
                            }
                        }
                    }
                }
            };

            _fakeTakerRepository.Setup(m => m.GetTakerWithQuiz(id)).ReturnsAsync(model);
            _fakeMapper.Setup(m => m.Map<TakerQuizzesDto>(model)).Returns(taker);

            //Act
            var result = await _TakerService.GetTakerWithQuizById(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TakerQuizzesDto>(result);
            Assert.Equal(taker, result);
        }

        //GetTakerByQuizId returns Null
        [Fact]
        public async Task GetTakerByQuizId_WhenCalled_ReturnsNull()
        {
            // Arrange
            var quizId = It.IsAny<int>();
            var taker = new TakerQuizzesDto();
            var taker2 = new Taker();

            _fakeTakerRepository.Setup(m => m.GetTakerWithQuiz(quizId)).ReturnsAsync(taker2);
            _fakeMapper.Setup(m => m.Map<TakerQuizzesDto>(taker2)).Returns(taker);

            //Act
            var result = await _TakerService.GetTakerWithQuizById(quizId);

            // Assert
            Assert.Equal(taker, result);
        }

        //GetTakerByQuizId ThrowsException
        [Fact]
        public async Task GetTakerByQuizId_WhenCalled_ThrowsException()
        {
            // Arrange
            var quizId = It.IsAny<int>();
            _fakeTakerRepository.Setup(m => m.GetTakerWithQuiz(quizId))
                                             .Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _TakerService.GetTakerWithQuizById(quizId));

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //GetTakerById returns TakerUserNameDto
        [Fact]
        public async Task GetTakerById_WithValidTaker_ReturnsTakerUserNameDto()
        {
            // Arrange
            var id = It.IsAny<int>();
            var taker = new TakerUserNameDto
            {
                Id = id,
                Name = "Test Name",
                Address = "Test Address",
                Email = "test@gmail.com",
                Username = "testUsername",
                Password = "testPassword",
                TakerType = "T",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Quizzes = new List<QuizDto>
                {
                    new QuizDto
                    {
                        Id = id,
                        Name = "Test Name",
                        Description = "Test Desciption",
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
                                        Question = "Test Question",
                                        CorrectAnswer = "Test Answer",
                                        CreatedDate = DateTime.Now,
                                        UpdatedDate = DateTime.Now
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var taker2 = new Taker
            {
                Id = id,
                Name = "Test Name",
                Address = "Test Address",
                Email = "test@gmail.com",
                Username = "testUsername",
                Password = "testPassword",
                TakerType = "T",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Quizzes = new List<QuizDto>
                {
                    new QuizDto
                    {
                        Id = id,
                        Name = "Test Name",
                        Description = "Test Desciption",
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
                                        Question = "Test Question",
                                        CorrectAnswer = "Test Answer",
                                        CreatedDate = DateTime.Now,
                                        UpdatedDate = DateTime.Now
                                    }
                                }
                            }
                        }
                    }
                }
            };

            _fakeMapper.Setup(m => m.Map<Taker>(taker)).Returns(taker2);
            _fakeTakerRepository.Setup(m => m.GetTaker(id)).ReturnsAsync(taker2);
            _fakeMapper.Setup(m => m.Map<TakerUserNameDto>(taker2)).Returns(taker);

            //Act
            var result = await _TakerService.GetTakerById(id);

            //Assert
            Assert.Equal(taker, result);
            Assert.NotNull(result);
            Assert.IsType<TakerUserNameDto>(result);
        }

        //GetTakerById returns Null
        [Fact]
        public async Task GetTakerById_WhenCalled_ReturnsNull()
        {
            // Arrange
            var takerId = It.IsAny<int>();
            var taker = new TakerUserNameDto();
            var taker2 = new Taker();

            _fakeMapper.Setup(m => m.Map<Taker>(taker)).Returns(taker2);
            _fakeTakerRepository.Setup(m => m.GetTaker(takerId)).ReturnsAsync(taker2);
            _fakeMapper.Setup(m => m.Map<TakerUserNameDto>(taker2)).Returns(taker);

            //Act
            var result = await _TakerService.GetTakerById(takerId);

            //Assert
            Assert.Equal(taker, result);
        }

        //GetTakerById ThrowsException
        [Fact]
        public async Task GetTakerById_WhenCalled_ThrowsException()
        {
            // Arrange
            var takerId = It.IsAny<int>();
            _fakeTakerRepository.Setup(m => m.GetTaker(takerId))
                                .Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _TakerService.GetTakerById(takerId));

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //GetTakerByUsername returns TakerUserNameDto
        [Fact]
        public async Task GetTakerByUsername_WithValidTaker_ReturnsTakerUserNameDto()
        {
            // Arrange
            var username = It.IsAny<string>();
            var id = It.IsAny<int>();
            var taker = new TakerUserNameDto
            {
                Id = id,
                Name = "Test Name",
                Address = "Test Address",
                Email = "test@gmail.com",
                Username = "testUsername",
                Password = "testPassword",
                TakerType = "T",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Quizzes = new List<QuizDto>
                {
                    new QuizDto
                    {
                        Id = id,
                        Name = "Test Name",
                        Description = "Test Desciption",
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
                                        Question = "Test Question",
                                        CorrectAnswer = "Test Answer",
                                        CreatedDate = DateTime.Now,
                                        UpdatedDate = DateTime.Now
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var taker2 = new Taker
            {
                Id = id,
                Name = "Test Name",
                Address = "Test Address",
                Email = "test@gmail.com",
                Username = "testUsername",
                Password = "testPassword",
                TakerType = "T",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Quizzes = new List<QuizDto>
                {
                    new QuizDto
                    {
                        Id = id,
                        Name = "Test Name",
                        Description = "Test Desciption",
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
                                        Question = "Test Question",
                                        CorrectAnswer = "Test Answer",
                                        CreatedDate = DateTime.Now,
                                        UpdatedDate = DateTime.Now
                                    }
                                }
                            }
                        }
                    }
                }
            };

            _fakeMapper.Setup(m => m.Map<Taker>(taker)).Returns(taker2);
            _fakeTakerRepository.Setup(m => m.GetTakerByUsername(username)).ReturnsAsync(taker2);
            _fakeMapper.Setup(m => m.Map<TakerUserNameDto>(taker2)).Returns(taker);

            //Act
            var result = await _TakerService.GetTakerByUsername(username);

            //Assert
            Assert.Equal(taker, result);
        }

        //GetTakerByUsername returns Null
        [Fact]
        public async Task GetTakerByUsername_WhenCalled_ReturnsNull()
        {
            // Arrange
            var username = It.IsAny<string>();
            var taker = new TakerUserNameDto();
            var taker2 = new Taker();

            _fakeMapper.Setup(m => m.Map<Taker>(taker)).Returns(taker2);
            _fakeTakerRepository.Setup(m => m.GetTakerByUsername(username)).ReturnsAsync(taker2);
            _fakeMapper.Setup(m => m.Map<TakerUserNameDto>(taker2)).Returns(taker);

            //Act
            var result = await _TakerService.GetTakerByUsername(username);

            //Assert
            Assert.Equal(taker, result);
        }

        //GetTakerByUsername ThrowsException
        [Fact]
        public async Task GetTakerByUsername_WhenCalled_ThrowsException()
        {
            // Arrange
            var username = It.IsAny<string>();
            _fakeTakerRepository.Setup(m => m.GetTakerByUsername(username))
                                .Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _TakerService.GetTakerByUsername(username));

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //UpdateTaker returns TakerUserNameDto
        [Fact]
        public async Task UpdateTaker_WithValidTaker_ReturnsTakerUserNameDto()
        {
            // Arrange
            var id = It.IsAny<int>();
            var takerToUpdate = new TakerCreationDto
            {
                Name = "Test Name",
                Address = "Test Address",
                Email = "test@gmail.com",
                Username = "testUsername",
                Password = "testPassword"
            };

            var taker = new TakerUserNameDto
            {
                Id = id,
                Name = "Test Name",
                Address = "Test Address",
                Email = "test@gmail.com",
                Username = "testUsername",
                Password = "testPassword",
                TakerType = "T",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Quizzes = new List<QuizDto>
                {
                    new QuizDto
                    {
                        Id = id,
                        Name = "Test Name",
                        Description = "Test Desciption",
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
                                        Question = "Test Question",
                                        CorrectAnswer = "Test Answer",
                                        CreatedDate = DateTime.Now,
                                        UpdatedDate = DateTime.Now
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var model = new Taker
            {
                Id = id,
                Name = "Test Name",
                Address = "Test Address",
                Email = "test@gmail.com",
                Username = "testUsername",
                Password = "testPassword",
                TakerType = "T",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Quizzes = new List<QuizDto>
                {
                    new QuizDto
                    {
                        Id = id,
                        Name = "Test Name",
                        Description = "Test Desciption",
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
                                        Question = "Test Question",
                                        CorrectAnswer = "Test Answer",
                                        CreatedDate = DateTime.Now,
                                        UpdatedDate = DateTime.Now
                                    }
                                }
                            }
                        }
                    }
                }
            };

            _fakeMapper.Setup(m => m.Map<Taker>(takerToUpdate)).Returns(model);
            _fakeTakerRepository.Setup(m => m.UpdateTaker(model)).ReturnsAsync(true);
            _fakeMapper.Setup(m => m.Map<TakerUserNameDto>(It.IsAny<Taker>())).Returns(taker);

            // Act
            var result = await _TakerService.UpdateTaker(id, takerToUpdate);

            // Assert
            Assert.Equal(taker, result);
            Assert.NotNull(result);
            Assert.IsType<TakerUserNameDto>(result);
        }

        //UpdateTaker returns Null
        [Fact]
        public async Task UpdateTaker_WhenCalled_ReturnsNull()
        {
            // Arrange
            var takerId = It.IsAny<int>();
            var takerToUpdate = new TakerCreationDto();
            var model = new Taker();
            var taker = new TakerUserNameDto();

            _fakeMapper.Setup(m => m.Map<Taker>(takerToUpdate)).Returns(model);
            _fakeTakerRepository.Setup(m => m.UpdateTaker(model)).ReturnsAsync(true);
            _fakeMapper.Setup(m => m.Map<TakerUserNameDto>(It.IsAny<Taker>())).Returns(taker);

            // Act
            var result = await _TakerService.UpdateTaker(takerId, takerToUpdate);

            // Assert
            Assert.Equal(taker, result);
        }

        //UpdateTaker ThrowsException
        [Fact]
        public async Task UpdateTaker_WhenCalled_ThrowsException()
        {
            // Arrange
            var takerId = It.IsAny<int>();
            var takerToUpdate = new TakerCreationDto();
            var model = new Taker();
            var taker = new TakerUserNameDto();

            _fakeMapper.Setup(m => m.Map<Taker>(takerToUpdate)).Returns(model);
            _fakeTakerRepository.Setup(m => m.UpdateTaker(model)).Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _TakerService.UpdateTaker(takerId, takerToUpdate));

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //DeleteTaker returns True
        [Fact]
        public async Task DeleteTaker_WithValidTaker_ReturnsTrue()
        {
            // Arrange
            var takerId = It.IsAny<int>();
            _fakeTakerRepository.Setup(m => m.DeleteTaker(takerId)).ReturnsAsync(true);

            //Act
            var result = await _TakerService.DeleteTaker(takerId);

            //Assert
            Assert.True(result);


        }

        //DeleteTaker returns False
        [Fact]
        public async Task DeleteTaker_WhenCalled_ReturnsFalse()
        {
            // Arrange
            var takerId = It.IsAny<int>();
            _fakeTakerRepository.Setup(m => m.DeleteTaker(takerId)).ReturnsAsync(false);

            //Act
            var result = await _TakerService.DeleteTaker(takerId);

            //Assert
            Assert.False(result);
        }

        //DeleteTaker ThrowsException
        [Fact]
        public async Task DeleteTaker_WhenCalled_ReturnsThrowsException()
        {
            // Arrange
            var takerId = It.IsAny<int>();
            _fakeTakerRepository.Setup(m => m.DeleteTaker(takerId))
                                .Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _TakerService.DeleteTaker(takerId));

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //LetTakerTakeQuiz Returns Int
        [Fact]
        public async Task LetTakerTakeQuiz_WithValidTaker_ReturnsInt()
        {
            // Arrange
            var takerId = It.IsAny<int>();
            var quizId = It.IsAny<int>();
            var newId = It.IsAny<int>();
            var id = It.IsAny<int>();
            var takerToUpdate = new TakerCreationDto
            {
                Name = "Test Name",
                Address = "Test Address",
                Email = "test@gmail.com",
                Username = "testUsername",
                Password = "testPassword"
            };

            var taker = new TakerUserNameDto
            {
                Id = id,
                Name = "Test Name",
                Address = "Test Address",
                Email = "test@gmail.com",
                Username = "testUsername",
                Password = "testPassword",
                TakerType = "T",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Quizzes = new List<QuizDto>
                {
                    new QuizDto
                    {
                        Id = id,
                        Name = "Test Name",
                        Description = "Test Desciption",
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
                                        Question = "Test Question",
                                        CorrectAnswer = "Test Answer",
                                        CreatedDate = DateTime.Now,
                                        UpdatedDate = DateTime.Now
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var model = new Taker
            {
                Id = id,
                Name = "Test Name",
                Address = "Test Address",
                Email = "test@gmail.com",
                Username = "testUsername",
                Password = "testPassword",
                TakerType = "T",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Quizzes = new List<QuizDto>
                {
                    new QuizDto
                    {
                        Id = id,
                        Name = "Test Name",
                        Description = "Test Desciption",
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
                                        Question = "Test Question",
                                        CorrectAnswer = "Test Answer",
                                        CreatedDate = DateTime.Now,
                                        UpdatedDate = DateTime.Now
                                    }
                                }
                            }
                        }
                    }
                }
            };

            _fakeMapper.Setup(m => m.Map<Taker>(taker)).Returns(model);
            _fakeTakerRepository.Setup(m => m.LetTakerTakeQuiz(takerId, quizId)).ReturnsAsync(newId);
            _fakeMapper.Setup(m => m.Map<TakerUserNameDto>(model)).Returns(taker);

            //Act
            var result = await _TakerService.LetTakerTakeQuiz(takerId, quizId);

            // Assert
            Assert.Equal(newId, result);
            Assert.IsType<int>(result);
        }

        //LetTakerTakeQuiz Returns Null
        [Fact]
        public async Task LetTakerTakeQuiz_WhenCalled_ReturnsNull()
        {
            // Arrange
            var takerId = It.IsAny<int>();
            var quizId = It.IsAny<int>();
            var newId = It.IsAny<int>();
            var taker = new TakerUserNameDto();
            var model = new Taker();

            _fakeMapper.Setup(m => m.Map<Taker>(taker)).Returns(model);
            _fakeTakerRepository.Setup(m => m.LetTakerTakeQuiz(takerId, quizId)).ReturnsAsync(newId);
            _fakeMapper.Setup(m => m.Map<TakerUserNameDto>(model)).Returns(taker);

            //Act
            var result = await _TakerService.LetTakerTakeQuiz(takerId, quizId);

            //Assert
            Assert.Equal(newId, result);
        }

        //LetTakerTakeQuiz ThrowsException
        [Fact]
        public async Task LetTakerTakeQuiz_WhenCalled_ThrowsException()
        {
            // Arrange
            var takerId = It.IsAny<int>();
            var quizId = It.IsAny<int>();
            var newId = It.IsAny<int>();

            _fakeTakerRepository.Setup(m => m.LetTakerTakeQuiz(takerId, quizId))
                                .Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _TakerService.LetTakerTakeQuiz(takerId, quizId));

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //HasTakerTakenQuiz returns True
        [Fact]
        public async Task HasTakerTakenQuiz_WithValidTaker_ReturnsTrue()
        {
            // Arrange
            var takerId = It.IsAny<int>();
            var quizId = It.IsAny<int>();
            var taker = new TakerUserNameDto();
            var model = new Taker();

            _fakeMapper.Setup(m => m.Map<Taker>(taker)).Returns(model);
            _fakeTakerRepository.Setup(m => m.HasTakerTakenQuiz(takerId, quizId)).ReturnsAsync(true);
            _fakeMapper.Setup(m => m.Map<TakerUserNameDto>(model)).Returns(taker);

            //Act
            var result = await _TakerService.HasTakerTakenQuiz(takerId, quizId);

            //Assert
            Assert.True(result);
        }

        //HasTakerTakenQuiz returns False
        [Fact]
        public async Task HasTakerTakenQuiz_WhenCalled_ReturnsFalse()
        {
            // Arrange
            var takerId = It.IsAny<int>();
            var quizId = It.IsAny<int>();
            var taker = new TakerUserNameDto();
            var model = new Taker();

            _fakeMapper.Setup(m => m.Map<Taker>(taker)).Returns(model);
            _fakeTakerRepository.Setup(m => m.HasTakerTakenQuiz(takerId, quizId)).ReturnsAsync(false);
            _fakeMapper.Setup(m => m.Map<TakerUserNameDto>(model)).Returns(taker);

            //Act
            var result = await _TakerService.HasTakerTakenQuiz(takerId, quizId);

            //Assert
            Assert.False(result);
        }

        //HasTakerTakenQuiz ThrowsException
        [Fact]
        public async Task HasTakerTakenQuiz_WhenCalled_ThrowsException()
        {
            // Arrange
            var takerId = It.IsAny<int>();
            var quizId = It.IsAny<int>();

            _fakeTakerRepository.Setup(m => m.HasTakerTakenQuiz(takerId, quizId))
                                .Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _TakerService.HasTakerTakenQuiz(takerId, quizId));

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //TakerAnswersQuiz returns TakerAnswersDto
        [Fact]
        public async Task TakerAnswersQuiz_WithValidTaker_ReturnsTakerAnswersDto()
        {
            // Arrange
            var newId = It.IsAny<int>();
            var id = It.IsAny<int>() ;
            var taker = new TakerAnswer
            {
                Id = id,
                TakerId = id,
                QuizId = id,
                QuestionId = id,
                Answer = "Test Answer"
            };
            var res = new TakerAnswersDto
            {
                Id = id,
                TakerId = id,
                QuizId = id,
                QuestionId = id,
                Answer = "Test Answer"
            };
            var answerToRecord = new TakerAnswerCreationDto
            {
                TakerId = id,
                QuizId = id,
                QuestionId = id,
                Answer = "Test Answer"
            };

            _fakeMapper.Setup(m => m.Map<TakerAnswer>(answerToRecord)).Returns(taker);
            _fakeTakerRepository.Setup(m => m.TakerAnswersQuiz(taker)).ReturnsAsync(newId);
            _fakeMapper.Setup(m => m.Map<TakerAnswersDto>(taker)).Returns(res);

            //Act
            var result = await _TakerService.TakerAnswersQuiz(answerToRecord);

            //Assert
            Assert.Equal(res, result);
            Assert.NotNull(result);
            Assert.IsType<TakerAnswersDto>(result);
        }

        //TakerAnswersQuiz returns Null
        [Fact]
        public async Task TakerAnswersQuiz_WhenCalled_ReturnsNull()
        {
            // Arrange
            var newId = It.IsAny<int>();
            var taker = new TakerAnswer();
            var res = new TakerAnswersDto();
            var answerToRecord = new TakerAnswerCreationDto();

            _fakeMapper.Setup(m => m.Map<TakerAnswer>(answerToRecord)).Returns(taker);
            _fakeTakerRepository.Setup(m => m.TakerAnswersQuiz(taker)).ReturnsAsync(newId);
            _fakeMapper.Setup(m => m.Map<TakerAnswersDto>(taker)).Returns(res);

            //Act
            var result = await _TakerService.TakerAnswersQuiz(answerToRecord);

            //Assert
            Assert.Equal(res, result);
        }

        //TakerAnswersQuiz ThrowsException
        [Fact]
        public async Task TakerAnswersQuiz_WhenCalled_ThrowsException()
        {
            // Arrange
            var newId = It.IsAny<int>();
            var taker = new TakerAnswer();
            var answerToRecord = new TakerAnswerCreationDto();

            _fakeMapper.Setup(m => m.Map<TakerAnswer>(answerToRecord)).Returns(taker);
            _fakeTakerRepository.Setup(m => m.TakerAnswersQuiz(taker))
                                .Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _TakerService.TakerAnswersQuiz(answerToRecord));

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //GetTakerQuizScore returns TakerQuizDto
        [Fact]
        public async Task GetTakerQuizScore_WithValidTaker_ReturnsTakerQuizDto()
        {
            // Arrange
            var takerId = It.IsAny<int>();
            var quizId = It.IsAny<int>();
            var id = It.IsAny<int>();
            var taker = new TakerQuizDto
            {
                Id = id, 
                TakerId = takerId,
                QuizId = quizId,
                AssignedDate = DateTime.Now,
                Score = 1, 
                TakenDate = DateTime.Now,
                FinishedDate = DateTime.Now,
                CanRetake = 0
            };

            var model = new TakerQuiz
            {
                Id = id,
                TakerId = takerId,
                QuizId = quizId,
                AssignedDate = DateTime.Now,
                Score = 1,
                TakenDate = DateTime.Now,
                FinishedDate = DateTime.Now,
                CanRetake = 0
            };

            _fakeMapper.Setup(m => m.Map<TakerQuiz>(taker)).Returns(model);
            _fakeTakerRepository.Setup(m => m.GetTakerQuizScore(takerId, quizId)).ReturnsAsync(model);
            _fakeMapper.Setup(m => m.Map<TakerQuizDto>(model)).Returns(taker);

            //Act
            var result = await _TakerService.GetTakerQuizScore(takerId, quizId);

            //Assert
            Assert.Equal(taker, result);
            Assert.NotNull(result);
            Assert.IsType<TakerQuizDto>(result);
        }

        //GetTakerQuizScore returns Null
        [Fact]
        public async Task GetTakerQuizScore_WhenCalled_ReturnsNull()
        {
            // Arrange
            var takerId = It.IsAny<int>();
            var quizId = It.IsAny<int>();
            var taker = new TakerQuizDto();
            var model = new TakerQuiz();

            _fakeMapper.Setup(m => m.Map<TakerQuiz>(taker)).Returns(model);
            _fakeTakerRepository.Setup(m => m.GetTakerQuizScore(takerId, quizId)).ReturnsAsync(model);
            _fakeMapper.Setup(m => m.Map<TakerQuizDto>(model)).Returns(taker);

            //Act
            var result = await _TakerService.GetTakerQuizScore(takerId, quizId);

            //Assert
            Assert.Equal(taker, result);
        }

        //GetTakerQuizScore ThrowsException
        [Fact]
        public async Task GetTakerQuizScore_WhenCalled_ThrowsException()
        {
            // Arrange
            var takerId = It.IsAny<int>();
            var quizId = It.IsAny<int>();
            _fakeTakerRepository.Setup(m => m.GetTakerQuizScore(takerId, quizId))
                                .Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _TakerService.GetTakerQuizScore(takerId, quizId));

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //GetTakerAnswer returns AllAnswersofTaker
        [Fact]
        public async Task GetTakerAnswer_WithValidAnswer_ReturnsAllAnswersofTaker()
        {
            // Arrange
            var id = It.IsAny<int>();
            var taker = new List<TakerAnswersDto>
            {
                new TakerAnswersDto
                {
                    Id = id,
                    TakerId = id,
                    QuizId = id,
                    QuestionId = id,
                    Answer = "Test Answer"
                }
            };
            var model = new List<TakerAnswer>
            {
                new TakerAnswer
                {
                    Id = id,
                    TakerId = id,
                    QuizId = id,
                    QuestionId = id,
                    Answer = "Test Answer"
                }
            };

            _fakeMapper.Setup(m => m.Map<IEnumerable<TakerAnswer>>(taker)).Returns(model);
            _fakeTakerRepository.Setup(m => m.GetTakerAnswer(id)).ReturnsAsync(model);
            _fakeMapper.Setup(m => m.Map<IEnumerable<TakerAnswersDto>>(model)).Returns(taker);

            //Act
            var result = await _TakerService.GetTakerAnswer(id);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<TakerAnswersDto>>(result);
            Assert.Equal(taker.Count(), result.Count());
        }

        //GetTakerAnswer returns Empty
        [Fact]
        public async Task GetTakerAnswer_WhenCalled_ReturnsEmpty()
        {
            // Arrange
            var answerId = It.IsAny<int>();
            var id = It.IsAny<int>();
            var taker = new List<TakerAnswersDto>();
            var model = new List<TakerAnswer>();

            _fakeMapper.Setup(m => m.Map<IEnumerable<TakerAnswer>>(taker)).Returns(model);
            _fakeTakerRepository.Setup(m => m.GetTakerAnswer(id)).ReturnsAsync(model);
            _fakeMapper.Setup(m => m.Map<IEnumerable<TakerAnswersDto>>(model)).Returns(taker);

            //Act
            var result = await _TakerService.GetTakerAnswer(answerId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<TakerAnswersDto>>(result);
            Assert.Empty(result);
        }

        //GetTakerAnswer ThrowsException
        [Fact]
        public async Task GetTakerAnswer_WhenCalled_ThrowsException()
        {
            // Arrange
            var answerId = It.IsAny<int>();

            _fakeTakerRepository.Setup(m => m.GetTakerAnswer(answerId))
                                .Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _TakerService.GetTakerAnswer(answerId));

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //GetAllAnswers returns TakerAnswersDto
        [Fact]
        public async Task GetAllAnswers_WithValidAnswers_ReturnsTakerAnswersDto()
        {
            // Arrange
            var id = It.IsAny<int>();
            var taker = new List<TakerAnswersDto>
            {
                new TakerAnswersDto
                {
                    Id = id,
                    TakerId = id,
                    QuizId = id,
                    QuestionId = id,
                    Answer = "Test Answer"
                }
            };
            var model = new List<TakerAnswer>
            {
                new TakerAnswer
                {
                    Id = id,
                    TakerId = id,
                    QuizId = id,
                    QuestionId = id,
                    Answer = "Test Answer"
                }
            };

            _fakeTakerRepository.Setup(m => m.GetAllAnswers()).ReturnsAsync(model);
            _fakeMapper.Setup(m => m.Map<IEnumerable<TakerAnswersDto>>(model)).Returns(taker);

            //Act
            var result = await _TakerService.GetAllAnswers();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<TakerAnswersDto>>(result);
            Assert.Equal(taker.Count(), result.Count());
        }

        //GetAllAnswers returns Empty
        [Fact]
        public async Task GetAllAnswers_WhenCalled_ReturnsEmpty()
        {
            // Arrange
            var taker = new List<TakerAnswersDto>();
            var model = new List<TakerAnswer>();

            _fakeTakerRepository.Setup(m => m.GetAllAnswers()).ReturnsAsync(model);
            _fakeMapper.Setup(m => m.Map<IEnumerable<TakerAnswersDto>>(model)).Returns(taker);

            //Act
            var result = await _TakerService.GetAllAnswers();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<TakerAnswersDto>>(result);
            Assert.Empty(result);
        }

        //GetAllAnswers ThrowsException
        [Fact]
        public async Task GetAllAnswers_WhenCalled_ThrowsException()
        {
            // Arrange
            var model = new List<TakerAnswer>();

            _fakeTakerRepository.Setup(m => m.GetAllAnswers()).ReturnsAsync(model);
            _fakeMapper.Setup(m => m.Map<IEnumerable<TakerAnswersDto>>(model))
                       .Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _TakerService.GetAllAnswers());

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //GetAnswerById returns TakerAnswersDto
        [Fact]
        public async Task GetAnswerById_WithValidAnswers_ReturnsTakerAnswersDto()
        {
            // Arrange
            var answerId = It.IsAny<int>();
            var id = It.IsAny<int>();
            var taker = new TakerAnswersDto
            {
                Id = id,
                TakerId = id,
                QuizId = id,
                QuestionId = id,
                Answer = "Test Answer"
            };
            var model = new TakerAnswer
            {
                Id = id,
                TakerId = id,
                QuizId = id,
                QuestionId = id,
                Answer = "Test Answer"
            };

            _fakeTakerRepository.Setup(m => m.GetAnswerById(answerId)).ReturnsAsync(model);
            _fakeMapper.Setup(m => m.Map<TakerAnswersDto>(model)).Returns(taker);

            //Act
            var result = await _TakerService.GetAnswerById(answerId);

            //Assert
            Assert.Equal(taker, result);
            Assert.NotNull(result);
            Assert.IsType<TakerAnswersDto>(result);
        }

        //GetAnswerById returns Null
        [Fact]
        public async Task GetAnswerById_WhenCalled_ReturnsNull()
        {
            // Arrange
            var answerId = It.IsAny<int>();
            var taker = new TakerAnswersDto();
            var model = new TakerAnswer();

            _fakeTakerRepository.Setup(m => m.GetAnswerById(answerId)).ReturnsAsync(model);
            _fakeMapper.Setup(m => m.Map<TakerAnswersDto>(model)).Returns(taker);

            //Act
            var result = await _TakerService.GetAnswerById(answerId);

            //Assert
            Assert.Equal(taker, result);
        }

        //GetAnswerById returns ThrowsException
        [Fact]
        public async Task GetAnswerById_WhenCalled_ThrowsException()
        {
            // Arrange
            var answerId = It.IsAny<int>();
            var taker = new TakerAnswersDto();
            var model = new TakerAnswer();

            _fakeTakerRepository.Setup(m => m.GetAnswerById(answerId)).ReturnsAsync(model);
            _fakeMapper.Setup(m => m.Map<TakerAnswersDto>(model))
                                    .Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _TakerService.GetAnswerById(answerId));

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //VerifyPassword returns True
        [Fact]
        public async Task VerifyPassword_WithValidTaker_ReturnsTrue()
        {
            // Arrange
            var inputtedPassword = It.IsAny<string>();
            var password = It.IsAny<string>();

            //Act
            var result = _TakerService.VerifyPassword(password, inputtedPassword);

            //Assert
            Assert.True(result);
        }

        //CreateToken
        //Cant test because of the token that it would give back would be different everytime

        //TakerUpdateTakenDate returns TakerQuizDto
        [Fact]
        public async Task TakerUpdateTakenDate_WithValidTaker_ReturnsTakerQuizDto()
        {
            // Arrange
            var takerId = It.IsAny<int>();
            var quizId = It.IsAny<int>();
            var id = It.IsAny<int>();
            var taker = new TakerQuizDto
            {
                Id = id,
                TakerId = takerId,
                QuizId = quizId,
                AssignedDate = DateTime.Now,
                Score = 1,
                TakenDate = DateTime.Now,
                FinishedDate = DateTime.Now,
                CanRetake = 0
            };

            var model = new TakerQuiz
            {
                Id = id,
                TakerId = takerId,
                QuizId = quizId,
                AssignedDate = DateTime.Now,
                Score = 1,
                TakenDate = DateTime.Now,
                FinishedDate = DateTime.Now,
                CanRetake = 0
            };

            _fakeTakerRepository.Setup(m => m.TakerUpdateTakenDate(takerId, quizId)).ReturnsAsync(model);
            _fakeMapper.Setup(m => m.Map<TakerQuizDto>(model)).Returns(taker);

            //Act
            var result = await _TakerService.TakerUpdateTakenDate(takerId, quizId);

            // Assert
            Assert.Equal(taker, result);
            Assert.NotNull(result);
            Assert.IsType<TakerQuizDto>(result);
        }

        //TakerUpdateTakenDate returns Null
        [Fact]
        public async Task TakerUpdateTakenDate_WhenCalled_ReturnsNull()
        {
            // Arrange
            var takerId = It.IsAny<int>();
            var quizId = It.IsAny<int>();
            var taker = new TakerQuizDto();
            var model = new TakerQuiz();

            _fakeTakerRepository.Setup(m => m.TakerUpdateTakenDate(takerId, quizId)).ReturnsAsync(model);
            _fakeMapper.Setup(m => m.Map<TakerQuizDto>(model)).Returns(taker);

            //Act
            var result = await _TakerService.TakerUpdateTakenDate(takerId, quizId);

            //Assert
            Assert.Equal(taker, result);
        }

        //TakerUpdateTakenDate ThrowsException
        [Fact]
        public async Task TakerUpdateTakenDate_WhenCalled_ThrowsException()
        {
            // Arrange
            var takerId = It.IsAny<int>();
            var quizId = It.IsAny<int>();
            var taker = new TakerQuizDto();
            var model = new TakerQuiz();

            _fakeTakerRepository.Setup(m => m.TakerUpdateTakenDate(takerId, quizId)).ReturnsAsync(model);
            _fakeMapper.Setup(m => m.Map<TakerQuizDto>(model))
                                    .Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _TakerService.TakerUpdateTakenDate(takerId, quizId));

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //TakerUpdateFinishedDate returns TakerQuizDto
        [Fact]
        public async Task TakerUpdateFinishedDate_WithValidTaker_ReturnsTakerQuizDto()
        {
            // Arrange
            var takerId = It.IsAny<int>();
            var quizId = It.IsAny<int>();
            var id = It.IsAny<int>();
            var taker = new TakerQuizDto
            {
                Id = id,
                TakerId = takerId,
                QuizId = quizId,
                AssignedDate = DateTime.Now,
                Score = 1,
                TakenDate = DateTime.Now,
                FinishedDate = DateTime.Now,
                CanRetake = 0
            };

            var model = new TakerQuiz
            {
                Id = id,
                TakerId = takerId,
                QuizId = quizId,
                AssignedDate = DateTime.Now,
                Score = 1,
                TakenDate = DateTime.Now,
                FinishedDate = DateTime.Now,
                CanRetake = 0
            };

            _fakeTakerRepository.Setup(m => m.TakerUpdateFinishedDate(takerId, quizId)).ReturnsAsync(model);
            _fakeMapper.Setup(m => m.Map<TakerQuizDto>(model)).Returns(taker);

            //Act
            var result = await _TakerService.TakerUpdateFinishedDate(takerId, quizId);

            //Assert
            Assert.Equal(taker, result);
        }

        //TakerUpdateFinishedDate returns Null
        [Fact]
        public async Task TakerUpdateFinishedDate_WhenCalled_ReturnsNull()
        {
            // Arrange
            var takerId = It.IsAny<int>();
            var quizId = It.IsAny<int>();
            var taker = new TakerQuizDto();
            var model = new TakerQuiz();

            _fakeTakerRepository.Setup(m => m.TakerUpdateFinishedDate(takerId, quizId)).ReturnsAsync(model);
            _fakeMapper.Setup(m => m.Map<TakerQuizDto>(model)).Returns(taker);

            //Act
            var result = await _TakerService.TakerUpdateFinishedDate(takerId, quizId);

            //Assert
            Assert.Equal(taker, result);
        }

        //TakerUpdateFinishedDate ThrowsException
        [Fact]
        public async Task TakerUpdateFinishedDate_WhenCalled_ThrowsException()
        {
            // Arrange
            var takerId = It.IsAny<int>();
            var quizId = It.IsAny<int>();
            var taker = new TakerQuizDto();
            var model = new TakerQuiz();

            _fakeTakerRepository.Setup(m => m.TakerUpdateFinishedDate(takerId, quizId)).ReturnsAsync(model);
            _fakeMapper.Setup(m => m.Map<TakerQuizDto>(model))
                                    .Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _TakerService.TakerUpdateFinishedDate(takerId, quizId));

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //DeleteAnswer returns True
        [Fact]
        public async Task DeleteAnswer_WithValidAnswer_ReturnsTrue()
        {
            // Arrange
            var answerId = It.IsAny<int>();

            _fakeTakerRepository.Setup(m => m.DeleteAnswer(answerId)).ReturnsAsync(true);

            //Act
            var result = await _TakerService.DeleteAnswer(answerId);

            //Assert
            Assert.True(result);
        }

        //DeleteAnswer returns False
        [Fact]
        public async Task DeleteAnswer_WhenCalled_ReturnsFalse()
        {
            // Arrange
            var answerId = It.IsAny<int>();

            _fakeTakerRepository.Setup(m => m.DeleteAnswer(answerId)).ReturnsAsync(false);

            //Act
            var result = await _TakerService.DeleteAnswer(answerId);

            //Assert
            Assert.False(result);
        }

        //DeleteAnswer ThrowsException
        [Fact]
        public async Task DeleteAnswer_WhenCalled_ThrowsException()
        {
            // Arrange
            var answerId = It.IsAny<int>();

            _fakeTakerRepository.Setup(m => m.DeleteAnswer(answerId))
                                .Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _TakerService.DeleteAnswer(answerId));

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //GetAnswerDetails returnd TakerAnswersDto
        [Fact]
        public async Task GetAnswerDetails_WithValidAnswer_ReturnsTakerAnswersDto()
        {
            // Arrange
            var id = It.IsAny<int>();
            var takerId = It.IsAny<int>();
            var quizId = It.IsAny<int>();
            var questionId = It.IsAny<int>();
            var taker = new TakerAnswersDto
            {
                Id = id,
                TakerId = takerId,
                QuizId = quizId,
                QuestionId = questionId,
                Answer = "Test Answer"
            };
            var model = new TakerAnswer
            {
                Id = id,
                TakerId = takerId,
                QuizId = quizId,
                QuestionId = questionId,
                Answer = "Test Answer"
            };

            _fakeTakerRepository.Setup(m => m.GetAnswerDetails(takerId, quizId, questionId)).ReturnsAsync(model);
            _fakeMapper.Setup(m => m.Map<TakerAnswersDto>(model)).Returns(taker);

            //Act
            var result = await _TakerService.GetAnswerDetails(takerId, quizId, questionId);

            //Assert
            Assert.Equal(taker, result);
            Assert.NotNull(result);
            Assert.IsType<TakerAnswersDto>(result);
        }

        //GetAnswerDetails returnd Null
        [Fact]
        public async Task GetAnswerDetails_WhenCalled_ReturnsNull()
        {
            // Arrange
            var takerId = It.IsAny<int>();
            var quizId = It.IsAny<int>();
            var questionId = It.IsAny<int>();
            var taker = new TakerAnswersDto();
            var model = new TakerAnswer();

            _fakeTakerRepository.Setup(m => m.GetAnswerDetails(takerId, quizId, questionId)).ReturnsAsync(model);
            _fakeMapper.Setup(m => m.Map<TakerAnswersDto>(model)).Returns(taker);

            //Act
            var result = await _TakerService.GetAnswerDetails(takerId, quizId, questionId);

            //Assert
            Assert.Equal(taker, result);
        }

        //GetAnswerDetails ThrowsException
        [Fact]
        public async Task GetAnswerDetails_WhenCalled_ThrowsException()
        {
            // Arrange
            var takerId = It.IsAny<int>();
            var quizId = It.IsAny<int>();
            var questionId = It.IsAny<int>();
            var taker = new TakerAnswersDto();
            var model = new TakerAnswer();

            _fakeTakerRepository.Setup(m => m.GetAnswerDetails(takerId, quizId, questionId))
                                .Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _TakerService.GetAnswerDetails(takerId, quizId, questionId));

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //ValidateCreate returns True
        [Fact]
        public async Task ValidateCreate_WithValidTaker_ReturnsTrue()
        {
            // Arrange
            var taker = new TakerCreationDto();
            var model = new Taker();

            _fakeTakerRepository.Setup(m => m.ValidateCreate(taker)).ReturnsAsync(true);

            //Act
            var result = await _TakerService.ValidateCreate(taker);

            //Assert
            Assert.True(result);
        }

        //ValidateCreate returns False
        [Fact]
        public async Task ValidateCreate_WhenCalled_ReturnsFalse()
        {
            // Arrange
            var taker = new TakerCreationDto();
            var model = new Taker();

            _fakeTakerRepository.Setup(m => m.ValidateCreate(taker)).ReturnsAsync(false);

            //Act
            var result = await _TakerService.ValidateCreate(taker);

            //Assert
            Assert.False(result);
        }

        //ValidateCreate ThrowsException
        [Fact]
        public async Task ValidateCreate_WhenCalled_ThrowsException()
        {
            // Arrange
            var taker = new TakerCreationDto();
            var model = new Taker();

            _fakeTakerRepository.Setup(m => m.ValidateCreate(taker))
                                .Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _TakerService.ValidateCreate(taker));

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }
    }
}
