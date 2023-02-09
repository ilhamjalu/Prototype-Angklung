using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelButton : MonoBehaviour
{
    public TMP_Text text;
    public int number;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Click()
    {
        GameManager.instance.levels.Pick(number);
    }
}
