using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterData_SO characterData;

    #region Read data from SO
    public int maxHp
    {
        get { if (characterData != null) return characterData.maxHp; else return 0;}
        set { characterData.maxHp = value;}

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
    #endregion
}
