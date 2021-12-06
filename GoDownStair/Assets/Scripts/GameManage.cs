using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManage : MonoBehaviour
{
    [SerializeField]GameObject PauseMenu;
    [SerializeField]GameObject DeadMenu;
    [SerializeField]Text DeadText;
    bool PauseCalled = false;

    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(PauseCalled)
            {
                return;
            }
            else
            {
                CallPauseMenu();
            }
        }
    }
    public void BackToStartMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartMenuScene");
    }
    public void CallDeadMenu()
    {
        ChangeDeadText();
        Time.timeScale = 0;
        DeadMenu.SetActive(true);
        PauseCalled = true;
    }
    public void CallPauseMenu()
    {
        if(PauseCalled == true)
        {
            return;
        }
        
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
        PauseCalled = true;
    }
    public void Resume()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        PauseCalled = false;
    }
    public void Retry()
    {
        Time.timeScale = 1;
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
    void ChangeDeadText()
    {
        DeadText.text = "You die in "+((int)(Time.time/5)).ToString() + " floor";
    }
}
