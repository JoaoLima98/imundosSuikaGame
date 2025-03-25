using UnityEngine;

[CreateAssetMenu(fileName = "NewBall", menuName = "Game/Ball")]
public class Balls : ScriptableObject
{
    public string ballName;
    public int pointCarry;
    public int ballLvl;
    public GameObject ball;

    // Outros atributos podem ser adicionados conforme necessário.
}
