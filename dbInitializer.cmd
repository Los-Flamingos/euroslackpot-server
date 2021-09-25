choco install sqlite
sqlite3 ..\src\API\euroslackpot.db < db\dbInitScript.sql
echo script complete, db has been re-initialized