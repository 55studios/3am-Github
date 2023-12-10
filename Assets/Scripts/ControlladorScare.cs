using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ControlladorScare : MonoBehaviour
{
    [SerializeField] private List<Transform> pointMoveCamera = new List<Transform>();
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private float speedMove;
    [SerializeField] private int cameraActve;
    [SerializeField] private int timeWait;

    private void Start()
    {
        UpdateCam();
    }

    private void UpdateCam()
    {
        StartCoroutine(MoveCamWithTime());
    }

    IEnumerator MoveCamWithTime()
    {
        while ( mainCamera.transform.position!=pointMoveCamera[cameraActve].position)
        {
            MoveCamera();
        }
        yield return new WaitForSeconds(timeWait);
        cameraActve = Random.Range(0, pointMoveCamera.Count);
        UpdateCam();
    }
    public void MoveCamera()
    {
        mainCamera.transform.position = Vector3.MoveTowards(mainCamera.gameObject.transform.position, pointMoveCamera[cameraActve].position, speedMove);
    }
}
