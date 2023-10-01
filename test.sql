CREATE TABLE IF NOT EXISTS
test (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    text_field TEXT NOT NULL,
    number INTEGER,
    number2 REAL,
    data BLOB
);

INSERT INTO test (text_field, number, number2, data)
VALUES (
   "this is a text field",
   123,
   456.7,
   '{"foo":"bar"}'
);
