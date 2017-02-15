using UnityEngine;
using CI.HttpClient;
using System;
using System.Net;

public class InfoServerConnectionManager : MonoBehaviour
{

    private InfoServerIntefaceManager interfaceManager;
    private bool gotInterface;
    private static HttpClient httpClient = new HttpClient();

    public void Start()
    {

        InfoServerConnectionSettings.LoadPlayerSettings();
        interfaceManager = GetComponent<InfoServerIntefaceManager>();
        gotInterface = interfaceManager != null; 

    }

    public void OnDestroy()
    {
        PlayerPrefs.Save();
        if (httpClient != null) httpClient.Abort();
        httpClient = null;
    }

    private void SetSettings()
    {

        if (gotInterface) interfaceManager.SetSettingsFromInput();

    }


    // Тест подключения
    public void ConnectionTest()
    {
        SetSettings();

        if (gotInterface) interfaceManager.Log(string.Format("Попытка соединения с {0}", InfoServerConnectionSettings.Adress), true);

        ConnectionTest(ConnectionResponseHandler);

    }

    private void ConnectionResponseHandler(HttpResponseMessage<string> obj)
    {

        if (gotInterface) interfaceManager.Log(obj);
    }


    // Получение ресурса по метке
    public void GetMarkerInfo(int id, System.Action<string> Action)
    {

        GetMarkerInfoByID(id, Action);

    }

    private static void GetMarkerInfoByID(int id, System.Action<string> Action)
    {

        if (string.IsNullOrEmpty(InfoServerConnectionSettings.Adress)
           || string.IsNullOrEmpty(InfoServerConnectionSettings.Publication))
        {
            return;
        }

        string actionString = @"http://{0}/{1}/hs/v1/getMarkerData/{2}";

        System.Uri ActionURI = new System.Uri(string.Format(actionString, InfoServerConnectionSettings.Adress, InfoServerConnectionSettings.Publication, id));
        httpClient.GetString(ActionURI, (r) => {
            Action(r.Data);
        });
    }

    private static void ConnectionTest(System.Action<HttpResponseMessage<string>> callback)
    {

        if (string.IsNullOrEmpty(InfoServerConnectionSettings.Adress)
                || string.IsNullOrEmpty(InfoServerConnectionSettings.Publication))
        {
            return;
        }

        if (!string.IsNullOrEmpty(InfoServerConnectionSettings.Username))
        {
            httpClient.Credentials = new NetworkCredential(InfoServerConnectionSettings.Username, InfoServerConnectionSettings.Password);
        }
        else
        {
            httpClient.Credentials = null;
        }

        string actionString = @"http://{0}/{1}/hs/v1/check";
        System.Uri ActionURI = new System.Uri(string.Format(actionString, InfoServerConnectionSettings.Adress, InfoServerConnectionSettings.Publication));
        httpClient.GetString(ActionURI, callback);

    }


}
