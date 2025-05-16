using UnityEngine;
using System.Collections.Generic;

public class CheckPointManager : MonoBehaviour
{
    private Transform checkPointsTransform;
    private RaceManager raceManager;
    private ObstacleSpawner obstacleSpawner;
    private List<CheckPoint> checkPointList;
    private int nextCheckPointIndex;
    public int lap = 1;
    public void Init(Transform pCheckPointsTransform, ObstacleSpawner pObstacleSpawner)
    {
        checkPointsTransform = pCheckPointsTransform;
        obstacleSpawner = pObstacleSpawner;

        checkPointList = new List<CheckPoint>();
        //Order of checkpoints in the heigherarchy is important
        foreach (Transform checkPointChild in checkPointsTransform)
        {
            CheckPoint checkPoint = checkPointChild.GetComponent<CheckPoint>();
            checkPointList.Add(checkPoint);
        }
        
        nextCheckPointIndex = 0;
    }

    public void PassCheckPoint(CheckPoint checkPointScript)
    {
        //IF THERE ARE MORE THAN ONE PLAYERS. THE LAP PROGRESS STOPS COUNTING 
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
                if (lap == 4)
                {
                    raceManager.EndRace();
                }
            }
        }
        else
        {
            //passed wrong checkpoint

        }
    }
    public void ResetCheckPoints()
    {
        lap = 1;
        nextCheckPointIndex = 0;
    }

    public int GetLapProgress()
    {
        Debug.Log(gameObject.name + nextCheckPointIndex);
        return nextCheckPointIndex;
        
    }

    public GameObject GetLastPassedCheckpoint()
    {
        if (nextCheckPointIndex == 0) return checkPointList[0].gameObject;
        return checkPointList[nextCheckPointIndex - 1].gameObject;
    }
}
