----------------------------------------------------------------------------

USE Chronocross;

------ affiche les badges -----
select * from table_transpondeur;
------ affiche la table inscrit -----
select * from table_inscrit;
------ affiche les inscrits -> affiche le bagde -----
select numero_dossard,nom,prenom,tag from table_inscrit,table_transpondeur,table_eleve WHERE table_inscrit.ideleve=table_eleve.id_eleve AND table_inscrit.idtranspondeur=table_transpondeur.id_transpondeur;
----------------------------------------------------------------------------