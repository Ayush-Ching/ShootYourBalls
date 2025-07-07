using UnityEngine;
using System.Collections;

public class BallManager : MonoBehaviour
{
    public bool hasWon;
    public bool wasVictoryAlreadyConfirmed = false;

    public BallManager()
    {
        hasWon = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            hasWon = true;
        }
    }
    
    
}
