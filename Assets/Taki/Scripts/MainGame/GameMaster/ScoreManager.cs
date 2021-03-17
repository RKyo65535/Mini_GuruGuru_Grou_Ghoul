using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    const string InitialScoreText = "Score:";
    const string InitialCurrentHitText = "hit数:";
    const string InitialMinHitText = "最小hit数:";
    const string InitialMaxHitText = "最大hit数:";

    /// <summary>
    /// スコア。読み取りはできるよ。書き込みはマネージャーの特権
    /// </summary>
    public int basicScore { get; private set; }
    /// <summary>
    /// 一撃で当てた最大数
    /// </summary>
    public int maxhit { get; private set; }
    /// <summary>
    /// 一撃で当てた最小数
    /// </summary>
    public int minhit { get; private set; }


    public int tempHitNum { get; private set; }//敵から受け取ったヒットしたメッセージの数


    [SerializeField] TextMeshProUGUI scoreText;//スコア表示用のテキスト
    [SerializeField] TextMeshProUGUI currentHitText;//一撃で当てた最大の数のテキスト
    [SerializeField] TextMeshProUGUI maxHitText;//一撃で当てた最大の数のテキスト
    [SerializeField] TextMeshProUGUI minHitText;//一撃で当てた最小の数のテキスト

    // Start is called before the first frame update
    void Start()
    {
        maxhit = 0;
        minhit = 999;
        UpdataScoreText();

    }

    public void AddBasicScore(int addingScore)
    {
        basicScore += addingScore;//スコアの加算
        UpdataScoreText();
    }

    public void HitPlayersAttack()
    {
        tempHitNum = tempHitNum + 1;
        currentHitText.text = InitialCurrentHitText + tempHitNum;
    }

    public void CurriculateMaxMinHit()
    {


        if (tempHitNum > maxhit)
        {
            maxhit = tempHitNum;
        }

        if(tempHitNum < minhit)
        {
            minhit = tempHitNum;
        }
        tempHitNum = 0;

        minHitText.text = InitialMinHitText + minhit;
        maxHitText.text = InitialMaxHitText + maxhit;
    }


    void UpdataScoreText()
    {
        scoreText.text = InitialScoreText + basicScore;
    }

}
