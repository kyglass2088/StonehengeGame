using System;
using UnityEngine;

public class FlyingStone : MonoBehaviour
{
    public static event Action OnMissionComplete;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            OnMissionComplete?.Invoke();
            Destroy(gameObject, 1);
        }

        if (collision.gameObject.CompareTag("Target"))
        {
            OnMissionComplete?.Invoke();
            Destroy(gameObject, 3);
        }
    }
}
