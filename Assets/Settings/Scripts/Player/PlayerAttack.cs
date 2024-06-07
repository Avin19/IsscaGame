using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public void FinishAttacking()
    {
        Player._status = "Idle";


    }
    public void Attack()
    {
        GameObject pfAttack = Instantiate(Player._pfAttack, transform.position + transform.forward * 5f, Quaternion.identity);
        pfAttack.GetComponent<Rigidbody>().velocity = transform.forward * 30f;
        GameObject.Destroy(pfAttack, 10f);
    }
    void Update()
    {
        if (Input.GetButton("Fire1") && Player._status == "Idle")
        {
            Player._status = "Attacking";
            string attackPatternNumber = Random.Range(1, 5).ToString();
            Player._animator.Play("Attack" + attackPatternNumber);
        }
    }
}
