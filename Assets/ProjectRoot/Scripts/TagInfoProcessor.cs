using UnityEngine;
using InfoServerObjectModel;
using System.Collections;

public class TagInfoProcessor : MonoBehaviour {

    private static GameObject managerRoot;
    private static InfoServerConnectionManager connectionManager;
    private static InfoPanelManager infoPanel;


    private ARTrackedObject trackedObject;
    private Collider tagCollider;

    void Start()
    {

        if (managerRoot == null) managerRoot = GameObject.Find("Managers");
        if (connectionManager == null) connectionManager = managerRoot.GetComponent<InfoServerConnectionManager>();
        if (infoPanel == null) infoPanel = managerRoot.GetComponent<InfoPanelManager>();

        trackedObject = GetComponentInParent<ARTrackedObject>();
        tagCollider = GetComponent<Collider>();

    }

    public void ProcessTagPress()
    {

        tagCollider.enabled = false;
        connectionManager.GetMarkerInfoByID(trackedObject.GetMarker().BarcodeID, GetMarkerInfoHandler);     

    }

    private void GetMarkerInfoHandler(InfoServerResponse<InfoServerMarkerResponse> response)
    {
        if (response.code != 0) return;

        infoPanel.ShowPanel(response.data.description);
        tagCollider.enabled = true;

    }

}
