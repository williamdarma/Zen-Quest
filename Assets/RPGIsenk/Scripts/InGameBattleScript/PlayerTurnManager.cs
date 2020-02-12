using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerTurnManager : MonoBehaviour
{
    public PlayerVariableScript pvs;
    public Slider PlayerTurnSlider;
    public Slider PlayerHPSlider;
    public Button PlayerButton;
    public Image PlayerPicture;
    public GameObject PlayerReadyIndicator;
    public float speed;
    public GameObject PlayerControlPosition;
    public GameObject PlayerControl;
    public bool PlayerTurn;
    public int CharacterNumber;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerButton.gameObject.SetActive(false);
        PlayerButton.enabled = false;
        StartCoroutine(IePlayerSliderCountDown());
    }

    public void ResetTurnSlider()
    {
        PlayerTurnSlider.value = 1;
        PlayerTurn = false;
        PlayerReadyIndicator.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        /* if (PlayerTurnSlider.value > 0 && !PlayerTurn && pvs.isPlayable)
         {
             print("lala");
             PlayerTurnSlider.value -= Time.deltaTime * speed * .1f;
             PlayerButton.enabled = false;
         }
         else
         {
             // PlayerButton.gameObject.SetActive(true);
             PlayerButton.enabled = true;
             PlayerReadyIndicator.SetActive(true);
         }*/
    }
    void PlayerSliderCountDown()
    {
        StartCoroutine(IePlayerSliderCountDown());
    }
    IEnumerator IePlayerSliderCountDown()
    {
        while (true)
        {
            if (pvs != null)
            {
                if (PlayerTurnSlider.value > 0 && !PlayerTurn && pvs.isPlayable)
                {
                    PlayerTurnSlider.value -= Time.deltaTime * speed * 10f * (pvs.CharacterSpeed / 100);
                    PlayerButton.enabled = false;
                }
                else
                {
                    PlayerButton.enabled = true;
                    PlayerReadyIndicator.SetActive(true);
                }
            }
            yield return new WaitForSeconds(.1f);

        }
    }
    public void PlayerAttack(int NoAttack)
    {
        if (NoAttack == 0)
        {
            print("Player Normal Attack");
        }
        else if (NoAttack == 1)
        {
            print("Player Skill1");
        }
        else if (NoAttack == 2)
        {
            print("Player Skill2");
        }
        else if (NoAttack == 3)
        {
            print("Player Skill3");
        }
    }

}
