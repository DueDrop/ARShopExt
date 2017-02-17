using UnityEngine;
using UnityEngine.UI;

public class InfoPanelManager : MonoBehaviour {

    public RectTransform InfoPanel;
    public Text InfoText;
	
    public void ShowPanel(string info) {

        InfoText.text = info;
        InfoPanel.gameObject.SetActive(true);

    }

    public void  HidePanel() {

        InfoPanel.gameObject.SetActive(false);

    }

}
