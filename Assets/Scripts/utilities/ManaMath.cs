namespace Manapotion.Utilities
{
    /// <summary>
    /// This class holds useful math functions. 
    /// </summary>
    public static class ManaMath
    {
        /// <summary>
        /// Calculate a base movement speed value based on the DEX stat.
        /// </summary>
        /// <param name="dex">DEX modified value.</param>
        /// <returns></returns>
        public static float DexCalc_MoveSp(int dex)
        {
            var slope = 0.008f * dex;
            var yInt = 2f;
            return slope + yInt;
        }

        /// <summary>
        /// Calculate a sprint modifier based on the DEX stat.
        /// </summary>
        /// <param name="dex">DEX modified value.</param>
        /// <returns></returns>
        public static float DexCalc_SprMod(int dex)
        {
            var slope = 0.007f * dex;
            var yInt = 1.3f;
            return slope + yInt;
        }

        /// <summary>
        /// Calculate a dash modifier based on the DEX stat.
        /// </summary>
        /// <param name="dex">DEX modified value.</param>
        /// <returns></returns>
        public static float DexCalc_DshMod(int dex)
        {
            var slope = 0.007f * dex;
            var yInt = 1.4f;
            return slope + yInt;
        }
    }
}
