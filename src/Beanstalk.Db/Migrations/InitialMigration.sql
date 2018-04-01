CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" varchar(150) NOT NULL,
    "ProductVersion" varchar(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);


DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180401180933_InitialMigration') THEN
    CREATE TABLE "Teams" (
        "Id" uuid NOT NULL,
        "Drawn" int4 NOT NULL,
        "Lost" int4 NOT NULL,
        "Name" text NULL,
        "Stadium" text NULL,
        "Won" int4 NOT NULL,
        CONSTRAINT "PK_Teams" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180401180933_InitialMigration') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20180401180933_InitialMigration', '2.0.2-rtm-10011');
    END IF;
END $$;
