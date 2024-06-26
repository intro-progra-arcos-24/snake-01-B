﻿using System.Collections.Generic;
using UnityEngine;

public class Snake : GridItem
{
    public Vector2Int direction = Vector2Int.right;
    public float speed = 20f;
    public float speedMultiplier = 1f;
    public SnakeSegment SegmentoPrefab;


    private Vector2Int input;
    private float nextUpdate;
    private SnakeSegment aMover;

    bool lost;
    GridArenaManager gam;

    private void Start()
    {
        gam= GameObject.Find("GridArea").GetComponent<GridArenaManager>();
    }

    private void Update()
    {
        // Only allow turning up or down while moving in the x-axis
        if (direction.x != 0f && !lost)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                input = Vector2Int.up;
            } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                input = Vector2Int.down;
            }
        }
        // Only allow turning left or right while moving in the y-axis
        else if (direction.y != 0f && !lost)
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
            //mover la cabeza, guardar una referencia a la posicion en la que estaba
            var posAnterior = currentGridSlot.indiceGrilla;
            gridArenaManager.CambiarItemEnGrilla(posGrilla, this);
            //si hay una cola que mover, moverla a la referencia que se acaba de guardad
            if (aMover != null)
            {
                //mover el objeto
                SnakeSegment cuello = aMover.encontrarCuello(aMover);
                cuello.siguiente = aMover;
                gridArenaManager.CambiarItemEnGrilla(posAnterior,aMover);
                aMover.UpdateRealTransform();
                if (aMover.siguiente != null){
                    SnakeSegment movido = aMover;
                    aMover = aMover.siguiente;
                    movido.siguiente = null;
                }
            }
            // Move the snake in the direction it is facing
            float x = transform.position.x + direction.x;
            float y = transform.position.y + direction.y;
            transform.position = new Vector2(x, y);
        }
        else if(
            (item.itemEnSlot is Wall) || 
            (item.itemEnSlot is SnakeSegment)
            )
        {
            Debug.Log("Hazard");
            lost=true;
            gridArenaManager.Perder();
        }
        else if(item.itemEnSlot is Food food)
        {
            Debug.Log("Comida");
            //spawnear un segmento de la cola en la posicion actual
            NuevoSegmento(currentGridSlot.indiceGrilla);
            
            //mover al player a la posicion de la comida
            gridArenaManager.CambiarItemEnGrilla(posGrilla, this);
            float x = transform.position.x + direction.x;
            float y = transform.position.y + direction.y;
            transform.position = new Vector2(x, y);

            gridArenaManager.Score(1);
            
            if(gam.score%5 <1)
            {
                speed=speed+1;
            }
            Debug.Log(speed);
            
            food.Reposicionar();
        }
        else if (item.itemEnSlot is SnakeSegment segment)
        {
            Debug.Log("Segmento");
        }
    }

    private void NuevoSegmento(Vector2Int posicion){
        //instanciar el SnakeSegment
        SnakeSegment itemSegmento = Instantiate<SnakeSegment>(
            SegmentoPrefab,
            new Vector3(0, 0, 0),
            Quaternion.identity
            );
        itemSegmento.gridArenaManager = gridArenaManager;
        itemSegmento.Place(posicion,aMover);
        //si no hay un aMover aun, osea este es el primer segmento, flagear el que acabamos de crear como aMover
        if (aMover == null) aMover = itemSegmento;
    }
}
