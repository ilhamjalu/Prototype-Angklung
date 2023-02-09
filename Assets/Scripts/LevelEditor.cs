using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class LevelEditor : MonoBehaviour
{
    public TMP_Text text;
    public string fileName;
    public float delay = 0.2f;
    public AudioSource music;

    float time;
    bool start;

    bool[] pressing = new bool[5];
    float[] hold = new float[5];
    KeyCode[] keys = {KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.J, KeyCode.K};

    string result;

    //Blocks blocks = new Blocks();
    List<Block> block;

    public void StartOrStopRecording()
    {
        if (start)
        {
            start = false;
            Blocks blocks = new Blocks();
            blocks.block = block.ToArray();
            result = JsonUtility.ToJson(blocks);
            //Debug.Log(result);
            SaveJson();
        } else
        {
            start = true;
            music.Play();

        }
    }

    void SaveJson()
    {
        string path = Application.persistentDataPath + "/" + fileName;
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(result);
        writer.Close();
    }

    // Start is called before the first frame update
    void Start()
    {
        block = new List<Block>();
        Debug.Log(Application.persistentDataPath);
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            time += Time.deltaTime;
            text.text = time + "s";

            for (int i = 0; i < keys.Length; i++)
            {
                if (pressing[i])
                {
                    hold[i] += Time.deltaTime;
                }
                if (Input.GetKeyDown(keys[i]))
                {
                    hold[i] = 0;
                    pressing[i] = true;
                }
                if (Input.GetKeyUp(keys[i]))
                {
                    pressing[i] = false;
                    Debug.Log("Pressing " + i + " from " + (time - hold[i]) + " for " + hold[i] + "s");
                    Block newBlock = new Block();
                    newBlock.track = i;
                    newBlock.start = time - hold[i];

                    newBlock.length = (hold[i] < 0.2f) ? 0f : hold[i];

                    block.Add(newBlock);
                    //Debug.Log(block);
                }
            }
        }
    }
}
