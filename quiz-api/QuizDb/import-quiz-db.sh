for i in {1..50};
do
    echo "Attempting to import QuizDb (attempt $i)"
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "Qu1z_wI3z@rD*" -d master -i "quizdb-schema.sql"
    if [ $? -eq 0 ]
    then
        echo "QuizDb imported" && pkill sqlservr 
        sleep 10
        break
    else
        echo "Loading..."
        sleep 1
    fi
done

# restart SQL Server in the foreground
/opt/mssql/bin/sqlservr