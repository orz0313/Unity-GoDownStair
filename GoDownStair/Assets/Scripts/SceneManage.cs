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
        #if (UNITY_EDITOR)
        UnityEditor.EditorApplication.isPlaying = false;
        #elif (UNITY_STANDALONE) 
        Application.Quit();
        #elif (UNITY_WEBGL)
        Application.OpenURL("about:blank");
        #endif
    }
}
