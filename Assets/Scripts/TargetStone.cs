using System;
using System.Collections;
using UnityEngine;

public enum StoneType
{
    Low,
    Middle,
    High
}

public class TargetStone : MonoBehaviour
{
    public static event Action<StoneType> OnHitByProjectile;
    public static event Action<StoneType> OnKnockDownEvent;
    public Renderer renderer;
    public StoneType stoneType;

    bool isHit = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (isHit)
        {
            isHit = false;
            if(Math.Abs(transform.rotation.z) < 1)
            {
                StartCoroutine(StoneKnockDown());
            }
        }
    }

    IEnumerator StoneKnockDown()
    {
        yield return new WaitForSeconds(0.5f);
        renderer.enabled = false;
        yield return new WaitForSeconds(0.5f);
        renderer.enabled = false;
        yield return new WaitForSeconds(0.5f);
        renderer.enabled = false;
        OnKnockDownEvent?.Invoke(stoneType);
        Destroy(gameObject);
    }
}
