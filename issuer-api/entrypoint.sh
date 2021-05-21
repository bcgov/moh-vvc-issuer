#!/bin/bash
echo "Running the migrations..."
#psql -d postgres -f databaseMigration.sql

if [ -n $(printenv database-name) ]
then
export PGPASSWORD=$(printenv database-password)
export POSTGRESQL_USERNAME=$(printenv database-user)
export POSTGRESQL_DATABASE=$(printenv database-name)
fi

if [ -z "${DB_CONNECTION_STRING}" ]
then
export DB_CONNECTION_STRING="host=${DB_HOST};port=5432;database=${POSTGRESQL_DATABASE};username=${POSTGRESQL_USERNAME};password=${PGPASSWORD}"
fi
export AUTH=$(printf $PHARMANET_API_USERNAME:$PHARMANET_API_PASSWORD|base64)
export logfile=prime.logfile.out
# Wait for database connection
function PG_IS_READY() {
psql -h $DB_HOST -U ${POSTGRESQL_USERNAME} -d ${POSTGRESQL_DATABASE} -t -c "select 'READY'" | awk '{print $1}'
}

until PG_IS_READY | grep -m 1 "READY";
do
    echo "Waiting for the database ..." ;
    sleep 3 ;
done

psql -h $DB_HOST -U ${POSTGRESQL_USERNAME} -d ${POSTGRESQL_DATABASE} -a -f ./out/databaseMigrations.sql

echo "Resting 5 seconds to let things settle down..."
echo "Running .NET..."
dotnet ./out/issuer.API.dll -v 2>&1> $logfile &
echo "Launched, waiting for connection to API internally..."

function waitForIt() {
until [[ "$response" -eq "$2" ]]
do
    echo "Waiting for the host ..." ;
    sleep 1 ;
    response=`curl -s -o /dev/null -w "%{http_code}" $1`
done
echo "$1 responded $2"
}

waitForIt localhost:${API_PORT}/api/patients 401 2>&1 | logger

echo -e "\nThe system is up."

tail -f $logfile
