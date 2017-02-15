using UnityEngine;
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

        trackedObject = GetComponent<ARTrackedObject>();
        tagCollider = GetComponent<Collider>();

    }

    public void ProcessTagPress()
    {

        tagCollider.enabled = false;
        connectionManager.GetMarkerInfo(trackedObject.GetMarker().BarcodeID, GetMarkerInfoHandler);

    }

    private void GetMarkerInfoHandler(string info)
    {
        infoPanel.ShowPanel(info);
        tagCollider.enabled = true;
    }

}
