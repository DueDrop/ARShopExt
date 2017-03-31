using UnityEngine;
using InfoServerObjectModel;
using MarkLight.Views.UI;

[RequireComponent(typeof(InfoServerConnectionManager))]
public class InfoServerMarkerPool: MonoBehaviour {
    
    public GameObject MarkerPoolRoot;    
    public GameObject OriginObject;

    public bool demoZeroBarcode = false;

    private InfoServerConnectionManager connectionManager;
    private ARController arController;
    private AROrigin arOrigin;

    private ARMarkerContentView markerTagView;

    #region MonoBehaviour

    public void Start(){

        connectionManager = GetComponent<InfoServerConnectionManager>();
        arController = MarkerPoolRoot.GetComponent<ARController>();
        arOrigin = OriginObject.GetComponent<AROrigin>();
        markerTagView = FindObjectOfType<ARMarkerContentView>();

        // Если работаем в деморежиме - используем уже настроенный 0-ШК.
        // Иначе, пытаемся получить массив маркеров с сервера.
        if (demoZeroBarcode){
            arOrigin.FindMarkers();
            arController.StartAR();
            return;
        }

        if (connectionManager != null){
           connectionManager.GetMarkerPool(GetMarkerPoolHandler);
        }

    }

    void OnDestroy()
    {
        arController.StopAR();
    }

    #endregion

    protected void GetMarkerPoolHandler(InfoServerResponse<InfoServerMarkerPoolResponse> r) {

        if (r.code != 0) return;
        init(r.data.markers);        
    }

    public void init(MarkerInfo[] markers){

        // Уничтожение всех маркеров, как правило они будут находиться на корневом объекте.
        ARMarker[] existingMarkers = MarkerPoolRoot.GetComponents<ARMarker>();
        
        foreach (ARMarker am in existingMarkers)
        {
            am.Unload();
            DestroyImmediate(am);            
        }      

        // Уничтожение всех отслеживаемых объектов. Убиваем не компонент, а весь объект.
        ARTrackedObject[] existingObjects = FindObjectsOfType<ARTrackedObject>();
        foreach (ARTrackedObject ato in existingObjects) DestroyImmediate(ato.gameObject);        

        // Создание новых маркеров.
        foreach(MarkerInfo m in markers){

            ARMarker newMarker = MarkerPoolRoot.AddComponent<ARMarker>();

            newMarker.MarkerType = MarkerType.SquareBarcode;
            newMarker.Tag = "barcode" + m.ToString();
            newMarker.BarcodeID = m.markerID;
            newMarker.UseContPoseEstimation = true;
            newMarker.PatternWidth = m.markerSize;
            newMarker.Load();

            GameObject NewTag = new GameObject(newMarker.Tag);        

            switch (m.markerType) {
                case MarkerTypes.Item:
                    markerTagView.AddElement(NewTag);
                    break;

                case MarkerTypes.Discount:
                    markerTagView.AddElement(NewTag);
                    break;

                default:
                    continue;                   

            }
           
            ARTrackedObject to = NewTag.AddComponent<ARTrackedObject>();

            to.MarkerTag = newMarker.Tag;
            to.objectName = m.name;
            to.markerType = m.markerType;
            to.secondsToRemainVisible = 0.4f;
                                   
            NewTag.transform.parent = OriginObject.transform;            
            NewTag.transform.position = Vector3.zero;            

            NewTag.SetActive(true);

        }

        arOrigin.FindMarkers();
        arController.StartAR();
        
    }

  
	
}
