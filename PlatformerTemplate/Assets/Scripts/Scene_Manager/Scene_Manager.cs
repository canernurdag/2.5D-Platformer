using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    public static Scene_Manager _Instance;

    public int _currentSceneIndex;


    #region SINGLETON Pattern
    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private void Start()
    {
        SceneManager.activeSceneChanged += GetCurrentSceneIndex;
        SceneManager.activeSceneChanged += Sound_Manager._Instance.SelectMusicAndPlay;
        Game_Events._Instance._onCharacterDieSecond += GetGameMainScene;
        Game_Events._Instance._onLevelCompletedSecond += GetGameMainScene;
        Game_Events._Instance._onGameFinished += GetGameMainScene;

    }

    public void GetCurrentSceneIndex(Scene _current, Scene _next)
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void GetGameMainScene(GameObject _null)
    {
        SceneManager.LoadScene(2);
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= GetCurrentSceneIndex;
        SceneManager.activeSceneChanged -= Sound_Manager._Instance.SelectMusicAndPlay;
        Game_Events._Instance._onCharacterDieSecond -= GetGameMainScene;
        Game_Events._Instance._onLevelCompletedSecond -= GetGameMainScene;
        Game_Events._Instance._onGameFinished -= GetGameMainScene;
    }
    
  

}
