--
-- PostgreSQL database dump
--

-- Dumped from database version 9.6.6
-- Dumped by pg_dump version 9.6.8

-- Started on 2018-04-02 17:35:25 BST

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 3045 (class 0 OID 16406)
-- Dependencies: 186
-- Data for Name: Teams; Type: TABLE DATA; Schema: public; Owner: penguin
--

COPY public."Teams" ("Id", "Drawn", "Lost", "Name", "Stadium", "Won") FROM stdin;
2dbacd33-6b02-4ba6-b0ab-296bfa874cac	0	0	Fulchester Rovers	\N	15
43c00fd7-a5d9-4bd4-9928-4020f73ebc56	2	5	Melchester Town	\N	8
a0e469aa-9dda-4ef1-af4f-43e099f31b0b	2	13	Pretty Shitty City	\N	1
\.


--
-- TOC entry 3044 (class 0 OID 16401)
-- Dependencies: 185
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: penguin
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
20180401180933_InitialMigration	2.0.2-rtm-10011
\.


-- Completed on 2018-04-02 17:35:29 BST

--
-- PostgreSQL database dump complete
--

