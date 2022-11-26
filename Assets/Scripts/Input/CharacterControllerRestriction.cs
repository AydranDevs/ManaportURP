namespace Manapotion.Input
{
    [System.Serializable]
    public struct CharacterControllerRestriction
    {
        public bool canIdle;
        public bool canWalk;
        public bool canSprint;
        public bool canDash;

        public bool canUsePrimary;
        public bool canUseSecondary;
        public bool canUseAux;
    }
}