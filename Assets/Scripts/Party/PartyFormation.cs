using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manapotion.PartySystem
{
    public class PartyFormation
    {
        public int sizeX, sizeY;
        public Dictionary<PartyMember, Vector2Int> formationPositions;

        public PartyFormation(int size)
        {
            sizeX = size;
            sizeY = size;

            formationPositions = new Dictionary<PartyMember, Vector2Int>();
        }
    }
}
