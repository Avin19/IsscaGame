using UnityEngine;



public class BombScript : MonoBehaviour
{
    private Transform bomb;

    private void EndBomb()
    {
        Player._status = "Idle";
        Player._playerStaff.SetActive(true);
    }
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("" + other.gameObject.name);
        if (other.gameObject.name != "StickWizardTutorial")
        {
            GameObject.Destroy(Instantiate(Player._pfBombExplosion, transform.position, Quaternion.identity), 3f);
            EndBomb();
            GameObject.Destroy(gameObject);

        }
    }
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
            // Invoke(nameof(EndBomb), 1f);
            Player._status = "ThrowingBomb";
            Player._animator.Play("ThrowingBomb");
            bomb.transform.parent = null;
            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
            rb.freezeRotation = true;
            rb.velocity = transform.forward * 15 + new Vector3(0, 5, 20);
        }



    }
    //Could add object lazy object pooling 


}


