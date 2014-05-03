/*
* Install script for creating the SQL database for RIMS
*/
use [master]
-- check to see if if we have a database
if(exists(select * from sys.databases where name = '$DATABASE_NAME$'))
begin
	print 'drop database'
	
	alter database [$DATABASE_NAME$] set
		SINGLE_USER WITH ROLLBACK IMMEDIATE
	drop database [$DATABASE_NAME$]
end
create database [$DATABASE_NAME$]
