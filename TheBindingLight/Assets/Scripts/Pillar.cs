using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    public int RequiredOrbs;

    public bool IsPillarActive = false;
    public GameObject[] TargetObject;

    bool _executeOnce;

    void Update()
    { 
        if (IsPillarActive && _executeOnce)
        {
            ChangeTargetObjectsState();

            _executeOnce = false;
        }
        else if (!IsPillarActive && !_executeOnce)
        {
            ChangeTargetObjectsState();

            _executeOnce = true;
        }
    }

    private void ChangeTargetObjectsState()
    {
        foreach (GameObject _object in TargetObject)
        {
            if (_object.GetComponent<MoveBetweenPoints>() != false)
            {
                _object.GetComponent<MoveBetweenPoints>().ActivateObject = this.IsPillarActive;
            }
            else if (_object.GetComponent<Teleporter>() != false)
            {
                _object.GetComponent<Teleporter>().ActivateObject = this.IsPillarActive;
            }
            else
            {
                Debug.LogError("Object known as: '" + _object + "' was not recognized.");
            }
        }
    }
}
