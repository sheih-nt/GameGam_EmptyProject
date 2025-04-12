using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button backButton;

    // Добавляем статическую переменную для отслеживания завершения игры
    private static bool gameCompleted = false;

    private void Start()
    {
        // Назначаем методы обработчики для кнопок
        newGameButton.onClick.AddListener(StartNewGame);
        creditsButton.onClick.AddListener(ShowCredits);
        exitButton.onClick.AddListener(ExitGame);
        backButton.onClick.AddListener(ShowMainMenu);
        
        // Проверяем, завершена ли игра
        if (gameCompleted)
        {
            // Если игра завершена, скрываем кнопку "Новая игра"
            newGameButton.gameObject.SetActive(false);
        }
        
        // Показываем главное меню при старте
        ShowMainMenu();
    }

    private void StartNewGame()
    {
        // Сбрасываем флаг завершения игры при старте новой игры
        gameCompleted = false;
        SceneManager.LoadScene(2);
    }

    // Добавляем статический метод для вызова при завершении игры
    public static void GameCompleted()
    {
        gameCompleted = true;
    }

    private void ShowCredits()
    {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    private void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

    private void ExitGame()
    {
        Application.Quit();
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}

//Где-то в коде, когда игра завершается
//MainMenu.GameCompleted();
//SceneManager.LoadScene("MainMenuScene"); // Замените на имя вашей сцены с меню
