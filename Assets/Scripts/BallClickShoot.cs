using UnityEngine;
using UnityEngine.EventSystems;

public class BallClickShoot : MonoBehaviour, IPointerClickHandler
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
            rb.isKinematic = false;
            rb.AddForce(Camera.main.transform.up * 500f); // Adjust force as needed
            rb.AddForce(Camera.main.transform.forward * 500f); // Adjust force as needed
    }
}
