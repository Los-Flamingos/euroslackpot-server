BEGIN TRANSACTION;

CREATE TABLE Player (
	PlayerId INT NOT NULL,
	PlayerName NVARCHAR(32) NOT NULL UNIQUE,
	Email NVARCHAR(64) NOT NULL UNIQUE,
	PhoneNumber NVARCHAR(32) NOT NULL UNIQUE,
	PRIMARY KEY(PlayerId)
);

CREATE TABLE Number (
	NumberId INT IDENTITY(1, 1),
	RowId INT NOT NULL,
	PlayerId INT NOT NULL,
	Week NVARCHAR(10) NOT NULL,
	NumberType INT NOT NULL,
	Value INT NOT NULL,
	PRIMARY KEY (NumberId),
	FOREIGN KEY(PlayerId) REFERENCES player(PlayerId)
);

INSERT INTO
	Player (PlayerId, PlayerName)
VALUES
	(1, 'Maran'),
	(2, 'Ellis'),
	(3, 'Gullberg'),
	(4, 'Lord Nelson'),
	(5, 'Heman'),
	(6, 'Euro Slackpot Admin');

INSERT INTO
	Number (
		RowId,
		PlayerId,
		Week,
		NumberType,
		Value
	)
VALUES
	(
		1,
		1,
		'W41',
		0,
		5
	),
	(
		1,
		2,
		'W41',
		0,
		10
	),
	(
		1,
		3,
		'W41',
		0,
		13
	),
	(
		1,
		4,
		'W41',
		0,
		14
	),
	(
		1,
		5,
		'W41',
		0,
		23
	),
	(
		1,
		1,
		'W41',
		1,
		4
	),
	(
		1,
		5,
		'W41',
		1,
		8
	);

COMMIT;