using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ParticleSystem : MonoBehaviour
{
    public ParticleSystem _myParticleSystem { get; set; }
    

     public void Start()
    {
        _myParticleSystem = GetComponent<ParticleSystem>();
        Game_Events._Instance._onEnemyDie += PlayDieParticleEffect;

    }

    public void PlayDieParticleEffect(GameObject _enemy)
    {
       this.gameObject.transform.position = _enemy.transform.position; //Particle System TO Enemy's location.
       _myParticleSystem.Play();
        
    }

    public void OnDestroy()
    {
        Game_Events._Instance._onEnemyDie -= PlayDieParticleEffect;
    }

    public void OnDisable()
    {
        Game_Events._Instance._onEnemyDie -= PlayDieParticleEffect;
    }
}
