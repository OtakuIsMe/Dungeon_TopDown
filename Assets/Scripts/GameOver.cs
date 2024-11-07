using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void LoadGameMenu()
    {

        var persistentObjects = GameObject.FindGameObjectsWithTag("PersistentObject");
        foreach (var obj in persistentObjects)
        {
            Destroy(obj);
        }

        SceneManager.LoadScene("MainMenu");
    }
}
