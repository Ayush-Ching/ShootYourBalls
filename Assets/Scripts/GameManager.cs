using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class GameManager : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int numberOfGoalsToWin = 3;
    [SerializeField] private int numberOfBalls = 5;

    [Space]
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
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject defeatPanel;

    [Space]
    [SerializeField] private GameObject pointsTextObject;

    [Space]
    [SerializeField] private GameObject _XROriginGameObject;

    private GameObject spawnedGoalPost;
    private GameObject spawnedBall;
    private float floorHeight;
    private int points = 0;
    private TextMeshProUGUI pointsText;
    private bool hasGameStarted;
    private float timer;

    private void Start()
    {
        points = 0;
        hasGameStarted = false;
        timer = 0;

        //lookDownCommandPanel.SetActive(true);
        spawningBallPanel.SetActive(false);
        spawningGoalPostPanel.SetActive(false);
        victoryPanel.SetActive(false);
        defeatPanel.SetActive(false);
        pointsTextObject.SetActive(false);

        StartCoroutine(SpawnEverything());
    }

    private void Update()
    {
        if(spawnedBall != null && !spawnedBall.GetComponent<BallManager>().wasVictoryAlreadyConfirmed && spawnedBall.GetComponent<BallManager>().hasWon)
        {
            spawnedBall.GetComponent<BallManager>().wasVictoryAlreadyConfirmed = true;
            points++;
            spawnedGoalPost.GetNamedChild("Motu").GetComponent<Goalie>().moveSpeed *= 1.5f;

            if (points >= numberOfGoalsToWin)
            {
                victoryPanel.SetActive(true);

                timer = 0;
                //numberOfBalls = numberOfBalls - 1; // NOT REQUIRED 
                Destroy(spawnedBall, 1f);
                Destroy(spawnedGoalPost.GetNamedChild("Motu"), 2f);
                Destroy(spawnedGoalPost, 3f);
                return;
            }
            else
            {
                StartCoroutine(SpawnNextBall());
                return;
            }
        }

        if (hasGameStarted)
        {
            pointsText.text = $"\n\n     Points : {points}\n     Balls Left : {numberOfBalls}";
        }

        if(spawnedBall!= null && spawnedBall.GetComponent<BallSwipeShoot>().ballWasSwiped && numberOfBalls > 0)
        {
            timer += Time.deltaTime;

            if(timer > 2f)
            {
                StartCoroutine(SpawnNextBall());
                return;
            }
        }

        if(spawnedBall != null && numberOfBalls <= 0 && !spawnedBall.GetComponent<BallManager>().hasWon)
        {
            defeatPanel.SetActive(true);

            timer = 0;
            Destroy(spawnedBall, 1f);
            Destroy(spawnedGoalPost.GetNamedChild("Motu"), 2f);
            Destroy(spawnedGoalPost, 3f);
            return;
        }
    }

    private IEnumerator SpawnEverything()
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

        //Instantiate(planePrefab, spawnPosition, Quaternion.identity);
        WPS.Instance.SpawnObjectAtPositionFromCamera(planePrefab, spawnPosition);
        //lookDownCommandPanel.SetActive(false);
        spawningBallPanel.SetActive(true);

        floorHeight = spawnPosition.y;

        yield return new WaitForSeconds(1f);

        spawnPosition = cam.transform.position + cam.transform.forward * 2f;
        spawnPosition.y = floorHeight + 1f;

        //spawnedBall = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
        spawnedBall = WPS.Instance.SpawnObjectAtPositionFromCamera(ballPrefab, spawnPosition);
        spawningBallPanel.SetActive(false);
        spawningGoalPostPanel.SetActive(true);

        yield return new WaitForSeconds(1f);

        spawnPosition = cam.transform.position + cam.transform.forward * 10f;
        spawnPosition.y = floorHeight;
        //spawnedGoalPost = Instantiate(goalPostPrefab, spawnPosition, Quaternion.identity);
        spawnedGoalPost = WPS.Instance.SpawnObjectAtPositionFromCamera(goalPostPrefab, spawnPosition);
        spawnedGoalPost.transform.forward = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z);
        spawningGoalPostPanel.SetActive(false);
        pointsTextObject.SetActive(true);
        pointsText = pointsTextObject.GetComponent<TextMeshProUGUI>();
        hasGameStarted = true;
    }

    private IEnumerator SpawnNextBall()
    {
        timer = 0;
        numberOfBalls--;

        yield return new WaitForSeconds(1f);

        Destroy(spawnedBall);

        Vector3 spawnPosition;
        spawnPosition = cam.transform.position + cam.transform.forward * 2f;
        spawnPosition.y = floorHeight + 1f;

        //spawnedBall = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
        spawnedBall = WPS.Instance.SpawnObjectAtPositionFromCamera(ballPrefab, spawnPosition);
    }
}
