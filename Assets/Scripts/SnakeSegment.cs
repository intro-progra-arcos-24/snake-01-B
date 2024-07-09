using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class SnakeSegment : GridItem
{
    public Snake snake;

    public int segmentNumber;
    public bool firstSegment;
    public Vector2 lastPos; 
    public Vector2Int lastGridSlot;
    public SnakeSegment lastSegment;
    public SnakeSegment snakeSegment;

    private void Awake()
    {
        snakeSegment = this;
    }

    private void Update()
    {
        if (firstSegment)
        {
            segmentNumber = 1;
        }
    }

    public void UpdateSegment()
    {
        Vector2Int posGrilla = currentGridSlot.indiceGrilla;
        lastPos = transform.position;
        lastGridSlot = posGrilla;

        if (firstSegment) { gridArenaManager.CambiarItemEnGrilla(snake.lastGridSlot, this); transform.position = snake.lastPos; }
        else { gridArenaManager.CambiarItemEnGrilla(lastSegment.lastGridSlot, this); transform.position = lastSegment.lastPos; }
    }

    public void AddSegment()
    {        
        GridItem newSegment = Instantiate(snakeSegment, transform.position, Quaternion.identity);
        SnakeSegment segment = newSegment.gameObject.GetComponent<SnakeSegment>();
        gridArenaManager.CambiarItemEnGrilla(lastGridSlot, newSegment);
        newSegment.gridArenaManager = gridArenaManager;
        segment.segmentNumber = segmentNumber + 1;
        segment.firstSegment = false;
        segment.lastSegment = this;
        segment.snake = snake;
        snake.segments.Add(segment);
    }
}
