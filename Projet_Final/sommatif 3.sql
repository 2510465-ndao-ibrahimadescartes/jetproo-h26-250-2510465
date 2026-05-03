use h26_somm3_sem12;

alter table somm3_enseignant add column ens_salaire varbinary(250), ADD column ens_info_sante varbinary(250);



 GRANT CREATE ON  h26_somm3_sem12.somm3_enseignant to 'Développeur Sénior'@'localhost';
 
  GRANT CREATE ON  h26_somm3_sem12.somm3_enseignant to 'dba_assistant'@'%';
 ALTER TABLE somm3_etudiant
MODIFY etu_passwork VARCHAR(255);
ALTER TABLE somm3_enseignant
ADD COLUMN ens_password VARCHAR(128) NOT NULL;

alter TABLE somm3_etudiant ADD column etu_passwork varchar(128);
ALTER TABLE somm3_enseignant
ADD COLUMN ens_salt VARCHAR(64) NOT NULL;

UPDATE somm3_etudiant
SET etu_passwork= SHA2('motdepasse201', 512)
WHERE somm3_etu_id= 201;

UPDATE somm3_etudiant
SET etu_passwork= SHA2('motdepasse202', 512)
WHERE somm3_etu_id =202;

SET @salt1 = UUID();
UPDATE somm3_enseignant
SET 
    ens_salt = @salt1,
    ens_password = SHA2(CONCAT('mdpEnseignant101', @salt1), 512)
WHERE somm3_ens_id = 101;


SET @salt2 = UUID();

UPDATE somm3_enseignant
SET 
    ens_salt = @salt2,
    ens_password = SHA2(CONCAT('mdpEnseignant102', @salt2), 512)
WHERE  somm3_ens_id= 102;
GRANT insert on h26_somm3_sem12.somm3_enseignant to 'Développeur Sénior'@'localhost';

GRANT insert on h26_somm3_sem12.somm3_etudiant to 'Développeur Sénior'@'localhost';

SELECT 
    somm3_etu_id,
somm3_etu_nom,
    somm3_etu_prenom
FROM somm3_etudiant
WHERE etu_passwork = SHA2('motdepasse201', 512);


SELECT 
    somm3_ens_id,
    HEX(ens_salaire) AS salaire_chiffre,
    HEX(ens_info_sante) AS info_sante_chiffree
FROM somm3_enseignant;

SELECT 
    somm3_ens_id,
    ens_salt,
    ens_password
FROM somm3_enseignant
WHERE somm3_ens_id = 101;

--   Écrire les requêtes nécessaires à la création des trois nouveaux utilisateurs décrits dans le 
-- tableau ci-dessus. 
-- Écrire les requêtes nécessaires à l'attribution des privilèges à chaque utilisateur, conformément 
-- à leur profil.

 CREATE USER 'dba_assistant'@'%' IDENTIFIED BY 'dba1234';
 
GRANT ALL PRIVILEGES ON *.* TO 'dba_assistant'@'%' WITH GRANT OPTION;
  CREATE USER 'Développeur Sénior'@'localhost' IDENTIFIED BY 'Dev123';
  
  GRANT select , insert ,update,create,alter ,delete on *.* to 'Développeur Sénior'@'localhost';
  
  create user 'Technicien stagiaire'@'localhost' identified by 'Tech123';
  grant select on  *.* to 'Technicien stagiaire'@'localhost' ;
  grant  select ,insert  on h26_somm3_sem12.somm3_programme to 'Technicien stagiaire'@'localhost';
grant select (somm3_etu_nom,somm3_etu_prenom,somm3_etu_date_inscription) on h26_somm3_sem12.somm3_etudiant to 'Technicien stagiaire'@'localhost';
-- Écrire les requêtes permettant de révoquer les droits du développeur sénior qui lui permettent 
-- de créer des tables et de modifier la structure des tables, et ce, sur l'ensemble des tables 
-- de la base de données. 

revoke  create ,alter on *.* from 'Développeur Sénior'@'localhost';

SELECT * FROM mysql.user ORDER BY User;
