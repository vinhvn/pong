﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Game objects
    public Ball ball;
    public Paddle paddle;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(ball);
        Instantiate(paddle);
    }
}
