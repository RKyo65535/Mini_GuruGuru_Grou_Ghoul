using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{

    /// <summary>
    /// 状態一覧
    /// </summary>
    enum State
    {
        NORMAL,
        MUTEKI
    }
    State state;
    [SerializeField] int maxMutekiCount;
    int mutekiCount;

    const string playerSkillTagName = "PlayerSkill";

    [SerializeField] GameObject deathEffect;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] AudioSource damageAudio;
    [SerializeField] AudioSource deathAudio;
    [SerializeField] ScoreManager scoreManager;
    Transform TF;
    SpriteRenderer spriteRenderer;
    
    [SerializeField]int life;//自身のHP





    private void Awake()
    {
        TF = transform;//キャッシュ
        spriteRenderer = GetComponent<SpriteRenderer>();

        //ライフを1～3の間にする。
        if(life < 1)
        {
            life = 1;
        }
        else if(life > 4)
        {
            life = 4;
        }
        ChageShapeAndColor(life);
    }


    private void FixedUpdate()
    {
        if(state == State.MUTEKI)
        {
            mutekiCount += 1;
            if(mutekiCount >= maxMutekiCount)
            {
                state = State.NORMAL;
                mutekiCount = 0;
            }
        }
    }


    /// <summary>
    /// 自身にダメージを与える。
    /// </summary>
    public void Damage()
    {
        if(state == State.NORMAL)
        {
            life -= 1;//HPを1減らす
            state = State.MUTEKI;//一端無敵にする。
            ChageShapeAndColor(life);//自身の見た目を変える
            scoreManager.HitPlayersAttack();//攻撃が当った旨を伝える

            if (life <= 0)//死んだ場合
            {
                MakeDeathEffect();
                deathAudio.Play();
                scoreManager.AddBasicScore(1000);
                Destroy(gameObject);
            }
            else//生きている場合
            {
                damageAudio.pitch = 1 + Random.value * 0.1f;
                damageAudio.Play();
                MakeHitEffect();//当った時に出すエフェクト
            }
        }

    }

    /// <summary>
    /// 死んだときに出すエフェクト
    /// </summary>
    void MakeDeathEffect()
    {
        GameObject obj = Instantiate(deathEffect);
        obj.transform.position = TF.position;
    }

    /// <summary>
    /// 攻撃が当った時に出すエフェクト
    /// </summary>
    void MakeHitEffect()
    {
        hitEffect.Play();
    }

    /// <summary>
    /// ライフに応じた形に変化する。
    /// </summary>
    /// <param name="currentLife"></param>
    void ChageShapeAndColor(int currentLife)
    {
        if (currentLife == 4)
        {
            TF.localScale = Vector3.one * 0.1f * Mathf.Pow(2, 1.5f);
            spriteRenderer.color = new Color(0.33f, 1, 0.66f);
        }
        else if (currentLife == 3)
        {
            TF.localScale = Vector3.one * 0.1f * 2;
            spriteRenderer.color = new Color(0.66f, 1, 0.66f);
        }
        else if(currentLife == 2)
        {
            TF.localScale = Vector3.one * 0.1f * Mathf.Pow(2,0.5f) ;
            spriteRenderer.color = new Color(1, 1, 0.66f);
        }
        else
        {
            TF.localScale = Vector3.one * 0.1f;
            spriteRenderer.color = new Color(1, 0.66f, 0.66f);
        }
    }





    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == playerSkillTagName)
        {
            Damage();
        }
    }


}
