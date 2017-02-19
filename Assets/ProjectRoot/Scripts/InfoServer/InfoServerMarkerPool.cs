using UnityEngine;
using InfoServerObjectModel;

[RequireComponent(typeof(InfoServerConnectionManager))]
public class InfoServerMarkerPool: MonoBehaviour {
    
    public GameObject MarkerPoolRoot;
    public GameObject TagPrefab;
    private InfoServerConnectionManager connectionManager;
	
    public void Start(){

        connectionManager = GetComponent<InfoServerConnectionManager>();

        if (connectionManager != null)
        {
            connectionManager.GetMarkerPool(GetMarkerPoolHandler);
        }
    }

    protected void GetMarkerPoolHandler(InfoServerResponse<InfoServerMarkerPoolResponse> r) {

        if (r.code != 0) return;
        init(r.data.markers);        

    }

    public void init(int[] markers){

        // Уничтожение всех маркеров, как правило они будут находиться на корневом объекте.
        foreach (ARMarker am in MarkerPoolRoot.GetComponents<ARMarker>()) Destroy(am);     
           
        // Уничтожение всех отслеживаемых объектов. Убиваем не компонент, а весь объект.
        foreach(ARTrackedObject ato in FindObjectsOfType<ARTrackedObject>()) Destroy(ato.gameObject);        

        // Создание новых маркеров.
        foreach(int m in markers){

            ARMarker newMarker = MarkerPoolRoot.AddComponent<ARMarker>();
            newMarker.MarkerType = MarkerType.SquareBarcode;
            newMarker.Tag = m.ToString();
            newMarker.BarcodeID = m;
            newMarker.UseContPoseEstimation = true;
            newMarker.PatternWidth = 0.08f;
            newMarker.Load();

            GameObject NewTag = Instantiate(TagPrefab);
            NewTag.GetComponent<ARTrackedObject>().MarkerTag = m.ToString();

        }

    }
	
}
