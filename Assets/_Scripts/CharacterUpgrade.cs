using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUpgrade : Upgrade
{
    [SerializeField] private Character character;

    public override void ProcessUpgrade()
    {
        PlayerManager.Instance.AddCharacter(character);
    }
}
