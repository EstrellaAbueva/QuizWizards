CREATE PROCEDURE [dbo].[spQuestion_Delete]
    @id INT
AS
BEGIN
    -- Declare variables
    DECLARE @delimiter NVARCHAR(1) = ',';

    -- Delete the parent row
    DELETE FROM Questions WHERE id = @id;

    -- Update the child rows to remove the topic ID from the TopicId column
    UPDATE Topics
    SET QuestionId = CASE
        WHEN QuestionId = @delimiter THEN '0' 
        WHEN QuestionId = CONCAT(@delimiter, CAST(@id AS NVARCHAR(MAX)), @delimiter) THEN @delimiter
        ELSE REPLACE(
            CONCAT(
                @delimiter,
                CAST(QuestionId AS NVARCHAR(MAX)),
                @delimiter
            ),
            CONCAT(
                @delimiter,
                CAST(@id AS NVARCHAR(MAX)),
                @delimiter
            ),
            @delimiter
        )
        END;   

    -- Update the number of question column
    UPDATE Topics
    SET NumberOfQuestions = (
        SELECT COUNT(*)
        FROM Questions
        WHERE Questions.Id IN (
            SELECT value
            FROM STRING_SPLIT(CASE WHEN QuestionId = @delimiter THEN '0' ELSE QuestionId END, @delimiter)
        )
    );

    -- Update the MaxScore column from Quizzes Table
    UPDATE Quizzes 
    SET MaxScore = (
        SELECT NumberOfQuestions FROM Topics WHERE Id = Quizzes.TopicId
    );
END