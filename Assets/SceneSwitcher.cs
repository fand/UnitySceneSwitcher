using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] List<string> _scenes = new List<string>();
    [SerializeField] int _initialScene = 0;
    string _currentScene;

    void Start()
    {
        var sceneName = _scenes[_initialScene];
        LoadScene(sceneName);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { LoadScene(_scenes[0]); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { LoadScene(_scenes[1]); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { LoadScene(_scenes[2]); }
    }

    void LoadScene(string sceneName)
    {
        if (_currentScene != null)
        {
            SceneManager.UnloadSceneAsync(_currentScene);
        }

        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);

        _currentScene = sceneName;
    }

}
