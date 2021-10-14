BEGIN TRANSACTION;

CREATE TABLE Player (
	PlayerId INT IDENTITY(1, 1),
	PlayerName NVARCHAR(32) NOT NULL UNIQUE,
	Email NVARCHAR(64) NOT NULL UNIQUE,
	PhoneNumber NVARCHAR(32) NOT NULL UNIQUE,
	PRIMARY KEY(PlayerId)
);

CREATE TABLE Row (
	RowId INT IDENTITY(1, 1),
	Week NVARCHAR(10) NOT NULL,
	Earnings FLOAT NULL,
	PRIMARY KEY (RowId)
)

CREATE TABLE Number (
	NumberId INT IDENTITY(1, 1),
	RowId INT NOT NULL,
	PlayerId INT NOT NULL,
	NumberType INT NOT NULL,
	Value INT NOT NULL,
	PRIMARY KEY (NumberId),
	FOREIGN KEY(RowId) REFERENCES Row(RowId),
	FOREIGN KEY(PlayerId) REFERENCES Player(PlayerId)
);

INSERT INTO
	Player (PlayerName, Email, PhoneNumber)
VALUES
	('Maran', 'maran@euroslackpot.com', '0700111111'),
	('Ellis', 'ellis@euroslackpot.com', '0700222222'),
	('Gullberg', 'gullberg@euroslackpot.com', '0700333333'),
	('Lord Nelson', 'lordnelson@euroslackpot.com', '0700444444'),
	('Heman', 'heman@euroslackpot.com', '0700555555'),
	('Euro Slackpot Admin', 'admin@euroslackpot.com', '0700666666');

INSERT INTO
	Row (Week)
VALUES
	(41),
	(41),
	(42);

INSERT INTO
	Number (RowId, PlayerId, NumberType, Value)
VALUES
	(1, 1, 0, 5),
	(1, 2, 0, 10),
	(1, 3, 0, 15),
	(1, 4, 0, 20),
	(1, 5, 0, 25),
	(1, 1, 1, 5),
	(1, 4, 1, 6),
	(2, 1, 0, 5),
	(2, 2, 0, 10),
	(2, 3, 0, 15),
	(2, 4, 0, 20),
	(2, 5, 0, 25),
	(2, 1, 1, 5),
	(2, 4, 1, 6),
	(3, 1, 0, 5),
	(3, 2, 0, 10),
	(3, 3, 0, 15),
	(3, 4, 0, 20),
	(3, 5, 0, 25),
	(3, 1, 1, 5),
	(3, 4, 1, 6);

COMMIT;