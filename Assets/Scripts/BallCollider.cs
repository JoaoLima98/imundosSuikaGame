using System.Collections.Generic;
using UnityEngine;

public class BallCollider : MonoBehaviour
{
    ScoreManager scoreManager;
    public int ballLevel; // N�vel da bola atual
    private bool isProcessing = false; // Flag para evitar processamento duplo por inst�ncia
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

        // Verifica se a bola colidiu com outra bola do mesmo n�vel e se a l�gica n�o est� em processamento
        if (otherBall != null && collision.gameObject.tag == collision.otherCollider.tag && !isProcessing && !otherBall.isProcessing)
        {

            // Define a flag de ambas as bolas como verdadeira para evitar inst�ncia duplicada
            isProcessing = true;
            scoreManager.contador_ += ballLevel * 5;
            if (ballLevel == 0)
            {
                scoreManager.contador_ += 3;
            }

            otherBall.isProcessing = true;

            // Calcula a posi��o m�dia para a nova bola
            Vector3 spawnPosition = (transform.position + collision.transform.position) / 2;

            // Destr�i as bolas atuais
            Destroy(gameObject);
            Destroy(collision.gameObject);

            // Chama o BallsManager para criar a bola do pr�ximo n�vel
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

            // Ativa a tela de Game Over atrav�s do GameManager singleton
            if (GameManager.Instance != null)
            {
                GameManager.Instance.ShowGameOver();
            }
            else
            {
                Debug.LogError("GameManager n�o encontrado.");
            }
        }
    }
}
