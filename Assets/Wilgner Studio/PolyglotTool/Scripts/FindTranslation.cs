using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Polyglot;
#if TMP
using TMPro;
#endif

public class FindTranslation : MonoBehaviour {

    public string nameId;
    public Text text;
	public Dropdown dropdown;
#if TMP
    public TMP_Text textP;
	public TMP_Dropdown dropdownTMP;
#endif
    public PolyglotSave polyglot;
    public List<Translation> searchTranslations;

    private LanguageControl lc;

    // Use this for initialization
    void Start () {
        this.text = this.GetComponent<Text> ();
		this.dropdown = this.GetComponent<Dropdown> ();

#if TMP
        this.textP = this.GetComponent<TMP_Text>();
		this.dropdownTMP = this.GetComponent<TMP_Dropdown> ();
#endif

        this.lc = GameObject.FindObjectOfType<LanguageControl>();
        if (polyglot == null)
            Debug.Log("Polyglot is null!");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public string GetSaveLocalPath()
    {
        return "Assets/Resources/Polyglot.asset";
    }

    public void SetText()
    {
        Translation t = polyglot.GetTranslationByName(nameId, lc.selectedLanguage);
        if (t != null)
        {
#if TMP
            if(textP != null)
                this.textP.text = t.translation;
#endif

            if (text != null)
                this.text.text = t.translation;
			else if (t.isDropdown == true){
				int i = 0;
				foreach (var a in t.dropdownTranslation){
					if(dropdown != null)
						dropdown.options [i].text = a.ToString ();
					
					#if TMP
					if(dropdownTMP != null)
						dropdownTMP.options [i].text = a.ToString ();
					#endif
					i++;
				}
				if(dropdown != null)
					dropdown.captionText.text = dropdown.options [dropdown.value].text;
				#if TMP
				if(dropdownTMP != null){
					dropdownTMP.captionText.text = dropdownTMP.options [dropdownTMP.value].text;

				}
				#endif
			}

        }
    }
}
