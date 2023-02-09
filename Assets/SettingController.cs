using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingController : MonoBehaviour
{
    public GameObject[] orbs;
    public int orbType;

    void Start()
    {
        orbType = PlayerPrefs.GetInt("orbType");
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        foreach(GameObject orb in orbs)
        {
            Image image = orb.GetComponent<Image>();
            if (orbType == i) image.color = new Color(.5f, .5f, .5f);
            else image.color = new Color(1f, 1f, 1f);

            i++;
        }
    }

    public void PickOrb(int number)
    {
        orbType = number;
        PlayerPrefs.SetInt("orbType", orbType);
        PlayerPrefs.Save();
    }

    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
