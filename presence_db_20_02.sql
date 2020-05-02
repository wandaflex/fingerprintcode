-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1
-- Généré le : jeu. 30 avr. 2020 à 20:57
-- Version du serveur :  10.4.11-MariaDB
-- Version de PHP : 7.4.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `presence_db`
--

DELIMITER $$
--
-- Procédures
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `AdmimDeleteByID` (`_AdminID` INT)  BEGIN
	update administrateur 
    set visible = false
    where idADMINISTRATEUR=_AdminID;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `AdminAddOrEdit` (`_AdminID` INT, `_AdminNom` VARCHAR(24), `_AdminPremon` VARCHAR(24), `_AdminType` VARCHAR(24), `_AdminLogin` VARCHAR(45), `_AdminPwd` VARCHAR(45))  BEGIN
	if _AdminID = 0 then
		INSERT INTO administrateur (Nom,Prenom,Login,Password,type_utilisateur,visible)
		VALUES (_AdminNom,_AdminPremon,_AdminLogin,_AdminPwd,_AdminType,true);
	else
		UPDATE administrateur
        SET
			Nom=_AdminNom,
			Prenom = _AdminPremon,
            Login = _AdminLogin,
            Password = _AdminPwd,
            type_utilisateur = _AdminType
		where idADMINISTRATEUR = _AdminID;
	END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `AdminSearchByValue` (`_SearchValue` VARCHAR(45))  BEGIN
	SELECT * 
	FROM administrateur
    WHERE (Nom like concat('%',_SearchValue,'%') || Prenom like concat('%',_SearchValue,'%')) AND visible = true;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `AdminViewAll` ()  BEGIN
	select *
    From administrateur
    where visible = true;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `adminViewByID` (`_AdminID` INT)  BEGIN
	select * 
    From Administrateur
    where idADMINISTRATEUR = _AdminID;    
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `ClasseAddOrEdit` (`_ClasseID` INT, `_ClasseCode` VARCHAR(24), `_ClasseNom` VARCHAR(24), `_ClasseCycle` INT(2), `_ClasseDescription` VARCHAR(24))  BEGIN
	if _ClasseID = 0 then
		INSERT INTO classe (Code,Nom,Cycle,Description,visible)
		VALUES (_ClasseCode,_ClasseNom,_ClasseCycle,_ClasseDescription,true);
	else
		UPDATE classe
        SET
		Code=_ClasseCode,
            	Nom = _ClasseNom,
            	Cycle = _ClasseCycle,
		Description = _ClasseDescription            
		where idCLASSE = _ClasseID;
	END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `ClasseDeleteByID` (`_ClasseID` INT)  BEGIN
	update Classe
    set visible = false
    where idCLASSE =_ClasseID;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `ClasseSearchByValue` (`_SearchValue` VARCHAR(45))  BEGIN
	SELECT * 
	FROM classe
    WHERE (Nom like concat('%',_SearchValue,'%')|| Code like concat('%',_SearchValue,'%') || Cycle like concat('%',_SearchValue,'%'))and visible = true;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `ClasseViewAll` ()  BEGIN
	select * 
    From Classe
    where visible = true;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `ClasseViewByID` (`_ClasseID` INT)  BEGIN
	select * 
    From Classe
    where idCLASSE = _ClasseID
    and visible = true;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `MatiereAddOrEdit` (`_MatiereID` INT, `_MatiereCode` VARCHAR(10), `_MatiereNom` VARCHAR(10))  BEGIN
	if _MatiereID = 0 then
		INSERT INTO Matiere (Code,Nom,visible)
		VALUES (_MatiereCode,_MatiereNom,true);
	else
		UPDATE Matiere
        SET
			Code=_MatiereCode,
            Nom = _MatiereNom

		where idMatiere = _MatiereID;
	END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `MatiereCycleComboViewAll` ()  BEGIN
	select idCYCLE, CONCAT(NumeroCycle, ' ', Description) cycle
    From cycle
    where visible = true;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `MatiereDeleteByID` (`_MatiereID` INT)  BEGIN
	update Matiere
    set visible = false
    where idMATIERE =_MatiereID;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `MatiereProfMatiereComboViewAll` ()  BEGIN
	select idMatiere, concat(code , ' : ', nom) "Matiere"
    From matiere     
    where visible = true;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `MatiereProfProfComboViewAll` ()  BEGIN
	select idProfesseur, concat(idProfesseur,' ',Nom,'  ',prenom) "Professeur"
    From professeur     
    where visible = true;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `MatiereSearchByValue` (`_SearchValue` VARCHAR(45))  BEGIN
	SELECT * 
	FROM matiere
    WHERE (Nom like concat('%',_SearchValue,'%')|| Code like concat('%',_SearchValue,'%'))and visible = true;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `MatiereViewAll` ()  BEGIN
	select idMAtiere,code,Nom,visible
    From Matiere 
    where visible = true;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `ProfAddOrEdit` (`_ProfID` INT, `_ProfNom` VARCHAR(20), `_ProfPrenom` VARCHAR(20), `_ProfTel_1` VARCHAR(10), `_ProfTel_2` VARCHAR(10), `_Diplome` VARCHAR(244), `_ProfType` VARCHAR(45), `_ProfEmpreinte_1` VARCHAR(45), `_ProfEmpreinte_2` VARCHAR(45), `_ProfProfil` VARCHAR(45), `_ProfETaux_Horaire_1` DECIMAL(2,0), `_ProfETaux_Horaire_2` DECIMAL(2,0), `_profDate_recrutement` DATE, `_Description` VARCHAR(254))  BEGIN
	if _ProfID = 0 then
		INSERT INTO Professeur (Nom ,Prenom, Telephone ,Telephone_2,Diplome,TypeProfesseur, Empreinte_1, Empreinte_2 , Photo_profil, Taux_horaire_1, Taux_horaire_2, Description,Date_recrutement,visible)
		VALUES (_ProfNom, _ProfPrenom,_ProfTel_1, _ProfTel_2,_Diplome,_ProfType, _ProfEmpreinte_1, _ProfEmpreinte_2, _ProfProfil, _ProfETaux_Horaire_1, _ProfETaux_Horaire_2, _Description,_profDate_recrutement,true);
	else
		UPDATE Professeur
        SET
			nom=_ProfNom,
			Prenom = _ProfPrenom,
            Telephone = _ProfTel_1,
            Telephone_2 = _ProfTel_2,
            Diplome = _Diplome,
            TypeProfesseur =_ProfType,
            Empreinte_1 = _ProfEmpreinte_1,
            Empreinte_2 = _ProfEmpreinte_2,            
            Photo_profil = _ProfProfil, 
            Taux_horaire_1 = _ProfETaux_Horaire_1, 
            Taux_horaire_2 = _ProfETaux_Horaire_2, 
            Date_recrutement=_profDate_recrutement,
            Description = _Description
		where idProfesseur = _ProfID;
	END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `ProfDeleteByID` (`_ProfID` INT)  BEGIN
	update Professeur
    set visible = false
    where idPROFESSEUR=_ProfID;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `ProfMatiereAddOrEdit` (`_idProfMatiere` INT(11), `_idProf` INT(11), `_idMatiere` INT(11))  BEGIN
	if _idProfMatiere = 0 then
		INSERT INTO professeur_matiere (idProfesseur,idMatiere,visible)
		VALUES (_idProf,_idMatiere,true);
	else
		UPDATE professeur_matiere
        SET
				idProfesseur=_idProf,
            	idMatiere = _idMatiere     
		where idProfesseur = _idProfMatiere;
	END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `ProfMatiereDeleteByID` (`_ProfMatiereID` INT)  BEGIN
	update Professeur_matiere
    set visible = false
    where idPROFESSEUR_MATIERE=_ProfMatiereID;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `ProfSearchByValue` (`_SearchValue` VARCHAR(45))  BEGIN
	SELECT * 
	FROM professeur
    WHERE Nom like concat('%',_SearchValue,'%') || Prenom like concat('%',_SearchValue,'%')|| idPROFESSEUR like concat('%',_SearchValue,'%');
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `ProfViewAll` ()  BEGIN
	select idPROFESSEUR, Nom ,Prenom, Telephone ,Telephone_2,Diplome,TypeProfesseur, Empreinte_1, Empreinte_2, Photo_profil, Taux_horaire_1, Taux_horaire_2, Description,Date_recrutement,visible
    From professeur
    where visible = true;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `ProfViewByID` (`_ProfID` INT)  BEGIN
	select * 
    From professeur
    where idPROFESSEUR = _ProfID;    
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `ProgrammeAddOrEdit` (`_ProgrammesID` INT, `_ProgrammeDate` DATE, `_ProgrammeHeureDebut` TIME, `_ProgrammeHeureFin` TIME, `_ProgrammeIDClasse` INT(11), `_ProgrammeIDAdmin` INT(11), `_ProgrammeIDProfMatiere` INT(11))  BEGIN
	if _ProgrammesID = 0 then
		INSERT INTO Programmes(Date,Heure_Debut,Heure_Fin,CLASSE_idCLASSE,idADMINISTRATEUR,idPROFESSEUR_MATIERE,visible)
		VALUES (_ProgrammeDate,_ProgrammeHeureDebut,_ProgrammeHeureFin,_ProgrammeIDClasse,_ProgrammeIDAdmin,_ProgrammeIDProfMatiere,true);
	else
		UPDATE programmes
        SET
			Date=_ProgrammeDate,
			Heure_Debut = _ProgrammeHeureDebut,
            Heure_Fin = _ProgrammeHeureFin,
            CLASSE_idCLASSE = _ProgrammeIDClasse,
            idADMINISTRATEUR = _ProgrammeIDAdmin,
            idPROFESSEUR_MATIERE =_ProgrammeIDProfMatiere
		where idPROGRAMMES = _ProgrammesID;
	END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `ProgrammeClasseComboViewAll` ()  BEGIN
	select idClasse, nom
    From classe
    where visible = true;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `ProgrammeMatiereComboViewAll` ()  BEGIN
	select idPROFESSEUR_MATIERE, concat((select concat(nom,' ',prenom) from professeur p where p.idProfesseur=pm.idProfesseur  and p.visible=true),'         ',(select m.code from matiere m where m.idMatiere=pm.idMatiere  and m.visible=true)) "ProfMatiere"
    From professeur_matiere pm
    where pm.visible = true;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `ProgrammesDeleteByID` (`_ProgrammesID` INT)  BEGIN
	update Programmes
    set visible = false
    where idProgrammes =_ProgrammesID;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `ProgrammesSearchByValue` (`_SearchValue` VARCHAR(45))  BEGIN
	SELECT *
	FROM programmes
    WHERE (Date like concat('%',_SearchValue,'%')) AND visible = true;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `ProgrammesViewAll` ()  BEGIN
	select *
    From programmes
    where visible = true;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `ProgrammesViewByID` (`_ProgrammesID` INT)  BEGIN
	select * 
    From Programmes
    where idProgrammes = _ProgrammesID
    and visible = true;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `ProgrammeViewFrorein` ()  BEGIN
select p.idProgrammes, p.Date,p.Heure_Debut,p.Heure_Fin, c.nom "Classe",concat(pr.nom,' ',pr.prenom,'     ', m.code ) " Professeur matière",p.visible
    From programmes p, classe c, administrateur a,professeur pr,matiere m,professeur_matiere pm
    where p.CLASSE_idCLASSE = c.idCLASSE and
		p.idADMINISTRATEUR = a.idADMINISTRATEUR and
        p.idPROFESSEUR_MATIERE = pm.idPROFESSEUR_MATIERE and 
        pm.idPROFESSEUR = pr.idPROFESSEUR and 
        pm.idMATIERE = m.idMATIERE  and 
	p.visible = true and c.visible = true and pr.visible = true and m.visible = true and pm.visible = true;
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Structure de la table `administrateur`
--

CREATE TABLE `administrateur` (
  `idADMINISTRATEUR` int(11) NOT NULL,
  `Nom` varchar(24) DEFAULT NULL,
  `Prenom` varchar(24) DEFAULT NULL,
  `Login` varchar(45) DEFAULT NULL,
  `PassWord` varchar(45) DEFAULT NULL,
  `type_utilisateur` varchar(45) DEFAULT NULL,
  `visible` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `administrateur`
--

INSERT INTO `administrateur` (`idADMINISTRATEUR`, `Nom`, `Prenom`, `Login`, `PassWord`, `type_utilisateur`, `visible`) VALUES
(1, 'raoul', 'buguem', 'raoul', 'raoul', NULL, 0),
(2, 'boris1', 'boris', 'boris', 'borisfgfc', NULL, 0),
(3, 'admin', 'admin', 'admin', 'admin', NULL, 0),
(4, 'hgkjhygj', 'hnjkh', 'hguyg', 'jikhg', NULL, 0),
(5, 'nvnhgfhgf54675', 'mnm', 'vbmnb', 'vmnhv', NULL, 0),
(6, 'jhkujh', 'hkj', 'mkh', 'hklhujlju', NULL, 0),
(7, 'zcv', 'zdv', 'cv', 'cbcb', NULL, 0),
(8, 'ergew', 'etre', 'er', 'ere', NULL, 0),
(9, 'ergew', 'etre', 'er', 'ere', NULL, 0),
(10, 'ergew', 'etre', 'er', 'ere', NULL, 1),
(11, 'ergew', 'etreadsd', 'er', 'ere', NULL, 1),
(12, 'SDKJFK88', 'DMFN', 'DMFN', 'SFN', 'Administrateur', 0),
(13, 'zxcdaskjnkjjk', 'sdfv', 'asdf', 'asdckjkjkj', 'Utilisateur', 0),
(14, 'zxcdas', 'sdfv', 'asdf', 'asdc', NULL, 0),
(15, 'sdflad', 'asldk', 'Sdfa', 'asdf', NULL, 0),
(16, 'ghgkjdvlkjd', 'hjh', 'hjh', 'jkh', 'Utilisateur', 1),
(17, 'ghg', 'hjh', 'hjh', 'jkh', 'Administrateur', 1),
(18, 'ljoljk', ',uhiuh', 'khbkbh', 'jkjuhuo', 'Administrateur', 1),
(19, 'ZOGONA', 'Jonathan', 'jozog01', 'venise235@dd', 'Administrateur', 1),
(20, '', '', '', '', 'Utilisateur', 1);

-- --------------------------------------------------------

--
-- Structure de la table `classe`
--

CREATE TABLE `classe` (
  `idCLASSE` int(11) NOT NULL,
  `Nom` varchar(45) DEFAULT NULL,
  `Code` varchar(45) DEFAULT NULL,
  `Cycle` int(2) NOT NULL,
  `Description` varchar(45) DEFAULT NULL,
  `Visible` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `classe`
--

INSERT INTO `classe` (`idCLASSE`, `Nom`, `Code`, `Cycle`, `Description`, `Visible`) VALUES
(1, '1er C', '1 D', 2, 'premiere C', 1),
(2, NULL, 'vjh', 2, 'jhjh', 0),
(3, NULL, 'jhjh', 1, 'True', 0),
(4, NULL, '6u', 2, '', 0),
(5, '6 ieme b2', '6 b 2', 1, 'JAKSJFSFDWS', 1),
(6, '5 ieme b1', '5 b 1', 1, 'eg', 1),
(7, 'jkck', 'cva', 2, 'zc', 1);

-- --------------------------------------------------------

--
-- Structure de la table `matiere`
--

CREATE TABLE `matiere` (
  `idMATIERE` int(11) NOT NULL,
  `Code` varchar(10) NOT NULL,
  `Nom` varchar(20) NOT NULL,
  `visible` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `matiere`
--

INSERT INTO `matiere` (`idMATIERE`, `Code`, `Nom`, `visible`) VALUES
(1, 'math', 'Mathimetiq', 1),
(2, 'pct', 'ph ch tech', 1),
(3, 'ec', 'education', 1),
(4, 'HG', 'HistGeo', 1),
(5, 'Ang', 'Anglais', 1),
(6, '', '', 1),
(7, 'philo', 'philosophi', 1),
(8, '', '', 1);

-- --------------------------------------------------------

--
-- Structure de la table `presence`
--

CREATE TABLE `presence` (
  `idPRESENCE` int(11) NOT NULL,
  `HeureDebut` time DEFAULT NULL,
  `HeureFin` time DEFAULT NULL,
  `PROGRAMMES_idPROGRAMMES` int(11) NOT NULL,
  `visible` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `professeur`
--

CREATE TABLE `professeur` (
  `idPROFESSEUR` int(11) NOT NULL,
  `Nom` varchar(20) CHARACTER SET latin1 DEFAULT NULL,
  `Prenom` varchar(20) CHARACTER SET latin1 DEFAULT NULL,
  `Telephone` varchar(10) CHARACTER SET latin1 DEFAULT NULL,
  `Telephone_2` varchar(10) CHARACTER SET latin1 DEFAULT NULL,
  `Diplome` varchar(255) DEFAULT NULL,
  `TypeProfesseur` varchar(45) CHARACTER SET latin1 DEFAULT NULL,
  `Empreinte_1` varchar(45) CHARACTER SET latin1 DEFAULT NULL,
  `Empreinte_2` varchar(45) CHARACTER SET latin1 DEFAULT NULL,
  `Photo_profil` varchar(45) CHARACTER SET latin1 DEFAULT NULL,
  `Taux_horaire_1` decimal(2,0) DEFAULT NULL,
  `Taux_horaire_2` decimal(2,0) DEFAULT NULL,
  `Description` varchar(254) CHARACTER SET latin1 DEFAULT NULL,
  `Date_recrutement` date DEFAULT NULL,
  `visible` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `professeur`
--

INSERT INTO `professeur` (`idPROFESSEUR`, `Nom`, `Prenom`, `Telephone`, `Telephone_2`, `Diplome`, `TypeProfesseur`, `Empreinte_1`, `Empreinte_2`, `Photo_profil`, `Taux_horaire_1`, `Taux_horaire_2`, `Description`, `Date_recrutement`, `visible`) VALUES
(1, 'cbnhg', 'hjgjhgjh', 'jhgjhgjhg', 'hgjhgh', '', 'jjhghgjhgh', 'hjgjgjgh', 'jhgjhghj', 'Profil IMG', '44', '25', 'jhjghghjgj', '2019-12-22', 0),
(3, 'kamdem', 'nestor', 'sfd5454', '54564', 'fsd', 'sdsdf', 'sds', 'dfse', 'Profil IMG', '45', '45', 'werfs', '2019-12-22', 1),
(4, 'tremblay', 'michel', 'sfd5454', '54564', 'fsd', 'sdsdf', 'sds', 'dfse', 'Profil IMG', '45', '46', 'werfs', '2019-12-04', 1),
(5, 'Boudreau', 'joel', 'jhgjhgjhg', 'hgjhgh', 'fsd', 'sdsdf', 'sds', 'dfse', 'Profil IMG', '45', '45', 'werfs', '2019-12-04', 1),
(6, 'raoul', 'Buguem', '54521', '54454', 'sdsdf', 'lsdljk', 'safsdf', 'sdfsd', 'Profil IMG', '45', '8', 'sfdfls', '2019-12-27', 1);

-- --------------------------------------------------------

--
-- Structure de la table `professeur_matiere`
--

CREATE TABLE `professeur_matiere` (
  `idPROFESSEUR_MATIERE` int(11) NOT NULL,
  `idPROFESSEUR` int(11) NOT NULL,
  `idMATIERE` int(11) NOT NULL,
  `visible` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `professeur_matiere`
--

INSERT INTO `professeur_matiere` (`idPROFESSEUR_MATIERE`, `idPROFESSEUR`, `idMATIERE`, `visible`) VALUES
(1, 4, 4, 1),
(2, 6, 3, 1),
(3, 5, 4, 1);

-- --------------------------------------------------------

--
-- Structure de la table `programmes`
--

CREATE TABLE `programmes` (
  `idPROGRAMMES` int(11) NOT NULL,
  `Date` date DEFAULT NULL,
  `Heure_Debut` time DEFAULT NULL,
  `Heure_Fin` time DEFAULT NULL,
  `CLASSE_idCLASSE` int(11) NOT NULL,
  `idADMINISTRATEUR` int(11) NOT NULL,
  `idPROFESSEUR_MATIERE` int(11) NOT NULL,
  `visible` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `programmes`
--

INSERT INTO `programmes` (`idPROGRAMMES`, `Date`, `Heure_Debut`, `Heure_Fin`, `CLASSE_idCLASSE`, `idADMINISTRATEUR`, `idPROFESSEUR_MATIERE`, `visible`) VALUES
(1, '2020-01-20', '08:40:00', '08:40:00', 1, 1, 1, 1),
(2, '2020-01-23', '09:35:00', '10:30:00', 5, 1, 2, 1),
(3, '2020-01-25', '09:35:00', '10:30:00', 1, 1, 1, 1),
(4, '2020-01-30', '10:45:00', '12:35:00', 5, 1, 2, 1),
(5, '2020-01-25', '07:45:00', '09:35:00', 6, 1, 1, 1),
(6, '2020-04-20', '10:30:00', '12:35:00', 1, 1, 1, 1),
(7, '2020-04-21', '12:35:00', '13:30:00', 5, 1, 3, 1),
(8, '2020-04-29', '11:40:00', '13:30:00', 5, 1, 2, 1);

--
-- Index pour les tables déchargées
--

--
-- Index pour la table `administrateur`
--
ALTER TABLE `administrateur`
  ADD PRIMARY KEY (`idADMINISTRATEUR`);

--
-- Index pour la table `classe`
--
ALTER TABLE `classe`
  ADD PRIMARY KEY (`idCLASSE`);

--
-- Index pour la table `matiere`
--
ALTER TABLE `matiere`
  ADD PRIMARY KEY (`idMATIERE`);

--
-- Index pour la table `presence`
--
ALTER TABLE `presence`
  ADD PRIMARY KEY (`idPRESENCE`),
  ADD KEY `fk_PRESENCE_PROGRAMMES1_idx` (`PROGRAMMES_idPROGRAMMES`);

--
-- Index pour la table `professeur`
--
ALTER TABLE `professeur`
  ADD PRIMARY KEY (`idPROFESSEUR`);

--
-- Index pour la table `professeur_matiere`
--
ALTER TABLE `professeur_matiere`
  ADD PRIMARY KEY (`idPROFESSEUR_MATIERE`),
  ADD KEY `fk_PROFESSEUR_has_MATIERE_MATIERE1_idx` (`idMATIERE`),
  ADD KEY `fk_PROFESSEUR_has_MATIERE_PROFESSEUR1_idx` (`idPROFESSEUR`);

--
-- Index pour la table `programmes`
--
ALTER TABLE `programmes`
  ADD PRIMARY KEY (`idPROGRAMMES`),
  ADD KEY `fk_PROGRAMMES_CLASSE1_idx` (`CLASSE_idCLASSE`),
  ADD KEY `fk_PROGRAMMES_ADMINISTRATEUR1_idx` (`idADMINISTRATEUR`),
  ADD KEY `fk_PROGRAMMES_PROFESSEUR_MATIERE1_idx` (`idPROFESSEUR_MATIERE`);

--
-- AUTO_INCREMENT pour les tables déchargées
--

--
-- AUTO_INCREMENT pour la table `administrateur`
--
ALTER TABLE `administrateur`
  MODIFY `idADMINISTRATEUR` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT pour la table `classe`
--
ALTER TABLE `classe`
  MODIFY `idCLASSE` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT pour la table `matiere`
--
ALTER TABLE `matiere`
  MODIFY `idMATIERE` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT pour la table `presence`
--
ALTER TABLE `presence`
  MODIFY `idPRESENCE` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `professeur`
--
ALTER TABLE `professeur`
  MODIFY `idPROFESSEUR` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT pour la table `professeur_matiere`
--
ALTER TABLE `professeur_matiere`
  MODIFY `idPROFESSEUR_MATIERE` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT pour la table `programmes`
--
ALTER TABLE `programmes`
  MODIFY `idPROGRAMMES` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `presence`
--
ALTER TABLE `presence`
  ADD CONSTRAINT `fk_PRESENCE_PROGRAMMES1` FOREIGN KEY (`PROGRAMMES_idPROGRAMMES`) REFERENCES `programmes` (`idPROGRAMMES`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Contraintes pour la table `programmes`
--
ALTER TABLE `programmes`
  ADD CONSTRAINT `fk_PROGRAMMES_ADMINISTRATEUR1` FOREIGN KEY (`idADMINISTRATEUR`) REFERENCES `administrateur` (`idADMINISTRATEUR`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_PROGRAMMES_CLASSE1` FOREIGN KEY (`CLASSE_idCLASSE`) REFERENCES `classe` (`idCLASSE`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_PROGRAMMES_PROFESSEUR_MATIERE1` FOREIGN KEY (`idPROFESSEUR_MATIERE`) REFERENCES `professeur_matiere` (`idPROFESSEUR_MATIERE`) ON DELETE NO ACTION ON UPDATE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
