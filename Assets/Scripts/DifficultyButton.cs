using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DifficultyButton : MonoBehaviour
{
    public TMP_Text text;
    public int difficulty;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Click()
    {
        GameManager.instance.levels.Difficulty(difficulty);
    }
}
