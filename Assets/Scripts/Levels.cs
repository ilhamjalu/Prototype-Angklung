using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
    Level[] levels;
    List<GameObject> buttons = new List<GameObject>();
    List<GameObject> difficultyButtons = new List<GameObject>();

    int picked = -1;
    int difficulty = -1;
    
    public GameObject contentPanel;
    public GameObject difficultyPanel;
    public GameObject levelComponent;
    public GameObject difficultyComponent;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        levels = LevelList.levelList.levels;
        int i = 0;
        GameManager.instance.levels = this;
        foreach (Level level in levels)
        {
            buttons.Add(contentPanel.transform.GetChild(0).gameObject);
            LevelButton levelButton = contentPanel.GetComponentInChildren<LevelButton>();
            levelButton.number = i;
            TMP_Text text = levelButton.text;
            text.text = level.name;
            i++;
        }
    }

    public void Pick(int number)
    {
        foreach(GameObject diff in difficultyButtons)
        {
            Destroy(diff);
        }
        picked = number;
        difficulty = 0;
        difficultyButtons.Clear();


        difficultyPanel.transform.GetChild(0).gameObject.SetActive(true);
        difficultyButtons.Add(difficultyPanel.transform.GetChild(0).gameObject);

        //for (int i = 0; i < levels[number].difficulties.Length; i++)
        //{
        //    //Debug.Log(i);
        //    //GameObject component = Instantiate(difficultyComponent, difficultyPanel.transform);
        //    difficultyButtons.Add(difficultyPanel.transform.GetChild(0).gameObject);
        //    //RectTransform rect = component.GetComponent<RectTransform>();
        //    DifficultyButton difficultyButton = difficultyPanel.transform.GetComponentInChildren<DifficultyButton>();
        //    difficultyButton.difficulty = i;
        //    TMP_Text text = difficultyButton.text;
           
        //    text.text = levels[number].difficulties[i].name;


        //    //Vector3 pos = new Vector3(0f, (i * rect.rect.height) - rect.rect.height, 0f);
        //    //component.transform.position = difficultyPanel.transform.position - pos;
        //}
    }

    public void Difficulty(int number)
    {
        difficulty = number;
    }
    private void Update()
    {
        int i = 0;
        foreach(GameObject button in buttons)
        {
            Image image = button.GetComponent<Image>();
            if (picked == i) image.color = new Color(0, 0, 1);
            else image.color = new Color(1, 1, 1);
            i++;
        }
        int a = 0; 
        if (picked!=-1)
        {
            foreach(GameObject difficultyButton in difficultyButtons)
            {
                Image image = difficultyButton.GetComponent<Image>();
                if (difficulty == a) image.color = new Color(0, 0, 1f);
                else image.color = new Color(1, 1, 1);
                a++;
            }
            
        }
    }

    public void Play()
    {
        if (picked!=-1)
        {
            //GameManager.instance.levelJSON = levels[picked].json[difficulty];
            GameManager.instance.levelJSON = levels[picked].difficulties[difficulty].json;
            GameManager.instance.levelMusic = levels[picked].music;
            GameManager.instance.speed = levels[picked].difficulties[difficulty].speed;
            SceneManager.LoadScene("Level1");
        }
    }
}
