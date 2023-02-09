using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelList : MonoBehaviour
{
    public static LevelList levelList;
    public Level[] levels;

    // Start is called before the first frame update
    private void Awake()
    {
        if (levelList != null && levelList != this)
        {
            Destroy(this.gameObject);
            return;
        }
        levelList = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
