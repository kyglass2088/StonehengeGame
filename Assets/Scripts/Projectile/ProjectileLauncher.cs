using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class ProjectileLauncher : MonoBehaviour
{
    public GameUI gameUI;

    public Transform launchPoint;
    public GameObject projectile;
    public float launchSpeed = 10f;

    [Header("****Trajectory Display")]
    public LineRenderer lineRenderer;
    public int linePoints = 175;
    public float timeIntervalinPoints = 0.01f;

    public bool isDrawing = false;

    private void Update()
    {
        if (lineRenderer != null)
        {
            if(isDrawing)
            {
                DrawTrajectory();
                lineRenderer.enabled = true;
            }
            else
                lineRenderer.enabled = false;
        }
        //if (Input.GetMouseButtonDown(0))
        //{
        //    var _projectile = Instantiate(projectile, launchPoint.position, launchPoint.rotation);
        //    _projectile.GetComponent<Rigidbody>().linearVelocity = launchSpeed * launchPoint.up;
        //}
    }

    public void ThrowStone()
    {
        var _projectile = Instantiate(projectile, launchPoint.position, launchPoint.rotation);
        _projectile.GetComponent<Rigidbody>().linearVelocity = launchSpeed * launchPoint.up;

    }

    void DrawTrajectory()
    {
        Vector3 origin = launchPoint.position;
        Vector3 startVelocity = launchSpeed * launchPoint.up;
        lineRenderer.positionCount = linePoints;
        float time = 0;
        for (int i = 0; i < linePoints; i++)
        {
            // s = u*t + 1/2*g*t*t
            var x = (startVelocity.x * time) + (Physics.gravity.x / 2 * time * time);
            var y = (startVelocity.y * time) + (Physics.gravity.y / 2 * time * time);
            Vector3 point = new Vector3(x, y, 0);
            lineRenderer.SetPosition(i, origin + point);
            time += timeIntervalinPoints;
        }
    }



}
