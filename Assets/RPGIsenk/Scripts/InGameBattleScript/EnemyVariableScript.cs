using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVariableScript : MonoBehaviour
{

    public int Enemyid;
    public string EnemyName;
    public int EnemyLvl;
    public float EnemyHP;
    public float EnemyCurrentHP;
    public float EnemySpeed;
    public float EnemyAttack;
    public float EnemyDefend;
    public float EnemyRecovery;
    public Sprite EnemyAvatar;
    public Animator EnemyAnimator;
    public EnemyTurnManager etm;
    public bool isPlayable;
    public BattleGameManager BGM;

    // Start is called before the first frame update
    void Start()
    {
        EnemyCurrentHP = EnemyHP;
        isPlayable = true;
        BGM = GameObject.Find("BattleGameManager").GetComponent<BattleGameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void EnemyGetHit(float dmg)
    {
        StartCoroutine(IeEnemyGetHit(dmg));
    }
    IEnumerator IeEnemyGetHit(float dmg)
    {
        float dmgTaken = dmg * (100 / (100 + EnemyDefend));
        EnemyCurrentHP -= dmgTaken;
        print(dmgTaken);
        EnemyAnimator.SetTrigger("GetHit");
        etm.EnemyHPSlider.value = EnemyCurrentHP;
        yield return new WaitForSeconds(.5f);
        if (EnemyCurrentHP <= 0)
        {
            isPlayable = false;
            BGM.TotalEnemyDefeated += 1;
            BGM.SummonedEnemy.Remove(gameObject);
            etm.gameObject.SetActive(false);
            gameObject.SetActive(false);
            BGM.SummonedEnemy.Remove(gameObject);
            BGM.CheckingLoseOrWin();
        }
    }

    public void EnemyAttacking()
    {
        StartCoroutine(IeEnemyAttack());
    }
    IEnumerator IeEnemyAttack()
    {
        while (BattleGameManager.instanceBattleGameManager.playerisAttacking)
        {
            yield return null;
        }
        etm.enemyTurn = true;
        BattleGameManager.instanceBattleGameManager.enemyisAttacking = true;
        int intTarget = Random.Range(0, BGM.SummonedCharacter.Count);
        // BGM.TargetPlayer = null;
        BGM.TargetPlayer = BGM.SummonedCharacter[intTarget];
        yield return new WaitForSeconds(.1f);
        EnemyAnimator.SetTrigger("EnemyAttack");
        yield return new WaitForSeconds(1.2f);
        BGM.TargetPlayer.GetComponent<PlayerVariableScript>().CharacterGetHit(EnemyAttack);
        etm.enemyTurn = false;
        BattleGameManager.instanceBattleGameManager.enemyisAttacking = false;
    }
}
