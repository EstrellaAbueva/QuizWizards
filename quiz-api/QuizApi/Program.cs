using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using QuizApi.Context;
using QuizApi.Repositories;
using QuizApi.Repositories.Quizzes;
using QuizApi.Repositories.Takers;
using QuizApi.Repositories.Topics;
using QuizApi.Services.Questions;
using QuizApi.Services.Quizzes;
using QuizApi.Services.Takers;
using QuizApi.Services.Topics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add services authorization
builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    // Add header documentation in swagger
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Quiz System API",
        Description = "The most effective API for handling student quizzes.",
        Contact = new OpenApiContact
        {
            Name = "Group 6 - F1",
            Url = new Uri("https://github.com/CITUCCS/csit341-final-project-group-6-quiz-wizards/")
        },
    });
    // Feed generated xml api docs to swagger
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Initialize the configuration object
IConfiguration configuration = builder.Configuration;

// Configure our services
ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

void ConfigureServices(IServiceCollection services)
{
    // Add CORS policy
    services.AddCors(options =>
    {
        options.AddDefaultPolicy(builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
    });
    
    // Trasient -> create new instance of DapperContext everytime.
    services.AddTransient<DapperContext>();
    // Configure Automapper
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    //Services
    services.AddScoped<ITakerService, TakerService>();
    services.AddScoped<ITopicService, TopicService>();
    services.AddScoped<IQuizService, QuizService>();
    services.AddScoped<IQuestionService, QuestionService>();

    // Repos
    services.AddScoped<ITakerRepository, TakerRepository>();
    services.AddScoped<ITopicRepository, TopicRepository>();
    services.AddScoped<IQuizRepository, QuizRepository>();
    services.AddScoped<IQuestionRepository, QuestionRepository>();

    // Add authentication
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });
    services.AddMvc();

}