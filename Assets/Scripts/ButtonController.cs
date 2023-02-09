using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public int number;
    public float tolerance = .5f;
    public bool clicking = false;
    public RythmController controller;
    public SpriteRenderer sprite;
    Orb orb;
    float time;

    Vector3 touchPosWorld;
    Vector2 touchPosWorld2D;
    RaycastHit2D hitInformation;

    //Change me to change the touch phase used.
    //TouchPhase touchPhase = TouchPhase.Ended;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (clicking)
        {
            time += Time.deltaTime;
        }

        if(Input.GetKeyDown(controller.keys[number]))
        {
            sprite.color = new Color(.5f, .5f, .5f);
            Press();
        }

        if(Input.GetKeyUp(controller.keys[number]))
        {
            sprite.color = new Color(1f, 1f, 1f);
            Leave();
        }
    }

    
    public void Press()
    {
        orb = controller.pressable[number];
        if (orb != null && !orb.pressed)
        {
            time = 0;
            clicking = true;
            orb.pressed = true;
            
            if (orb.length > 0)
                orb.speed = 0;
                orb.longPress = true;
        }
    }

    public void Leave()
    {
        if (!clicking) return;
        clicking = false;
        
        if (orb.length > 0)
            orb.speed = orb.GetCurrentSpeed();
            orb.longPress = false;

        if (time > orb.length - tolerance/2 && time < orb.length + tolerance / 2)
        {
            controller.score += 100;
            orb.success = true;
        } else
        {
            controller.Miss();
        }
    }
    /*
    private void OnMouseDown()
    {
        Press();
    }
    private void OnMouseUp()
    {
        Leave();
    }*/
}
