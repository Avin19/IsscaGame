using UnityEngine;

public class CorountineManager : MonoBehaviour
{
    private static CorountineManager instance;
    public static CorountineManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        instance = this;
    }

}




