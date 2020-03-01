CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

CREATE TABLE "Albuns" (
    "Id" serial NOT NULL,
    "Descricao" text NULL,
    "Titulo" text NULL,
    CONSTRAINT "PK_Albuns" PRIMARY KEY ("Id")
);

CREATE TABLE "Usuarios" (
    "Id" serial NOT NULL,
    "Nome" text NULL,
    "Senha" text NULL,
    "Email" text NULL,
    "ChavePublica" text NULL,
    CONSTRAINT "PK_Usuarios" PRIMARY KEY ("Id")
);

CREATE TABLE "Postagens" (
    "Id" serial NOT NULL,
    "AlbumId" integer NULL,
    CONSTRAINT "PK_Postagens" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Postagens_Albuns_AlbumId" FOREIGN KEY ("AlbumId") REFERENCES "Albuns" ("Id") ON DELETE RESTRICT
);

CREATE TABLE "Comentarios" (
    "Id" serial NOT NULL,
    "Descricao" text NULL,
    "PostagemId" integer NULL,
    CONSTRAINT "PK_Comentarios" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Comentarios_Postagens_PostagemId" FOREIGN KEY ("PostagemId") REFERENCES "Postagens" ("Id") ON DELETE RESTRICT
);

CREATE TABLE "Fotos" (
    "Id" serial NOT NULL,
    "Conteudo" text NULL,
    "Descricao" text NULL,
    "PostagemId" integer NULL,
    CONSTRAINT "PK_Fotos" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Fotos_Postagens_PostagemId" FOREIGN KEY ("PostagemId") REFERENCES "Postagens" ("Id") ON DELETE RESTRICT
);

CREATE INDEX "IX_Comentarios_PostagemId" ON "Comentarios" ("PostagemId");

CREATE INDEX "IX_Fotos_PostagemId" ON "Fotos" ("PostagemId");

CREATE INDEX "IX_Postagens_AlbumId" ON "Postagens" ("AlbumId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20191223014303_InitialMigration', '2.1.14-servicing-32113');

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20191223025212_Identity', '2.1.14-servicing-32113');

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20191223025410_Identity_Blog', '2.1.14-servicing-32113');

CREATE TABLE "AspNetRoles" (
    "Id" text NOT NULL,
    "Name" character varying(256) NULL,
    "NormalizedName" character varying(256) NULL,
    "ConcurrencyStamp" text NULL,
    CONSTRAINT "PK_AspNetRoles" PRIMARY KEY ("Id")
);

CREATE TABLE "AspNetUsers" (
    "Id" text NOT NULL,
    "UserName" character varying(256) NULL,
    "NormalizedUserName" character varying(256) NULL,
    "Email" character varying(256) NULL,
    "NormalizedEmail" character varying(256) NULL,
    "EmailConfirmed" boolean NOT NULL,
    "PasswordHash" text NULL,
    "SecurityStamp" text NULL,
    "ConcurrencyStamp" text NULL,
    "PhoneNumber" text NULL,
    "PhoneNumberConfirmed" boolean NOT NULL,
    "TwoFactorEnabled" boolean NOT NULL,
    "LockoutEnd" timestamp with time zone NULL,
    "LockoutEnabled" boolean NOT NULL,
    "AccessFailedCount" integer NOT NULL,
    CONSTRAINT "PK_AspNetUsers" PRIMARY KEY ("Id")
);

CREATE TABLE "AspNetRoleClaims" (
    "Id" serial NOT NULL,
    "RoleId" text NOT NULL,
    "ClaimType" text NULL,
    "ClaimValue" text NULL,
    CONSTRAINT "PK_AspNetRoleClaims" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_AspNetRoleClaims_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AspNetUserClaims" (
    "Id" serial NOT NULL,
    "UserId" text NOT NULL,
    "ClaimType" text NULL,
    "ClaimValue" text NULL,
    CONSTRAINT "PK_AspNetUserClaims" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_AspNetUserClaims_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AspNetUserLogins" (
    "LoginProvider" text NOT NULL,
    "ProviderKey" text NOT NULL,
    "ProviderDisplayName" text NULL,
    "UserId" text NOT NULL,
    CONSTRAINT "PK_AspNetUserLogins" PRIMARY KEY ("LoginProvider", "ProviderKey"),
    CONSTRAINT "FK_AspNetUserLogins_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AspNetUserRoles" (
    "UserId" text NOT NULL,
    "RoleId" text NOT NULL,
    CONSTRAINT "PK_AspNetUserRoles" PRIMARY KEY ("UserId", "RoleId"),
    CONSTRAINT "FK_AspNetUserRoles_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_AspNetUserRoles_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AspNetUserTokens" (
    "UserId" text NOT NULL,
    "LoginProvider" text NOT NULL,
    "Name" text NOT NULL,
    "Value" text NULL,
    CONSTRAINT "PK_AspNetUserTokens" PRIMARY KEY ("UserId", "LoginProvider", "Name"),
    CONSTRAINT "FK_AspNetUserTokens_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_AspNetRoleClaims_RoleId" ON "AspNetRoleClaims" ("RoleId");

CREATE UNIQUE INDEX "RoleNameIndex" ON "AspNetRoles" ("NormalizedName");

CREATE INDEX "IX_AspNetUserClaims_UserId" ON "AspNetUserClaims" ("UserId");

CREATE INDEX "IX_AspNetUserLogins_UserId" ON "AspNetUserLogins" ("UserId");

CREATE INDEX "IX_AspNetUserRoles_RoleId" ON "AspNetUserRoles" ("RoleId");

CREATE INDEX "EmailIndex" ON "AspNetUsers" ("NormalizedEmail");

CREATE UNIQUE INDEX "UserNameIndex" ON "AspNetUsers" ("NormalizedUserName");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20191223025626_Identity_Blog2', '2.1.14-servicing-32113');

DROP TABLE "Usuarios";

ALTER TABLE "AspNetUserTokens" ALTER COLUMN "Name" TYPE character varying(128);
ALTER TABLE "AspNetUserTokens" ALTER COLUMN "Name" SET NOT NULL;
ALTER TABLE "AspNetUserTokens" ALTER COLUMN "Name" DROP DEFAULT;

ALTER TABLE "AspNetUserTokens" ALTER COLUMN "LoginProvider" TYPE character varying(128);
ALTER TABLE "AspNetUserTokens" ALTER COLUMN "LoginProvider" SET NOT NULL;
ALTER TABLE "AspNetUserTokens" ALTER COLUMN "LoginProvider" DROP DEFAULT;

ALTER TABLE "AspNetUserLogins" ALTER COLUMN "ProviderKey" TYPE character varying(128);
ALTER TABLE "AspNetUserLogins" ALTER COLUMN "ProviderKey" SET NOT NULL;
ALTER TABLE "AspNetUserLogins" ALTER COLUMN "ProviderKey" DROP DEFAULT;

ALTER TABLE "AspNetUserLogins" ALTER COLUMN "LoginProvider" TYPE character varying(128);
ALTER TABLE "AspNetUserLogins" ALTER COLUMN "LoginProvider" SET NOT NULL;
ALTER TABLE "AspNetUserLogins" ALTER COLUMN "LoginProvider" DROP DEFAULT;

ALTER TABLE "Albuns" ADD "Id_Dono" integer NOT NULL DEFAULT 0;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20191223072829_Ajustes_DB', '2.1.14-servicing-32113');

ALTER TABLE "Fotos" ADD "Id_Album" integer NOT NULL DEFAULT 0;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20191223073936_Ajustes_DB_2', '2.1.14-servicing-32113');

ALTER TABLE "Fotos" DROP COLUMN "Id_Album";

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20191223075332_Ajustes_DB_3', '2.1.14-servicing-32113');

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20200301174442_InitialCreate', '2.1.14-servicing-32113');