using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<CheckPointManager>(out var manager))
        {
            manager.PassCheckPoint(this);
        }
    }
}
