using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehaviour : MonoBehaviour // Base class wrapper for all MonoBehaviours
{

    public Transform FindInChild(string childName)
    {

        return FindInChild(childName, transform);
    }

    private Transform FindInChild(string childName, Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.name == childName)
            {
                return child;
            }
            else
            {
                Transform found = FindInChild(childName, child);
                if (found != null)
                {
                    return found;
                }
            }
        }
        return null;
    }
}
