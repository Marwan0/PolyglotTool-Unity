/*
* Author: Wilgner Fábio
* Contributors: N0BODE
*/
using UnityEngine;
using UnityEditor;
using Polyglot;

public class DropDownWindow : EditorWindow {
	public Translation t;
	public Vector2 scrollTranslations;
	public int selectedLanguage;
	public int selectedLanguageCategories;

	// Save/Load data in ScriptableObject (PolyglotSave)
	private PolyglotSave polyglot;

	#region Path where data is saved
	public string GetSaveLocalPath()
	{
		return "Assets/Resources/Polyglot.asset";
	}
	#endregion

	private void OnEnable()
	{
		
		#region Attempts to load data from the ScriptableObject if it exists, if there is no create a new
		this.polyglot = AssetDatabase.LoadAssetAtPath<PolyglotSave>(this.GetSaveLocalPath());
		#endregion
	}

	void OnGUI() {
		#region List Dropdown Translation HEADER
		GUILayout.BeginHorizontal();
		GUILayout.BeginVertical("Box", GUILayout.MinWidth(position.width*0.99f));
		GUILayout.Label(string.Format("{0}: Dropdown Translation", t.nameID), EditorStyles.boldLabel);
		float size3 = (position.width*0.98f)/3;
		GUILayout.BeginHorizontal("CN Box", GUILayout.MinWidth(position.width*0.98f), GUILayout.MaxHeight(7));
		GUILayout.BeginHorizontal();
		GUILayout.BeginVertical(GUILayout.MinWidth(size3));
		GUILayout.Label("ID", EditorStyles.boldLabel);
		GUILayout.EndVertical();
		GUILayout.BeginVertical(GUILayout.MinWidth(size3));
		GUILayout.Label("Translation", EditorStyles.boldLabel);
		GUILayout.EndVertical();
		GUILayout.BeginVertical(GUILayout.MinWidth(size3));
		GUILayout.Label("Action", EditorStyles.boldLabel);
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
		GUILayout.EndHorizontal();
		#endregion

		#region List Translations of DropDown
		scrollTranslations = EditorGUILayout.BeginScrollView(scrollTranslations, GUILayout.Width(position.width*0.99f), GUILayout.Height(170));
		for(int i=0; i<t.dropdownTranslation.Count; i++){
			GUILayout.BeginHorizontal();
			GUILayout.BeginVertical(GUILayout.MinWidth(size3));
			GUILayout.Label(i.ToString());
			GUILayout.EndVertical();
			GUILayout.BeginVertical(GUILayout.MinWidth(size3));
			t.dropdownTranslation[i] = GUILayout.TextField(t.dropdownTranslation[i]);
			GUILayout.EndVertical();
			GUILayout.BeginVertical(GUILayout.MinWidth(size3));
			if (GUILayout.Button("\u00D7", GUILayout.MaxWidth(30)))
			{
				if (EditorUtility.DisplayDialog("Delete Translation (Dropdown)", string.Format("Are you sure you want to delete the ( {0} {1} ) ?", i, t.dropdownTranslation[i]), "Yes", "No"))
				{
					RemoveElementBrother(t.idUniqueElements, i);
					t.dropdownTranslation.RemoveAt(i);
				}
			}
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
		}
		#endregion
		EditorGUILayout.EndScrollView ();
		GUILayout.FlexibleSpace ();
		GUILayout.BeginHorizontal();
		if (GUILayout.Button ("Add New Translation", GUILayout.MinWidth(position.width*0.98f))) {
			//t.dropdownTranslation.Add (string.Format("Option {0}", t.dropdownTranslation.Count));
			// Creates the new translation in all available languages
			int count = t.dropdownTranslation.Count;
			t.dropdownTranslation.Add (string.Format("Option {0}", count));
			AddElementBrother (t.idUniqueElements, count);
		}
		GUILayout.EndHorizontal();
		GUILayout.EndVertical();
	}

	// Changes the brother element in the other languages and get the brother
	void AddElementBrother(int idUniqueElements, int count)
	{
		// Scroll through the translations list
		foreach (Translation te in polyglot.translations)
		{
			// Check if the translation is from the language selected
			if (te.indexLanguage != selectedLanguage)
			{
				// Check if the translation is from the selected category
				if (selectedLanguageCategories == te.categories.index)
				{
					
					// Check if id's are brothers
					if (te.idUniqueElements == idUniqueElements) {
						te.dropdownTranslation.Add (string.Format ("Option {0}", count));
						Debug.Log ("ADD");
					}
				}
			}
		}
	}

	void RemoveElementBrother(int idUniqueElements, int i)
	{
		// Scroll through the translations list
		foreach (Translation te in polyglot.translations)
		{
			// Check if the translation is from the language selected
			if (te.indexLanguage != selectedLanguage)
			{
				// Check if the translation is from the selected category
				if (selectedLanguageCategories == te.categories.index)
				{
					// Check if id's are brothers
					if (te.idUniqueElements == idUniqueElements)
					{
						te.dropdownTranslation.RemoveAt (i);
					}
				}
			}
		}
	}

	public void Init(Translation tn, int selectedLanguage, int selectedLanguageCategories){
		t = tn;
		this.selectedLanguage = selectedLanguage;
		this.selectedLanguageCategories = selectedLanguageCategories;
	}
}
