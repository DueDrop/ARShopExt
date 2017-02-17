using UnityEngine;

public static class InfoServerConnectionSettings {
    // Настройки соединения
    private static string srvAdress;
    private static string srvPublicationID;
    private static string srvUsername;
    private static string srvPassword;

    public static string Username
    {
        set { srvUsername = value; }
        get { return srvUsername; }
    }

    public static string Password
    {
        set { srvPassword = value; }
        get { return srvPassword; }
    }

    public static string Adress{
        set{ srvAdress = value;}
        get{ return srvAdress; }
    }


    public static string Publication
    {
        set { srvPublicationID = value;}
        get { return srvPublicationID; }
    }

    public static void LoadPlayerSettings()
    {
        InfoServerConnectionSettings.Adress = PlayerPrefs.GetString("conn_Adress");
        InfoServerConnectionSettings.Publication = PlayerPrefs.GetString("conn_Publication");
        InfoServerConnectionSettings.Username = PlayerPrefs.GetString("conn_UserName");
        InfoServerConnectionSettings.Password = PlayerPrefs.GetString("conn_UserPassword");
    }

}
