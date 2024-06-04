using System;
using System.Collections;
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
    private void DrawPlayerHeath()
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
        for (int i = 0; i < Mathf.FloorToInt(Player.maxHealth - Player.health); i++)
        {
            DrawHealth(emptyHealth);
        }
    }
    private void Start()
    {
        DrawPlayerHeath();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log(Player.health);
            TakeDamage(2);
        }
        if (Input.GetKey(KeyCode.H))
        {
            DrawPlayerHeath();
        }
    }


    private void TakeDamage(int damage)
    {
        if (!Player.invincible)
        {
            Player.invincible = true;
            CorountineManager.Instance.StartCoroutine(Uninvinvible());
            Player.health -= damage;
            if (Player.health <= 0)
            {
                CorountineManager.Instance.StartCoroutine(Die());
            }
            else
            {
                CorountineManager.Instance.StartCoroutine(WaitAndRedrawHearts());
            }

        }
    }
    public static IEnumerator Die()
    {
        Player._diePanel.SetActive(true);
        yield return new WaitForSeconds(0);
        foreach (RectTransform child in Player._healthPanel.transform.GetComponentsInChildren<RectTransform>())
        {
            Destroy(child.gameObject);
        }
        Player._animator.Play("Die");
        Player.invincible = true;
        Destroy(Player.transform.GetComponent<PlayerMovement>());
        Destroy(Player.transform.GetComponent<PlayerAttack>());
        Destroy(Player.transform.GetComponent<ChangeRoom>());
        Destroy(Player.transform.GetComponent<BombScript>());
        Player._diePanel.SetActive(false);

    }
    public IEnumerator WaitAndRedrawHearts()
    {
        Player._diePanel.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        Player._diePanel.SetActive(false);
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < Player._healthPanel.transform.childCount; i++)
        {
            Destroy(Player._healthPanel.transform.GetChild(i).gameObject);
        }
        DrawPlayerHeath();
    }
    public static IEnumerator Uninvinvible()
    {
        yield return new WaitForSeconds(0.5f);
        Player.invincible = false;
    }
}


