using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void Rules()
    {
        SceneManager.LoadScene("Rules");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
