using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Pour le score : est-ce que mon cube a été clické ? Et quelle est sa valeur ?
public class CubeBehavior : MonoBehaviour
{   //Même si la variable est publique, elle est cachée dans l'Inspector : gens n'ont pas besoin de savoir à quoi sert ce Manager
    //Permet de travailler de façon propre sans perturber l'utilisateur
    //Par exemple, éviter d'exposer ce genre de trucs à un level manager qui bosse avec notre jeu
    
    [HideInInspector] public LevelManager manager;

    //Vérifier si le cube a été cliqué
    [HideInInspector] public bool clicked = false;

    //Donner la valeur des cubes dans l'inspecteur (les points)
    public int value = 1;

    void Start()
    {   //Sois détruit entre 3 et 6 secondes
        //--> float seconds = Random.Range(3f, 5f);
        //--> Destroy(gameObject, seconds);
        Destroy(gameObject, Random.Range(3, 6));
    }

    private void OnDestroy() {
        print (gameObject.name);
        manager.RemoveCube(gameObject); //appelez le Manager, retirez le cube, le cube c'est moi ! (petit g = sur lequel se trouve le cube)
    }
    
    //Destroy également asynchrone comme une CoRoutine, excepté qu'avant la destruction il joue l'event OnDestroy
    private void OnMouseDown() {
        clicked = true;
        Destroy(gameObject);
    }
}
