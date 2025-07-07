using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class uimanager : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void rules()
    {
        SceneManager.LoadScene("Rules");
    }
    public void exit()
    {
        Application.Quit();
    }
}
