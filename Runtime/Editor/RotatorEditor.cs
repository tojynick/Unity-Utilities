using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Rotator))]
public class RotatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Rotator rotator = target as Rotator;
        
        if(GUILayout.Button("StartRotation"))
            rotator.StartRotation();
        
        if(GUILayout.Button("StopRotation"))
            rotator.StopRotation();
    }
}
