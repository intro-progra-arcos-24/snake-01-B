using System;
using UnityEngine;

public class Food : GridItem
{
    public void Reposicionar()
    {
        GridSlot slot = gridArenaManager.ObtenerSlotVacioRandom();
        if (slot.indiceGrilla.x != 0 && slot.indiceGrilla.y != 0 && slot.indiceGrilla.x != gridArenaManager.ancho - 1 && slot.indiceGrilla.y != gridArenaManager.alto - 1)
        {
            Debug.Log("Placed food in " + slot.indiceGrilla);
            gridArenaManager.CambiarItemEnGrilla(slot.indiceGrilla, this);
            transform.position = slot.posicionMundo;
        }
        else Reposicionar();
        
    }
}
