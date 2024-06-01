using UnityEngine;



public class BombScript : MonoBehaviour
{
    private Transform bomb;
    private void Update()
    {
        if (Input.GetButtonUp("Bomb") && Player._status == "Idle")
        {
            Player._status = "HoldingBomb";
            Player._playerStaff.SetActive(false);
            Player._animator.Play("HoldingBomb");
            bomb = Instantiate(Player._pfBomb, transform.position + new Vector3(0, 7, 0), Player._pfBomb.transform.rotation, transform).transform;
        }
        if (Input.GetButtonUp("Fire1") && Player._status == "HoldingBomb")
        {
            Player._status = "ThrowingBomb";
            Player._animator.Play("ThrowingBomb");
            bomb.transform.parent = null;
            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
            rb.freezeRotation = true;
            rb.velocity = transform.forward + new Vector3(0, 10, 20);
        }



    }

}


