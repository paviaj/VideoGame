using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter {
    private string characterName;
    private int alive;

    public string CharacterName
    {
        get { return characterName; }
        set { characterName = value; }
    }

    public int Alive
    {
        get { return alive; }
        set { alive = value; }
    }
}
