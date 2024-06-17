using System;
using UnityEngine;

public class Food : GridItem
{
    public void Reposicionar()
    {
        GridSlot slot = gridArenaManager.ObtenerSlotVacioRandom();
        Debug.Log("Placed food in " + slot.indiceGrilla);

        gridArenaManager.CambiarItemEnGrilla(slot.indiceGrilla, this);
        transform.position = slot.posicionMundo;
    }
}
