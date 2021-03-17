using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class PlayerSkillController : MonoBehaviour
{
    const int normalFontSize = 32;
    const int bigFontSize = 48;


    enum State
    {
        WaitingSelectSkill,
        CanUseSkill,
        WaitingUseSkill,
        GameEnd
    }
    State state;

    [SerializeField] SkillMasterData skillMasterData;//スキルの情報をいっぱい持ったデータ

    [SerializeField] GameFlow gameFlow;
    IPlayerUseSkill playerUseSkill;//マスターに情報を送るインターフェース

    [SerializeField] Text currentChar;//現在の文字を表示するテキスト
    [SerializeField] Text currentSkillNameText;//スキル名を表示するテキスト
    [SerializeField] Text currentSkillExprainText;//スキルを説明するテキスト

    int currentSkillNum;//現在のスキル番号
    public bool[] usedSkill { get; private set; } = new bool[26];//使用済みスキル

    [SerializeField] AudioSource skillNameAudioSource;
    [SerializeField] AudioSource skillSEAudio;



    //========================================================================================================
    //========================================================================================================
    //属性音楽たち
    //========================================================================================================
    //========================================================================================================
    [SerializeField] AudioSource fireSEAudio;
    [SerializeField] AudioSource aquaSEAudio;
    [SerializeField] AudioSource windSEAudio;
    [SerializeField] AudioSource holySEAudio;
    [SerializeField] AudioSource darkSEAudio;
    [SerializeField] AudioSource bombSEAudio;
    [SerializeField] AudioSource slashSEAudio;



    //========================================================================================================
    //========================================================================================================
    //ここから攻撃用のプレハブたち
    //========================================================================================================
    //========================================================================================================
    [SerializeField,Header("攻撃用のプレハブ")] GameObject whiteAttackBar;//攻撃判定のある普通の棒
    [SerializeField] GameObject blackAttackBar;//攻撃判定のある普通の棒
    [SerializeField] GameObject poisonAttackBar;//攻撃判定のある普通の棒
    [SerializeField] GameObject orangeBomb;//爆発パーティクルと判定
    [SerializeField] GameObject fireEffect;//炎パーティクルと判定
    [SerializeField] GameObject aquaEffect;//水パーティクルと判定
    [SerializeField] GameObject windEffect;//風パーティクルと判定
    [SerializeField] GameObject holyEffect;//聖パーティクルと判定
    [SerializeField] GameObject darkEffect;//闇パーティクルと判定
    [SerializeField] GameObject misticEffect;//不思議パーティクルと判定

    //========================================================================================================
    //========================================================================================================
    //アルファベットテキストたちと攻撃予測範囲たちとか、26種類あるやつ
    //========================================================================================================
    //========================================================================================================
    [SerializeField] Text[] alphabetTexts;//キーボード風に並んでるアレ
    [SerializeField] GameObject[] attacRangePredictions;//攻撃範囲を示すオブジェクトたち
    [SerializeField] AudioClip[] skillNameSounds;//技名を叫ぶやつ


    // Start is called before the first frame update
    void Start()
    {
        currentSkillNum = -1;//何も選択していない。
        playerUseSkill = gameFlow;//インターフェースのみが欲しいので

        currentChar.text = "";
        currentSkillNameText.text = "キーを押してスキルを選んでね";
        currentSkillExprainText.text = "連続でキーを押すと\n技が出るよ!!";
    }

    // Update is called once per frame
    void Update()
    {

        SelectingSkill();

    }

    /// <summary>
    /// スキルの番号を選択する。
    /// </summary>
    void SelectingSkill()
    {
        if (!playerUseSkill.CanSelectSkill())
        {
            return;//スキルがつかえないなら早期リターン
        }
        if(usedSkill.Where(i => (i == false)).Count() == 0)
        {
            gameFlow.EndGame();//すべての技を使い切ったらゲーム終了
        }

        for (int i = 0; i < 26; i++)
        {
            //97がA、122がZでその間は順番。だから、97から122までチェックして行く。
            if (Input.GetKeyDown((KeyCode)(i + 97)))
            {

                if (currentSkillNum == i && !usedSkill[i])//前回と同じスキルを選択した場合
                {
                    TryToUseSkill();//スキルの使用を試みる(条件があるので)
                }

                //直前がちゃんとした番号の場合は、直前のやつをもとに戻す
                if (currentSkillNum >= 0 && currentSkillNum < 26)
                {
                    BackAlphabetInfomations(currentSkillNum);
                }

                currentSkillNum = i;//選択中スキル番号の更新

                SetAlphabetInfomations(i);//新しい情報を入力する

                currentChar.text = (System.Convert.ToChar(i + 97 - 32).ToString());//大文字のASCIIに変換
            }
        }

        //エンターキーなどを押したときに発動を試みる
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {

            if ( currentSkillNum >=0 && currentSkillNum < 26)//範囲内かどうかの判定
            {
                if (!usedSkill[currentSkillNum])//使えるかどうかの判定
                {
                    TryToUseSkill();//スキルの使用を試みる(条件があるので)
                    BackAlphabetInfomations(currentSkillNum);
                    SetAlphabetInfomations(currentSkillNum);
                }
            }
        }

    }

    void BackAlphabetInfomations(int num)
    {
        attacRangePredictions[num].SetActive(false);//攻撃範囲を明示する
        alphabetTexts[num].fontSize = normalFontSize;//フォントサイズをデカくする
        AlphabetColorChange(num, new Color(50 / 255f, 50 / 255f, 50 / 255f));//色を変える
    }

    void SetAlphabetInfomations(int num)
    {
        attacRangePredictions[num].SetActive(true);//攻撃範囲を明示する
        alphabetTexts[num].fontSize = bigFontSize;//フォントサイズをデカくする
        AlphabetColorChange(num, Color.yellow);//色を変える
        currentSkillNameText.text = skillMasterData.skills[num].skillName;//スキルの名前を設定する
        currentSkillExprainText.text = skillMasterData.skills[num].skillExpraination;//スキルの説明を入力する
    }

    void AlphabetColorChange(int num, Color color)
    {
        if (usedSkill[num])//使用済みの場合
        {
            alphabetTexts[num].color = new Color(50 / 255f, 50 / 255f, 50 / 255f,0.3f);//黒く、やや透明にする
        }
        else
        {
            alphabetTexts[num].color = color;//決められた色にする。
        }
    }

    void TryToUseSkill()
    {
        if (state == State.WaitingSelectSkill)
        {
            if (playerUseSkill.CanUseSkill())
            {
                Debug.Log("スキルが使えたよ！！");
                usedSkill[currentSkillNum] = true;
                UseSkill();
            }
        }
    }


    void UseSkill()
    {
        switch (currentSkillNum)
        {
            case 0:
                StartCoroutine(Skill0());
                break;
            case 1:
                StartCoroutine(Skill1());
                break;
            case 2:
                StartCoroutine(Skill2());
                break;
            case 3:
                StartCoroutine(Skill3());
                break;
            case 4:
                StartCoroutine(Skill4());
                break;
            case 5:
                StartCoroutine(Skill5());
                break;
            case 6:
                StartCoroutine(Skill6());
                break;
            case 7:
                StartCoroutine(Skill7());
                break;
            case 8:
                StartCoroutine(Skill8());
                break;
            case 9:
                StartCoroutine(Skill9());
                break;
            case 10:
                StartCoroutine(Skill10());
                break;
            case 11:
                StartCoroutine(Skill11());
                break;
            case 12:
                StartCoroutine(Skill12());
                break;
            case 13:
                StartCoroutine(Skill13());
                break;
            case 14:
                StartCoroutine(Skill14());
                break;
            case 15:
                StartCoroutine(Skill15());
                break;
            case 16:
                StartCoroutine(Skill16());
                break;
            case 17:
                StartCoroutine(Skill17());
                break;
            case 18:
                StartCoroutine(Skill18());
                break;
            case 19:
                StartCoroutine(Skill19());
                break;
            case 20:
                StartCoroutine(Skill20());
                break;
            case 21:
                StartCoroutine(Skill21());
                break;
            case 22:
                StartCoroutine(Skill22());
                break;
            case 23:
                StartCoroutine(Skill23());
                break;
            case 24:
                StartCoroutine(Skill24());
                break;
            case 25:
                StartCoroutine(Skill25());
                break;
            default:
                break;
        }
    }

    IEnumerator Skill0()//Aのスキル
    {
        yield return  PlaySkillSEAndWait(skillNameSounds[0]);//技発動前の溜め

        //まずは斜め下に行く棒２つ
        GameObject obj1 = CreateAttackBar(240, new Vector3(2, 8, 0), 20, whiteAttackBar);
        GameObject obj2 = CreateAttackBar(-60, new Vector3(-2, 8, 0), 20, whiteAttackBar);
        //あと下の方に横一線
        GameObject obj3 = CreateAttackBar(0, new Vector3(-20, -2, 0), 40, whiteAttackBar);
        obj1.transform.localScale = new Vector3(10, 6, 10);
        obj2.transform.localScale = new Vector3(10, 6, 10);
        obj3.transform.localScale = new Vector3(10, 6, 10);

        yield return new WaitForSeconds(2f);//2秒待つ

        Destroy(obj1);
        Destroy(obj2);
        Destroy(obj3);

        playerUseSkill.EndUseSkill();//スキル終了の宣言
        yield return null;
    }
    IEnumerator Skill1()//Bのスキル
    {

        yield return  PlaySkillSEAndWait(skillNameSounds[1]);//技発動前の溜め

        //爆発2つ
        CreateBomb(new Vector3(1.5f,1.5f,0), 4);
        CreateBomb(new Vector3(1.5f,-1.5f * Mathf.Pow(2, 1 / 2f), 0), 5);
        yield return new WaitForSeconds(2.5f);//2秒待つ

        //パーティクルは終了時自滅してくれる。

        playerUseSkill.EndUseSkill();//スキル終了の宣言
        yield return null;
    }
    IEnumerator Skill2()//Cのスキル
    {

        yield return  PlaySkillSEAndWait(skillNameSounds[2]);//溜め


        //3回に分けて爆発が起こる。
        CreateBomb(new Vector3(-3f,0, 0), 4);
        yield return new WaitForSeconds(0.3f);//待ち
        CreateBomb(new Vector3(-2.414f, 1, 0), 4);
        CreateBomb(new Vector3(-2.414f, -1, 0), 4);
        yield return new WaitForSeconds(0.3f);//待ち
        CreateBomb(new Vector3(-2f, 1.414f, 0), 3);
        CreateBomb(new Vector3(-2f, -1.414f, 0), 3);

        yield return new WaitForSeconds(1f);//待つ

        //パーティクルは削除不要

        playerUseSkill.EndUseSkill();//スキル終了の宣言
        yield return null;
    }
    IEnumerator Skill3()//Dのスキル
    {
        yield return  PlaySkillSEAndWait(skillNameSounds[3]);//溜め


        //複数回に分けて爆発が起こる。
        CreateDark(new Vector3(1, 0, 0), 3f);
        yield return new WaitForSeconds(0.1f);//待ち
        CreateDark(new Vector3(1, 4, 0), 1.5f);
        yield return new WaitForSeconds(0.1f);//待ち
        CreateDark(new Vector3(5, 0, 0), 1.5f);
        yield return new WaitForSeconds(0.1f);//待ち
        CreateDark(new Vector3(3, -3, 0), 1.5f);
        yield return new WaitForSeconds(0.1f);//待ち
        CreateDark(new Vector3(4, 2, 0), 1.5f);
        yield return new WaitForSeconds(0.1f);//待ち
        CreateDark(new Vector3(4, -2, 0), 1.5f);
        yield return new WaitForSeconds(0.1f);//待ち
        CreateDark(new Vector3(3, 3, 0), 1.5f);
        yield return new WaitForSeconds(0.1f);//待ち
        CreateDark(new Vector3(1, -4, 0), 1.5f);
        yield return new WaitForSeconds(0.1f);//待ち


        yield return new WaitForSeconds(1f);//待つ

        //パーティクルは削除不要

        playerUseSkill.EndUseSkill();//スキル終了の宣言
        yield return null;
    }
    IEnumerator Skill4()//Eのスキル
    {
        yield return  PlaySkillSEAndWait(skillNameSounds[4]);//溜め
        //下に行く棒２つ
        GameObject obj1 = CreateAttackBar(-90, new Vector3(-1, 10, 0), 20, whiteAttackBar);
        yield return new WaitForSeconds(0.5f);//1秒待つ
        CreateFire(new Vector3(0, 3, 0), 1);
        CreateAqua(new Vector3(0, 0, 0), 1);
        CreateWind(new Vector3(0, -3, 0), 1);
        yield return new WaitForSeconds(0.1f);//0.2秒待つ
        CreateFire(new Vector3(1, 3, 0), 1);
        CreateAqua(new Vector3(1, 0, 0), 1);
        CreateWind(new Vector3(1, -3, 0), 1);

        yield return new WaitForSeconds(0.1f);//0.2秒待つ
        CreateFire(new Vector3(2, 3, 0), 1);
        CreateAqua(new Vector3(2, 0, 0), 1);
        CreateWind(new Vector3(2, -3, 0), 1);

        yield return new WaitForSeconds(0.1f);//0.2秒待つ
        CreateFire(new Vector3(3, 3, 0), 1);
        CreateAqua(new Vector3(3, 0, 0), 1);
        CreateWind(new Vector3(3, -3, 0), 1);

        yield return new WaitForSeconds(1f);//1秒待つ

        Destroy(obj1);
        playerUseSkill.EndUseSkill();//スキル終了の宣言
        yield return null;
    }
    IEnumerator Skill5()//Fのスキル
    {
        yield return  PlaySkillSEAndWait(skillNameSounds[5]);//溜め
        CreateFire(new Vector3(-3, -4.5f, 0), 1);
        CreateFire(new Vector3(-2.5f, -2.5f, 0), 1);
        CreateFire(new Vector3(-1.5f, -2, 0), 1);
        yield return new WaitForSeconds(0.1f);//待つ
        CreateFire(new Vector3(-2, 0, 0), 1);
        CreateFire(new Vector3(-0.5f, 2, 0), 1);
        CreateFire(new Vector3(0.5f, 3.5f, 0), 1);
        CreateFire(new Vector3(2.5f, 3.5f, 0), 1);
        yield return new WaitForSeconds(0.1f);//待つ
        CreateFire(new Vector3(0.5f, -1.25f, 0), 1);
        CreateFire(new Vector3(2.25f, -1, 0), 1);
        CreateFire(new Vector3(4f, -1.5f, 0), 1);
        CreateFire(new Vector3(4f, 2.5f, 0), 1);

        yield return new WaitForSeconds(1f);//1秒待つ



        playerUseSkill.EndUseSkill();//スキル終了の宣言
        yield return null;
    }
    IEnumerator Skill6()//Gのスキル
    {
        yield return  PlaySkillSEAndWait(skillNameSounds[6]);//溜め

        CreateMistic(new Vector3(-1, 0, 0), 5);
        yield return new WaitForSeconds(0.5f);//4秒待つ

        CreateDark(new Vector3(2.5f, -1.5f, 0), 1);
        CreateDark(new Vector3(3, -1.5f, 0), 1);
        CreateDark(new Vector3(3.5f, -1.5f, 0), 1);
        CreateDark(new Vector3(4, -1.5f, 0), 1);
        CreateDark(new Vector3(3, -2.5f, 0), 1);
        CreateDark(new Vector3(3, -3.5f, 0), 1);


        yield return new WaitForSeconds(1f);//4秒待つ



        playerUseSkill.EndUseSkill();//スキル終了の宣言
        yield return null;
    }
    IEnumerator Skill7()//Hのスキル
    {
        yield return  PlaySkillSEAndWait(skillNameSounds[7]);//溜め
        //まずは斜め下に行く棒２つ
        GameObject obj1 = CreateAttackBar(-90, new Vector3(4, 8, 0), 20, whiteAttackBar);
        GameObject obj2 = CreateAttackBar(-90, new Vector3(-4, 8, 0), 20, whiteAttackBar);
        yield return new WaitForSeconds(0.5f);//4秒待つ
        CreateHoly(new Vector3(4, 0, 0), 3);
        CreateHoly(new Vector3(1, 0, 0), 2);
        CreateHoly(new Vector3(-1, 0, 0), 2);
        CreateHoly(new Vector3(-4, 0, 0), 3);

        yield return new WaitForSeconds(1.5f);//待つ

        Destroy(obj1);
        Destroy(obj2);

        playerUseSkill.EndUseSkill();//スキル終了の宣言
        yield return null;
    }
    IEnumerator Skill8()//Iのスキル
    {
        yield return  PlaySkillSEAndWait(skillNameSounds[8]);//溜め
        //棒3つ
        GameObject obj1 = CreateAttackBar(-90, new Vector3(0, 10, 0), 20, whiteAttackBar);
        GameObject obj2 = CreateAttackBar(-90, new Vector3(0.5f, 13, 0), 20, whiteAttackBar);
        GameObject obj3 = CreateAttackBar(-90, new Vector3(-0.5f, 14, 0), 20, whiteAttackBar);
        GameObject obj4 = CreateAttackBar(-90, new Vector3(1f, 11, 0), 20, whiteAttackBar);
        GameObject obj5 = CreateAttackBar(-90, new Vector3(-1f, 10, 0), 20, whiteAttackBar);
        yield return new WaitForSeconds(3f);//2秒待つ

        Destroy(obj1);
        Destroy(obj2);
        Destroy(obj3);
        Destroy(obj4);
        Destroy(obj5);

        playerUseSkill.EndUseSkill();//スキル終了の宣言
        yield return null;
    }
    IEnumerator Skill9()//Jのスキル
    {
        yield return  PlaySkillSEAndWait(skillNameSounds[9]);//溜め

        CreateMistic(new Vector3(2.5f, 3.5f, 0), 2);
        yield return new WaitForSeconds(0.1f);//0.1秒待つ

        CreateFire(new Vector3(2.5f, 3, 0), 1);
        yield return new WaitForSeconds(0.1f);//0.1秒待つ

        CreateAqua(new Vector3(3, 2, 0), 1);
        yield return new WaitForSeconds(0.1f);//0.1秒待つ

        CreateWind(new Vector3(2.5f, 1, 0), 1);
        yield return new WaitForSeconds(0.1f);//0.1秒待つ

        CreateDark(new Vector3(2.5f, 0, 0), 1);
        yield return new WaitForSeconds(0.1f);//0.1秒待つ

        CreateFire(new Vector3(4, 0, 0), 1);
        yield return new WaitForSeconds(0.1f);//0.1秒待つ

        CreateHoly(new Vector3(2, -1, 0), 1);
        yield return new WaitForSeconds(0.1f);//0.1秒待つ

        CreateWind(new Vector3(2.5f, -3, 0), 2);
        yield return new WaitForSeconds(0.1f);//0.1秒待つ

        CreateMistic(new Vector3(1, -4, 0), 1);
        yield return new WaitForSeconds(0.3f);//多めに待つ

        CreateMistic(new Vector3(-2, -4, 0), 3);
        yield return new WaitForSeconds(1f);//4秒待つ



        playerUseSkill.EndUseSkill();//スキル終了の宣言
        yield return null;
    }
    IEnumerator Skill10()//Kのスキル
    {
        yield return  PlaySkillSEAndWait(skillNameSounds[10]);//溜め

        //まずは斜め下に行く棒２つ
        GameObject obj1 = CreateAttackBar(90, new Vector3(-2, 0, 0), 10, poisonAttackBar);
        GameObject obj2 = CreateAttackBar(-90, new Vector3(-2, 0, 0), 10, poisonAttackBar);
        //あと下の方に横一線                                              
        GameObject obj3 = CreateAttackBar(60, new Vector3(0, 2*1.732f, 0), 10, poisonAttackBar);
        GameObject obj4 = CreateAttackBar(-60, new Vector3(0, -2 * 1.732f, 0), 10, poisonAttackBar);

        yield return new WaitForSeconds(3f);//4秒待つ

        Destroy(obj1);
        Destroy(obj2);
        Destroy(obj3);
        Destroy(obj4);

        playerUseSkill.EndUseSkill();//スキル終了の宣言
        yield return null;
    }
    IEnumerator Skill11()//Lのスキル
    {
        yield return  PlaySkillSEAndWait(skillNameSounds[11]);//溜め


        GameObject obj1 = CreateAttackBar(-120, new Vector3(-4 + 5, 5*1.732f, 0), 20, whiteAttackBar);
        GameObject obj2 = CreateAttackBar(-120, new Vector3(-3 + 5, 5 * 1.732f, 0), 20, whiteAttackBar);
        obj1.transform.localScale = new Vector3(10, 6, 10);
        obj2.transform.localScale = new Vector3(10, 6, 10);

        yield return new WaitForSeconds(0.5f);//4秒待つ


        GameObject obj3 = CreateAttackBar(-30, new Vector3(-4- 5 * 1.732f, -2 + 5, 0), 20, whiteAttackBar);
        GameObject obj4 = CreateAttackBar(-30, new Vector3(-4- 5 * 1.732f, -1f + 5, 0), 20, whiteAttackBar);
        obj3.transform.localScale = new Vector3(10, 6, 10);
        obj4.transform.localScale = new Vector3(10, 6, 10);

        yield return new WaitForSeconds(2f);//4秒待つ

        Destroy(obj1);
        Destroy(obj2);
        Destroy(obj3);
        Destroy(obj4);

        playerUseSkill.EndUseSkill();//スキル終了の宣言
        yield return null;
    }
    IEnumerator Skill12()//Mのスキル
    {
        yield return  PlaySkillSEAndWait(skillNameSounds[12]);//溜め
        //まずは斜め下に行く棒２つ
        GameObject obj1 = CreateAttackBar(240, new Vector3(+2+5, 5 * 1.732f, 0), 20, whiteAttackBar);
        GameObject obj2 = CreateAttackBar(-60, new Vector3(-2-5, 5 * 1.732f, 0), 20, whiteAttackBar);
        yield return new WaitForSeconds(0.5f);//待つ
        CreateMistic(new Vector3(4, 1, 0), 1);
        CreateMistic(new Vector3(-4, 1, 0), 1);
        yield return new WaitForSeconds(0.26f);//待つ
        CreateMistic(new Vector3(4.5f, -1, 0), 1);
        CreateMistic(new Vector3(-4.5f, -1, 0), 1);
        yield return new WaitForSeconds(0.24f);//待つ
        CreateMistic(new Vector3(0, -3, 0), 3);





        yield return new WaitForSeconds(1f);//4秒待つ

        Destroy(obj1);
        Destroy(obj2);


        playerUseSkill.EndUseSkill();//スキル終了の宣言
        yield return null;
    }
    IEnumerator Skill13()//Nのスキル
    {
        yield return  PlaySkillSEAndWait(skillNameSounds[13]);//溜め
        //まずは斜め下に行く棒２つ
        GameObject obj1 = CreateAttackBar(90, new Vector3(3.5f, 0, 0), 0, blackAttackBar);
        GameObject obj2 = CreateAttackBar(-90, new Vector3(-3.5f, 0, 0), 0, blackAttackBar);
        GameObject obj3 = CreateAttackBar(0 - 45, new Vector3(-0, 0, 0), 0, whiteAttackBar);
        yield return new WaitForSeconds(2.5f);//待つ
        Destroy(obj1);
        Destroy(obj2);
        Destroy(obj3);

        yield return new WaitForSeconds(0.5f);//待つ

        playerUseSkill.EndUseSkill();//スキル終了の宣言
        yield return null;
    }
    IEnumerator Skill14()//Oのスキル
    {
        yield return  PlaySkillSEAndWait(skillNameSounds[14]);//溜め

        CreateMistic(new Vector3(5, 0, 0), 2);
        yield return new WaitForSeconds(0.1f);//0.1秒待つ
        CreateFire(new Vector3(3.5f, 3.5f, 0), 2);
        yield return new WaitForSeconds(0.1f);//0.1秒待つ
        CreateWind(new Vector3(0, 5, 0), 2);
        yield return new WaitForSeconds(0.1f);//0.1秒待つ
        CreateDark(new Vector3(-3.5f, 3.5f, 0), 2);
        yield return new WaitForSeconds(0.1f);//0.1秒待つ
        CreateHoly(new Vector3(-5, 0, 0), 2);
        yield return new WaitForSeconds(0.1f);//0.1秒待つ
        CreateAqua(new Vector3(-3.5f,-3.5f, 0), 2);
        yield return new WaitForSeconds(0.1f);//0.1秒待つ
        CreateMistic(new Vector3(0, -5, 0), 2);
        yield return new WaitForSeconds(0.1f);//0.1秒待つ
        CreateAqua(new Vector3(3.5f, -3.5f, 0), 2);
        yield return new WaitForSeconds(0.1f);//0.1秒待つ



        yield return new WaitForSeconds(1f);//4秒待つ



        playerUseSkill.EndUseSkill();//スキル終了の宣言
        yield return null;
    }
    IEnumerator Skill15()//Pのスキル
    {
        yield return  PlaySkillSEAndWait(skillNameSounds[15]);//溜め
        //まずは斜め下に行く棒２つ
        GameObject obj1 = CreateAttackBar(-90, new Vector3(-1, 10, 0), 20, whiteAttackBar);
        yield return new WaitForSeconds(0.3f);//待つ
        CreateHoly(new Vector3(2, 3, 0), 5);
        yield return new WaitForSeconds(0.3f);//待つ
        CreateHoly(new Vector3(1, 2, 0), 5);
        CreateHoly(new Vector3(1, 4, 0), 5);

        yield return new WaitForSeconds(1f);//4秒待つ

        Destroy(obj1);


        playerUseSkill.EndUseSkill();//スキル終了の宣言
        yield return null;
    }
    IEnumerator Skill16()//Qのスキル
    {
        yield return  PlaySkillSEAndWait(skillNameSounds[16]);//溜め

        CreateFire(new Vector3(2.5f, 0, 0), 2);
        CreateAqua(new Vector3(-2.5f, 0, 0), 2);
        CreateWind(new Vector3(0, 2.5f, 0), 2);
        CreateBomb(new Vector3(0, -2.5f, 0), 2);
        GameObject obj1 = CreateAttackBar(-45, new Vector3(3, -3, 0), 10, poisonAttackBar);
        obj1.transform.localScale = new Vector3(10, 8, 10);
        yield return new WaitForSeconds(2f);//4秒待つ
        Destroy(obj1);



        playerUseSkill.EndUseSkill();//スキル終了の宣言
        yield return null;
    }
    IEnumerator Skill17()//Rのスキル
    {
        yield return  PlaySkillSEAndWait(skillNameSounds[17]);//溜め

        CreateBomb(new Vector3(-3, 3, 0), 4);
        yield return new WaitForSeconds(0.24f);//待つ
        CreateBomb(new Vector3(-1, 3, 0), 3);
        CreateBomb(new Vector3(-3, 1, 0), 3);
        CreateBomb(new Vector3(-1, 1, 0), 3);
        yield return new WaitForSeconds(0.24f);//待つ
        CreateBomb(new Vector3(1, 3, 0), 2);
        CreateBomb(new Vector3(-3, -1, 0), 2);
        CreateBomb(new Vector3(1, -1, 0), 2);
        yield return new WaitForSeconds(0.24f);//待つ
        CreateBomb(new Vector3(2, 3, 0), 1);
        CreateBomb(new Vector3(-3, -2, 0), 1);
        CreateBomb(new Vector3(3, -3, 0), 1);
        yield return new WaitForSeconds(1.08f);//待つ



        playerUseSkill.EndUseSkill();//スキル終了の宣言
        yield return null;
    }
    IEnumerator Skill18()//Sのスキル
    {
        yield return  PlaySkillSEAndWait(skillNameSounds[18]);//溜め

        GameObject obj1 = CreateAttackBar(0, new Vector3(-15, 4.5f, 0), 20, whiteAttackBar);
        GameObject obj2 = CreateAttackBar(180, new Vector3(15, -4.5f, 0), 20, whiteAttackBar);
        CreateBomb(new Vector3(0, 0, 0), 3);

        yield return new WaitForSeconds(0.24f);//待つ
        CreateBomb(new Vector3(2, -2, 0), 2);
        CreateBomb(new Vector3(-2, 2, 0), 2);
        yield return new WaitForSeconds(0.24f);//待つ
        CreateBomb(new Vector3(0.5f, -3, 0), 1);
        CreateBomb(new Vector3(-0.5f, 3, 0), 1);

        yield return new WaitForSeconds(0.24f);//待つ
        CreateBomb(new Vector3(-1f, -4, 0), 2);
        CreateBomb(new Vector3(1f, 4, 0), 2);

        yield return new WaitForSeconds(1.76f);//2秒待つ

        Destroy(obj1);
        Destroy(obj2);


        playerUseSkill.EndUseSkill();//スキル終了の宣言
        yield return null;
    }
    IEnumerator Skill19()//Tのスキル
    {
        yield return  PlaySkillSEAndWait(skillNameSounds[19]);//溜め

        GameObject obj = CreateAttackBar(90, new Vector3(0, -11, 0), 20, whiteAttackBar);


        //まずは斜め下に行く棒２つ
        GameObject obj1 = CreateAttackBar(0, new Vector3(15, 4, 0), -20, whiteAttackBar);
        GameObject obj2 = CreateAttackBar(0, new Vector3(18, 3, 0), -20, whiteAttackBar);
        GameObject obj3 = CreateAttackBar(0, new Vector3(21, 2, 0), -20, whiteAttackBar);
        //まずは斜め下に行く棒２つ
        GameObject obj4 = CreateAttackBar(0, new Vector3(-15, 4, 0), 20, whiteAttackBar);
        GameObject obj5 = CreateAttackBar(0, new Vector3(-18, 3, 0), 20, whiteAttackBar);
        GameObject obj6 = CreateAttackBar(0, new Vector3(-21, 2, 0), 20, whiteAttackBar);

        yield return new WaitForSeconds(3f);//4秒待つ
        Destroy(obj);
        Destroy(obj1);
        Destroy(obj2);
        Destroy(obj3);
        Destroy(obj4);
        Destroy(obj5);
        Destroy(obj6);

        playerUseSkill.EndUseSkill();//スキル終了の宣言
        yield return null;
    }
    IEnumerator Skill20()//Uのスキル
    {
        yield return  PlaySkillSEAndWait(skillNameSounds[20]);//溜め
        //まずは斜め下に行く棒3つ
        GameObject obj1 = CreateAttackBar(-60, new Vector3(-5-5, 5*1.732f, 0), 20, whiteAttackBar);
        GameObject obj2 = CreateAttackBar(-60, new Vector3(-4.5f-5, 5 * 1.732f, 0), 20, whiteAttackBar);
        GameObject obj3 = CreateAttackBar(-60, new Vector3(-4-5, 5 * 1.732f, 0), 20, whiteAttackBar);
        yield return new WaitForSeconds(1f);//4秒待つ

        CreateFire(new Vector3(0, -3, 0), 2);
        yield return new WaitForSeconds(0.3f);//4秒待つ

        CreateFire(new Vector3(3, -1, 0), 2);
        yield return new WaitForSeconds(0.3f);//4秒待つ

        CreateFire(new Vector3(5, 3, 0), 2);




        yield return new WaitForSeconds(2f);//4秒待つ

        Destroy(obj1);
        Destroy(obj2);
        Destroy(obj3);

        playerUseSkill.EndUseSkill();//スキル終了の宣言
        yield return null;
    }
    IEnumerator Skill21()//Vのスキル
    {
        yield return  PlaySkillSEAndWait(skillNameSounds[21]);//溜め

        CreateBomb(new Vector3(0, -4, 0), 4);
        yield return new WaitForSeconds(0.3f);//待つ


        CreateFire(new Vector3(2, -1, 0), 2);
        CreateFire(new Vector3(-2, -1, 0), 2);
        yield return new WaitForSeconds(0.14f);//待つ
        CreateFire(new Vector3(4, 2, 0), 2);
        CreateFire(new Vector3(-4, 2, 0), 2);
        yield return new WaitForSeconds(1f);//4秒待つ


        playerUseSkill.EndUseSkill();//スキル終了の宣言
        yield return null;
    }
    IEnumerator Skill22()//Wのスキル
    {
        yield return PlaySkillSEAndWait(skillNameSounds[22]);//溜め

        CreateWind(new Vector3(2, -4, 0), 2);
        CreateWind(new Vector3(-2, -4, 0), 2);
        yield return new WaitForSeconds(0.3f);//待つ
        CreateWind(new Vector3(3, -2, 0), 2);
        CreateWind(new Vector3(0, -2, 0), 2);
        CreateWind(new Vector3(-3, -2, 0), 2);
        yield return new WaitForSeconds(0.14f);//待つ

        CreateWind(new Vector3(4, 0, 0), 2);
        CreateWind(new Vector3(-4, 0, 0), 2);
        yield return new WaitForSeconds(1f);//4秒待つ


        playerUseSkill.EndUseSkill();//スキル終了の宣言
        yield return null;
    }
    IEnumerator Skill23()//Xのスキル
    {

        yield return  PlaySkillSEAndWait(skillNameSounds[23]);//溜め
        //斜め下に行く棒２つ
        GameObject obj1 = CreateAttackBar(240, new Vector3(10, 17.32f, 0), 20, whiteAttackBar);
        GameObject obj2 = CreateAttackBar(-60, new Vector3(-10, 17.32f, 0), 20, whiteAttackBar);
        obj1.transform.localScale = new Vector3(10, 10, 10);
        obj2.transform.localScale = new Vector3(10, 10, 10);

        yield return new WaitForSeconds(3f);//3秒待つ

        Destroy(obj1);
        Destroy(obj2);

        playerUseSkill.EndUseSkill();//スキル終了の宣言
        yield return null;
    }
    IEnumerator Skill24()//Yのスキル
    {
        yield return  PlaySkillSEAndWait(skillNameSounds[24]);//溜め
        //斜め下に行く棒２つ
        GameObject obj1 = CreateAttackBar(30, new Vector3(2 * 1.732f, 2, 0), 3, poisonAttackBar);
        GameObject obj2 = CreateAttackBar(150, new Vector3(-2 * 1.732f, 2, 0), 3, poisonAttackBar);
        GameObject obj3 = CreateAttackBar(270, new Vector3(0, -4, 0), 3, poisonAttackBar);
        obj1.transform.localScale = new Vector3(10, 10, 10);
        obj2.transform.localScale = new Vector3(10, 10, 10);
        obj3.transform.localScale = new Vector3(10, 10, 10);
        yield return new WaitForSeconds(4f);//5秒待つ

        Destroy(obj1);
        Destroy(obj2);
        Destroy(obj3);

        playerUseSkill.EndUseSkill();//スキル終了の宣言
        yield return null;
    }
    IEnumerator Skill25()//Zのスキル
    {
        yield return  PlaySkillSEAndWait(skillNameSounds[25]);//溜め
        //まずは横に平行な棒２つ
        GameObject obj1 = CreateAttackBar(0, new Vector3(15, 3.5f, 0), -20,whiteAttackBar);
        GameObject obj1_2 = CreateAttackBar(0, new Vector3(-15, 3, 0), 20, blackAttackBar);

        GameObject obj2 = CreateAttackBar(180, new Vector3(-15, -3.5f, 0), -20, whiteAttackBar);
        GameObject obj2_2 = CreateAttackBar(180, new Vector3(15, -3, 0), 20, blackAttackBar);

        //あと下の方に横一線
        GameObject obj3 = CreateAttackBar(-135, new Vector3(10.5f, 10, 0), 20, whiteAttackBar);
        GameObject obj3_2 = CreateAttackBar(-135, new Vector3(-10.5f, -10, 0), -20, blackAttackBar);

        yield return new WaitForSeconds(3f);//3秒待つ

        Destroy(obj1);
        Destroy(obj2);
        Destroy(obj3); 
        Destroy(obj1_2);
        Destroy(obj2_2);
        Destroy(obj3_2);

        playerUseSkill.EndUseSkill();//スキル終了の宣言
        yield return null;
    }


    IEnumerator PlaySkillSEAndWait(AudioClip skillNameSound)
    {
        skillSEAudio.Play();//効果音を鳴らす
        yield return new WaitForSeconds(0.5f);

        skillSEAudio.PlayOneShot(skillNameSound);
        //セリフを言い始める
        yield return new WaitForSeconds(0.2f);

    }




    /// <summary>
    /// 飛んでいく角度、初期座標、速度を指定して棒を生成、飛ばすことができます。
    /// </summary>
    /// <param name="angle"></param>
    /// <param name="initialPos"></param>
    /// <param name="speed"></param>
    /// <returns>作った棒のオブジェクト</returns>
    GameObject CreateAttackBar(float angle, Vector3 initialPos,float speed,GameObject barObj)
    {
        GameObject obj = Instantiate(barObj);
        Transform objTF = obj.transform;
        objTF.position = initialPos;
        objTF.localScale = new Vector3(10, 4, 1);
        objTF.eulerAngles = new Vector3(0, 0, angle);
        obj.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * speed;
        slashSEAudio.Play();

        return obj;
    }

    GameObject CreatePerticle(Vector3 initialPos, float size, GameObject perticle)
    {
        GameObject obj = Instantiate(perticle);
        Transform objTF = obj.transform;
        objTF.position = initialPos;
        objTF.localScale = new Vector3(size, size, size);
        return obj;
    }


    GameObject CreateFire(Vector3 initialPos, float size)
    {
        GameObject obj = Instantiate(fireEffect);
        Transform objTF = obj.transform;
        objTF.position = initialPos;
        objTF.localScale = new Vector3(size, size, size);
        fireSEAudio.Play();
        return obj;
    }
    GameObject CreateAqua(Vector3 initialPos, float size)
    {
        GameObject obj = Instantiate(aquaEffect);
        Transform objTF = obj.transform;
        objTF.position = initialPos;
        objTF.localScale = new Vector3(size, size, size);
        aquaSEAudio.Play();
        return obj;
    }
    GameObject CreateWind(Vector3 initialPos, float size)
    {
        GameObject obj = Instantiate(windEffect);
        Transform objTF = obj.transform;
        objTF.position = initialPos;
        objTF.localScale = new Vector3(size, size, size);
        windSEAudio.Play();
        return obj;
    }
    GameObject CreateHoly(Vector3 initialPos, float size)
    {
        GameObject obj = Instantiate(holyEffect);
        Transform objTF = obj.transform;
        objTF.position = initialPos;
        objTF.localScale = new Vector3(size, size, size);
        holySEAudio.Play();
        return obj;
    }
    GameObject CreateDark(Vector3 initialPos, float size)
    {
        GameObject obj = Instantiate(darkEffect);
        Transform objTF = obj.transform;
        objTF.position = initialPos;
        objTF.localScale = new Vector3(size, size, size);
        darkSEAudio.Play();
        return obj;
    }

    GameObject CreateMistic(Vector3 initialPos, float size)
    {
        GameObject obj = Instantiate(misticEffect);
        Transform objTF = obj.transform;
        objTF.position = initialPos;
        objTF.localScale = new Vector3(size, size, size);
        darkSEAudio.Play();
        return obj;
    }

    GameObject CreateBomb(Vector3 initialPos ,float size)
    {
        GameObject obj = Instantiate(orangeBomb);
        Transform objTF = obj.transform;
        objTF.position = initialPos;
        objTF.localScale = new Vector3(size, size, size);
        bombSEAudio.Play();
        return obj;
    }



}
