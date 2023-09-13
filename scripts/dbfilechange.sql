CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `files` (
    `Name` varchar(250) CHARACTER SET utf8mb4 NOT NULL,
    `Size` bigint NOT NULL,
    `LastModified` datetime(6) NULL,
    CONSTRAINT `PK_files` PRIMARY KEY (`Name`)
) CHARACTER SET=utf8mb4;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20230912010943_Initial', '7.0.10');

COMMIT;

