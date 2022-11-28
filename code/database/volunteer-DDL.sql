SET SCHEMA 'volunteer_management';
-- EVENT TABLE MUST BE MADE BEFORE VOLUNTEER-DDL IS RAN

CREATE TABLE IF NOT EXISTS volunteer(
    volunteer_id BIGSERIAL PRIMARY KEY,
    full_name varchar(128) NOT NULL,
    shifts_taken integer DEFAULT 0,
    rating integer DEFAULT 0
);

CREATE TABLE IF NOT EXISTS manager (
    manager_id BIGSERIAL PRIMARY KEY,
    volunteer_id BIGINT UNIQUE REFERENCES volunteer.volunteer_id
);

CREATE TABLE IF NOT EXISTS administrator (
    administrator_id BIGSERIAL PRIMARY KEY,
    manager_id BIGINT UNIQUE REFERENCES manager.manager_id
);

-- creating a table to keep track of multiple volunteers per event
-- this could be altered to become a shift instead
CREATE TABLE IF NOT EXISTS event_participant (
    event_id BIGINT REFERENCES event.event_id,
    volunteer_id BIGINT REFERENCES volunteer.volunteer_id,
    PRIMARY KEY (event_id, volunteer_id)
);

-- creating a table to keep track of who manages the event
CREATE TABLE IF NOT EXISTS event_manager (
    event_id BIGINT REFERENCES event.event_id,
    manager_id BIGINT REFERENCES manager.manager_id,
    PRIMARY KEY (event_id, manager_id)
);

--CREATE TABLE IF NOT EXISTS shifts(
--    id serial NOT NULL PRIMARY KEY,
--    eventId bigint REFERENCES volunteerManager.events,
--    timestamp varchar(9),
--    volunteerId bigint REFERENCES volunteerManager.volunteer
--);

-- is this necessary?
--GRANT SELECT, INSERT, UPDATE, DELETE
--    ON dsr.volunteermanager.volunteer, dsr.volunteermanager.volunteer, dsr.volunteermanager.shifts
--    TO dsr_service;
