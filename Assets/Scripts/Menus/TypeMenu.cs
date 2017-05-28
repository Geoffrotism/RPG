using UnityEngine;
using UnityEngine.UI;
using AssemblyCSharp;

public class TypeMenu : MonoBehaviour {
	public void Build(Button btn) {
        WorldController.Instance.buildMenu.BuildType = btn;
        WorldController.currentMode = WorldController.Mode.Build;
	}

    public void SetBuildingButton(Button btn)
    {
        WorldController.Instance.buildMenu.BuildObject = btn;
    }
}
