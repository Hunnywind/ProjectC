using UnityEngine;
using System;
using System.Collections;

public class ShowPanels : MonoBehaviour {

	public GameObject optionsPanel;							//Store a reference to the Game Object OptionsPanel 
    public GameObject ruleListPanel;

    private void Update()
    {
        if(Input.GetKeyDown("escape"))
        {
            if(optionsPanel.activeSelf || ruleListPanel.activeSelf)
            {
                optionsPanel.SetActive(false);
                ruleListPanel.SetActive(false);
            }
            else
            {
                Action<bool> action = a =>
                {
                    Application.Quit();
                };
                PopupMng.GetInstance.PopupMessage("Quit", "", BUTTON_KIND.YES | BUTTON_KIND.NO, action, null, null);
            }
        }
    }

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
    public void ShowLeaderBoard()
    {
        if(GPGSMng.GetInstance != null)
            GPGSMng.GetInstance.ShowLeaderboardUI();
    }
}
