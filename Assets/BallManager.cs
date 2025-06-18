using UnityEngine;

public class BallManager : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform arCamera;
    private GameObject currentBall;

    void Start()
    {
        SpawnBall();
    }

    public void SpawnBall()
    {
        if (currentBall != null) Destroy(currentBall);

        Vector3 spawnPos = arCamera.position + arCamera.forward * 1f;
        currentBall = Instantiate(ballPrefab, spawnPos, Quaternion.identity);
    }
}
