using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    //Popper dans une longueur, largeur et profondeur : utiliser un Vector3
    //Vector3.one car si on fait 0*0*0, c'est une zone vide, les objets popperont tous au même endroit
    public Vector3 randomZone = Vector3.one * 5;
    //Accéder à mes prefabs
    public GameObject greyPrefab;
    public GameObject bluePrefab;

    private int cubeNbr = 1;

    //Privée car sollicite uniquement l'objet. Accueille tous les cubes. La déclarer pour créer une liste vide.
    private List<GameObject> cubes = new List<GameObject>();

    //Pour faire le score
     
    [HideInInspector] public int score = 0;
    [HideInInspector] public int scoreMax = 30;

    public UnityEvent WhenPlayerWins;
    public UnityEvent WhenPlayerLoses;

    //appeler fonction dans Update
    public void Start()
    {
        StartCoroutine(PopCubes());
        StartCoroutine(UpdatePopRate());
    }


    //Faire une CoRoutine qui va popper UN cube. IENum = sortie de la coroutine. 
    //PopCubes car elle va gérer TOUS cubes à la fin.

    //Coroutine = sorte de sous-script qui va se lancer parallèlement à mon code
    //Chaque fois qu'il tombe sur un yield, fait l'action demandée puis sort.
    //Permet de faire une sortie dans la boucle.
    //Un peu comme une fonction Update qui tourne toutes les secondes.


    private IEnumerator PopCubes()
    {
        while (true)
        { //Faire à l'infini
          //Tant que ma coroutine tourne, toutes les secondes elle pop un cube
            yield return new WaitForSeconds(1f);
            //Faire à l'infini
            for (int n = 0; n < cubeNbr; n++)
            {
                PopCube();
            }
        }
    }

    //Créer la CoRoutine pour augmenter le rate de 5 secondes

    private IEnumerator UpdatePopRate()
    {
        while (true)
        { //Faire à l'infini
          //Toutes les 5 secondes, ma coroutine pop un cube
            yield return new WaitForSeconds(5f);
            cubeNbr++;
        }
    }

    //Fonction pour faire popper objets. UN cube, pas tous.
    private void PopCube()

    //Return sort de la fonction : Si j'ai + ou = à 10 cubes dans ma liste, on sort de la fonction, on ne pourra plus popper.
    {
        if (cubes.Count >= 10) return;
        //x, y et z sont les trois parties du Vector3 qui donnent la position de mon objet.
        //Propriétés du V3 en gros.

        float x = Random.Range(0, randomZone.x);
        float y = Random.Range(0, randomZone.y);
        float z = Random.Range(0, randomZone.z);

        //Donne la position pour notre objet qui va popper.
        Vector3 position = new Vector3(x, y, z);

        GameObject cube;

        //Si t'as fait 0 sur un chiffre entre 0 et 4 inclus, tu vas créer un prefab bleu
        //Il faut une condition pour séparer pop bleu du pop gris!!
        if (Random.Range(0, 5) == 0)
        {
            cube = Instantiate(bluePrefab, position, Quaternion.identity);
        }
        else
        {
            cube = Instantiate(greyPrefab, position, Quaternion.identity);
        }

        //Maintenant il faut que mon CubeBehavior connaisse mon cube.
        //Liaison entre le cube et le manager.
        //this = mot clé qui signifie "l'instance en cours"
        //Dit que le manager du manager est le script en cours, variable qui contient l'objet lui-même. 
        //Utilisé quand besoin de se nommer soi-même en tant que script

        cube.GetComponent<CubeBehavior>().manager = this;

        //Chaque fois que je créé un objet, je l'ajoute à ma liste.

        cubes.Add(cube);

    }
    
    //Doit être public pour que CubeBehaviour puisse l'atteindre
    //J'ai reçu un cube, je l'enlève de ma liste

    //Peu importe l'événement, le score change quand on retire le cube, donc c'est bien de s'en occuper là
    public void RemoveCube(GameObject cube)
    {   
        //Tout ce qui concerne le score est mis dans le même objet.
        //Tout le comportement du cube est gardé dans ce qui gère l'objet.
        //Technique de l'encapsulation

        //Permet d'accéder à tout CB (valeur et click) au lieu de mettre deux GetComponent

        //!! Pas optimal de mettre le score sur l'Update car il va vérifier à chaque frame.

        CubeBehavior cubeBehavior = cube.GetComponent<CubeBehavior>();
        if (cubeBehavior.clicked) {
            score += cubeBehavior.value;
            // ? vérifie si l'objet n'est pas nul; s'il est nul, ignore ce qui suit.
            if (score >= scoreMax) WhenPlayerWins?.Invoke(); 
        
        }
        else {
            score -= cubeBehavior.value;
            if (score < 0 ) WhenPlayerLoses?.Invoke();
        }
        cubes.Remove(cube);
    }
}
