CREATE SCHEMA IF NOT EXISTS volunteer_management;
SET SCHEMA 'volunteer_management';

CREATE TABLE event (
    event_id      BIGSERIAL PRIMARY KEY ,
    event_name    VARCHAR(50) NOT NULL ,
    date          DATE NOT NULL ,
    start_time    TIME NOT NULL ,
    end_time      TIME NOT NULL ,
    location      VARCHAR(50)
);

