using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeBtn : MonoBehaviour
{
    public void OnChangeScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void ReloardScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
