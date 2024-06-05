using System.Collections.Generic;
using System.Linq;
using UnityEditor.Callbacks;
using UnityEngine;



public class EnemyPathScript : MonoBehaviour
{
    [SerializeField] private float distance = 3f;
    [SerializeField] private float repeatTimer = 2f;

    private List<Vector3> pathPoints = new List<Vector3>();

    private bool pathDrawn = false;
    int numberpoint = 0;
    private Rigidbody rb;
    private Vector3 moveTowardPlayer = Vector3.zero;
    [SerializeField] private float speed = 2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!pathDrawn)
        {
            pathDrawn = true;
            InvokeRepeating(nameof(DrawPath), 0, repeatTimer);
        }
        if (pathPoints.Count > 0)
        {
            moveTowardPlayer = pathPoints[0] - transform.position;
            rb.velocity = moveTowardPlayer.normalized * speed;
            if (Vector3.Distance(transform.position, pathPoints[0]) < 0.2f)
            {
                pathPoints.RemoveAt(0);
            }
            else
            {
                moveTowardPlayer = pathPoints[0] - transform.position;
                rb.velocity = moveTowardPlayer.normalized * speed;
            }
        }
    }
    private void DrawPoint(Vector3 currentPoint)
    {

        Vector3 dir = Player.transform.position - currentPoint;
        if (Vector3.Distance(Player.transform.position, currentPoint) < 5) return;
        bool checkDirection = true;
        int failsafe = -1;
        int counter = 0;
        RaycastHit ray;
        LayerMask layerMask = ~(1 << LayerMask.NameToLayer("Enemy")) | (1 << LayerMask.NameToLayer("Floor"));
        while (checkDirection)
        {
            failsafe++;
            if (failsafe > 15) return;
            int result = counter % 2 == 0 ? counter / 2 * 5 : -counter / 2 * 5;
            counter++;

            // Converting Vector 3 dir to rotation 
            Quaternion rotation = Quaternion.LookRotation(dir);
            // Adding the pendulum result to the rotation 
            rotation *= Quaternion.Euler(0f, result, 0f);
            //get back the vector3 from rotation 
            Vector3 adjustDir = rotation * Vector3.forward;
            if (!Physics.Raycast(currentPoint, adjustDir, out ray, distance, layerMask))
            {
                checkDirection = false;
                pathPoints.Add(currentPoint + dir.normalized * distance);
            }
            else
            {
                if (ray.transform.name == "StickWizardTutorial")
                {

                    break;

                }
            }
        }
        DrawPoint(pathPoints.Last());

    }
    private void DrawPath()
    {
        if (pathPoints.Count > 0) { pathPoints.Clear(); }
        DrawPoint(transform.position);
    }
}


