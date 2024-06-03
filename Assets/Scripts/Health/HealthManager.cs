using UnityEngine;
using UnityEngine.UI;
public class HealthManager : MonoBehaviour
{
    [SerializeField] private GameObject heathPanel;
    [SerializeField] private Sprite fullhealth;
    [SerializeField] private Sprite halfHealth;
    [SerializeField] private Sprite emptyHealth;


    private void DrawHealth(Sprite Type)
    {
        GameObject health = new GameObject(Type.name);
        health.transform.parent = heathPanel.transform;
        Image image = health.AddComponent<Image>();
        image.sprite = Type;

    }
    public void DrawPlayerHeath()
    {

        if (Player.health % 1 != 0)
        {
            for (int i = 0; i < Player.health - 1; i++)
            {
                DrawHealth(fullhealth);
            }
            DrawHealth(halfHealth);
        }
        else
        {
            for (int i = 0; i < Player.health; i++)
            {
                DrawHealth(fullhealth);
            }

        }
        for (int i = 0; i < Player.maxHealth; i++)
        {
            DrawHealth(emptyHealth);
        }
    }
    private void Start()
    {
        DrawPlayerHeath();
    }

}


