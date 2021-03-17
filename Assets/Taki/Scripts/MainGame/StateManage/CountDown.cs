using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{

    [SerializeField] GameFlow gameFlow;
    [SerializeField] Text countText;
    [SerializeField] AudioSource countAudio;
    int count;


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gameFlow.IsCountDown())
        {
            return;
        }

        if(count == 0)
        {
            countAudio.Play();
        }

        count = count + 1;

        countText.text = "" + (3 -count/50);
        if(count == 150)
        {
            countText.text = "";
            gameFlow.EndCountDown();
        }
    }
}
