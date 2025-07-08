using Niantic.Experimental.Lightship.AR.WorldPositioning;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class WPS : MonoBehaviour { 
    
    public static WPS Instance { get; private set; }

    [SerializeField] private ARCameraManager cameraMaganer;
    [SerializeField] private ARWorldPositioningManager positioningManager;
    [SerializeField] private ARWorldPositioningObjectHelper objectHelper;

    private void Awake() {
        Instance = this;
    }

    public GameObject SpawnObjectAtPositionFromCamera(GameObject objectPrefab, Vector3 position) {
        Transform camTransform = cameraMaganer.transform;
        (double latOffsetCam, double longOffsetCam) = GetGeographicOffsetfromCameraPosition(camTransform.position);
        (double latOffsetObj, double longOffsetObj) = GetGeographicOffsetfromCameraPosition(position);

        double latitude = positioningManager.WorldTransform.OriginLatitude + latOffsetCam + latOffsetObj;
        double longitude = positioningManager.WorldTransform.OriginLatitude + longOffsetCam + longOffsetObj;
        double altitude = 0.0;

        GameObject spawnedObject = Instantiate(objectPrefab);

        objectHelper.AddOrUpdateObject(spawnedObject, latitude, longitude, altitude, Quaternion.identity);

        return spawnedObject;
    }

    public (double, double) GetGeographicOffsetfromCameraPosition(Vector3 position) { 
        double latOffset = position.z / 111000;
        double longOffset = position.x / (111000 * Mathf.Cos((float)positioningManager.WorldTransform.OriginLatitude * Mathf.Deg2Rad));

        return (latOffset, longOffset);
    }


}

public struct LatLong {
    public double latitude;
    public double longitude;
}