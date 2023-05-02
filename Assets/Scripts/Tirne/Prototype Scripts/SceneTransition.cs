using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField]
    string _sceneName = null;

    public void ChangeScene()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
