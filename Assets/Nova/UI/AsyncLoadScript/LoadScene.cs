using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public Slider slider;

    private float progress = 0f;
    private AsyncOperation async;
    private LoadSceneNext load;

    private void Awake()
    {
        load = GameObject.Find("LoadSceneNext(Clone)").GetComponent<LoadSceneNext>();
    }
    void Start()
    {
        if (load.q.Count == 0)
            load.CleanSceneList();
        StartCoroutine(LoadAsync());
    }

    private void Update()
    {
        if (async == null)
            return;

        slider.value = progress;
        progress += 0.01f;
        if (progress >= 1f)
            async.allowSceneActivation = true;
    }

    IEnumerator LoadAsync()
    {
        async = SceneManager.LoadSceneAsync(load.q.Dequeue());
        async.allowSceneActivation = false;
        yield return async;
    }
    //public void LoadGame()
    //{
    //    StartCoroutine(StartLoading(LoadSceneName));
    //}


    //public IEnumerator StartLoading(string SceneName)
    //{
    //    AsyncOperation op = Application.LoadLevelAsync(SceneName);
    //    op.allowSceneActivation = false;

    //    //while (op.progress < 0.9f)
    //    //{
    //    //    slider.value = op.progress;
    //    //    yield return new WaitForEndOfFrame();
    //    //}

    //    for (int i = 0; i < 100; i++)
    //    {
    //        slider.value = i / 100f;
    //        yield return new WaitForEndOfFrame();
    //    }

    //    slider.value = 1;
    //    yield return new WaitForEndOfFrame();
    //    op.allowSceneActivation = true;
    //}
}
