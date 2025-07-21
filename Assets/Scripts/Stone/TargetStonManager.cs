using System;
using System.Threading;
using UnityEngine;

public class TargetStoneManager : MonoBehaviour
{
    public static event Action OnStageClearEvent;

    public GameObject stonePrefab;

    Vector3 pos;

    [SerializeField] QuadCreator quadCreator;

    public Vector3 scale;

    public float minX, maxX;

    public float minZ, maxZ;

    public float newMass;

    private int clearCount;
    private int count;

    private void Start()
    {
        count = 0;
        clearCount = 3;
        TargetStone.OnKnockDownEvent += TargetStone_OnKnockDownEvent;
    }

    private void TargetStone_OnKnockDownEvent(StoneType obj)
    {
        count++;
        if(count == clearCount)
        {
            count = 0;
            ResetValue();
            OnStageClearEvent?.Invoke();
            return;
        }

        GameObject[] objects = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject item in objects)
        {
            Destroy(item);
        }

        CreateOneTargeStone();
    }

    public void CreateOneTargeStone()
    {
        stonePrefab.transform.localScale = scale;

        pos = quadCreator.GetArea();
        pos.y = 0.01f;

        Instantiate(stonePrefab, pos, Quaternion.identity);
    }

    void ResetValue()
    {
        scale = new Vector3(scale.x += 0.1f, scale.y += 0.3f, scale.z += 0.3f);

        newMass += 1;

        minX -= 0.1f;
        maxX += 0.1f;
        minZ -= 0.1f;
        minZ += 0.1f;
    }

#if (UNITY_EDITOR)
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Target");
            foreach (GameObject obj in objects)
            {
                Destroy(obj);
            }
            ResetValue();
            CreateOneTargeStone();
        }
    }
#endif
}