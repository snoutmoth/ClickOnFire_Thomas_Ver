using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    public static void Change(string name) {
        SceneManager.LoadScene(name);

    }
}
