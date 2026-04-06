-- Library Management Database Migration
-- Run this script in phpMyAdmin (SQL tab)

CREATE DATABASE IF NOT EXISTS `library_db`
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;

USE `library_db`;

-- Drop old tables for a clean re-run
DROP TABLE IF EXISTS `Borrowings`;
DROP TABLE IF EXISTS `Books`;

CREATE TABLE `Books` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Titre` VARCHAR(255) NOT NULL,
  `Auteur` VARCHAR(255) NOT NULL,
  `ISBN` VARCHAR(20) NOT NULL,
  `Annee` INT NULL,
  `Genre` VARCHAR(100) NULL,
  `Rayon` VARCHAR(100) NULL,
  `Etagere` VARCHAR(100) NULL,
  `Dispo` TINYINT(1) NOT NULL DEFAULT 1,
  `CoverPath` VARCHAR(500) NULL,
  `CreatedAt` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedAt` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UQ_Books_ISBN` (`ISBN`),
  INDEX `IX_Books_Titre` (`Titre`),
  INDEX `IX_Books_Auteur` (`Auteur`),
  INDEX `IX_Books_Genre` (`Genre`)
) ENGINE=InnoDB;

CREATE TABLE `Borrowings` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `BookId` INT NOT NULL,
  `BorrowerName` VARCHAR(255) NOT NULL,
  `BorrowDate` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `DueDate` DATETIME NULL,
  `ReturnDate` DATETIME NULL,
  `Notes` VARCHAR(500) NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_Borrowings_BookId` (`BookId`),
  CONSTRAINT `FK_Borrowings_Books`
    FOREIGN KEY (`BookId`) REFERENCES `Books`(`Id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE
) ENGINE=InnoDB;

-- Optional seed data for quick UI testing
INSERT INTO `Books` (`Titre`, `Auteur`, `ISBN`, `Annee`, `Genre`, `Rayon`, `Etagere`, `Dispo`)
VALUES
  ('Clean Code', 'Robert C. Martin', '9780132350884', 2008, 'Software', 'A', 'A1', 1),
  ('The Pragmatic Programmer', 'Andrew Hunt', '9780135957059', 2019, 'Software', 'A', 'A2', 1),
  ('Design Patterns', 'Erich Gamma', '9780201633610', 1994, 'Software', 'B', 'B1', 1);
