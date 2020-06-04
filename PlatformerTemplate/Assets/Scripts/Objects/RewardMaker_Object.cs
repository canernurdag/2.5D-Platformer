using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RewardMaker_Object : MonoBehaviour
{
    public GameObject _niagaraPrefab;
    public bool _IsPrefabInstantiated;
    public bool _IsInstantiatedObjectReadyToMove;
    public float _offsetToRaiseOfInstantiatedObject;

    public MeshRenderer _myMeshRenderer;
    public Material _emptyRewardObjectMaterial;
  

    private void Start()
    {
        DOTween.Init();
        _myMeshRenderer = GetComponent<MeshRenderer>();

        Game_Events._Instance._onCharacterHitRewardObject += ChangeRewardObjectMaterial;
        Game_Events._Instance._onCharacterHitRewardObject += InstantiateNiagaraFunction;

        _IsPrefabInstantiated = false;
        _IsInstantiatedObjectReadyToMove = false;
        _offsetToRaiseOfInstantiatedObject = 2f;

}

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint _myContactPoint = collision.GetContact(0);

        if(_myContactPoint.normal.y > 0.05f && !_IsPrefabInstantiated)
        { 
            Game_Events._Instance.CharacterHitRewardObjectSequence(collision.gameObject);
            _IsPrefabInstantiated = true;
        }
    }

    public void ChangeRewardObjectMaterial(GameObject _gameObject)
    {
        _myMeshRenderer.material = _emptyRewardObjectMaterial;
    }

    public void InstantiateNiagaraFunction(GameObject _gameObject)
    {
        GameObject _tempNiagara = Instantiate(_niagaraPrefab, transform.position, Quaternion.Euler(-90,0,0));
        _tempNiagara.transform.SetParent(this.transform);
        _tempNiagara.transform.localScale = Vector3.zero;
        _tempNiagara.transform.DOScale(Vector3.one, 1.5f); //Scale 0 to 1 within 1.5 seconds
        _tempNiagara.transform.DOMove(new Vector3(_tempNiagara.transform.position.x, _tempNiagara.transform.position.y + _offsetToRaiseOfInstantiatedObject, _tempNiagara.transform.position.z), 1.5f).OnComplete(()=> { _IsInstantiatedObjectReadyToMove = true; });

    }

    public void OnDisable()
    {
        Game_Events._Instance._onCharacterHitRewardObject -= ChangeRewardObjectMaterial;
        Game_Events._Instance._onCharacterHitRewardObject -= InstantiateNiagaraFunction;
    }



}
