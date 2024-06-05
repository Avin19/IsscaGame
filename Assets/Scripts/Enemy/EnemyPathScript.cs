using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class EnemyPathScript : MonoBehaviour
{
    [SerializeField] private float distance = 5f;

    [SerializeField] private List<Vector3> pathPoints = new List<Vector3>();

    private bool pathDrawn = false;
    int numberpoint = 0;

    private void Update()
    {
        if (!pathDrawn)
        {
            pathDrawn = true;
            DrawPath();
        }
    }
    private void DrawPoint(Vector3 currentPoint)
    {
        numberpoint++;
        Debug.Log(numberpoint);
        if (numberpoint >= 50) return;
        Vector3 dir = Player.transform.position - currentPoint;
        if (Vector3.Distance(Player.transform.position, currentPoint) < 5) return;
        bool checkDirection = true;
        int failsafe = -1;
        int counter = 0;
        RaycastHit ray;
        LayerMask layerMask = ~(1 << LayerMask.NameToLayer("Floor"));
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
            if (!Physics.Raycast(currentPoint, adjustDir, out ray, distance))
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
        DrawPoint(transform.position);
    }
}


