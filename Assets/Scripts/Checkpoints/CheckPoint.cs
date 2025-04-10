using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private CheckPointManager checkPointManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            checkPointManager.PassCheckPoint(this);
        }
    }
    public void SetTrackCheckPointManager(CheckPointManager pcheckPointManager)
    {
        checkPointManager = pcheckPointManager;
    }
}
