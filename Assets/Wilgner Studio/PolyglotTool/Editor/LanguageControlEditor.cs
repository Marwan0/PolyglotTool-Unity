using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Polyglot;

[CustomEditor(typeof(LanguageControl))]
public class LanguageControlEditor : Editor
{
    private LanguageControl languageControl;
    private SerializedObject script;

    public SerializedProperty LanguageChanged;

    public void OnEnable()
    {
        languageControl = (LanguageControl)target;
        script = new SerializedObject(target);

        LanguageChanged = script.FindProperty("LanguageChanged");
    }

    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector ();
        script.Update();
        EditorGUI.BeginChangeCheck();
        this.languageControl.polyglot = AssetDatabase.LoadAssetAtPath<PolyglotSave>(languageControl.GetSaveLocalPath());
		if (this.languageControl.polyglot != null)
			languageControl.selectedLanguage = EditorGUILayout.Popup ("Selected Languages: ", languageControl.selectedLanguage, languageControl.polyglot.languages.ToArray ());
		else
			GUILayout.Label ("Polyglot Save was not found!");
        EditorGUILayout.PropertyField(LanguageChanged);

        if (EditorGUI.EndChangeCheck())
        {
            script.ApplyModifiedProperties();
        }
    }
}
