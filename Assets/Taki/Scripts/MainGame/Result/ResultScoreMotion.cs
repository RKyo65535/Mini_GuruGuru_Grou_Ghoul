using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class ResultScoreMotion : MonoBehaviour
{
    const int ScorePerEnemy = 3001;
    const int TimePerScore = 1000;

    public delegate void ScoreUpdater(int score);

    int tempSumScore;//一時的な最終スコア


    [SerializeField] GameObject scoreCanvas;//スコアキャンバス
    [SerializeField] GameObject buttons;//ボタンたちのの親
    [SerializeField] RectTransform ScoreRTF;//スコアのパネルのRectTransform

    [SerializeField] SimulateThinkingTime thinkingTime;//思考時間をスコアに適用！！
    [SerializeField] ScoreManager scoreManager;//スコアマネージャーか
    [SerializeField] PlayerSkillController playerSkill;//プレイヤースキルの残り数を見る

    [SerializeField] AudioSource domAudio;
    [SerializeField] AudioSource doomAudio;

    [SerializeField] Text basicScoreText;//基礎スコアのテキスト
    [SerializeField] Text basicScoreExpText;//基礎スコアの説明テキスト
    [SerializeField] Text timeScoreText;//時間経過のテキスト
    [SerializeField] Text timeScoreExpText;//時間経過のテキスト
    [SerializeField] Text remainSkillScoreText;//残り技数のテキスト
    [SerializeField] Text remainSkillExpScoreText;//残り技数のテキスト
    [SerializeField] Text nagiharaiScoreText;//敵同時倒し数ボーナスのテキスト
    [SerializeField] Text nagiharaiScoreExpText;//敵同時倒し数ボーナスのテキスト
    [SerializeField] Text nomissScoreText;//ノーミスの有無のテキスト
    [SerializeField] Text nomissScoreExpText;//ノーミスの有無のテキスト
    [SerializeField] Text sumScoreText;//合計スコア

    public IEnumerator ShowResult(ScoreUpdater score)
    {
        tempSumScore = 0;//最初の値は確かに0にする。

        scoreCanvas.SetActive(true);//キャンバスを見れるようにする。
        //すべて文字なしにする。
        basicScoreText.text = "";
        timeScoreText.text = "";
        remainSkillScoreText.text = "";
        nagiharaiScoreText.text = "";
        nomissScoreText.text = "";
        sumScoreText.text = "";

        basicScoreExpText.text = "";
        timeScoreExpText.text = "";
        remainSkillExpScoreText.text = "";
        nagiharaiScoreExpText.text = "";
        nomissScoreExpText.text = "";

        yield return DouwnScoreScreen();//スクリーンが降りてくる

        BasicScore();
        yield return new WaitForSeconds(0.5f);
        timeScore();
        yield return new WaitForSeconds(0.5f);
        RemainSkillScore();
        yield return new WaitForSeconds(0.5f);
        Chack50MaxHit();
        yield return new WaitForSeconds(0.5f);

        NoMissChack();      

        yield return new WaitForSeconds(1f);
        doomAudio.Play();
        sumScoreText.color = Color.yellow;//色を変える
        yield return new WaitForSeconds(0.5f);
        score(tempSumScore);//マスターの方に適用させる

        buttons.SetActive(true);

        yield return null;
    }



    IEnumerator DouwnScoreScreen()
    {
        int y = -900;

        for (y = -900; y <= 0; y = y + 10)
        {
            ScoreRTF.anchoredPosition = new Vector2(0, y);
            yield return new WaitForFixedUpdate();

        }

        yield return null;
    }


    void AddLastScore(int addingScore)
    {
        domAudio.Play();
        tempSumScore += addingScore;
        sumScoreText.text = tempSumScore + "点";

    }


    void BasicScore()
    {
        domAudio.Play();
        basicScoreExpText.text = scoreManager.basicScore / 1000 + "×1000= ";
        if (scoreManager.basicScore == 100000)
        {
            basicScoreExpText.color = Color.red;//いいスコアなら赤色表記
        }
        basicScoreText.text = scoreManager.basicScore+"点";
        AddLastScore(scoreManager.basicScore);
    }

    void timeScore()
    {
        int tempScore = 0;

        domAudio.Play();

        //秒数を書く
        timeScoreExpText.text = (thinkingTime.count / 50) + "秒!";

        //速さによる感想を書く
        if(thinkingTime.count < 750)//15秒以内ならば
        {
            timeScoreExpText.color = Color.red;
            timeScoreExpText.text += "光速!!";
            tempScore += 700;
        }
        else if(thinkingTime.count < 1000)//20秒以内クリア
        {
            timeScoreExpText.text += "超速!!";
            tempScore += 600;
        }
        else if (thinkingTime.count < 1250)//25秒以内クリア
        {
            timeScoreExpText.text += "爆速!!";
            tempScore += 500;
        }
        else if (thinkingTime.count < 1500)//30秒以内クリア
        {
            timeScoreExpText.text += "高速!";
            tempScore += 400;
        }
        else if (thinkingTime.count < 2000)//40秒クリア
        {
            timeScoreExpText.text += "速め!";
            tempScore += 300;
        }
        else if (thinkingTime.count < 2500)//50秒クリア
        {
            timeScoreExpText.text += "普通!";
            tempScore += 200;
        }
        else//50秒以上
        {
            timeScoreExpText.text += "慎重!";
            tempScore += 100;
        }

        tempScore += (60 - (thinkingTime.count / 50))*TimePerScore;
        if(tempScore < 100)
        {
            tempScore = 100;
        }

        timeScoreText.text = tempScore + "点";
        AddLastScore(tempScore);

    }

    void RemainSkillScore()
    {
        domAudio.Play();
        int remainSkillCount = playerSkill.usedSkill.Where(i => (i == false)).Count();//使ってないスキルの数を数える
        remainSkillExpScoreText.text = remainSkillCount + "×" + ScorePerEnemy + "= ";
        remainSkillScoreText.text = (remainSkillCount*ScorePerEnemy)+"点";
        if(remainSkillCount >= 15)
        {
            remainSkillExpScoreText.color = Color.red;
        }
        AddLastScore(remainSkillCount * ScorePerEnemy);

    }

    void Chack50MaxHit()
    {
        domAudio.Play();
        if (scoreManager.maxhit >= 50)
        {
            nagiharaiScoreExpText.color = Color.red;
            nagiharaiScoreExpText.text = "最高!!";
            nagiharaiScoreText.text = "10000点";
            AddLastScore(10000);
        }
        else if (scoreManager.maxhit >= 40)
        {
            nagiharaiScoreExpText.text = "十分!! ";
            nagiharaiScoreText.text = "5000点";
            AddLastScore(5000);
        }
        else
        {
            nagiharaiScoreExpText.text = "残念……";
            nagiharaiScoreText.text = "1000点";
            AddLastScore(1000);

        }
    }

    void NoMissChack()
    {
        domAudio.Play();
        if (scoreManager.minhit > 0)
        {
            nomissScoreExpText.color = Color.red;
            nomissScoreExpText.text = "ノーミス!!";
            nomissScoreText.text = "5000点";
            AddLastScore(5000);
        }
        else
        {
            nomissScoreExpText.text = "ミス!!";
            nomissScoreText.text = "1000点";
            AddLastScore(1000);
        }
    }

}
