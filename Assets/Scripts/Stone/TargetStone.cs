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
    public StoneType stoneType;
    public Renderer objRenderer;

    MeshCollider meshCollider;

    float fadeDuration = 2f;
    Color originalColor;
    bool isHit = false;

    private void Start()
    {
        meshCollider = GetComponent<MeshCollider>();
        originalColor = objRenderer.material.color;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stone"))
        {
            OnHitByProjectile?.Invoke(stoneType);
            isHit = true;

            foreach (ContactPoint hitcontact in collision.contacts)
            {
                VisualizeContact(hitcontact.point);
            }

            ContactPoint contact = collision.contacts[0];
            Vector3 contactPoint = contact.point;

            if (meshCollider != null)
            {
                float edgeDistance = EdgeDistanceCalculator.GetDistanceToNearestEdge(meshCollider, contactPoint);
                Debug.Log("Distance to nearest edge : " + edgeDistance);
            }
        }
    }

    void VisualizeContact(Vector3 point)
    {
        GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        marker.transform.position = point;
        marker.transform.localScale = Vector3.one * 0.1f;
        marker.GetComponent<Renderer>().material.color = Color.red;
        Destroy(marker, 2f);
    }

    void Update()
    {
        if (isHit)
        {
            if (Mathf.Abs(transform.eulerAngles.z - 270) < 30f || Mathf.Abs(transform.eulerAngles.z) == 90)
            {
                OnKnockDownEvent?.Invoke(stoneType);
                StartCoroutine(FadeOutObject());
                isHit = false;
            }
        }
    }



    IEnumerator FadeOutObject()
    {
        float elapsed = 0f;
        while (elapsed < 2)
        {
            float alpha = Mathf.Lerp(originalColor.a, 0f, elapsed / fadeDuration);
            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            objRenderer.material.color = newColor;
            elapsed += Time.deltaTime;
            yield return null;
        }
        Color finalColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        objRenderer.material.color = finalColor;
        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }
}