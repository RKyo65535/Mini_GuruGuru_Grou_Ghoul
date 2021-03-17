using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimulateThinkingTime : MonoBehaviour
{
    const string InitialThinkingTimeText = "思考時間:"; 

    [SerializeField]GameFlow gameFlow;
    [SerializeField] TextMeshProUGUI thinkingTimeText;
    [SerializeField] AudioSource[] clockSounds;
    public int count { get; private set; } = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameFlow.CanSelectSkill())
        {
            count = count + 1;
            if(count % 50 == 0)
            {
                PlayClockSound();
                thinkingTimeText.text = InitialThinkingTimeText + (count / 50);

            }
        }
    }

    void PlayClockSound()
    {
        int n = Random.Range(0, clockSounds.Length);
        clockSounds[n].pitch = 1 + Random.value * 0.1f;
        clockSounds[n].Play();
    }

}
