using UnityEngine;
using Manapotion.PartySystem;

public class EquipableTest : MonoBehaviour
{
    void Start()
    {
        Party.GetMember(0).Equip(0, new Manapotion.Equipables.EquipableData());
        Party.GetMember(0).Equip(1, new Manapotion.Equipables.EquipableData());
        Party.GetMember(0).Equip(2, new Manapotion.Equipables.EquipableData());
    }
}
