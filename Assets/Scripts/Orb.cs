using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public float speed;
    public int track;
    public int number;
    public float length;
    public float triggered;
    public RythmController controller;
    public bool pressed = false;
    public bool longPress = false;
    public int spriteIndex = 0;
    public Sprite[] sprite;
    public Sprite[] orbFeedbackSprite;
    public GameObject hitEffect;
    OrbLine tail;
    SpriteRenderer spriteRenderer;
    float curSpeed;

    public bool bait = false;
    float time;
    public bool success = false;

    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        spriteIndex = controller.orbType;
        curSpeed = speed;
        spriteRenderer.sprite = sprite[spriteIndex];
    }

    public void SetTail()
    {
        tail = transform.Find("tail").gameObject.GetComponent<OrbLine>();
        tail.length = length;
        tail.speed = speed;
    }

    public float GetCurrentSpeed()
    {
        return curSpeed;
    }

    IEnumerator ShortOrbFeedback()
    {
        if (orbFeedbackSprite[spriteIndex] == null) yield return null;
        spriteRenderer.sprite = orbFeedbackSprite[spriteIndex];
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    void LongOrbFeedback()
    {
        if (orbFeedbackSprite[spriteIndex] == null) return;
        spriteRenderer.sprite = orbFeedbackSprite[spriteIndex];
        tail.DecreaseTail();

    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.y -= speed * Time.deltaTime;
        transform.position = pos;

        if (pressed && length == 0f)
        {
            StartCoroutine(ShortOrbFeedback());
        }
        else if (pressed && longPress && length > 0)
        {
            LongOrbFeedback();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("triggered kah min");
        if (collision.gameObject.name == "triggerer")
        {
            if (bait)
            {
                //Debug.Log(time);
                triggered = time;
                //controller.delay = time;
                //Destroy(gameObject);
            } else
            {
                controller.EnterTrigger(this);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "triggerer")
        {
            //Debug.Log(controller);
            /*if (bait)
            {
                float between = triggered + ((time - triggered));
                controller.delay = time;
                Destroy(gameObject);
            }*/
            //if (bait) Destroy(gameObject);
            //else
            //Debug.Log("what about this");
            controller.LeaveTrigger(track);
        } 
        else if (collision.gameObject.name == "end")
        {
            // Destroy(gameObject, 0.1f);
        }
    }
}
