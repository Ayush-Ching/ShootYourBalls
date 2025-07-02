using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class backbuttonscript : MonoBehaviour
{
   public void back()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
