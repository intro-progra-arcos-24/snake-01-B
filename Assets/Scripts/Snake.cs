using System.Collections.Generic;
using UnityEngine;

public class Snake : GridItem
{
    public Vector2Int direction = Vector2Int.right;
    public float speed = 20f;
    public float speedMultiplier = 1f;


    private Vector2Int input;
    private float nextUpdate;

    

    private void Start()
    {
        
    }

    private void Update()
    {
        // Only allow turning up or down while moving in the x-axis
        if (direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                input = Vector2Int.up;
            } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                input = Vector2Int.down;
            }
        }
        // Only allow turning left or right while moving in the y-axis
        else if (direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                input = Vector2Int.right;
            } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                input = Vector2Int.left;
            }
        }

        UpdateSnake();
    }

    private void UpdateSnake()
    {
        // Si no ha pasado tiempo suficiente no hacer nada
        if (Time.time < nextUpdate)
        {
            return;
        }

        nextUpdate = Time.time + (1f / (speed * speedMultiplier));
        if (input != Vector2Int.zero)
        {
            direction = input;
        }
        
        Vector2Int posGrilla = currentGridSlot.indiceGrilla;
        posGrilla += direction;

        GridSlot item = gridArenaManager.ObtenerGrillaPorPosicion(posGrilla);

        if (item.itemEnSlot == null)
        {
            gridArenaManager.CambiarItemEnGrilla(posGrilla, this);
            // Move the snake in the direction it is facing
            float x = transform.position.x + direction.x;
            float y = transform.position.y + direction.y;
            transform.position = new Vector2(x, y);
        }
        else if(item.itemEnSlot is Wall)
        {
            Debug.Log("Wall");
            gridArenaManager.Perder();
        }
        if(item.itemEnSlot is Food food)
        {
            Debug.Log("Comida");
            food.Reposicionar();
        }
    }

}
