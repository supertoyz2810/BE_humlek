START TRANSACTION;

ALTER TABLE `Users` ADD `Email` longtext CHARACTER SET utf8mb4 NOT NULL;

ALTER TABLE `Users` ADD `Phone` longtext CHARACTER SET utf8mb4 NOT NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20241105080013_05112024_UpdateTableUser_V1', '8.0.10');

COMMIT;

