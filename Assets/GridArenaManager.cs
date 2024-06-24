using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GridArenaManager : MonoBehaviour
{
    public int ancho;
    public int alto;
    public Wall wallPrefab;
    public Snake SnakePrefab;
    public Food FoodPrefab;
    public Canvas perdiste;
    public AudioClip comer;
    public AudioSource AudioSource;
    public GridSlot[,] grilla;
    public TextMeshProUGUI contador;
    private int comidas = 0;
    // Start is called before the first frame update
    void Start()
    {
      AudioSource = GetComponent<AudioSource>();
      AudioSource.clip = comer;
        grilla = new GridSlot[ancho, alto];

        float puntoPartidaX = transform.position.x - (ancho/2);
        float puntoPartidaY = transform.position.y + (alto/2);

        Vector2 posicionActualEnGrilla = new Vector2();
        posicionActualEnGrilla.x = puntoPartidaX;
        posicionActualEnGrilla.y = puntoPartidaY;

        for (int i = alto - 1; i >= 0; i--)
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
        for (int i = 0; i < alto; i++)
        {
            for (int j = 0; j < ancho; j++)
            {
                GridSlot currGridSlot = grilla[j, i];
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

    public void Perder()
    {
        AbrirPantallaFin();
    }

    public void AbrirPantallaFin()
    {
    perdiste.gameObject.SetActive(true);
    }
    public void Restart()
    {
    Time.timeScale = 1f; // Asegura que el tiempo esté corriendo normalmente
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }
    public void AudioComer()
    {
        AudioSource.Play();
    }
    public void Contador()
    {
        comidas++;
            if (contador != null)
            {
            contador.text = "Comida : " + comidas;
            }
    }
}

public class GridSlot
{
    public Vector2Int indiceGrilla;
    public Vector2 posicionMundo;
    public GridItem itemEnSlot;

}
