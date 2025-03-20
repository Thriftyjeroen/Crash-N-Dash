using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private CheckPointManager checkPointManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Collision with: {other.gameObject.name}");
        //health test script should be replaced with a player manager
        if (other.TryGetComponent<HealthTestscript>(out HealthTestscript player))
        {
            checkPointManager.PassCheckPoint(this);
        }
    }
    public void SetcheckPointMangager(CheckPointManager checkPointManager)
    {
        this.checkPointManager = checkPointManager;
    }
}
