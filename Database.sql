CREATE DATABASE `client_schedule` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

USE `client_schedule`;

CREATE TABLE `country` (
  `countryId` int NOT NULL AUTO_INCREMENT,
  `country` varchar(50) NOT NULL,
  `createDate` datetime NOT NULL,
  `createdBy` varchar(40) NOT NULL,
  `lastUpdate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `lastUpdateBy` varchar(40) NOT NULL,
  PRIMARY KEY (`countryId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `city` (
  `cityId` int NOT NULL AUTO_INCREMENT,
  `city` varchar(50) NOT NULL,
  `countryId` int NOT NULL,
  `createDate` datetime NOT NULL,
  `createdBy` varchar(40) NOT NULL,
  `lastUpdate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `lastUpdateBy` varchar(40) NOT NULL,
  PRIMARY KEY (`cityId`),
  KEY `countryId` (`countryId`),
  CONSTRAINT `city_ibfk_1` FOREIGN KEY (`countryId`) REFERENCES `country` (`countryId`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `address` (
  `addressId` int NOT NULL AUTO_INCREMENT,
  `address` varchar(50) NOT NULL,
  `address2` varchar(50) NOT NULL,
  `cityId` int NOT NULL,
  `postalCode` varchar(10) NOT NULL,
  `phone` varchar(20) NOT NULL,
  `createDate` datetime NOT NULL,
  `createdBy` varchar(40) NOT NULL,
  `lastUpdate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `lastUpdateBy` varchar(40) NOT NULL,
  PRIMARY KEY (`addressId`),
  KEY `cityId` (`cityId`),
  CONSTRAINT `address_ibfk_1` FOREIGN KEY (`cityId`) REFERENCES `city` (`cityId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `customer` (
  `customerId` int NOT NULL AUTO_INCREMENT,
  `customerName` varchar(45) NOT NULL,
  `addressId` int NOT NULL,
  `active` tinyint(1) NOT NULL,
  `createDate` datetime NOT NULL,
  `createdBy` varchar(40) NOT NULL,
  `lastUpdate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `lastUpdateBy` varchar(40) NOT NULL,
  PRIMARY KEY (`customerId`),
  KEY `addressId` (`addressId`),
  CONSTRAINT `customer_ibfk_1` FOREIGN KEY (`addressId`) REFERENCES `address` (`addressId`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `user` (
  `userId` int NOT NULL AUTO_INCREMENT,
  `userName` varchar(50) NOT NULL,
  `password` varchar(50) NOT NULL,
  `active` tinyint NOT NULL,
  `createDate` datetime NOT NULL,
  `createdBy` varchar(40) NOT NULL,
  `lastUpdate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `lastUpdateBy` varchar(40) NOT NULL,
  PRIMARY KEY (`userId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `appointment` (
  `appointmentId` int NOT NULL AUTO_INCREMENT,
  `customerId` int NOT NULL,
  `userId` int NOT NULL,
  `title` varchar(255) NOT NULL,
  `description` text NOT NULL,
  `location` text NOT NULL,
  `contact` text NOT NULL,
  `type` text NOT NULL,
  `url` varchar(255) NOT NULL,
  `start` datetime NOT NULL,
  `end` datetime NOT NULL,
  `createDate` datetime NOT NULL,
  `createdBy` varchar(40) NOT NULL,
  `lastUpdate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `lastUpdateBy` varchar(40) NOT NULL,
  PRIMARY KEY (`appointmentId`),
  KEY `userId` (`userId`),
  KEY `appointment_ibfk_1` (`customerId`),
  CONSTRAINT `appointment_ibfk_1` FOREIGN KEY (`customerId`) REFERENCES `customer` (`customerId`),
  CONSTRAINT `appointment_ibfk_2` FOREIGN KEY (`userId`) REFERENCES `user` (`userId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- delete from appointment where appointmentID > 0;
-- delete from customer where customerid > 0;
-- delete from address where addressid > 0;
-- delete from city where cityid > 0;
-- delete from country where countryid > 0;
-- delete from user where userid > 0;

--
-- populate table `country`
--
INSERT INTO `country` VALUES 
(1,'US',now(),'test',now(),'test'),
(2,'Canada',now(),'test',now(),'test'),
(3,'Norway',now(),'test',now(),'test');
--
-- populate table `city`
--
INSERT INTO `city` VALUES 
(1,'New York',1,now(),'test',now(),'test'),
(2,'Los Angeles',1,now(),'test',now(),'test'),
(3,'Toronto',2,now(),'test',now(),'test'),
(4,'Vancouver',2,now(),'test',now(),'test'),
(5,'Oslo',3,now(),'test',now(),'test');
--
-- populate table `address`
--
INSERT INTO `address` VALUES 
(1,'123 Main','',1,'11111','555-1212',now(),'test',now(),'test'),
(2,'123 Elm','',3,'11112','555-1213',now(),'test',now(),'test'),
(3,'123 Oak','',5,'11113','555-1214',now(),'test',now(),'test');
--
-- populate table `customer`
--
INSERT INTO `customer` VALUES 
(1,'John Doe',1,1,now(),'test',now(),'test'),
(2,'Alfred E Newman',2,1,now(),'test',now(),'test'),
(3,'Ina Prufung',3,1,now(),'test',now(),'test'),
(4,'Ronald McDonald',2,0,now(),'test',now(),'test');
--
-- populate table `user`
--
INSERT INTO `user` VALUES 
(1,'test','test',1,now(),'test',now(),'test');
--
-- populate table `appointment`
--
INSERT INTO `appointment` VALUES 
(1,1,1,'Appt Title 1','Lorem ipsum','Home','Bill','Presentation','NA','2022-06-14 00:00:00','2022-06-14 00:15:00',now(),'test',now(),'test'),
(2,2,1,'Appt Title 2','Lorem ipsum','Away','Jim','Scrum','NA','2022-06-14 00:00:00','2022-06-14 00:15:00',now(),'test',now(),'test'),
(3,1,1,'Appt Title 3','Lorem ipsum','Here','Jeff','Cool bicycle trick','NA','2022-06-14 00:30:00','2022-06-14 00:45:00',now(),'test',now(),'test'),
(4,1,1,'Appt Title 4','Lorem ipsum','There','Ashley','Presentation','NA','2022-06-14 01:00:00','2022-06-14 01:30:00',now(),'test',now(),'test'),
(5,3,1,'Appt Title 5','Lorem ipsum','Over thataway','Jennifer','Juggling','NA','2022-06-14 00:00:00','2022-06-14 00:30:00',now(),'test',now(),'test'),
(6,3,1,'Appt Title 6','Lorem ipsum','Not here','Scott','Fishing','NA','2022-06-15 02:00:00','2022-06-15 02:30:00',now(),'test',now(),'test'),
(7,2,1,'Appt Title 7','Lorem ipsum','Not there','Michael','Soccer game','NA','2022-06-15 00:30:00','2022-06-15 01:00:00',now(),'test',now(),'test'),
(8,1,1,'Appt Title 8','Lorem ipsum','Back home','Holly','Juggling','NA','2022-06-15 02:00:00','2022-06-15 02:30:00',now(),'test',now(),'test'),
(9,2,1,'Appt Title 9','Lorem ipsum','Away again','Dwight','Scrum','NA','2022-06-15 04:00:00','2022-06-15 04:30:00',now(),'test',now(),'test');