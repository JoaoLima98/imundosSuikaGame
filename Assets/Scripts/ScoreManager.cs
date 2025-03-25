using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class ScoreManager : MonoBehaviour
{
    public Text contador;
    public int contador_;
    bool isPlaying = false;
    bool hasPlayedAudio = false;
    public AudioSource scoreSound;
    int[] metas = { 2000, 4000, 6000, 8000, 10000 };
    int currentMetaIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (currentMetaIndex < metas.Length)
        {
            scoreReach(metas[currentMetaIndex]);
        }
        contador.text = contador_.ToString();
    }
    void scoreReach(int meta)
    {
        if (contador_ >= meta && !hasPlayedAudio)
        {
            isPlaying = true;
            hasPlayedAudio = true; // Marque que o áudio já foi tocado
        }

        if (isPlaying)
        {
            PlayAudio();
            isPlaying = false; // Desative isPlaying após tocar o áudio uma vez

            // Avance para a próxima meta
            currentMetaIndex++;
            hasPlayedAudio = false; // Reseta hasPlayedAudio para a próxima meta
        }
    }

    void PlayAudio()
    {

        scoreSound.Play();

    }
}