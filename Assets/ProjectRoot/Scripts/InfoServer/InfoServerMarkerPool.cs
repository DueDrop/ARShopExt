using UnityEngine;
using InfoServerObjectModel;

[RequireComponent(typeof(InfoServerConnectionManager))]
public class InfoServerMarkerPool: MonoBehaviour {
    
    public GameObject MarkerPoolRoot;
    public GameObject TagPrefab;
    public GameObject OriginObject;

    public bool demoZeroBarcode = false;

    private InfoServerConnectionManager connectionManager;
    private ARController arController;
    private AROrigin arOrigin;
	
    public void Start(){

        connectionManager = GetComponent<InfoServerConnectionManager>();
        arController = MarkerPoolRoot.GetComponent<ARController>();
        arOrigin = OriginObject.GetComponent<AROrigin>();

        // Если работаем в деморежиме - используем уже настроенный 0-ШК.
        // Иначе, пытаемся получить массив маркеров с сервера.
        if (demoZeroBarcode){
            arController.StartAR();
            return;
        }

        if (connectionManager != null){
           connectionManager.GetMarkerPool(GetMarkerPoolHandler);
        }

    }

    protected void GetMarkerPoolHandler(InfoServerResponse<InfoServerMarkerPoolResponse> r) {

        if (r.code != 0) return;
        init(r.data.markers);
        arOrigin.FindMarkers();
    }

    public void init(MarkerInfo[] markers){

        // Уничтожение всех маркеров, как правило они будут находиться на корневом объекте.
        foreach (ARMarker am in MarkerPoolRoot.GetComponents<ARMarker>()) Destroy(am);     
           
        // Уничтожение всех отслеживаемых объектов. Убиваем не компонент, а весь объект.
        foreach(ARTrackedObject ato in FindObjectsOfType<ARTrackedObject>()) Destroy(ato.gameObject);        

        // Создание новых маркеров.
        foreach(MarkerInfo m in markers){

            ARMarker newMarker = MarkerPoolRoot.AddComponent<ARMarker>();

            newMarker.MarkerType = MarkerType.SquareBarcode;
            newMarker.Tag = "barcode" + m.ToString();
            newMarker.BarcodeID = m.markerID;
            newMarker.UseContPoseEstimation = true;
            newMarker.PatternWidth = 0.08f;
            newMarker.Load();

            GameObject NewTag = Instantiate(TagPrefab);

            ARTrackedObject to = NewTag.GetComponent<ARTrackedObject>();

            to.MarkerTag = newMarker.Tag;
            to.objectName = m.name;
            
                                   
            NewTag.transform.parent = OriginObject.transform;
            
            NewTag.transform.position = Vector3.zero;

            NewTag.GetComponentInChildren<TextMesh>().text = m.name;

        }

        arController.StartAR();
        
    }

    void OnDestroy()
    {
        arController.StopAR();
    }
	
}
