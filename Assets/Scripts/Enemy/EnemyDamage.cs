using UnityEngine;



public class EnemyDamage : MonoBehaviour
{

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.name == "StickWizardTutorial")
        {
            other.transform.GetComponent<HealthManager>().TakeDamage(1);
        }
    }

}


