using UnityEngine;
using System.Collections.Generic;

public class CheckPointManager : MonoBehaviour
{
    [SerializeField] private Transform checkPointsTransform;
    [SerializeField] private ObstacleSpawner obstacleSpawner;
    private List<CheckPoint> checkPointList;
    private int nextCheckPointIndex;
    private void Awake()
    {
        checkPointList = new List<CheckPoint>();
        //Order of checkpoints in the heigherarchy is important
        foreach (Transform checkPointChild in checkPointsTransform)
        {
            CheckPoint checkPoint = checkPointChild.GetComponent<CheckPoint>();
            checkPoint.SetcheckPointMangager(this);
            checkPointList.Add(checkPoint);
        }
        nextCheckPointIndex = 0;
    }
    public void PassCheckPoint(CheckPoint checkPointScript)
    {
        if (checkPointList.IndexOf(checkPointScript) == nextCheckPointIndex)
        {
            //passed correct checkpoint 
            Debug.Log("correct checkpoint");
            //modulo will return 0 if all checkpoints have been passes
            nextCheckPointIndex = (nextCheckPointIndex + 1) % checkPointList.Count;
            if (nextCheckPointIndex == 0)
            {
                //did a lap
                Debug.Log("did a full lap");
                obstacleSpawner.OnLap();
            }
        }
        else
        {
            //passed wrong checkpoint
            Debug.Log("wrong checkpoint");
        }
    }
}
