using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Switch),true), CanEditMultipleObjects]
public class SwitchEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Use"))
        {
            (target as Switch).Use();
        }
        base.OnInspectorGUI();
    }
}
