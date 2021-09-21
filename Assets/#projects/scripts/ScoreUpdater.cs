using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
// !! La liaison entre LevelManager et ce script-ci se fait lorsqu'on met LevelManager dans l'inspecteur
//!! Faire en sorte que GUI soit géré par des composants externes à la gestion. Par contre, GUI peut aller se renseigner dans le Manager.

//S'assurer qu'il y a bien unTMPro
[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreUpdater : MonoBehaviour
{
    public LevelManager manager;
    // Start is called before the first frame update
    void Start()
    {
        //Si vous n'avez précisé aucun manager dans l'inspecteur, il va aller chercher le manager au démarrage.
        //Au singulier, va chercher le premier qu'il trouve.
        if(manager == null) {
            manager = FindObjectOfType<LevelManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Je vais d'office mettre mon objet sur un endroit où il y a un TMPro, sinon ça marchera pas
        //m.score.tostring donne accès au score et le transforme en string.
        GetComponent<TextMeshProUGUI>().text = manager.score.ToString();
    }
}
