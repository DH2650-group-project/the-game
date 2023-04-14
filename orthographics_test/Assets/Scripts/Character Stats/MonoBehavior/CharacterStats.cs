using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHp = 100;
    public int currentHp = 100;
    public int maxMp = 100;
    public int currentMp = 100;

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            currentHp = 0;
            // TODO: Send message to gameobject that it should die
        }
    }

    public void Heal(int healAmount)
    {
        currentHp += healAmount;
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
    }

    public void UseMp(int useAmount)
    {
        currentMp -= useAmount;
        if (currentMp < 0)
        {
            currentMp = 0;
        }
    }

    public void RestoreMp(int restoreAmount)
    {
        currentMp += restoreAmount;
        if (currentMp > maxMp)
        {
            currentMp = maxMp;
        }
    }


}
