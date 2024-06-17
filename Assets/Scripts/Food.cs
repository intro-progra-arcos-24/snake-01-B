using System;
using UnityEngine;

public class Food : GridItem
{

    private void Start()
    {
        Reposicionar();
    }

    public void Reposicionar()
    {
        GridSlot slot = gridArenaManager.ObtenerSlotVacioRandom();

        gridArenaManager.CambiarItemEnGrilla(slot.indiceGrilla, this);
        // Move the snake in the direction it is facing
        float x = slot.posicionMundo.x;
        float y = slot.posicionMundo.y;
        transform.position = new Vector2(x, y);
    }
}
