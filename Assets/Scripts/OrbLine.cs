using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbLine : MonoBehaviour
{
    public float length = 0;
    public float speed;
    SpriteRenderer spriteRenderer;
    float time;
    bool finished = false;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (time < length)
        {
            spriteRenderer.size += new Vector2(0f, speed * Time.deltaTime);
            time += Time.deltaTime;
        }
    }

    public void DecreaseTail()
    {
        if (length > 0)
        {
            spriteRenderer.size -= new Vector2(0f, Time.deltaTime * speed);

            if (spriteRenderer.size.y <= 0)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
