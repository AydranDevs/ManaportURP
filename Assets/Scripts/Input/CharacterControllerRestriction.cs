namespace Manapotion.Input
{
    [System.Serializable]
    public struct CharacterControllerRestriction
    {
        public CharacterControllerRestriction(bool idle, bool walk, bool sprint, bool dash, bool primary, bool secondary, bool aux)
        {
            canIdle = idle;
            canWalk = walk;
            canSprint = sprint;
            canDash = dash;
            canUsePrimary = primary;
            canUseSecondary = secondary;
            canUseAux = aux;
        }
        
        public bool canIdle;
        public bool canWalk;
        public bool canSprint;
        public bool canDash;

        public bool canUsePrimary;
        public bool canUseSecondary;
        public bool canUseAux;

        /// <summary>
        /// Allows the character to perform anything without restriction.
        /// </summary>
        /// <value></value>
        public static CharacterControllerRestriction NoRestrictions
        {
            get 
            { 
                return new CharacterControllerRestriction(true, true, true, true, true, true, true);
            }
        }

        /// <summary>
        /// Restricts the character from performing actions.
        /// </summary>
        /// <value></value>
        public static CharacterControllerRestriction RestrictActions
        {
            get
            {
                return new CharacterControllerRestriction(true, true, true, true, false, false, false);
            }
        }

        /// <summary>
        /// Restricts the character from moving.
        /// </summary>
        /// <value></value>
        public static CharacterControllerRestriction RestrictMovement
        {
            get
            {
                return new CharacterControllerRestriction(true, false, false, false, true, true, true);
            }
        }

        public static CharacterControllerRestriction RestrictAll
        {
            get
            {
                return new CharacterControllerRestriction(false, false, false, false, false, false, false);
            }
        }
    }
}