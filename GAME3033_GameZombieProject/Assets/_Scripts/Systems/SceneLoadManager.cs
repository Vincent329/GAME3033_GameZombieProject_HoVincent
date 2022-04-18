using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    private static SceneLoadManager instance;
    public static SceneLoadManager Instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadStartScreen()
    {

    }

    public void LoadMenuScreen()
    {
        
    }

    public void LoadGame()
    {

    }

    public void LoadWinScreen()
    {

    }
    public void LoadLoseScreen()
    {

    }
}
