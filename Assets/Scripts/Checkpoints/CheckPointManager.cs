using UnityEngine;
using System.Collections.Generic;

public class CheckPointManager : MonoBehaviour
{
    private Transform checkPointsTransform;
    private ObstacleSpawner obstacleSpawner;
    private List<CheckPoint> checkPointList;
    private int nextCheckPointIndex;
    public int lap = 1;
    public void Init(Transform pCheckPointsTransform, ObstacleSpawner pObstacleSpawner)
    {
        Debug.Log("running checkpointManager Init");
        checkPointsTransform = pCheckPointsTransform;
        obstacleSpawner = pObstacleSpawner;

        checkPointList = new List<CheckPoint>();
        //Order of checkpoints in the heigherarchy is important
        foreach (Transform checkPointChild in checkPointsTransform)
        {
            CheckPoint checkPoint = checkPointChild.GetComponent<CheckPoint>();
            checkPoint.SetTrackCheckPointManager(this);
            checkPointList.Add(checkPoint);
        }
        
        nextCheckPointIndex = 0;
    }

    public void PassCheckPoint(CheckPoint checkPointScript)
    {
        if (checkPointList.IndexOf(checkPointScript) == nextCheckPointIndex)
        {
            //passed correct checkpoint 
            //modulo will return 0 if all checkpoints have been passes
            nextCheckPointIndex = (nextCheckPointIndex + 1) % checkPointList.Count;
            if (nextCheckPointIndex == 0)
            {
                //did a lap
                lap++;
                obstacleSpawner.OnLap();
            }
        }
        else
        {
            //passed wrong checkpoint

        }
    }

    public int GetLapProgress()
    {
        return nextCheckPointIndex;
    }

    public GameObject GetLastPassedCheckpoint()
    {
        return checkPointList[nextCheckPointIndex - 1].gameObject;
    }
}
