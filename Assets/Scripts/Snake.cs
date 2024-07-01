using System.Collections.Generic;
using UnityEngine;

public class Snake : GridItem
{
    public Vector2Int direction = Vector2Int.right;
    public float speed = 20f;
    public float speedMultiplier = 1f;
    public SnakeSegment snakeSegment;
    public List<SnakeSegment> segments;

    private float nextUpdate;
    public Vector2 lastPos;
    public Vector2Int lastGridSlot;

    private Vector2Int input;

    

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
        if(!gridArenaManager.isAlive) { return; }

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
        lastPos = transform.position;
        lastGridSlot = 
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
            gridArenaManager.Perder();
        }
        else if(item.itemEnSlot is Food)
        {
            item.itemEnSlot.GetComponent<Food>().Reposicionar();
            Debug.Log("Comida");
            AñadirSegmento();
        }
        else if(item.itemEnSlot is SnakeSegment)
        {
            gridArenaManager.Perder();
        }
    }

    public void AñadirSegmento()
    {
        if(segments.Count == 0)
        {
            GridItem newSegment = Instantiate(snakeSegment, lastPos, Quaternion.identity, transform);
            gridArenaManager.CambiarItemEnGrilla(lastGridSlot, newSegment);
            newSegment.gridArenaManager = gridArenaManager;
            segments.Add(snakeSegment);
        }
    }
}
