using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public const int MAXHP = 100;
    public int currentHP;
    public Image HPbar;

    private PlayerController player;

    private Coroutine hp_co;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();

        currentHP = MAXHP;
        StartDecreaseHP();
    }

    public void StartDecreaseHP()
    {
        if(hp_co == null)
        {
            hp_co = StartCoroutine(DecreaseHP_co());
        }
    }
    public void StopDecreaseHP()
    {
        if(hp_co != null)
        {
            StopCoroutine(hp_co);
            hp_co = null;
        }
    }

    public IEnumerator DecreaseHP_co()
    {
        while(currentHP > 0)
        {
            yield return new WaitForSeconds(1f);

            // 체력이 1씩 줄어듭니다...
            Damage(1);
        }
    }

    private void Damage(int amount)
    {
        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0, MAXHP);
        UpdateHP();

        if(currentHP <= 0)
        {
            player.HPZero();
            Debug.Log("게임오버");
        }
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        
        if(currentHP >= MAXHP)
        {
            currentHP = MAXHP;
        }

        UpdateHP();
    }

    private void UpdateHP()
    {
        if(HPbar != null)
        {
            HPbar.fillAmount = (float)currentHP / MAXHP;
        }
    }
}
