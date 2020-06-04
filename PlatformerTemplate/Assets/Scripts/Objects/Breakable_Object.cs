using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable_Object : MonoBehaviour
{
    public BoxCollider _myBoxCollider;
    public ParticleSystem _myParticleSystem;
    public MeshRenderer _myMeshRenderer;

    private void Start()
    {
        _myBoxCollider = GetComponent<BoxCollider>();
        _myParticleSystem = GetComponent<ParticleSystem>();
        _myMeshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("CharacterLayer"))
        {
            ContactPoint _myContactPoint = collision.GetContact(0);

            if(_myContactPoint.normal.y > 0.05f)
            {
                StartCoroutine(BreakObject(collision.gameObject)); 
            } 
        }
    }

    public IEnumerator BreakObject(GameObject _gameObject)
    {
        Game_Events._Instance.ObjectBreak(_gameObject);
        _myParticleSystem.Play();
        _myBoxCollider.enabled = false;
        _myMeshRenderer.enabled = false;

        yield return new WaitForSeconds(_myParticleSystem.main.startLifetime.constantMax);
        Destroy(this.gameObject);
    }

}
