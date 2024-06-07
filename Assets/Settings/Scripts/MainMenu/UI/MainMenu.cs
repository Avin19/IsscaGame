
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startBtn;
    [SerializeField] private Button settingBtn;
    [SerializeField] private Button quitBtn;

    private void OnEnable()
    {
        startBtn.onClick.AddListener(OnStartClicked);
    }
    private void OnDisable()
    {
        startBtn.onClick.RemoveListener(OnStartClicked);
    }

    private void OnStartClicked()
    {
        SceneManager.LoadScene(1);
    }
}


