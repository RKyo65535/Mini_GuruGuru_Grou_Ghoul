using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioSource mousageraremasenSound;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] GameObject[] SECircles;
    [SerializeField] GameObject[] BGMCircles;
    float SEVolume;
    float BGMVolume;

    private void Awake()
    {

        audioMixer.GetFloat("SEVolume", out SEVolume);
        audioMixer.GetFloat("BGMVolume", out BGMVolume);

        if(SEVolume <= -80)
        {
            SetSEIcons(0);
        }
        else if (SEVolume <= -40)
        {
            SetSEIcons(1);

        }
        else if (SEVolume <= -30)
        {
            SetSEIcons(2);
        }
        else if (SEVolume <= -20)
        {
            SetSEIcons(3);
        }
        else if (SEVolume <= -10)
        {
            SetSEIcons(4);
        }
        else
        {
            SetSEIcons(5);
        }

        if (BGMVolume <= -80)
        {
            SetBGMIcons(0);
        }
        else if (BGMVolume <= -40)
        {
            SetBGMIcons(1);
        }
        else if (BGMVolume <= -30)
        {
            SetBGMIcons(2);
        }
        else if (BGMVolume <= -20)
        {
            SetBGMIcons(3);
        }
        else if (BGMVolume <= -10)
        {
            SetBGMIcons(4);
        }
        else
        {
            SetBGMIcons(5);
        }


    }


    void SetSEIcons(int num)
    {
        Debug.Log("SEは"+num);
        for(int i = 0; i < 5; i++)
        {
            SECircles[i].SetActive(false);
        }
        for(int i = 0; i < num; i++)
        {
            SECircles[i].SetActive(true);
        }
    }

    void SetBGMIcons(int num)
    {
        Debug.Log("BGMは"+num);
        for (int i = 0; i < 5; i++)
        {
            BGMCircles[i].SetActive(false);
        }
        for (int i = 0; i < num; i++)
        {
            BGMCircles[i].SetActive(true);
        }
    }

    public void UpBGM()
    {

        if(BGMVolume >= -10)
        {
            BGMVolume = 0;
            SetBGMIcons(5);
        }
        else if (BGMVolume >= -20)
        {
            BGMVolume = -10;
            SetBGMIcons(4);
        }
        else if (BGMVolume >= -30)
        {
            BGMVolume = -20;
            SetBGMIcons(3);
        }
        else if (BGMVolume >= -40)
        {
            BGMVolume = -30;
            SetBGMIcons(2);
        }
        else
        {
            BGMVolume = -40;
            SetBGMIcons(1);
        }

        audioMixer.SetFloat("BGMVolume", BGMVolume);
    }
    public void DownBGM()
    {

        if (BGMVolume >= 0)
        {
            BGMVolume = -10;
            SetBGMIcons(4);
        }
        else if (BGMVolume >= -10)
        {
            BGMVolume = -20;
            SetBGMIcons(3);
        }
        else if (BGMVolume >= -20)
        {
            mousageraremasenSound.Play();
            return;
//ここ以下はもう実行されない＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝

            //BGMVolume = -20;
            //SetBGMIcons(2);
        }
        else if (BGMVolume >= -20)
        {
            BGMVolume = -30;
            SetBGMIcons(1);
        }
        else
        {
            BGMVolume = -80;
            SetBGMIcons(0);
        }
//ここ以下はもう実行されない＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        audioMixer.SetFloat("BGMVolume", BGMVolume);
    }
    public void UpSE()
    {
        if (SEVolume >= -10)
        {
            SEVolume = 0;
            SetSEIcons(5);
        }
        else if (SEVolume >= -20)
        {
            SEVolume = -10;
            SetSEIcons(4);
        }
        else if (SEVolume >= -30)
        {
            SEVolume = -20;
            SetSEIcons(3);
        }
        else if (SEVolume >= -40)
        {
            SEVolume = -30;
            SetSEIcons(2);
        }
        else
        {
            SEVolume = -40;
            SetSEIcons(1);
        }

        audioMixer.SetFloat("SEVolume", SEVolume);
    }
    public void DownSE()
    {
        if (SEVolume >= 0)
        {
            SEVolume = -10;
            SetSEIcons(4);
        }
        else if (SEVolume >= -10)
        {
            SEVolume = -20;
            SetSEIcons(3);
        }
        else if (SEVolume >= -20)
        {
            SEVolume = -30;
            SetSEIcons(2);
        }
        else if (SEVolume >= -30)
        {
            SEVolume = -40;
            SetSEIcons(1);
        }
        else
        {
            SEVolume = -80;
            SetSEIcons(0);
        }
        audioMixer.SetFloat("SEVolume", SEVolume);
    }

}
