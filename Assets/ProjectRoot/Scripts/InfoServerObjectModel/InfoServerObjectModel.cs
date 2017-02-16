using System.Net;

namespace InfoServerObjectModel
{
    // Базовый шаблон класса ответа
    [System.Serializable]
    public class InfoServerResponse
    {
        public int code;
        public string result;
        public HttpStatusCode httpCode;
    }

    [System.Serializable]
    public class InfoServerResponse<T>:InfoServerResponse
    {      
        public T data;
    }

    [System.Serializable]
    public class InfoServerMarkerResponse {

        public string description;                      

    }

    [System.Serializable]
    public class InfoServerMarkerPoolResponse
    {
        int[] idarray;
    }

}
