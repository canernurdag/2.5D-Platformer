using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Actions : MonoBehaviour
{
    public static Game_Actions _Instance;

    private void OnEnable()
    {
        Game_Events._Instance._onLevelCompletedSecond += LoadLevelSelectScene;
    }

    private void Start()
    {
        
    }


    public void LoadLevelSelectScene(GameObject _null)
    {
        SceneManager.LoadScene(2); //2 = Level Select Scene
    }
}
