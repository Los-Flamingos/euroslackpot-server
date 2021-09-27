DROP TABLE IF EXISTS row_number;
DROP TABLE IF EXISTS player;
DROP TABLE IF EXISTS row;

CREATE TABLE "player" (
	"player_id"	INTEGER,
	"name"	TEXT NOT NULL UNIQUE,
	"created_at"	TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	"modified_at"	TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	PRIMARY KEY("player_id" AUTOINCREMENT)
);

CREATE TABLE "row" (
	"row_id"	INTEGER,
	"week"	TEXT NOT NULL,
	"created_at"	TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	"modified_at"	TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	PRIMARY KEY("row_id" AUTOINCREMENT)
);

CREATE TABLE "row_number" (
	"number_id"	INTEGER,
	"row_id" INTEGER NOT NULL,
	"player_id" INTEGER NOT NULL,
	"type" INTEGER NOT NULL,
	"value" INTEGER NOT NULL,
	"created_at"	TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	"modified_at"	TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	PRIMARY KEY("number_id" AUTOINCREMENT),
	FOREIGN KEY(row_id) REFERENCES row(row_id),
	FOREIGN KEY(player_id) REFERENCES player(player_id)
);

INSERT INTO player (name)
VALUES 
	("Maran"),
	("Ellis"),
	("Gullberg"),
	("Lord Nelson"),
	("Heman");
	
INSERT INTO row (week)
VALUES 
	("W38"),
	("W39");
	
INSERT INTO row_number (row_id, player_id, type, value)
VALUES
	(1, 1, 0, 1),
	(1, 2, 0, 15),
	(1, 3, 0, 25),
	(1, 4, 0, 35),
	(1, 5, 0, 45),
	(1, 3, 1, 2),
	(1, 4, 1, 3);