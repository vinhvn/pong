using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // [SerializeField] allows variables below it to be seen
    // in Unity editor and modified during gameplay/debugging
    [SerializeField]
    float speed;

    float height;

    string input;
    public bool isRight; // public so we can see it from Ball.cs

    // Start is called before the first frame update
    void Start()
    {
        height = transform.localScale.y;
    }

    public void Init(bool isRightPaddle)
    {
        isRight = isRightPaddle;
        // initially zero
        Vector2 pos = Vector2.zero;
        if (isRightPaddle) // place paddle on right of screen
        {
            pos = new Vector2(GameManager.topRight.x, 0);
            pos -= Vector2.right * transform.localScale.x; // move a bit to the left
            input = "PaddleRight"; // set our input manager
        }
        else // place paddle on left side of screen
        {
            pos = new Vector2(GameManager.bottomLeft.x, 0);
            pos += Vector2.right * transform.localScale.x; // move a bit to the right
            input = "PaddleLeft";
        }
        // update paddle's position
        transform.position = pos;
        transform.name = input; // for clarity
    }

    // Update is called once per frame
    void Update()
    {
        // getAxis => a number in the range of [1, ..., -1] (1 for up, -1 for down)
        // deltaTime => divides by framerate to ensure movement will not differ based on framerate
        float move = Input.GetAxis(input) * Time.deltaTime * speed;

        // restrict movement based on top and bottom edge
        // height / 2 => make sure we're adjusting for the paddle's height
        // move < 0 => paddle is moving downwards and vice versa
        if (transform.position.y < GameManager.bottomLeft.y + height / 2 && move < 0 ||
            transform.position.y > GameManager.topRight.y - height / 2 && move > 0)
        {
            move = 0;
        }

        // update position
        transform.Translate(move * Vector2.up);
    }
}
