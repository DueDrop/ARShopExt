using UnityEngine;
using CI.HttpClient;
using System;
using System.Net;
using InfoServerObjectModel;

public class InfoServerConnectionManager : MonoBehaviour
{
    #region fields
    private static HttpClient httpClient = new HttpClient();
    #endregion

    #region MonoBehaviour

    public void Start()
    {

        InfoServerConnectionSettings.LoadPlayerSettings();
        if (httpClient == null) httpClient = new HttpClient();

    }

    public void OnDestroy()
    {
        PlayerPrefs.Save();
        if (httpClient != null) httpClient.Abort();
        httpClient = null;
    }

    #endregion

    #region CommonInternal
    private static bool ConnectionSettingsIncorrect()
    {
        return string.IsNullOrEmpty(InfoServerConnectionSettings.Adress)
               || string.IsNullOrEmpty(InfoServerConnectionSettings.Publication);

    }

    private static void SetCredentials() {

        if (!string.IsNullOrEmpty(InfoServerConnectionSettings.Username))
        {
            if (httpClient.Credentials == null) httpClient.Credentials = new NetworkCredential(InfoServerConnectionSettings.Username, InfoServerConnectionSettings.Password);
        }
        else
        {
            httpClient.Credentials = null;
        }

    }
    #endregion

    #region Connection
  
    public void ConnectionTest(Action<InfoServerResponse> Action)
    {

        if (ConnectionSettingsIncorrect()) return;

        SetCredentials();

        string actionString = @"http://{0}/{1}/hs/v1/check";
        Uri ActionURI = new Uri(string.Format(actionString, InfoServerConnectionSettings.Adress, InfoServerConnectionSettings.Publication));
        httpClient.GetString(ActionURI, (r) => {

            InfoServerResponse response = JsonUtility.FromJson<InfoServerResponse>(r.Data);
            response.httpCode = r.StatusCode;

            Action(response);

        });

    }

    #endregion

    #region Markers

    public void GetMarkerInfoByID(int id, Action<InfoServerResponse<InfoServerMarkerResponse>> Action)
    {

        if (ConnectionSettingsIncorrect()) return;

        SetCredentials();

        string actionString = @"http://{0}/{1}/hs/v1/markerinfo/{2}";

        Uri ActionURI = new Uri(string.Format(actionString, InfoServerConnectionSettings.Adress, InfoServerConnectionSettings.Publication, id));
        httpClient.GetString(ActionURI, (r) => {
             
            InfoServerResponse<InfoServerMarkerResponse> response = JsonUtility.FromJson<InfoServerResponse<InfoServerMarkerResponse>>(r.Data);
            response.httpCode = r.StatusCode;

            Action(response);

        });
    }

    public void GetMarkerPool(Action<InfoServerResponse<InfoServerMarkerPoolResponse>> Action)
    {

        if (ConnectionSettingsIncorrect()) return;

        SetCredentials();

        string actionString = @"http://{0}/{1}/hs/v1/getpool";

        Uri ActionURI = new Uri(string.Format(actionString, InfoServerConnectionSettings.Adress, InfoServerConnectionSettings.Publication));
        httpClient.GetString(ActionURI, (r) => {

            InfoServerResponse<InfoServerMarkerPoolResponse> response = JsonUtility.FromJson<InfoServerResponse<InfoServerMarkerPoolResponse>>(r.Data);

            response.httpCode = r.StatusCode;
            Action(response);

        });
    }

    #endregion

}
