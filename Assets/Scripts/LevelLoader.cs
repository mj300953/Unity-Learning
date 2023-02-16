using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Button loadLevelButton;
    [SerializeField] private string levelName;

    private void Awake()
    {
        loadLevelButton.onClick.AddListener(LoadLevel);
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene(levelName);
    }
}