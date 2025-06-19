using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goalie : MonoBehaviour
{
    public float moveSpeed = 3f;

    private Rigidbody rb;
    private GameObject goToMark;
    private int dir;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        dir = -1;
    }

    private void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x + dir * moveSpeed * Time.deltaTime, transform.localPosition.y, transform.localPosition.z);

        if(transform.localPosition.x < -2) dir = 1;
        if (transform.localPosition.x > 2.3) dir = -1;
    }


}
