using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterData_SO characterData;

    #region Read data from SO
    public int maxHp
    {
        get { if (characterData != null) return characterData.maxHp; else return 0; }
        set { characterData.maxHp = value; }

    }
    public int currentHp
    {
        get { if (characterData != null) return characterData.currentHp; else return 0; }
        set { characterData.currentHp = value; }

    }
    public int maxMp
    {
        get { if (characterData != null) return characterData.maxMp; else return 0; }
        set { characterData.maxMp = value; }

    }
    public int currentMp
    {
        get { if (characterData != null) return characterData.currentMp; else return 0; }
        set { characterData.currentMp = value; }

    }
    public int speed
    {
        get { if (characterData != null) return characterData.speed; else return 0; }
        set { characterData.speed = value; }

    }
    public Inventory_SO inventory1
    {
        get { if (characterData != null) return characterData.inventory1; else return null; }
        set { characterData.inventory1 = value; }
    }

    #endregion

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
