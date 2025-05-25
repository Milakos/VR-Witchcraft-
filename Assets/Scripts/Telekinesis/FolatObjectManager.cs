using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable] 
public class FloatObjects
{
    public FloatObject floatObjects;
    public bool isFloating;
}
public class FolatObjectManager : MonoBehaviour
{
    public List<FloatObjects> _floatObjects;
    public Vector3 offset;
    private void Awake()
    {
        var foundObjects = FindObjectsOfType<FloatObject>().ToList();
        _floatObjects.Clear();

        // Populate _floatObjects list
        foreach (var obj in foundObjects)
        {
            _floatObjects.Add(new FloatObjects
            {
                floatObjects = obj,
                isFloating = false // or true if you want by default
            });
        }
    }
}
