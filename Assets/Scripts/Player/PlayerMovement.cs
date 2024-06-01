

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _controller;

    private Camera _mainCamera;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();

        _mainCamera = Camera.main;
    }
    private void Update()
    {
        PlayerMove();
        LookAtCursor();

    }
    private void LateUpdate()
    {
        CameraMovement();
    }

    private void CameraMovement()
    {
        Vector3 targetPosition = transform.position + new Vector3(0, 69, 0);
        Vector3 newPosition = Vector3.MoveTowards(_mainCamera.transform.position, targetPosition, 50f * Time.deltaTime);
        _mainCamera.transform.position = newPosition;
    }

    private void PlayerMove()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if ((h != 0 || v != 0) && Player._status == "Idle")
        {
            Player._animator.SetFloat("Speed", 1);
            Player._animator.Play("Walk");

        }
        else
        {
            Player._animator.SetFloat("Speed", 0);
        }

        Vector3 move = new Vector3(h, 0, v);
        _controller.Move(move * Time.deltaTime * 20f);
    }
    private void LookAtCursor()
    {

        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        float hitDist;
        if (playerPlane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            this.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 50f * Time.deltaTime);
        }
    }
}
