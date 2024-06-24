using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSegment : GridItem
{
    public SnakeSegment siguiente;

    public void Place(Vector2Int posicion, SnakeSegment lejano){

        //poner este snakeSegment en la posicion correcta en el mundo y en la memoria de la grilla
        GridSlot slot = gridArenaManager.ObtenerGrillaPorPosicion(posicion);
        gridArenaManager.CambiarItemEnGrilla(slot.indiceGrilla, this);
        float x = slot.posicionMundo.x;
        float y = slot.posicionMundo.y;
        transform.position = new Vector2(x, y);

        //encontrar el segmento mas cercano al player (lejano es el mas lejano)
        SnakeSegment cercano;
        if (lejano != null){
            cercano = encontrarCuello(lejano);
            //añadir el actual a la cadena
            cercano.siguiente = this;
        }
    }

    public void UpdateRealTransform(){
        float x = currentGridSlot.posicionMundo.x;
        float y = currentGridSlot.posicionMundo.y;
        transform.position = new Vector2(x, y);
    }

    public SnakeSegment encontrarCuello(SnakeSegment segmento){
        SnakeSegment cuello = segmento;
        while(cuello.siguiente != null){
            cuello = cuello.siguiente;
        }
        return cuello;
    }
}
