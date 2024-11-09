CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `CategoryFoods` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `CategoryFoodName` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
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
    CONSTRAINT `PK_CategoryFoods` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Orders` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `OrderCode` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
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
    CONSTRAINT `PK_Orders` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Restaurants` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `RestaurantName` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `RestaurantAddress` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `RestaurantLinkImage` longtext CHARACTER SET utf8mb4 NOT NULL,
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
    CONSTRAINT `PK_Restaurants` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Users` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `UserName` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `UserPassword` longtext CHARACTER SET utf8mb4 NOT NULL,
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
    CONSTRAINT `PK_Users` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Foods` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `RestaurantId` char(36) COLLATE ascii_general_ci NOT NULL,
    `CategoryFoodId` char(36) COLLATE ascii_general_ci NOT NULL,
    `FoodName` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `FoodPrice` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `FoodLinkImage` longtext CHARACTER SET utf8mb4 NOT NULL,
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
    CONSTRAINT `PK_Foods` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Foods_CategoryFoods_CategoryFoodId` FOREIGN KEY (`CategoryFoodId`) REFERENCES `CategoryFoods` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_Foods_Restaurants_RestaurantId` FOREIGN KEY (`RestaurantId`) REFERENCES `Restaurants` (`Id`) ON DELETE RESTRICT
) CHARACTER SET=utf8mb4;

CREATE TABLE `OrderDetails` (
    `Id` char(36) COLLATE ascii_general_ci NOT NULL,
    `FoodId` char(36) COLLATE ascii_general_ci NOT NULL,
    `OrderId` char(36) COLLATE ascii_general_ci NOT NULL,
    `userId` char(36) COLLATE ascii_general_ci NOT NULL,
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
    CONSTRAINT `PK_OrderDetails` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_OrderDetails_Foods_FoodId` FOREIGN KEY (`FoodId`) REFERENCES `Foods` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_OrderDetails_Orders_OrderId` FOREIGN KEY (`OrderId`) REFERENCES `Orders` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_OrderDetails_Users_userId` FOREIGN KEY (`userId`) REFERENCES `Users` (`Id`) ON DELETE RESTRICT
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_Foods_CategoryFoodId` ON `Foods` (`CategoryFoodId`);

CREATE INDEX `IX_Foods_RestaurantId` ON `Foods` (`RestaurantId`);

CREATE INDEX `IX_OrderDetails_FoodId` ON `OrderDetails` (`FoodId`);

CREATE INDEX `IX_OrderDetails_OrderId` ON `OrderDetails` (`OrderId`);

CREATE INDEX `IX_OrderDetails_userId` ON `OrderDetails` (`userId`);

CREATE UNIQUE INDEX `IX_Orders_OrderCode` ON `Orders` (`OrderCode`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20241025081856_25102024_CreateDataBase_V1', '8.0.10');

COMMIT;

