using System;
using System.Net;

namespace InfoServerObjectModel
{
    // Базовый шаблон класса ответа
    [Serializable]
    public class InfoServerResponse
    {
        public int code;
        public string result;
        public HttpStatusCode httpCode;

        public virtual string LogMsg()
        {
            string msg = @"[{0:H:mm:ss}]: HTTP: {1} Код: {2} Описание: {3}";            
            return string.Format(msg, DateTime.Now, httpCode, code, result) + Environment.NewLine;
        }

    }

    [Serializable]
    public class InfoServerResponse<T>:InfoServerResponse
    {      
        public T data;

        public override string LogMsg()
        {
            string msg = @"[{0:H:mm:ss}]: HTTP: {1} Код: {2} Описание: {3}";
            if (data != null) msg += Environment.NewLine + data.ToString();
            return string.Format(msg, DateTime.Now, httpCode, code, result) + Environment.NewLine;
        }
    }

    [Serializable]
    public class InfoServerMarkerResponse {

        public string description;

        public override string ToString()
        {
            return description;
        }

    }

    [Serializable]
    public class InfoServerMarkerPoolResponse
    {
        public int[] markers;

        public override string ToString()
        {

            if (markers == null) return "Список маркеров пуст";
            string result = "[";

            bool first = true;
            foreach (int m in markers)
            {               
                result += (first ? "" : ",") + m.ToString();
            }

            result += "]";

            return result;
        }
    }

}
