using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridArenaManager : MonoBehaviour
{
    public int ancho;
    public int alto;
    public Wall wallPrefab;
    public Snake SnakePrefab;
    public Food FoodPrefab;
    public GameObject can;
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

        for (int y = 0; y < alto; y++)
        {
            for (int x = 0; x < ancho; x++)
            {
                GridSlot slotEnGrilla = new GridSlot();
                slotEnGrilla.indiceGrilla = new Vector2Int(x,y);
                slotEnGrilla.posicionMundo = new Vector2(posicionActualEnGrilla.x, posicionActualEnGrilla.y);
                grilla[x, y] = slotEnGrilla;

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
        //Detectar cual es el espacio en donde debemos inicializar al player
        float puntoMedioX = (ancho / 2);
        float puntoMedioY = (alto / 2);

        Vector2Int coordenadaCentro = new Vector2Int(
            (int)Math.Ceiling(puntoMedioX),
            (int)Math.Ceiling(puntoMedioY)
            );

        GridSlot slot = grilla[coordenadaCentro.x, coordenadaCentro.y];


        //Instanciar el player por prefab
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
        Food itemComida = Instantiate<Food>(
            FoodPrefab,
            new Vector3(0, 0, 0),
            Quaternion.identity
            );
        itemComida.gridArenaManager = this;
        itemComida.Reposicionar();
    }

    public bool CambiarItemEnGrilla(Vector2Int posGrilla, GridItem item)
    {
        var gridSlot = grilla[posGrilla.x, posGrilla.y];
        var preGridSlot = item.currentGridSlot;
        gridSlot.itemEnSlot = item;
        item.currentGridSlot = gridSlot;
        if(preGridSlot!=null)
            preGridSlot.itemEnSlot = null;
        
        return true;
    }

    public GridSlot ObtenerGrillaPorPosicion(Vector2Int posGrilla)
    {
        GridSlot gridSlot = null;
        try
        {
            gridSlot = grilla[posGrilla.x, posGrilla.y];
        }
        catch
        {
            return null;
        } 
        return gridSlot;
    }

    public GridSlot ObtenerSlotVacioRandom()
    {
        GridSlot slot = null;
        
        List<GridSlot> posicionesVacias = new List<GridSlot>();
        for (int y = 0; y < alto; y++)
        {
            for (int x = 0; x < ancho; x++)
            {
                GridSlot currGridSlot = grilla[x, y];
                if(currGridSlot.itemEnSlot == null)
                {
                    posicionesVacias.Add(currGridSlot);
                }
            }
        }
        int pos = Random.Range(0, posicionesVacias.Count);

        return posicionesVacias[pos];
    }


    private void CrearMurallas()
    {
        for (int y = 0; y < alto; y++)
        {
            for (int x = 0; x < ancho; x++)
            {
                if (x == 0 || x== (ancho-1) || y==0 || y==(alto-1))
                {
                    GridSlot slot = grilla[x, y];

                    GridItem itemEnGrilla = Instantiate<GridItem>(wallPrefab,
                        new Vector3(slot.posicionMundo.x, slot.posicionMundo.y, 0),
                        Quaternion.identity
                        );

                    slot.itemEnSlot = itemEnGrilla;
                }
            }
        }
    }

    public void Perder()
    {
        AbrirPantallaFin();
    }

    public void AbrirPantallaFin()
    {

        can.SetActive(true);

    }
}

public class GridSlot
{
    public Vector2Int indiceGrilla;
    public Vector2 posicionMundo;
    public GridItem itemEnSlot;

}
