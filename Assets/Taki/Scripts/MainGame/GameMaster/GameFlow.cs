using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour,IPlayerUseSkill
{

    [SerializeField] GameObject enemyParent;
    [SerializeField] ResultScoreMotion resultScoreMotion;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] HomeHomeVoice homehome;
    [SerializeField] GameObject threeTime;

    bool arrangeFlag1;//敵が初めて70以下になったら均等に再配置するフラグ。
    bool groupFlag2;//敵が初めて40以下になったら均等に再配置するフラグ。
    bool groupFlag3;//敵が初めて20以下になったら均等に再配置するフラグ。
    bool groupFlag4;//敵が初めて5以下になったら均等に再配置するフラグ。

    public bool nearEnding { get; private set; }

    /// <summary>
    /// 最終的なスコア
    /// </summary>
    public int finalScore { get; private set; }

    int previousEnemyNum;

    enum State
    {
        WaitingForStart,
        CountDown,
        PlayerSkillSelecting,
        PlayerSkillUsing,
        Ending,
        Ended,
        Setteing
    }
    State state;
    State tempState;//設定画面を開いているときに、一時的に前の状態を保存する変数

    private void Awake()
    {
        previousEnemyNum = enemyParent.transform.childCount;
        state = State.WaitingForStart;
    }


    public bool IsEnded()
    {
        if(state == State.Ended)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsCountDown()
    {
        if(state == State.CountDown)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void EndCountDown()
    {
        if(state == State.CountDown)
        {
            state = State.PlayerSkillSelecting;
        }
    }

    /// <summary>
    /// セッチングを開けるか否かを返す
    /// </summary>
    /// <returns></returns>
    public bool CanSetting()
    {
        if (state == State.WaitingForStart || state == State.PlayerSkillSelecting)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 設定画面を開いたとき
    /// </summary>
    public void StartSetting()
    {
        tempState = state;
        state = State.Setteing;
    }

    /// <summary>
    /// 設定中か見る
    /// </summary>
    /// <returns></returns>
    public bool IsSetting()
    {
        if(state == State.Setteing)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 設定画面を開いたとき
    /// </summary>
    public void EndSetting()
    {
        state = tempState;
    }



    public void EndGame()
    {
        state = State.Ending;

        //リザルトの演出を見る。
        StartCoroutine(resultScoreMotion.ShowResult((int score) => 
        {
            finalScore = score;
            EndResultMotion();
            
        }));
    }


    void EndResultMotion()
    {
        state = State.Ended;
        homehome.PlayEvalateFinelScore(finalScore);
    }

    /// <summary>
    /// ゲームが始まった時に呼び出す関数
    /// </summary>
    public void StartGame()
    {
        if(state == State.WaitingForStart)
        {
            state = State.CountDown;
        }
    }

    /// <summary>
    /// プレイヤーにスキルの使用を許可する関数
    /// </summary>
    public bool CanUseSkill()
    {
        if(state == State.PlayerSkillSelecting)
        {
            state = State.PlayerSkillUsing;
            return true;
        }
        return false;
    }

    /// <summary>
    /// プレイヤーがスキルの使用終了を宣言する際に呼び出す関数
    /// </summary>
    public void EndUseSkill()
    {
        state = State.PlayerSkillSelecting;//状態を変更

        int enemyCount = enemyParent.transform.childCount;

        //状態に応じて褒めボイスをナガス。ヒット数がリセットされる前にナガス。
        homehome.PlayEvalatePlayerSkill(previousEnemyNum,scoreManager.tempHitNum);

        scoreManager.CurriculateMaxMinHit();//ヒット数の計算とリセット


        if(enemyCount < 50)
        {
            //50を切ったら大さびモードに
            nearEnding = true;

        }
        if (enemyCount == 0)
        {
            EndGame();//全滅したら終了
        }
        else if (!groupFlag3 && enemyCount <= 16)//敵が25以下で再配置
        {
            threeTime.SetActive(true);
            GroupEnemys2();
            groupFlag3 = true;
        }
            previousEnemyNum = enemyCount;
    }

    /// <summary>
    /// 現在プレイヤーがスキルを利用できるかどうかを返す
    /// </summary>
    /// <returns></returns>
    public bool CanSelectSkill()
    {
        if(state == State.PlayerSkillSelecting)
        {
            return true;
        }
        else
        {
            return false;
        }

    }


    /// <summary>
    /// 敵のトランスフォームを全取得します。
    /// </summary>
    /// <returns></returns>
    List<EnemyMove> FindEnemys()
    {
        List<EnemyMove> enemys;
        enemys = new List<EnemyMove>();

        for(int i=0;i< enemyParent.transform.childCount; i++)
        {
            enemys.Add(enemyParent.transform.GetChild(i).GetComponent<EnemyMove>());
        }
        return enemys;
    }

    void ArrangeEnemys()
    {
        List<EnemyMove> enemys = FindEnemys();

        for(int i = 0; i < enemys.Count; i++)
        {
            enemys[i].SetAngle(i/(float)enemys.Count * 360);
            enemys[i].SetRadius(Random.value*3 + 2);
        }

    }

    /// <summary>
    /// 敵を180度に分かれた2ぐるーぷに分ける
    /// </summary>
    void GroupEnemys1()
    {
        List<EnemyMove> enemys = FindEnemys();

        float angle = Random.Range(0f, 360f);//角度の決定
        float angle2 = angle + 180;//正反対の角度

        for (int i = 0; i < enemys.Count; i++)
        {
            if (i % 2 == 0)
            {
                enemys[i].SetAngle(angle + Random.Range(0,45f));
                enemys[i].SetRadius(Random.value * 3 + 2);
            }
            else
            {
                enemys[i].SetAngle(angle2 + Random.Range(0, 45f));
                enemys[i].SetRadius(Random.value * 2 + 2);
            }

        }
    }

    /// <summary>
    /// 敵を900度に分かれた2ぐるーぷに分ける
    /// </summary>
    void GroupEnemys2()
    {
        List<EnemyMove> enemys = FindEnemys();

        float angle = Random.Range(0f, 360f);//角度の決定
        float angle2 = angle + 900;//直角の角度

        for (int i = 0; i < enemys.Count; i++)
        {
            if (i % 2 == 0)
            {
                enemys[i].SetAngle(angle + Random.Range(0, 30f));
                enemys[i].SetRadius(Random.value * 2 + 3);
            }
            else
            {
                enemys[i].SetAngle(angle2 + Random.Range(0, 30f));
                enemys[i].SetRadius(Random.value * 2 + 2);
            }

        }
    }

    /// <summary>
    /// 敵を1ぐるーぷに分ける
    /// </summary>
    void GroupEnemys3()
    {
        List<EnemyMove> enemys = FindEnemys();

        float angle = Random.Range(0f, 360f);//角度の決定

        for (int i = 0; i < enemys.Count; i++)
        {
            enemys[i].SetAngle(angle + Random.Range(0, 90f));
            enemys[i].SetRadius(Random.value*3  + 2);
        }
    }

}
