DROP DATABASE IF EXISTS champions ;
CREATE DATABASE champions ;


CREATE TABLE `champions`.`champion` (
  `identifiant` INT NOT NULL AUTO_INCREMENT,
  `nom` VARCHAR(45) NOT NULL,
  `surnom` VARCHAR(45) NOT NULL,
  `identifiantRegion` TINYINT UNSIGNED NOT NULL,
  PRIMARY KEY (`identifiant`),
  UNIQUE INDEX `identifiant_UNIQUE` (`identifiant` ASC));


CREATE TABLE `champions`.`region` (
  `identifiant` TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `nom` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`identifiant`));
  
  
  CREATE TABLE `champions`.`appartient` (
 `identifiantChampion` TINYINT UNSIGNED NOT NULL,
 `identifiantRôle` TINYINT UNSIGNED NOT NULL);
 
 
 CREATE TABLE `champions`.`rôle` (
  `identifiant` TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `nom` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`identifiant`));


ALTER TABLE `champions`.`champion` 
ADD INDEX `FK_Champion_Region_idx` (`identifiantRegion` ASC);


ALTER TABLE `champions`.`appartient` 
ADD INDEX `FK_Appartient_Champion_idx` (`identifiantChampion` ASC),
ADD INDEX `FK_Appartient_Rôle_idx` (`identifiantRôle` ASC);


ALTER TABLE `champions`.`champion` 
ADD CONSTRAINT `FK_Champion_Region`
  FOREIGN KEY (`identifiantRegion`)
  REFERENCES `Champions`.`region` (`identifiant`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  
  
ALTER TABLE `champions`.`appartient` 
ADD CONSTRAINT `FK_Appartient_Champion`
  FOREIGN KEY (`identifiantChampion`)
  REFERENCES `champions`.`appartient` (`identifiant`),
ADD CONSTRAINT `FK_Appartient_Rôle`
  FOREIGN KEY (`identifiantRôle`)
  REFERENCES `champions`.`appartient` (`identifiant`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  
  
  
