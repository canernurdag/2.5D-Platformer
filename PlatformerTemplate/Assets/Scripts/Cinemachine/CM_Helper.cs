using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CM_Helper : MonoBehaviour
{
    public CinemachineVirtualCamera _myCMVC;

    void Start()
    {
        _myCMVC = FindObjectOfType<CinemachineVirtualCamera>();
        Invoke("DelayTheFollowFunction", 0.4f);
    }

    void DelayTheFollowFunction() // Due to script execution order
    {
        _myCMVC.Follow = FindObjectOfType<Character_Movement>().gameObject.transform;
    }
}
