using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Rigidbody2D rb;
    private Camera mainCamera;


    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        FollowMouse();
    }

    private void FollowMouse()
    {
        // Obtém a posição do mouse no mundo
        Vector2 targetPosition = GetWorldPositionFromMouse();

        // Aplica os limites na posição X e Y
        targetPosition = new Vector2(
            Mathf.Clamp(targetPosition.x, -3.2f, 3.4f), // Limita o X entre -2.9 e 3.1
            Mathf.Clamp(targetPosition.y, 4f, 4f)      // Limita o Y para ficar fixo em 4
        );

        // Move o jogador para a posição limitada
        rb.MovePosition(targetPosition);
    }

    private Vector2 GetWorldPositionFromMouse()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
}
