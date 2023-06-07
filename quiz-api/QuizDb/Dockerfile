FROM mcr.microsoft.com/mssql/server:2022-latest

ENV ACCEPT_EULA=Y

ENV SA_PASSWORD="Qu1z_wI3z@rD*" 

USER root
RUN apt-get -y update  && \
  apt-get install -y dos2unix


RUN mkdir -p /usr/quizwizard-db/sql

COPY . /usr/quizwizard-db/sql

WORKDIR /usr/quizwizard-db/sql

RUN chmod +x /usr/quizwizard-db/sql/import-quiz-db.sh

EXPOSE 1433

USER mssql
ENTRYPOINT ["/bin/bash", "./import-quiz-db.sh"]