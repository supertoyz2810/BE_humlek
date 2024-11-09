START TRANSACTION;

CREATE TABLE `UserRoles` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `Role` int NOT NULL,
    `UserId` char(36) COLLATE ascii_general_ci NOT NULL,
    `Note` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `CreatedUser` char(36) COLLATE ascii_general_ci NOT NULL,
    `CreatedName` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `UpdatedDate` datetime(6) NOT NULL,
    `UpdatedUser` char(36) COLLATE ascii_general_ci NOT NULL,
    `UpdatedName` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `DeletedDate` datetime(6) NULL,
    `DeletedUser` char(36) COLLATE ascii_general_ci NULL,
    `DeletedName` varchar(255) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_UserRoles` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_UserRoles_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE RESTRICT
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_UserRoles_UserId` ON `UserRoles` (`UserId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20241104074453_04112024_CreateTable_UserRole_V1', '8.0.10');

COMMIT;

