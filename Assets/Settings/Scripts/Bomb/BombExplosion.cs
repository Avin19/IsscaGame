using UnityEngine;



public class BombExplosion : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("" + other.gameObject.name);
        if (other.gameObject.name != "StickWizardTutorial")
        {
            GameObject.Destroy(Instantiate(Player._pfBombExplosion, transform.position, Quaternion.identity), 3f);
            this.transform.GetComponent<BombScript>().EndBomb();
            GameObject.Destroy(gameObject);
        }
    }

}


