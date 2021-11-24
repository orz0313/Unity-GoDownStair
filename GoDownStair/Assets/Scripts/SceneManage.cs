using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManage : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("PlayScene");
    }
    
    public void Quit() 
    {
        Application.Quit();
    }
}
