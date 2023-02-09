using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelReader : MonoBehaviour
{
    TextAsset textJson;
    RythmController rythmController;
    AudioSource audioSource;

    void Start()
    {
        //textJson = GameManager.instance.levelJSON;
        rythmController = GetComponent<RythmController>();
        audioSource = GetComponent<AudioSource>();
        //rythmController.blocks = JsonUtility.FromJson<Blocks>(textJson.text);
        //rythmController.blockSpeed = GameManager.instance.speed;
        audioSource.clip = GameManager.instance.levelMusic;
        //Debug.Log("halo");
        audioSource.Play();
        rythmController.StartLevel();
    }
}
