CREATE TABLE `fide_joueurs` (
  `idJoueur` INT NOT NULL,
  `Nom` VARCHAR(45) NOT NULL,
  `Prenom` VARCHAR(45) NOT NULL,
  `Victoires` INT UNSIGNED NOT NULL DEFAULT 0,
  `Defaites` INT UNSIGNED NOT NULL DEFAULT 0,
  PRIMARY KEY (`idJoueur`),
  UNIQUE INDEX `idJoueur_UNIQUE` (`idJoueur` ASC) VISIBLE);