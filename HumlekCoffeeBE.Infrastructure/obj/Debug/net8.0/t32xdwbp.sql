START TRANSACTION;

DROP TABLE `UserRoles`;

DELETE FROM `__EFMigrationsHistory`
WHERE `MigrationId` = '20241104074453_04112024_CreateTable_UserRole_V1';

COMMIT;

