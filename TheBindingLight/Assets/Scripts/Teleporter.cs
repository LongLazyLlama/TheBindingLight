using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public bool ActivateObject;

    public GameObject TargetMirror;

    public void Teleport(GameObject player)
    {
        if (ActivateObject)
        {
            player.transform.position = TargetMirror.transform.position;
        }
        else
        {
            Debug.Log("Teleporter is inactive!");
        }
    }
}
