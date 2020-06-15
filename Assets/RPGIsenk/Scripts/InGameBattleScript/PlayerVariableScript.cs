using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class PlayerStatus
{
    public string NamaPlayer;
    public float PlayerHP;
    public float PlayerSpeed;
    public float PlayerAttack;
    public float PlayerDeffend;
    public float PlayerRecovery;
}
public class PlayerVariableScript : MonoBehaviour
{

    public int CharacterId;
    public string CharacterName;
    public int CharacterLvl;
    public float CharacterHP;
    public float CharacterCurrentHP;
    public float CharacterSpeed;
    public float CharacterAttack;
    public float CharacterDeffend;
    public float CharacterRecovery;
    public Sprite CharacterAvatar;
    public Sprite[] SkillSet;
    public PlayerStatus[] arrayPlayerStatus;
    public Animator CharacterAnimator;
    public PlayerTurnManager ptm;
    public bool isPlayable;
    public BattleGameManager BGM;

    // Start is called before the first frame update
    void Start()
    {
        CharacterCurrentHP = CharacterHP;
        isPlayable = true;
        BGM = GameObject.Find("BattleGameManager").GetComponent<BattleGameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CharacterGetHit(float dmg)
    {
        StartCoroutine(IeCharacterGetHit(dmg));

    }
    IEnumerator IeCharacterGetHit(float dmg)
    {
        float dmgTaken = dmg * (100 / (100 + CharacterDeffend));
        CharacterCurrentHP -= dmgTaken;
        CharacterAnimator.SetTrigger("GetHit");
        ptm.PlayerHPSlider.value = CharacterCurrentHP;
        yield return new WaitForSeconds(.2f);
        if (CharacterCurrentHP <= 0)
        {
            isPlayable = false;
            BGM.TotalPlayerDefeated += 1;
            BGM.SummonedCharacter.Remove(gameObject);
            ptm.gameObject.SetActive(false);
            gameObject.SetActive(false);
            BGM.CheckingLoseOrWin();
            BGM.deselectCharacter();
        }
    }
}
