using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallsManager : MonoBehaviour
{
    public List<GameObject> balls; // Lista dos prefabs das bolas organizados por nível
    public Transform playerTransform;
    public Image nextBallImage; // Referência à imagem da próxima bola
    private int nextBallIndex; // Índice da próxima bola a ser spawnada
    public float imageOffsetY = -300f;
    public AudioSource audioDrop;
    public AudioSource audioJuntar;
    private void Start()
    {
        nextBallIndex = Random.Range(0, 4); // Inicializa o índice da próxima bola aleatoriamente
        UpdateNextBallImage(); // Atualiza a imagem ao iniciar
    }

    void Update()
    {
        // Atualiza a posição da imagem da próxima bola para seguir o jogador
        RectTransform rectTransform = nextBallImage.GetComponent<RectTransform>();
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(playerTransform.position);

        // Define a posição da imagem da próxima bola abaixo do jogador
        rectTransform.position = new Vector3(playerScreenPosition.x, playerScreenPosition.y + imageOffsetY, 0f); // Z fixo em 0


        if (Input.GetMouseButtonDown(0)) // Clique do botão esquerdo do mouse
        {
            
            SpawnBall(nextBallIndex, playerTransform.position, false); // Passa a posição do jogador
            UpdateNextBallIndex(); // Atualiza o índice da próxima bola
            UpdateNextBallImage(); // Atualiza a imagem da próxima bola
        }
    }


    // Método para spawnar uma bola com um índice específico em uma posição específica
    public void SpawnBall(int index, Vector3 position, bool playSound)
    {
        if (balls.Count > 0 && index < balls.Count)
        {
            GameObject newBall = Instantiate(balls[index], position, Quaternion.identity);

            // Define o nível da bola instanciada
            BallCollider ballCollider = newBall.GetComponent<BallCollider>();
            if (ballCollider != null)
            {
                ballCollider.ballLevel = index; // Define o nível com base no índice
            }

            // Toca o som de juntar apenas se playSound for verdadeiro
            if (playSound == false)
            {
                audioDrop.Play();
            }
            else
            {
                audioJuntar.Play();
            }
        }
    }

    // Método para atualizar o índice da próxima bola
    private void UpdateNextBallIndex()
    {
        nextBallIndex = Random.Range(0, 4); // Aqui você pode escolher a lógica para definir a próxima bola
    }

    // Método para atualizar a imagem da próxima bola
    private void UpdateNextBallImage()
    {
        // Verifica se há bolas e atualiza a imagem da próxima bola
        if (balls.Count > 0 && nextBallIndex < balls.Count)
        {
            nextBallImage.sprite = balls[nextBallIndex].GetComponent<SpriteRenderer>().sprite; // Define o sprite da próxima bola na imagem
        }
    }
}