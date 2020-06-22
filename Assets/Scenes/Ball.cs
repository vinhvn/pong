using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    float speed;

    float radius;
    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = Vector2.one.normalized; // initially (1,1) normalized
        radius = transform.localScale.x / 2; // half of width
    }

    // Update is called once per frame
    void Update()
    {
        // remember to adjust for framerate
        transform.Translate(direction * speed * Time.deltaTime);
        // bounds checking
        if (transform.position.y < GameManager.bottomLeft.y + radius && direction.y < 0 ||
            transform.position.y > GameManager.topRight.y - radius && direction.y > 0)
        {
            direction.y *= -1;
        }
        // game over check
        if (transform.position.x < GameManager.bottomLeft.x + radius && direction.x < 0)
        {
            Debug.Log("Right player won!");
            // freeze game and stop updating script
            Time.timeScale = 0;
            enabled = false;
        }
        else if (transform.position.x > GameManager.topRight.x - radius && direction.x > 0)
        {
            Debug.Log("Left player won!");
            Time.timeScale = 0;
            enabled = false;
        }
    }

    // collision detection
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Paddle")
        {
            bool isRight = other.GetComponent<Paddle>().isRight;

            if (isRight && direction.x > 0 ||
                !isRight && direction.x < 0)
            {
                direction.x *= -1;
            }
        }
    }
}
