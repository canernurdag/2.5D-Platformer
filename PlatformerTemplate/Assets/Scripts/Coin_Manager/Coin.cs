using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void Start()
    {
        Game_Events._Instance._onCoinCollected += DestroyCoinAfterCollected;
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 2, 0));
    }

    public void DestroyCoinAfterCollected(GameObject _gameObject)
    {
        Destroy(_gameObject);
    }

    private void OnDisable()
    {
        Game_Events._Instance._onCoinCollected -= DestroyCoinAfterCollected;
    }

}
