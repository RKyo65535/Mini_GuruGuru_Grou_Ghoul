using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeHomeVoice : MonoBehaviour
{

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] bestSounds;
    [SerializeField] AudioClip[] greatSounds;
    [SerializeField] AudioClip[] niceSounds;
    [SerializeField] AudioClip[] goodSounds;
    [SerializeField] AudioClip[] missSounds;
    [SerializeField] AudioClip[] snipeSounds;

    [SerializeField] AudioClip upper20;
    [SerializeField] AudioClip upper18;
    [SerializeField] AudioClip upper15;
    [SerializeField] AudioClip upper11;
    [SerializeField] AudioClip downer11;

    public void PlayEvalatePlayerSkill(int enemyNum, int hitnum)
    {
        int n;//整数一次用

        if (hitnum == 0)//一人にも当らない場合
        {
            n = Random.Range(0, missSounds.Length);
            audioSource.PlayOneShot(missSounds[n]);

        }
        else if (hitnum < 5)//5人以下に当った場合
        {
            n = Random.Range(0, snipeSounds.Length);
            audioSource.PlayOneShot(snipeSounds[n]);
        }
        else if (hitnum / (float)enemyNum > 0.5f)//全体の50%以上に当てられた場合
        {
            n = Random.Range(0, bestSounds.Length);
            audioSource.PlayOneShot(bestSounds[n]);
        }
        else if (hitnum / (float)enemyNum > 0.4f)//全体の40%以上に当てられた場合
        {
            n = Random.Range(0, greatSounds.Length);
            audioSource.PlayOneShot(greatSounds[n]);
        }
        else if (hitnum / (float)enemyNum > 0.2f)//全体の20%以上に当てられた場合
        {
            n = Random.Range(0, niceSounds.Length);
            audioSource.PlayOneShot(niceSounds[n]);
        }
        else//そのほか
        {
            n = Random.Range(0, goodSounds.Length);
            audioSource.PlayOneShot(goodSounds[n]);
        }
    }



    public void PlayEvalateFinelScore(int score)
    {
        if(score >= 200000)
        {
            audioSource.PlayOneShot(upper20);

        }
        else if(score >= 180000)
        {
            audioSource.PlayOneShot(upper18);

        }
        else if(score >= 150000)
        {
            audioSource.PlayOneShot(upper15);

        }
        else if(score >= 110000)
        {
            audioSource.PlayOneShot(upper11);

        }
        else
        {
            audioSource.PlayOneShot(downer11);

        }
    }


}
