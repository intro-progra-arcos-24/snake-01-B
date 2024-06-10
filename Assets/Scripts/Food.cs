using UnityEngine;

public class Food : GridItem
{

    private void Start()
    {
        RandomizePosition();
    }

    public void RandomizePosition()
    {
        
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        RandomizePosition();
    }

}
