CREATE SCHEMA IF NOT EXISTS volunteerManager;
CREATE TABLE IF NOT EXISTS volunteerManager.events(
    id serial PRIMARY KEY,
    name varchar(32) NOT NULL,
    startTime date NOT NULL,
    endTime date NOT NULl
);
CREATE TABLE IF NOT EXISTS volunteerManager.volunteer(
    id serial NOT NULL PRIMARY KEY,
    name varchar(60) NOT NULL,
    shiftsTaken integer DEFAULT 0,
    rating integer DEFAULT 0
);
CREATE TABLE IF NOT EXISTS volunteerManager.shifts(
    id serial NOT NULL PRIMARY KEY,
    eventId bigint REFERENCES volunteerManager.events,
    timestamp varchar(9),
    volunteerId bigint REFERENCES volunteerManager.volunteer
);
GRANT SELECT, INSERT, UPDATE, DELETE
    ON dsr.volunteermanager.events, dsr.volunteermanager.volunteer, dsr.volunteermanager.shifts
    TO dsr_service;