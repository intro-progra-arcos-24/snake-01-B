using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridArenaManager : MonoBehaviour
{

    public int ancho;
    public int alto;
    public Wall wallPrefab;
    public Snake SnakePrefab;
    public Food FoodPrefab;

    public GridSlot[,] grilla;
    
    // Start is called before the first frame update
    void Start()
    {
        grilla = new GridSlot[ancho, alto];

        float puntoPartidaX = transform.position.x - (ancho/2);
        float puntoPartidaY = transform.position.y + (alto/2);

        Vector2 posicionActualEnGrilla = new Vector2();
        posicionActualEnGrilla.x = puntoPartidaX;
        posicionActualEnGrilla.y = puntoPartidaY;

        for (int i = 0; i < alto; i++)
        {
            for (int j = 0; j < ancho; j++)
            {
                GridSlot slotEnGrilla = new GridSlot();
                slotEnGrilla.indiceGrilla = new Vector2Int(j,i);
                slotEnGrilla.posicionMundo = new Vector2(posicionActualEnGrilla.x, posicionActualEnGrilla.y);
                grilla[j, i] = slotEnGrilla;

                posicionActualEnGrilla.x += 1;
            }
            posicionActualEnGrilla.x = puntoPartidaX;
            posicionActualEnGrilla.y -= 1;
        }
        CrearMurallas();
        CrearComida();
        CrearPersonaje();
    }

    private void CrearPersonaje()
    {

        float puntoMedioX = (ancho / 2);
        float puntoMedioY = (alto / 2);

        Vector2Int coordenadaCentro = new Vector2Int(
            (int)Math.Ceiling(puntoMedioX),
            (int)Math.Ceiling(puntoMedioY)
            );

        GridSlot slot = grilla[coordenadaCentro.x, coordenadaCentro.y];

        Snake itemEnGrilla = Instantiate<Snake>(
            SnakePrefab,
            new Vector3(slot.posicionMundo.x, slot.posicionMundo.y, 0),
            Quaternion.identity
            );

        slot.itemEnSlot = itemEnGrilla;
        itemEnGrilla.currentGridSlot = slot;
        itemEnGrilla.gridArenaManager = this;

    }

    private void CrearComida()
    {
        //throw new NotImplementedException();
    }

    public bool CambiarItemEnGrilla(Vector2Int posGrilla, GridItem item)
    {
        var gridSlot = grilla[posGrilla.x, posGrilla.y];
        gridSlot.itemEnSlot = item;
        item.currentGridSlot = gridSlot;
        return true;
    }


    private void CrearMurallas()
    {
        for (int i = 0; i < alto; i++)
        {
            for (int j = 0; j < ancho; j++)
            {
                if (j == 0 || j== (ancho-1) || i==0 || i==(alto-1))
                {
                    GridSlot slot = grilla[j, i];

                    GridItem itemEnGrilla = Instantiate<GridItem>(
                        wallPrefab,
                        new Vector3(slot.posicionMundo.x, slot.posicionMundo.y, 0),
                        Quaternion.identity
                        );

                    slot.itemEnSlot = itemEnGrilla;
                }
            }
        }
    }
}

public class GridSlot
{
    public Vector2Int indiceGrilla;
    public Vector2 posicionMundo;
    public GridItem itemEnSlot;

}
