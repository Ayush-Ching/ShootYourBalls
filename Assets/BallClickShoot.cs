using UnityEngine;
using UnityEngine.EventSystems;

public class BallClickShoot : MonoBehaviour, IPointerClickHandler
{
    private Rigidbody rb;
    //private bool hasBeenShot = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.isKinematic = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //if (!hasBeenShot)
        //{
            rb.isKinematic = false;
            rb.AddForce(Camera.main.transform.up * 200f); // Adjust force as needed
            rb.AddForce(Camera.main.transform.forward * 200f); // Adjust force as needed
            //hasBeenShot = true;
        //}
    }
}
