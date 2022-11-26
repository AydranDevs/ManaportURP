namespace Manapotion.Rendering
{
    [System.Serializable]
    public class DriverSet
    {
        public string driverName;
        public int set;

        [UnityEngine.Header("On Animation Complete")]
        public string watchDriver;
        public string onComplete_driverName;
        public int onComplete_set;
    }
}