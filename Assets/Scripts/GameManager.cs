using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject gameOverScreen; // Refer�ncia para o GameOverScreen

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Evita m�ltiplas inst�ncias
        }
    }

    public void ShowGameOver()
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
            Debug.Log("Tela de Game Over ativada.");
        }
        else
        {
            Debug.LogError("Refer�ncia para GameOverScreen est� nula.");
        }
    }
}
