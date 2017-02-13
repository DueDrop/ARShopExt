using UnityEngine;
using UnityEngine.UI;
using CI.HttpClient;
using System;

public class InfoServerIntefaceManager : MonoBehaviour {

    public Text DebugConsole;
    public InputField AdressText, PublicationText, UsernameText, PasswordText;
    public Button RunAppButton;

    // Use this for initialization
    void Start () {

        InfoServerConnectionSettings.LoadPlayerSettings();

        AdressText.text      = InfoServerConnectionSettings.Adress;
        PublicationText.text = InfoServerConnectionSettings.Publication;
        UsernameText.text    = InfoServerConnectionSettings.Username;
        PasswordText.text    = InfoServerConnectionSettings.Password;

    }

    // Логирование
    public void Log(HttpResponseMessage<string> s)
    {
        if (DebugConsole != null)
        {

            string msg = @"[{0:H:mm:ss}]: Статус: {1}  Тело: {2}";

            DebugConsole.text = string.Format(msg, DateTime.Now, s.StatusCode, s.Data) + Environment.NewLine + DebugConsole.text;
        }
    }

    public void Log(string s, bool drawSeparator = false)
    {
        if (DebugConsole != null)
        {

            string msg = @"[{0:H:mm:ss}]: {1}";
            if (drawSeparator)
            {
                msg = msg + Environment.NewLine + "-------------------------------------";
            }

            DebugConsole.text = string.Format(msg, DateTime.Now, s) + Environment.NewLine + DebugConsole.text;
        }
    }

    public void LockInterface() {

        RunAppButton.interactable = false;

    }

    public void UnockInterface()
    {

        RunAppButton.interactable = true;

    }

    public void SetSettingsFromInput()
    {
        InfoServerConnectionSettings.Adress = AdressText.text;
        InfoServerConnectionSettings.Publication = PublicationText.text;
        InfoServerConnectionSettings.Username = UsernameText.text;
        InfoServerConnectionSettings.Password = PasswordText.text;

        PlayerPrefs.SetString("conn_Adress", InfoServerConnectionSettings.Adress);
        PlayerPrefs.SetString("conn_Publication", InfoServerConnectionSettings.Publication);
        PlayerPrefs.SetString("conn_UserName", InfoServerConnectionSettings.Username);
        PlayerPrefs.SetString("conn_UserPassword", InfoServerConnectionSettings.Password);
    }
}
