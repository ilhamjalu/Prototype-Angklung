using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void SelectLevel() {
        SceneManager.LoadScene("LevelSelect");
    }

    public void Setting()
    {
        SceneManager.LoadScene("Setting");
    }
}
