using System.Collections.Generic;
using UnityEngine;

public class BallCollider : MonoBehaviour
{
    ScoreManager scoreManager;
    public int ballLevel; // Nível da bola atual
    private bool isProcessing = false; // Flag para evitar processamento duplo por instância
    BallsManager ballsManager;
    public AudioSource audioJuntar;
    void Start()
    {
        scoreManager = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreManager>();
        ballsManager = FindObjectOfType<BallsManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BallCollider otherBall = collision.gameObject.GetComponent<BallCollider>();

        // Verifica se a bola colidiu com outra bola do mesmo nível e se a lógica não está em processamento
        if (otherBall != null && collision.gameObject.tag == collision.otherCollider.tag && !isProcessing && !otherBall.isProcessing)
        {

            // Define a flag de ambas as bolas como verdadeira para evitar instância duplicada
            isProcessing = true;
            scoreManager.contador_ += ballLevel * 5;
            if (ballLevel == 0)
            {
                scoreManager.contador_ += 3;
            }

            otherBall.isProcessing = true;

            // Calcula a posição média para a nova bola
            Vector3 spawnPosition = (transform.position + collision.transform.position) / 2;

            // Destrói as bolas atuais
            Destroy(gameObject);
            Destroy(collision.gameObject);

            // Chama o BallsManager para criar a bola do próximo nível
            BallsManager ballManager = FindObjectOfType<BallsManager>();
            if (ballManager != null)
            {
                ballManager.SpawnBall(ballLevel + 1, spawnPosition, true);
                
            }
        }

        // Verifica se a bola colidiu com a zona de morte
        if (collision.gameObject.CompareTag("Death"))
        {
            ballsManager.enabled = false; // Desativa o script

            // Ativa a tela de Game Over através do GameManager singleton
            if (GameManager.Instance != null)
            {
                GameManager.Instance.ShowGameOver();
            }
            else
            {
                Debug.LogError("GameManager não encontrado.");
            }
        }
    }
}
