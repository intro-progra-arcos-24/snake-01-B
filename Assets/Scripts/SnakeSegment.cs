using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class SnakeSegment : GridItem
{
    public Snake snake;

    private void Awake()
    {
        snake = GetComponentInParent<Snake>();
    }

    private void Update()
    {
        gridArenaManager.CambiarItemEnGrilla(snake.lastGridSlot, this);
        transform.position = snake.lastPos;
    }
}
