using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallsManager : MonoBehaviour
{
    public List<GameObject> balls; // Lista dos prefabs das bolas organizados por n�vel
    public Transform playerTransform;
    public Image nextBallImage; // Refer�ncia � imagem da pr�xima bola
    private int nextBallIndex; // �ndice da pr�xima bola a ser spawnada
    public float imageOffsetY = -300f;
    public AudioSource audioDrop;
    public AudioSource audioJuntar;
    private void Start()
    {
        nextBallIndex = Random.Range(0, 4); // Inicializa o �ndice da pr�xima bola aleatoriamente
        UpdateNextBallImage(); // Atualiza a imagem ao iniciar
    }

    void Update()
    {
        // Atualiza a posi��o da imagem da pr�xima bola para seguir o jogador
        RectTransform rectTransform = nextBallImage.GetComponent<RectTransform>();
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(playerTransform.position);

        // Define a posi��o da imagem da pr�xima bola abaixo do jogador
        rectTransform.position = new Vector3(playerScreenPosition.x, playerScreenPosition.y + imageOffsetY, 0f); // Z fixo em 0


        if (Input.GetMouseButtonDown(0)) // Clique do bot�o esquerdo do mouse
        {
            
            SpawnBall(nextBallIndex, playerTransform.position, false); // Passa a posi��o do jogador
            UpdateNextBallIndex(); // Atualiza o �ndice da pr�xima bola
            UpdateNextBallImage(); // Atualiza a imagem da pr�xima bola
        }
    }


    // M�todo para spawnar uma bola com um �ndice espec�fico em uma posi��o espec�fica
    public void SpawnBall(int index, Vector3 position, bool playSound)
    {
        if (balls.Count > 0 && index < balls.Count)
        {
            GameObject newBall = Instantiate(balls[index], position, Quaternion.identity);

            // Define o n�vel da bola instanciada
            BallCollider ballCollider = newBall.GetComponent<BallCollider>();
            if (ballCollider != null)
            {
                ballCollider.ballLevel = index; // Define o n�vel com base no �ndice
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

    // M�todo para atualizar o �ndice da pr�xima bola
    private void UpdateNextBallIndex()
    {
        nextBallIndex = Random.Range(0, 4); // Aqui voc� pode escolher a l�gica para definir a pr�xima bola
    }

    // M�todo para atualizar a imagem da pr�xima bola
    private void UpdateNextBallImage()
    {
        // Verifica se h� bolas e atualiza a imagem da pr�xima bola
        if (balls.Count > 0 && nextBallIndex < balls.Count)
        {
            nextBallImage.sprite = balls[nextBallIndex].GetComponent<SpriteRenderer>().sprite; // Define o sprite da pr�xima bola na imagem
        }
    }
}