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
        bool gravity = LevelManager.Instance.Gravity;
        if( GUILayout.Button( string.Format("Turn {0} Gravity", gravity ? "off" : "on") ) )
        {
            LevelManager.Instance.Gravity = !gravity;
        }
        if (GUILayout.Button("Stop"))
        {
            LevelManager.Instance.Stop();
        }
        EditorUtility.SetDirty(target);
    }
}
