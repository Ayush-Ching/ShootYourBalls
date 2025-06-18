using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject planePrefab;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject _xrOrigin;

    [Space]
    [SerializeField] private Camera cam;

    private void Start()
    {
        StartCoroutine(SpawnPlane());
    }

    private IEnumerator SpawnPlane()
    {
        yield return new WaitForSeconds(2f);
        Vector3 spawnPosition = cam.transform.position;

        ARPlaneManager planeManager = _xrOrigin.GetComponent<ARPlaneManager>();
        if (planeManager != null)
        {
            planeManager.enabled = false;

            foreach (var plane in planeManager.trackables)
            {
                spawnPosition.y = plane.transform.position.y;
                plane.gameObject.SetActive(false);
            }
        }

        Instantiate(planePrefab, spawnPosition, Quaternion.identity);

        float temp = spawnPosition.y;
        spawnPosition = cam.transform.position + cam.transform.forward * 3f;
        spawnPosition.y = temp + 5f;

        yield return new WaitForSeconds(5f);
        Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
    }

}
