using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState { IntroGame, InBattleGame, EndGame }
public class BattleGameManager : MonoBehaviour
{
    public static BattleGameManager instanceBattleGameManager;

    [Header("Player Area")]

    public GameObject[] ArrayUICharacter;
    public GameObject[] ArrayPlayerGameObjectPlace;
    public GameObject[] ArrayAllPlayerPrefabs;
    public int[] SelectedCharactertoUsed;
    public GameObject SelectedCharacter;
    public GameObject SelectedUICharacter;
    public List<GameObject> SummonedCharacter = new List<GameObject>();
    public GameObject TargetedEnemy;
    public bool playerisAttacking;
    public Slider UltimateAttackSlider;
    int multiplierUlti;

    [Header("Enemy Area")]

    public int TotalEnemy;
    public GameObject[] ArrayUIEnemy;
    public GameObject[] ArrayEnemyGameObjectPlace;
    public GameObject[] ArrayAllEnemyPrefabs;
    public int[] SelectedEnemytoUsed;
    public List<GameObject> SummonedEnemy = new List<GameObject>();
    public GameObject TargetPlayer;
    public bool enemyisAttacking;

    [Header("Win Lose Area")]

    public int TotalEnemyToDefeat;
    public int TotalPlayerToDefeat;
    public int TotalEnemyDefeated;
    public int TotalPlayerDefeated;

    [Header("Test Area")]
    public GameObject[] Bebas;
    public GameObject apakek;
    public List<GameObject> listbebas = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if (!instanceBattleGameManager)
        {
            instanceBattleGameManager = this;
        }
        // closeAllPlayerControl();
        //CallCharacter();
        StartCoroutine(IeStartGame());
        // foreach (Transform child in apakek.transform)
        // {
        //     listbebas.Add(child.gameObject);
        // }
        TotalEnemyToDefeat = SummonedEnemy.Count;
        TotalPlayerToDefeat = SummonedCharacter.Count;
        UltimateAttackSlider.maxValue = 100;
        UltimateAttackSlider.value = 0;
        multiplierUlti = 4 - SummonedCharacter.Count;
    }
    IEnumerator IeStartGame()//Start the InGameBattle
    {
        CallCharacter();
        CallEnemy();
        yield return new WaitForSeconds(.2f);
        ChangeCharacterAvatarr();
        ChangeEnemyAvatar();
    }
    public void CheckingLoseOrWin()
    {
        if (TotalEnemyDefeated == TotalEnemyToDefeat)
        {
            //Player Win
            print("PLAYER WIN");
        }
        else if (TotalPlayerDefeated == TotalPlayerToDefeat)
        {
            //EnemyWin
            print("PLAYER LOSE");
        }
    }
    public void closeAllPlayerControl()
    {
        int limit = ArrayUICharacter.Length;
        for (int i = 0; i < limit; i++)
        {
            ArrayUICharacter[i].GetComponent<PlayerTurnManager>().PlayerControl.SetActive(false);
        }
        deselectCharacter();
    }
    public void CallCharacter()
    {
        for (int j = 0; j < SelectedCharactertoUsed.Length; j++)
        {
            for (int k = 0; k < ArrayAllPlayerPrefabs.Length; k++)
            {
                if (SelectedCharactertoUsed[j] == ArrayAllPlayerPrefabs[k].GetComponent<PlayerVariableScript>().CharacterId)
                {
                    ArrayAllPlayerPrefabs[k].transform.position = ArrayPlayerGameObjectPlace[j].transform.position;
                    ArrayAllPlayerPrefabs[k].transform.SetParent(ArrayPlayerGameObjectPlace[j].transform);
                    SummonedCharacter.Add(ArrayAllPlayerPrefabs[k]);
                }
            }
        }
    }
    public void CallEnemy()
    {
        for (int i = 0; i < SelectedEnemytoUsed.Length; i++)
        {
            for (int j = 0; j < ArrayAllEnemyPrefabs.Length; j++)
            {
                if (SelectedEnemytoUsed[i] == ArrayAllEnemyPrefabs[j].GetComponent<EnemyVariableScript>().Enemyid)
                {
                    GameObject tempSummonEnemy = Instantiate(ArrayAllEnemyPrefabs[j]);
                    tempSummonEnemy.transform.position = ArrayEnemyGameObjectPlace[i].transform.position;
                    tempSummonEnemy.transform.SetParent(ArrayEnemyGameObjectPlace[i].transform);
                    SummonedEnemy.Add(tempSummonEnemy);
                }
            }
        }
    }
    public void ChangeCharacterAvatarr()
    {
        for (int i = 0; i < SummonedCharacter.Count; i++)
        {
            PlayerVariableScript tempPVS = SummonedCharacter[i].GetComponent<PlayerVariableScript>();
            ArrayUICharacter[i].transform.GetChild(3).GetComponent<Image>().sprite = tempPVS.CharacterAvatar;
            ArrayUICharacter[i].transform.GetChild(5).GetChild(0).GetComponent<TextMeshProUGUI>().text = tempPVS.CharacterName;
            ArrayUICharacter[i].transform.GetChild(6).GetComponent<Slider>().maxValue = tempPVS.CharacterHP;
            ArrayUICharacter[i].transform.GetChild(6).GetComponent<Slider>().value = tempPVS.CharacterHP;
            ArrayUICharacter[i].GetComponent<PlayerTurnManager>().pvs = tempPVS;
            tempPVS.ptm = ArrayUICharacter[i].GetComponent<PlayerTurnManager>();
            ArrayUICharacter[i].SetActive(true);
        }

    }
    public void ChangeEnemyAvatar()
    {
        for (int i = 0; i < SummonedEnemy.Count; i++)
        {
            EnemyVariableScript tempEVS = SummonedEnemy[i].GetComponent<EnemyVariableScript>();
            // ArrayUIEnemy[i].transform.GetChild(3).GetComponent<Image>().sprite = tempEVS.EnemyAvatar;
            ArrayUIEnemy[i].transform.GetChild(6).GetComponent<Slider>().maxValue = tempEVS.EnemyHP;
            ArrayUIEnemy[i].transform.GetChild(6).GetComponent<Slider>().value = tempEVS.EnemyCurrentHP;
            ArrayUIEnemy[i].GetComponent<EnemyTurnManager>().evs = tempEVS;
            tempEVS.etm = ArrayUIEnemy[i].GetComponent<EnemyTurnManager>();
            ArrayUIEnemy[i].SetActive(true);
        }
    }
    public void CharacterGetHit(float dmg)
    {
        if (SelectedCharacter != null)
        {
            PlayerVariableScript tempPVS = SelectedCharacter.GetComponent<PlayerVariableScript>();
            tempPVS.CharacterGetHit(dmg);
            tempPVS.CharacterAnimator.SetTrigger("GetHit");
            if (tempPVS.CharacterCurrentHP <= 0)
            {
                tempPVS.isPlayable = false;
            }
        }

    }
    public void CharacterAttack(int AttackType)
    {
        if (SelectedCharacter != null)
        {
            StartCoroutine(IeCharacterAttacking(AttackType));
        }
    }
    void ChangeUltimateGaugeValue(float ultiGauge)
    {
        if (UltimateAttackSlider.value == UltimateAttackSlider.maxValue)
        {
            return;
        }
        UltimateAttackSlider.value += ultiGauge * multiplierUlti;


    }
    IEnumerator IeCharacterAttacking(int atktype)
    {
        closeAllPlayerControl();
        while (enemyisAttacking)
        {
            yield return null;
        }
        if (atktype == 0)
        {
            SelectedCharacter.GetComponent<PlayerVariableScript>().CharacterAnimator.SetTrigger("Attack1");
           ChangeUltimateGaugeValue(6f);
        }
        else if (atktype == 1)
        {
            SelectedCharacter.GetComponent<PlayerVariableScript>().CharacterAnimator.SetTrigger("Attack2");
              ChangeUltimateGaugeValue(4f);
        }
        else if (atktype == 2)
        {
            SelectedCharacter.GetComponent<PlayerVariableScript>().CharacterAnimator.SetTrigger("Attack3");
               ChangeUltimateGaugeValue(2f);
        }
        else
        {
            SelectedCharacter.GetComponent<PlayerVariableScript>().CharacterAnimator.SetTrigger("SpecialAttack");
             ChangeUltimateGaugeValue(0f);
        }

        print("player Attacing");
        if (TargetedEnemy == null)
        {
            TargetedEnemy = SummonedEnemy[0];
        }
        // yield return new WaitForSeconds(.1f);
        yield return new WaitForSeconds(1.2f);
        TargetedEnemy.GetComponent<EnemyVariableScript>().EnemyGetHit(SelectedCharacter.GetComponent<PlayerVariableScript>().CharacterAttack);
        TargetedEnemy = null;
        SelectedCharacter = null;
        SelectedUICharacter.GetComponent<PlayerTurnManager>().ResetTurnSlider();
    }
    void deselectCharacter()
    {
        for (int i = 0; i < ArrayPlayerGameObjectPlace.Length; i++)
        {
            ArrayPlayerGameObjectPlace[i].transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    void deselectEnemy()
    {
        for (int i = 0; i < ArrayEnemyGameObjectPlace.Length; i++)
        {
            ArrayEnemyGameObjectPlace[i].transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void PlayerCanAttack(PlayerTurnManager PTM)
    {
        closeAllPlayerControl();
        PTM.PlayerTurn = true;
        PTM.PlayerControl.transform.position = PTM.PlayerControlPosition.transform.position;
        PTM.PlayerControl.SetActive(true);
        SelectedCharacter = null;
        SelectedCharacter = ArrayPlayerGameObjectPlace[PTM.CharacterNumber].transform.GetChild(1).gameObject;
        SelectedUICharacter = null;
        SelectedUICharacter = PTM.gameObject;
        deselectCharacter();
        ArrayPlayerGameObjectPlace[PTM.CharacterNumber].transform.GetChild(0).gameObject.SetActive(true);
    }
    public void SelectTargetedEnemy(EnemyTurnManager ETM)
    {
        TargetedEnemy = null;
        TargetedEnemy = ArrayEnemyGameObjectPlace[ETM.EnemyNumber].transform.GetChild(1).gameObject;
        deselectEnemy();
        ArrayEnemyGameObjectPlace[ETM.EnemyNumber].transform.GetChild(0).gameObject.SetActive(true);
    }
}
