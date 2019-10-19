
\connect app-locker-db

CREATE TABLE app (
  id          SERIAL       PRIMARY KEY,
  code        INTEGER      UNIQUE NOT NULL,
  name        VARCHAR(256) NOT NULL,
  description VARCHAR(256),
  is_locked   BOOLEAN      NOT NULL,
  reason      VARCHAR(256)
);

ALTER TABLE app OWNER TO joe;

INSERT INTO app(code, name, description, is_locked, reason) VALUES(
  123456789,
  'Test App', 
  'A Test App created by the seed.sql file.', 
  true,
  'Invoice has yet to be paid.'
);
