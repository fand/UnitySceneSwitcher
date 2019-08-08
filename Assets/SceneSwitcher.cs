using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] List<string> _scenes = new List<string>();
    [SerializeField] int _initialScene = 0;
    string _currentScene;

    [SerializeField] Material _material;

    void Start()
    {
        var sceneName = _scenes[_initialScene];

        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        _currentScene = sceneName;
        _material.SetFloat("_Level", 1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { LoadScene(_scenes[0]); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { LoadScene(_scenes[1]); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { LoadScene(_scenes[2]); }
    }

    void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    IEnumerator LoadSceneCoroutine(string sceneName)
    {
        var duration = 1;

        // Fade out
        var prevStart = Time.time;
        var prevEnd = Time.time + duration;

        while (Time.time < prevEnd)
        {
            var t = (prevEnd - Time.time) / duration;
            _material.SetFloat("_Level", t);
            yield return new WaitForEndOfFrame();
        }

        _material.SetFloat("_Level", 0);

        SceneManager.UnloadSceneAsync(_currentScene);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);

        // Wait til scene load
        while (!SceneManager.GetSceneByName(sceneName).isLoaded) {
            yield return new WaitForEndOfFrame();
        }

        // Fade in
        var nextStart = Time.time;
        var nextEnd = Time.time + duration;
        while (Time.time < nextEnd)
        {
            var t = (Time.time - nextStart) / duration;
            _material.SetFloat("_Level", t);
            yield return new WaitForEndOfFrame();
        }

        _currentScene = sceneName;
        yield return null;
    }
}
