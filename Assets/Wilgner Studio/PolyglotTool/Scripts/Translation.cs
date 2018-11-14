/*
* Author: Wilgner Fábio
* Contributors: N0BODE
*/
using System;
using System.Collections.Generic;

namespace Polyglot
{
	[System.Serializable]
	public class Translation
	{
		public int indexLanguage;
		public string nameID;
		public string translation;
		public int idUniqueElements;
		public bool isDropdown = false;
		public List<string> dropdownTranslation = new List<string> ();
		public Categories categories;


		public Translation(int indexLanguage, string nameID, string translation, int idUniqueElements, Categories categories, bool isDropdown)
		{
			this.indexLanguage = indexLanguage;
			this.nameID = nameID;
			this.translation = translation;
			this.categories = categories;
			this.idUniqueElements = idUniqueElements;
			this.isDropdown = isDropdown;
		}
	}
}