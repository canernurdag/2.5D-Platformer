using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Particles : MonoBehaviour
{
    public ParticleSystem _myParticleSystem;

    private void OnEnable()
    {
        Game_Events._Instance._onLevelCompletedFirst += LevelCompleteCharacterParticles;
    }

    private void Start()
    {
 
        _myParticleSystem = GetComponent<ParticleSystem>();
    }

    public void LevelCompleteCharacterParticles(GameObject _null)
    {
        //Play Particles Here
    }

  
    private void OnDisable()
    {
        Game_Events._Instance._onLevelCompletedFirst -= LevelCompleteCharacterParticles;
    }

}
