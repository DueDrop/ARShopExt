using UnityEngine;
using UnityEngine.UI;
using CI.HttpClient;
using InfoServerObjectModel;
using System;

[RequireComponent(typeof(InfoServerConnectionManager))]
public class InfoServerIntefaceManager : MonoBehaviour {

    public Text DebugConsole;
    public InputField AdressText, PublicationText, UsernameText, PasswordText;
    public Button RunAppButton;

    private InfoServerConnectionManager connectionManager;

    #region MonoBehaviour
    void Start () {

        connectionManager = GetComponent<InfoServerConnectionManager>();

        InfoServerConnectionSettings.LoadPlayerSettings();

        AdressText.text      = InfoServerConnectionSettings.Adress;
        PublicationText.text = InfoServerConnectionSettings.Publication;
        UsernameText.text    = InfoServerConnectionSettings.Username;
        PasswordText.text    = InfoServerConnectionSettings.Password;

    }

    #endregion

    #region Logging
    // Логирование
    public void Log(InfoServerResponse r,  bool drawSeparator = false)
    {
        if (DebugConsole != null)
        {

            string msg = @"[{0:H:mm:ss}]: HTTP: {1} Код: {2} Описание: {3}";

            {
                msg = msg + Environment.NewLine + "-------------------------------------";
            }

            DebugConsole.text = string.Format(msg, DateTime.Now, r.httpCode, r.code, r.result) + Environment.NewLine + DebugConsole.text;
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

    #endregion

    #region PublicInterface

    public void LockInterface() {
        RunAppButton.interactable = false;
    }

    public void UnockInterface(){
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

    public void CheckConnection(){

        SetSettingsFromInput();
        Log(string.Format(@"Соединение с {0} ...", InfoServerConnectionSettings.Adress), true);
        connectionManager.ConnectionTest(ConnectionTestResponseHandler);
    }
    #endregion

    #region InfoServerManager_Callbacks

    private void ConnectionTestResponseHandler(InfoServerResponse response){
        Log(response);
    }

    #endregion
}
