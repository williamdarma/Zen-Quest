using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyTurnManager : MonoBehaviour
{
    [SerializeField]


    public EnemyVariableScript evs;
    public Slider EnemyTurnSlider;
    public Slider EnemyHPSlider;
    public Button EnemyButton;
    public Image EnemyPicture;
    public float speed;
    public bool enemyTurn;
    public int EnemyNumber;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IeEnemySliderCountDown());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ResetTurnslider()
    {
        EnemyTurnSlider.value = 1;

    }
    IEnumerator IeEnemySliderCountDown()
    {
        float RandomizeSpeed = Random.Range(0.6f, 1);
        while (true)
        {
            if (evs != null)
            {
                if (EnemyTurnSlider.value > 0 && !enemyTurn && evs.isPlayable)
                {
                    EnemyTurnSlider.value -= Time.deltaTime  * RandomizeSpeed * speed * 10f * (evs.EnemySpeed / 100);
                }
                else if (EnemyTurnSlider.value <= 0)
                {
                    ResetTurnslider();
                    evs.EnemyAttacking();
                }
            }
            yield return new WaitForSeconds(.1f);
        }
    }
}
