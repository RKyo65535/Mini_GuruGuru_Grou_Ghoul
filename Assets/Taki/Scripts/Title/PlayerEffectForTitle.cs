using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectForTitle : MonoBehaviour
{


    int count;
    int currentSkillNum;

    [SerializeField, Header("攻撃用のプレハブ")] GameObject whiteAttackBar;//攻撃判定のある普通の棒
    [SerializeField] GameObject blackAttackBar;//攻撃判定のある普通の棒
    [SerializeField] GameObject poisonAttackBar;//攻撃判定のある普通の棒
    [SerializeField] GameObject orangeBomb;//爆発パーティクルと判定
    [SerializeField] GameObject fireEffect;//炎パーティクルと判定
    [SerializeField] GameObject aquaEffect;//水パーティクルと判定
    [SerializeField] GameObject windEffect;//風パーティクルと判定
    [SerializeField] GameObject holyEffect;//聖パーティクルと判定
    [SerializeField] GameObject darkEffect;//闇パーティクルと判定
    [SerializeField] GameObject misticEffect;//不思議パーティクルと判定




    // Update is called once per frame
    void FixedUpdate()
    {
        count = count + 1;
        if(count == 75)
        {
            count = 0;

            UseSkill();
            currentSkillNum = (currentSkillNum + 1) % 26;
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

        //まずは斜め下に行く棒２つ
        GameObject obj1 = CreateAttackBar(240, new Vector3(2, 8, 0), 20, whiteAttackBar);
        GameObject obj2 = CreateAttackBar(-60, new Vector3(-2, 8, 0), 20, whiteAttackBar);
        //あと下の方に横一線
        GameObject obj3 = CreateAttackBar(0, new Vector3(-20, -2, 0), 40, whiteAttackBar);

        yield return new WaitForSeconds(2f);//2秒待つ

        Destroy(obj1);
        Destroy(obj2);
        Destroy(obj3);

        yield return null;
    }
    IEnumerator Skill1()//Bのスキル
    {


        //爆発2つ
        CreateBomb(new Vector3(1.5f, 1.5f, 0), 3);
        CreateBomb(new Vector3(1.5f, -1.5f * Mathf.Pow(2, 1 / 2f), 0), 3 * Mathf.Pow(2, 1 / 2f));
        yield return new WaitForSeconds(2.5f);//2秒待つ

        //パーティクルは終了時自滅してくれる。

        yield return null;
    }
    IEnumerator Skill2()//Cのスキル
    {



        //3回に分けて爆発が起こる。
        CreateBomb(new Vector3(-3f, 0, 0), 3);
        yield return new WaitForSeconds(0.3f);//待ち
        CreateBomb(new Vector3(-2.414f, 1, 0), 3);
        CreateBomb(new Vector3(-2.414f, -1, 0), 3);
        yield return new WaitForSeconds(0.3f);//待ち
        CreateBomb(new Vector3(-2f, 1.414f, 0), 3);
        CreateBomb(new Vector3(-2f, -1.414f, 0), 3);

        yield return new WaitForSeconds(1f);//待つ

        //パーティクルは削除不要

        yield return null;
    }
    IEnumerator Skill3()//Dのスキル
    {


        //複数回に分けて爆発が起こる。
        CreateDark(new Vector3(1, 0, 0), 1);
        yield return new WaitForSeconds(0.1f);//待ち
        CreateDark(new Vector3(1, 4, 0), 1);
        yield return new WaitForSeconds(0.1f);//待ち
        CreateDark(new Vector3(5, 0, 0), 1);
        yield return new WaitForSeconds(0.1f);//待ち
        CreateDark(new Vector3(3, -3, 0), 1);
        yield return new WaitForSeconds(0.1f);//待ち
        CreateDark(new Vector3(4, 2, 0), 1);
        yield return new WaitForSeconds(0.1f);//待ち
        CreateDark(new Vector3(4, -2, 0), 1);
        yield return new WaitForSeconds(0.1f);//待ち
        CreateDark(new Vector3(3, 3, 0), 1);
        yield return new WaitForSeconds(0.1f);//待ち
        CreateDark(new Vector3(1, -4, 0), 1);
        yield return new WaitForSeconds(0.1f);//待ち


        yield return new WaitForSeconds(1f);//待つ

        //パーティクルは削除不要

        yield return null;
    }
    IEnumerator Skill4()//Eのスキル
    {
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
        yield return null;
    }
    IEnumerator Skill5()//Fのスキル
    {
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



        yield return null;
    }
    IEnumerator Skill6()//Gのスキル
    {

        CreateMistic(new Vector3(-1, 0, 0), 4);
        yield return new WaitForSeconds(0.5f);//4秒待つ

        CreateDark(new Vector3(2.5f, -1.5f, 0), 1);
        CreateDark(new Vector3(3, -1.5f, 0), 1);
        CreateDark(new Vector3(3.5f, -1.5f, 0), 1);
        CreateDark(new Vector3(4, -1.5f, 0), 1);
        CreateDark(new Vector3(3, -2.5f, 0), 1);
        CreateDark(new Vector3(3, -3.5f, 0), 1);


        yield return new WaitForSeconds(1f);//4秒待つ



        yield return null;
    }
    IEnumerator Skill7()//Hのスキル
    {
        //まずは斜め下に行く棒２つ
        GameObject obj1 = CreateAttackBar(-90, new Vector3(4, 8, 0), 20, whiteAttackBar);
        GameObject obj2 = CreateAttackBar(-90, new Vector3(-4, 8, 0), 20, whiteAttackBar);
        yield return new WaitForSeconds(0.5f);//4秒待つ
        CreateHoly(new Vector3(3, 0, 0), 2);
        CreateHoly(new Vector3(1, 0, 0), 2);
        CreateHoly(new Vector3(-1, 0, 0), 2);
        CreateHoly(new Vector3(-3, 0, 0), 2);

        yield return new WaitForSeconds(1.5f);//待つ

        Destroy(obj1);
        Destroy(obj2);

        yield return null;
    }
    IEnumerator Skill8()//Iのスキル
    {
        //棒3つ
        GameObject obj1 = CreateAttackBar(-90, new Vector3(0, 10, 0), 20, whiteAttackBar);
        GameObject obj2 = CreateAttackBar(-90, new Vector3(0.5f, 13, 0), 20, whiteAttackBar);
        GameObject obj3 = CreateAttackBar(-90, new Vector3(-0.5f, 14, 0), 20, whiteAttackBar);

        yield return new WaitForSeconds(3f);//2秒待つ

        Destroy(obj1);
        Destroy(obj2);
        Destroy(obj3);

        yield return null;
    }
    IEnumerator Skill9()//Jのスキル
    {

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



        yield return null;
    }
    IEnumerator Skill10()//Kのスキル
    {

        //まずは斜め下に行く棒２つ
        GameObject obj1 = CreateAttackBar(90, new Vector3(-2, 0, 0), 10, poisonAttackBar);
        GameObject obj2 = CreateAttackBar(-90, new Vector3(-2, 0, 0), 10, poisonAttackBar);
        //あと下の方に横一線                                              
        GameObject obj3 = CreateAttackBar(60, new Vector3(0, 2 * 1.732f, 0), 10, poisonAttackBar);
        GameObject obj4 = CreateAttackBar(-60, new Vector3(0, -2 * 1.732f, 0), 10, poisonAttackBar);

        yield return new WaitForSeconds(3f);//4秒待つ

        Destroy(obj1);
        Destroy(obj2);
        Destroy(obj3);
        Destroy(obj4);

        yield return null;
    }
    IEnumerator Skill11()//Lのスキル
    {


        GameObject obj1 = CreateAttackBar(-120, new Vector3(-4 + 5, 5 * 1.732f, 0), 20, whiteAttackBar);
        GameObject obj2 = CreateAttackBar(-120, new Vector3(-3.5f + 5, 5 * 1.732f, 0), 20, whiteAttackBar);

        yield return new WaitForSeconds(0.5f);//4秒待つ


        GameObject obj3 = CreateAttackBar(-30, new Vector3(-4 - 5 * 1.732f, -2 + 5, 0), 20, whiteAttackBar);
        GameObject obj4 = CreateAttackBar(-30, new Vector3(-4 - 5 * 1.732f, -1.5f + 5, 0), 20, whiteAttackBar);

        yield return new WaitForSeconds(2f);//4秒待つ

        Destroy(obj1);
        Destroy(obj2);
        Destroy(obj3);
        Destroy(obj4);

        yield return null;
    }
    IEnumerator Skill12()//Mのスキル
    {
        //まずは斜め下に行く棒２つ
        GameObject obj1 = CreateAttackBar(240, new Vector3(+2 + 5, 5 * 1.732f, 0), 20, whiteAttackBar);
        GameObject obj2 = CreateAttackBar(-60, new Vector3(-2 - 5, 5 * 1.732f, 0), 20, whiteAttackBar);
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


        yield return null;
    }
    IEnumerator Skill13()//Nのスキル
    {
        //まずは斜め下に行く棒２つ
        GameObject obj1 = CreateAttackBar(90, new Vector3(3.5f, 0, 0), 0, blackAttackBar);
        GameObject obj2 = CreateAttackBar(-90, new Vector3(-3.5f, 0, 0), 0, blackAttackBar);
        yield return new WaitForSeconds(0.3f);//待つ
        GameObject obj3 = CreateAttackBar(0 - 45, new Vector3(-0, 0, 0), 0, whiteAttackBar);
        yield return new WaitForSeconds(0.2f);//待つ
        Destroy(obj1);
        Destroy(obj2);
        yield return new WaitForSeconds(0.2f);//待つ
        Destroy(obj3);

        yield return new WaitForSeconds(0.5f);//待つ

        yield return null;
    }
    IEnumerator Skill14()//Oのスキル
    {

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
        CreateAqua(new Vector3(-3.5f, -3.5f, 0), 2);
        yield return new WaitForSeconds(0.1f);//0.1秒待つ
        CreateMistic(new Vector3(0, -5, 0), 2);
        yield return new WaitForSeconds(0.1f);//0.1秒待つ
        CreateAqua(new Vector3(3.5f, -3.5f, 0), 2);
        yield return new WaitForSeconds(0.1f);//0.1秒待つ



        yield return new WaitForSeconds(1f);//4秒待つ



        yield return null;
    }
    IEnumerator Skill15()//Pのスキル
    {
        //まずは斜め下に行く棒２つ
        GameObject obj1 = CreateAttackBar(-90, new Vector3(-1, 10, 0), 20, whiteAttackBar);
        yield return new WaitForSeconds(0.3f);//待つ
        CreateHoly(new Vector3(2, 3, 0), 5);
        yield return new WaitForSeconds(0.3f);//待つ
        CreateHoly(new Vector3(1, 2, 0), 5);
        CreateHoly(new Vector3(1, 4, 0), 5);

        yield return new WaitForSeconds(1f);//4秒待つ

        Destroy(obj1);


        yield return null;
    }
    IEnumerator Skill16()//Qのスキル
    {

        CreateFire(new Vector3(2.5f, 0, 0), 2);
        CreateAqua(new Vector3(-2.5f, 0, 0), 2);
        CreateWind(new Vector3(0, 2.5f, 0), 2);
        CreateBomb(new Vector3(0, -2.5f, 0), 2);
        GameObject obj1 = CreateAttackBar(-45, new Vector3(3, -3, 0), 10, poisonAttackBar);
        obj1.transform.localScale = new Vector3(10, 8, 10);
        yield return new WaitForSeconds(2f);//4秒待つ
        Destroy(obj1);



        yield return null;
    }
    IEnumerator Skill17()//Rのスキル
    {

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



        yield return null;
    }
    IEnumerator Skill18()//Sのスキル
    {

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


        yield return null;
    }
    IEnumerator Skill19()//Tのスキル
    {

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

        yield return null;
    }
    IEnumerator Skill20()//Uのスキル
    {
        //まずは斜め下に行く棒3つ
        GameObject obj1 = CreateAttackBar(-60, new Vector3(-5 - 5, 5 * 1.732f, 0), 20, whiteAttackBar);
        GameObject obj2 = CreateAttackBar(-60, new Vector3(-4.5f - 5, 5 * 1.732f, 0), 20, whiteAttackBar);
        GameObject obj3 = CreateAttackBar(-60, new Vector3(-4 - 5, 5 * 1.732f, 0), 20, whiteAttackBar);
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

        yield return null;
    }
    IEnumerator Skill21()//Vのスキル
    {

        CreateBomb(new Vector3(0, -4, 0), 4);
        yield return new WaitForSeconds(0.3f);//待つ


        CreateFire(new Vector3(2, -1, 0), 2);
        CreateFire(new Vector3(-2, -1, 0), 2);
        yield return new WaitForSeconds(0.14f);//待つ
        CreateFire(new Vector3(4, 2, 0), 2);
        CreateFire(new Vector3(-4, 2, 0), 2);
        yield return new WaitForSeconds(1f);//4秒待つ


        yield return null;
    }
    IEnumerator Skill22()//Wのスキル
    {

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


        yield return null;
    }
    IEnumerator Skill23()//Xのスキル
    {

        //斜め下に行く棒２つ
        GameObject obj1 = CreateAttackBar(240, new Vector3(10, 17.32f, 0), 20, whiteAttackBar);
        GameObject obj2 = CreateAttackBar(-60, new Vector3(-10, 17.32f, 0), 20, whiteAttackBar);
        obj1.transform.localScale = new Vector3(10, 10, 10);
        obj2.transform.localScale = new Vector3(10, 10, 10);

        yield return new WaitForSeconds(3f);//3秒待つ

        Destroy(obj1);
        Destroy(obj2);

        yield return null;
    }
    IEnumerator Skill24()//Yのスキル
    {
        //斜め下に行く棒２つ
        GameObject obj1 = CreateAttackBar(30, new Vector3(0, 0, 0), 5, poisonAttackBar);
        GameObject obj2 = CreateAttackBar(150, new Vector3(0, 0, 0), 5, poisonAttackBar);
        GameObject obj3 = CreateAttackBar(270, new Vector3(0, 0, 0), 5, poisonAttackBar);
        obj1.transform.localScale = new Vector3(10, 10, 10);
        obj2.transform.localScale = new Vector3(10, 10, 10);
        obj3.transform.localScale = new Vector3(10, 10, 10);
        yield return new WaitForSeconds(4f);//5秒待つ

        Destroy(obj1);
        Destroy(obj2);
        Destroy(obj3);

        yield return null;
    }
    IEnumerator Skill25()//Zのスキル
    {
        //まずは横に平行な棒２つ
        GameObject obj1 = CreateAttackBar(0, new Vector3(15, 4, 0), -20, whiteAttackBar);
        GameObject obj1_2 = CreateAttackBar(0, new Vector3(-15, 3, 0), 20, blackAttackBar);

        GameObject obj2 = CreateAttackBar(180, new Vector3(-15, -4, 0), -20, whiteAttackBar);
        GameObject obj2_2 = CreateAttackBar(180, new Vector3(15, -3, 0), 20, blackAttackBar);

        //あと下の方に横一線
        GameObject obj3 = CreateAttackBar(-135, new Vector3(11, 10, 0), 20, whiteAttackBar);
        GameObject obj3_2 = CreateAttackBar(-135, new Vector3(-11, -10, 0), -20, blackAttackBar);

        yield return new WaitForSeconds(3f);//3秒待つ

        Destroy(obj1);
        Destroy(obj2);
        Destroy(obj3);
        Destroy(obj1_2);
        Destroy(obj2_2);
        Destroy(obj3_2);

        yield return null;
    }


    IEnumerator PlaySkillSEAndWait(AudioClip skillNameSound)
    {
        yield return new WaitForSeconds(0.5f);

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
    GameObject CreateAttackBar(float angle, Vector3 initialPos, float speed, GameObject barObj)
    {
        GameObject obj = Instantiate(barObj);
        Transform objTF = obj.transform;
        objTF.position = initialPos;
        objTF.localScale = new Vector3(10, 4, 1);
        objTF.eulerAngles = new Vector3(0, 0, angle);
        obj.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * speed;

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
        return obj;
    }
    GameObject CreateAqua(Vector3 initialPos, float size)
    {
        GameObject obj = Instantiate(aquaEffect);
        Transform objTF = obj.transform;
        objTF.position = initialPos;
        objTF.localScale = new Vector3(size, size, size);
        return obj;
    }
    GameObject CreateWind(Vector3 initialPos, float size)
    {
        GameObject obj = Instantiate(windEffect);
        Transform objTF = obj.transform;
        objTF.position = initialPos;
        objTF.localScale = new Vector3(size, size, size);
        return obj;
    }
    GameObject CreateHoly(Vector3 initialPos, float size)
    {
        GameObject obj = Instantiate(holyEffect);
        Transform objTF = obj.transform;
        objTF.position = initialPos;
        objTF.localScale = new Vector3(size, size, size);
        return obj;
    }
    GameObject CreateDark(Vector3 initialPos, float size)
    {
        GameObject obj = Instantiate(darkEffect);
        Transform objTF = obj.transform;
        objTF.position = initialPos;
        objTF.localScale = new Vector3(size, size, size);
        return obj;
    }

    GameObject CreateMistic(Vector3 initialPos, float size)
    {
        GameObject obj = Instantiate(misticEffect);
        Transform objTF = obj.transform;
        objTF.position = initialPos;
        objTF.localScale = new Vector3(size, size, size);
        return obj;
    }

    GameObject CreateBomb(Vector3 initialPos, float size)
    {
        GameObject obj = Instantiate(orangeBomb);
        Transform objTF = obj.transform;
        objTF.position = initialPos;
        objTF.localScale = new Vector3(size, size, size);
        return obj;
    }

}
