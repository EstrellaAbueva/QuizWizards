CREATE PROCEDURE [dbo].[spTopic_DeleteTopic]
    @id INT
AS
BEGIN
    -- Declare variables
    DECLARE @delimiter NVARCHAR(1) = ',';

    -- Delete the parent row
    DELETE FROM Topics WHERE id = @id;

    -- Update the child rows to remove the topic ID from the TopicId column
    UPDATE Quizzes
    SET TopicId = CASE
        WHEN TopicId = @delimiter THEN '0'  -- If TopicId is only the delimiter, set it to '0'
        ELSE REPLACE(
            CONCAT(
                @delimiter,
                TopicId,
                @delimiter
            ),
            CONCAT(
                @delimiter,
                CAST(@id AS NVARCHAR),
                @delimiter
            ),
            @delimiter
        )
        END
    WHERE CHARINDEX(
        CONCAT(
            @delimiter,
            CAST(@id AS NVARCHAR),
            @delimiter
        ),
        CONCAT(
            @delimiter,
            TopicId,
            @delimiter
        )
    ) > 0;

    -- Update the MaxScore column
    UPDATE Quizzes
    SET MaxScore = (
        SELECT COUNT(*)
        FROM Questions
        WHERE Questions.Id IN (
            SELECT value
            FROM STRING_SPLIT(TopicId, @delimiter)
        )
    );
END
