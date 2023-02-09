using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class RythmController : MonoBehaviour
{
    public GameObject orb;
    public GameObject[] spawner;
    public ButtonController[] buttons;
    public DataManager dataLevel;
    public float blockSpeed = 200f;
    public Blocks blocks = new Blocks();
    public KeyCode[] keys = { KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.J, KeyCode.K };
    public float delay = 0.7f;
    public TMP_Text scoreText;
    public Image overlay;
    public GameObject homeButton;
    AudioSource audioSource;
    public GameObject[] stars;
    public Sprite starInactive;

    public int orbType;
    public int star;

    float time;
    int number = 0;
    int length;
    float levelLength;
    public bool start = false;
    bool ended = false;

    Vector3 touchPosWorld;
    Vector2 touchPosWorld2D;
    RaycastHit2D hitInformation;

    [Header("Dont Touch")]
    public Orb[] pressable;

    [Header("Scoring")]
    public int score = 0;
    public int maxScore = 0;
    public int scoreIncrement = 100;
    void Start()
    {
        orbType = PlayerPrefs.GetInt("orbType");
        audioSource = GetComponent<AudioSource>();

        foreach(GameObject star in stars)
        {
            star.SetActive(false);
        }

        homeButton.SetActive(false);

        foreach (ButtonController button in buttons)
        {
            button.controller = this;
        }
        
    }

    public void StartLevel()
    {
        //length = blocks.block.Length;
        length = dataLevel.dataLevels.Length;
        maxScore = dataLevel.dataLevels.Length * scoreIncrement;
        levelLength = spawner[0].transform.position.y - buttons[0].transform.position.y;
        delay = levelLength / blockSpeed;
        start = true;

    }

    void Update()
    {
        scoreText.text = score+"";
        if (start)
        {
            time += Time.deltaTime;
            overlay.color = new Color(0, 0, 0, Mathf.Lerp(overlay.color.a, 0f, 0.1f));

            if (number < length)
            {
                float starting = levelLength / (blockSpeed / Time.deltaTime);
                if (time >= dataLevel.dataLevels[number].start-delay)
                {
                    int track = dataLevel.dataLevels[number].track;
                    GameObject newOrb = Instantiate(orb);
                    Orb orbCompponent = newOrb.GetComponent<Orb>();
                    orbCompponent.track = track;
                    orbCompponent.speed = blockSpeed;
                    orbCompponent.number = number;
                    orbCompponent.length = dataLevel.dataLevels[number].length;
                    orbCompponent.controller = this;
                    orbCompponent.SetTail();
                    newOrb.transform.position = spawner[track].transform.position;
                    //maxScore += (orbCompponent.length > 0) ? ((int)(scoreIncrement * 2 * orbCompponent.length)) : 100;
                    number++;
                    //Debug.Log(starting);
                }
            }
            if (!audioSource.isPlaying)
            {
                EndLevel();
            }
            for (int i = 0; i < pressable.Length; i++)
            {
                if (pressable[i] && pressable[i].length > 0f && pressable[i].triggered + pressable[i].length < time)
                {
                    DestroyPressable(i);
                }
            }

            for (int i = 0; i < Input.touchCount; i++)
            {
                touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
                touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
                hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);

                Debug.Log(hitInformation.collider);

                if (hitInformation.collider != null)
                {
                    //We should have hit something with a 2D Physics collider!
                    GameObject touchedObject = hitInformation.transform.gameObject;
                    for (int x = 0; x < buttons.Length; x++)
                    {
                        if (touchedObject == buttons[x].gameObject)
                        {
                            switch (Input.GetTouch(i).phase)
                            {
                                case TouchPhase.Began:
                                    buttons[x].sprite.color = new Color(.5f, .5f, .5f);
                                    buttons[x].Press();
                                    break;
                                case TouchPhase.Ended:
                                    buttons[x].sprite.color = new Color(1f, 1f, 1f);
                                    buttons[x].Leave();
                                    break;
                            }
                        }

                    }
                    //touchedObject should be the object someone touched.
                    //Debug.Log("Touched " + touchedObject.transform.name);
                }
            }
        } else
        {

            overlay.color = new Color(0, 0, 0, Mathf.Lerp(overlay.color.a, 0.8f, 0.1f));
        }
        
    }

    public void EnterTrigger (Orb orb)
    {
        pressable[orb.track] = orb;
        orb.triggered = time;
    }

    public void LeaveTrigger (int track)
    {
        if (pressable[track].length <= 0f)
        {
            Debug.Log(pressable[track].success);
            if (pressable[track].success == false)
            {
                Miss();

            }
            //Debug.Log(pressable[track]);
            DestroyPressable(track);
        }
    }

    public void Miss()
    {
        audioSource.volume = 0.5f;
        Invoke("ReturnVolume", 0.5f);
    }

    void ReturnVolume()
    {
        audioSource.volume = 1;
    }

    void DestroyPressable(int track)
    {
        Destroy(pressable[track], 1f);
        //pressable[track] = null;
    }

    void Overlay()
    {
        homeButton.SetActive(true);
        start = false;
    }

    public void PauseOrPlay()
    {
        if(start)
        {
            homeButton.SetActive(true);
            start = false;
            audioSource.Pause();
            Time.timeScale = 0;
        } else if (!ended)
        {
            homeButton.SetActive(false);
            start = true;
            audioSource.UnPause();
            Time.timeScale = 1;
        }
    }

    void EndLevel()
    {
        //int section = maxScore / 3;
        if (score > maxScore * 0.9) star = 3;
        else if (score > maxScore * 0.7) star = 2;
        else if (score > maxScore * 0.4) star = 1;
        for(int i = 0; i < 3; i++) {
            stars[i].SetActive(true);
            Image starSprite = stars[i].GetComponent<Image>();
            if (star <= i)
                starSprite.sprite = starInactive;
        }
        ended = true;
        PauseOrPlay();
    }

    public void Home()
    {

        SceneManager.LoadScene("MainMenu");
    }
}
