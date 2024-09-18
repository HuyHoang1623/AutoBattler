using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayOffline()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void PlayOnline()
    {
    }

    public void Setting()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}

