using UnityEngine;
using UnityEngine.UI;
using AssemblyCSharp;
// Covers the top in-game menu.
public class Menu_Common : MonoBehaviour {

	public void MenuToggle(GameObject menu){
		menu.SetActive(!menu.activeSelf);
	}	
}

