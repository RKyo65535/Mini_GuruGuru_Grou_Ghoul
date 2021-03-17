using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{

    [SerializeField] GameFlow gameFlow;
    [SerializeField] AudioSource intro;
    [SerializeField] AudioSource main;
    [SerializeField] AudioSource sabi;
    [SerializeField] AudioSource end;

    enum State
    {
        intro,
        main,
        sabi,
        end
    }

    State state;



    private void Awake()
    {
        intro.Play();
        state = State.intro;
    }

    // Update is called once per frame
    void Update()
    {

        if (state == State.end)
        {
            return;
        }

        //イントロが終わったらメイン部分をナガス。
        if(state == State.intro && !intro.isPlaying)
        {
            main.Play();
            state = State.main;
        }

        //メインを流し終わった時
        if(state == State.main && !main.isPlaying)
        {
            if (gameFlow.nearEnding)
            {
                state = State.sabi;
                sabi.Play();
            }
            else
            {
                main.Play();
            }
        }

        if(state == State.sabi && !sabi.isPlaying)
        {
            if (gameFlow.IsEnded())
            {
                state = State.end;
                end.Play();
            }
            else
            {
                sabi.Play();
            }
        }



    }
}
