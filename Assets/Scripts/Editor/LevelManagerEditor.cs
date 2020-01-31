using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelManager))]
public class LevelManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Function buttons
        bool gravity = LevelManager.instance.Gravity;
        if( GUILayout.Button( string.Format("Turn {0} Gravity", gravity ? "off" : "on") ) )
        {
            LevelManager.instance.Gravity = !gravity;
        }
        if (GUILayout.Button("Stop"))
        {
            LevelManager.instance.Stop();
        }

    }
}
