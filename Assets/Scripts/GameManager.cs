using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GameManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject planePrefab;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject goalPostPrefab;

    [Space]
    [Header("Camera")]
    [SerializeField] private Camera cam;

    [Space]
    [Header("UI Panels")]
    [SerializeField] private GameObject lookDownCommandPanel;
    [SerializeField] private GameObject spawningBallPanel;
    [SerializeField] private GameObject spawningGoalPostPanel;

    [Space]
    [SerializeField] private GameObject _XROriginGameObject;

    private void Start()
    {
        lookDownCommandPanel.SetActive(true);
        spawningBallPanel.SetActive(false);
        spawningGoalPostPanel.SetActive(false);

        StartCoroutine(SpawnPlane());
    }

    private IEnumerator SpawnPlane()
    {
        yield return new WaitForSeconds(2f);
        Vector3 spawnPosition = cam.transform.position;

        ARPlaneManager planeManager = _XROriginGameObject.GetComponent<ARPlaneManager>();
        if (planeManager != null)
        {
            while (planeManager.trackables.count <= 0)
            {
                yield return new WaitForSeconds(0.5f);
            }

            planeManager.enabled = false;

            foreach (var plane in planeManager.trackables)
            {
                spawnPosition.y = plane.transform.position.y;
                plane.gameObject.SetActive(false);
            }
        }

        Instantiate(planePrefab, spawnPosition, Quaternion.identity);
        lookDownCommandPanel.SetActive(false);
        spawningBallPanel.SetActive(true);

        float temp = spawnPosition.y;

        yield return new WaitForSeconds(3f);

        spawnPosition = cam.transform.position + cam.transform.forward * 3f;
        spawnPosition.y = temp + 3f;

        Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
        spawningBallPanel.SetActive(false);
        spawningGoalPostPanel.SetActive(true);

        yield return new WaitForSeconds(3f);

        spawnPosition = cam.transform.position + cam.transform.forward * 10f;
        spawnPosition.y = temp;
        Instantiate(goalPostPrefab, spawnPosition, Quaternion.identity);
        spawningGoalPostPanel.SetActive(false);
    }

}
