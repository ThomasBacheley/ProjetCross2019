# ProjetCross2019
Le projet a été réalisé dans le cadre de la validation de la deuxième année de BTS SNIR.

# BUT
Chaque année, un cross est organisé à l'Institut Lemonnier.
Le chronométrage et le classement sont réalisés de manière manuelle.
En 2018, un code-barres sur le dossard a permis d'améliorer la publication des résultats mais si celui-ci était plié, il faussait en partie les résultats et la lisibilité.
Une amélioration notable serait d'automatiser le chronométrage et la publication des résultats grâce à l'utilisation de transpondeurs RFID.
Le but de ce projet est donc de créer une solution complète de chronométrage et classements (générale et cas particulier) à l'aide de transpondeurs RFID et d’une base de données.

Cette application s’adresse principalement à l’institut Lemonnier et particulièrement aux organisateurs du cross qui pourront bénéficier d’une meilleure gestion et suivie de leur futur Cross.  

L'application fonctionne sur une fenêtre avec des Grids qui changent en fonction des boutons :
![main_window](https://raw.githubusercontent.com/ThomasBacheley/ProjetCross2019/main/screenshot/main_window.png)  

On peut ajouter des Élèves à la main , mais dans l'application finale, on peut les ajouter via un fichiers Excel :
![students_window](https://raw.githubusercontent.com/ThomasBacheley/ProjetCross2019/main/screenshot/add_student_window.png)

Cette fenêtre permet d'afficher un chronomètre en temps réel avec les temps qui s'actualisent au passage des coureurs :
![time_window](https://raw.githubusercontent.com/ThomasBacheley/ProjetCross2019/main/screenshot/time_window.png)

Le cahier des charges stipulés une fenêtre pour récupérer des classements en fonctions de plusieurs critères :
![ranking_window](https://raw.githubusercontent.com/ThomasBacheley/ProjetCross2019/main/screenshot/ranking_window.png)

# Code
Le code est principalement rédigé en C#, une DLL a été créée spécialement pour l'application ainsi qu'une base de données dont le [script](https://github.com/ThomasBacheley/ProjetCross2019/blob/main/script.sql "script.sql") est disponible.

# Données
 - [ListeEleve.xlsx](https://github.com/ThomasBacheley/ProjetCross2019/blob/main/ListeEleve.xlsx "ListeEleve.xlsx") : contient la liste des élèves de l'établissement
 - [BACHELEY_THOMAS_U62_IR.zip](https://github.com/ThomasBacheley/ProjetCross2019/blob/main/BACHELEY_THOMAS_U62_IR.zip "BACHELEY_THOMAS_U62_IR.zip") : compte rendu du projet ( partie commune et partie personnelle )