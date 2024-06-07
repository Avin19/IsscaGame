using UnityEngine;



public class BombScript : MonoBehaviour
{
    private Transform bomb;

    public void EndBomb()
    {
        Player._status = "Idle";
        Player._playerStaff.SetActive(true);
    }
    // private bool ableToDropBomb = false;

    // private void WaitForBombDrop()
    // {
    //     ableToDropBomb = true;
    // }



    private void Update()
    {
        if (Input.GetButtonUp("Bomb") && Player._status == "Idle")
        {
            Player._status = "HoldingBomb";
            Player._playerStaff.SetActive(false);
            Player._animator.Play("HoldingBomb");
            bomb = Instantiate(Player._pfBomb, transform.position + new Vector3(0, 7, 0), Player._pfBomb.transform.rotation, transform).transform;
            // Invoke(nameof(WaitForBombDrop), .1f);
        }
        // if (Input.GetButtonUp("Bomb") && Player._status == "HoldingBomb" && ableToDropBomb)
        // {
        //     Player._status = "Idle";
        //     Player._animator.Play("Idle");
        //     ableToDropBomb = false;
        //     Destroy(bomb.GetComponent<BombExplosion>());
        //     bomb.transform.parent = null;
        //     bomb.transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        //     Rigidbody rb = bomb.GetComponent<Rigidbody>();
        //     rb.constraints = RigidbodyConstraints.None;
        //     rb.freezeRotation = true;
        //     Invoke(nameof(EndBomb), .1f);

        // }
        if (Input.GetButtonUp("Fire1") && Player._status == "HoldingBomb")
        {
            // Invoke(nameof(EndBomb), 1f);
            Player._status = "ThrowingBomb";
            Player._animator.Play("ThrowingBomb");
            // ableToDropBomb = false;
            bomb.transform.parent = null;
            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
            rb.freezeRotation = true;
            rb.velocity = transform.forward * 15 + new Vector3(0, 5, 20);
        }



    }
    //Could add object lazy object pooling 


}


