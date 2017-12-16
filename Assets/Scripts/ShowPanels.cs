using UnityEngine;
using System.Collections;

public class ShowPanels : MonoBehaviour {

	public GameObject optionsPanel;							//Store a reference to the Game Object OptionsPanel 
    public GameObject ruleListPanel;

	public void ShowOptionsPanel()
	{
		optionsPanel.SetActive(true);
	}
	public void HideOptionsPanel()
	{
		optionsPanel.SetActive(false);
	}
    public void ShowRuleListPanel()
    {
        ruleListPanel.SetActive(true);
    }
    public void HideRuleListPanel()
    {
        ruleListPanel.SetActive(false);
    }
}
