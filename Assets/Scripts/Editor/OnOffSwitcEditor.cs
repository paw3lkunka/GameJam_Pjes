using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OnOffSwitch))]
public class OnOffSwitchEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Turn on"))
        {
            (target as OnOffSwitch).On();
        }
        if (GUILayout.Button("Turn off"))
        {
            (target as OnOffSwitch).Off();
        }
        if (GUILayout.Button("Use"))
        {
            (target as OnOffSwitch).Use();
        }

        GUILayout.EndHorizontal();

        base.OnInspectorGUI();
    }
}
