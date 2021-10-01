DROP TABLE IF EXISTS number;
DROP TABLE IF EXISTS player;
 
CREATE TABLE "player" (
        "Id"     INTEGER,
        "Name"  TEXT NOT NULL UNIQUE,
        "CreatedAt"    TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "ModifiedAt"   TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        PRIMARY KEY("Id" AUTOINCREMENT)
);
 
CREATE TABLE "number" (
		"Id" INTEGER NOT NULL UNIQUE,
		"Week" TEXT NOT NULL,
        "RowId" INTEGER NOT NULL,
        "PlayerId" INTEGER NOT NULL,
        "Type" INTEGER NOT NULL,
        "Value" INTEGER NOT NULL,
        "CreatedAt"    TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "ModifiedAt"   TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        PRIMARY KEY("Id" AUTOINCREMENT),
        FOREIGN KEY(PlayerId) REFERENCES player(Id)
);
 
INSERT INTO player (name)
VALUES
        ("Maran"),
        ("Ellis"),
        ("Gullberg"),
        ("Lord Nelson"),
        ("Heman");
 
INSERT INTO number (Week, RowId, PlayerId, Type, Value)
VALUES
        (38, 1, 1, 0, 5),
        (38, 1, 2, 0, 10),
        (38, 1, 3, 0, 15),
        (38, 1, 4, 0, 20),
        (38, 1, 5, 0, 25),
        (38, 1, 1, 1, 5),
        (38, 1, 3, 1, 7),
		(39, 2, 1, 0, 5),
        (39, 2, 2, 0, 10),
        (39, 2, 3, 0, 15),
        (39, 2, 4, 0, 20),
        (39, 2, 5, 0, 25),
        (39, 2, 1, 1, 5),
        (39, 2, 3, 1, 7);