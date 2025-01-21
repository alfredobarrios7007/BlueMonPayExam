USE sandbox
GO

DROP TABLE SessionsLog;
DROP TABLE ctUsers;
DROP TABLE TokensBlackList;

CREATE TABLE ctUsers(
	id int IDENTITY(1, 1) NOT NULL primary key,
	name varchar(250) NOT NULL,
	lastname varchar(250) NOT NULL,
	username varchar(50) NOT NULL,
	password varchar(250) NOT NULL,
	email varchar(250) NULL,
	created_dt datetime NOT NULL,
	status char(1) NOT NULL
) 
GO

CREATE TABLE SessionsLog(
	id int IDENTITY(1, 1) NOT NULL primary key,
	event_dt datetime DEFAULT(SYSDATETIME()) NOT NULL ,
	userId int NOT NULL,
	description varchar(250) NOT NULL,
	successError char(1) NOT NULL,
	CONSTRAINT FK_ctUsers_01 
		FOREIGN KEY (userId)
		REFERENCES ctUsers(id)
) 
GO

CREATE TABLE TokensBlackList(
	id int IDENTITY(1, 1) NOT NULL primary key,
	created_dt datetime DEFAULT(SYSDATETIME()) NOT NULL,
	token varchar(250) NOT NULL
) 
GO


-- select @@SERVERNAME, SYSDATETIME()



--select * from ctUsers
--select * from SessionsLog

