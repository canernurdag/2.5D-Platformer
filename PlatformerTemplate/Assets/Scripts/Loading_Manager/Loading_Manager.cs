using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading_Manager : MonoBehaviour
{
    public GameObject _myLoadingSlider; // To Control Loading Slider

    private void Start()
    {
        StartCoroutine(LoadGameAsynchronlusly());
    }

    public IEnumerator LoadGameAsynchronlusly()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        _myLoadingSlider.SetActive(true);

        while (operation.isDone == false)
        {
            float _progress = Mathf.Clamp01(operation.progress / 0.9f);
            _myLoadingSlider.GetComponent<Slider>().value = _progress;
            yield return null;
        }

    }
}
