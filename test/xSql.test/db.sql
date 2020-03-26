CREATE USER testuser WITH
    LOGIN
    NOSUPERUSER
    NOCREATEDB
    NOCREATEROLE
    INHERIT
    NOREPLICATION
    CONNECTION LIMIT -1
    PASSWORD 'hello world';

CREATE DATABASE testdb
    WITH 
    OWNER = testuser
    ENCODING = 'UTF8'
    CONNECTION LIMIT = -1;

\c defaultdatabase

create table (
    username varchar(250) not null ,
    password varchar(250) not null 
);

GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO testuser;
GRANT USAGE, SELECT ON ALL SEQUENCES IN SCHEMA public TO testuser;