using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private CheckPointManager checkPointManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Collision with: {other.gameObject.name}");
        if (other.TryGetComponent<Player>(out Player player))
        {
            checkPointManager.PassCheckPoint(this);
        }
    }
    public void SetcheckPointMangager(CheckPointManager checkPointManager)
    {
        this.checkPointManager = checkPointManager;
    }
}
