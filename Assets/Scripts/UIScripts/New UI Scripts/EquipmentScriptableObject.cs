using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Manapotion.Equipables;

[CreateAssetMenu]
public class EquipmentScriptableObject : ScriptableObject
{
    private EquipableData weapon;
    private EquipableData armour;
    private EquipableData vanity;
}