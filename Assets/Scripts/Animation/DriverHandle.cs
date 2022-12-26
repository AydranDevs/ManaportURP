using System.Collections.Generic;

namespace Manapotion.Animation
{
    public delegate void DriverHandleListener(string name, int value);

    [System.Serializable]
    public class DriverHandle
    {
        private List<DriverHandleListener> _listeners;
        
        public string driverName;
        public int driverValue;

        public bool conditional;
        public string conditionalDriverName;
        public int conditionalDriverValue;

        public DriverHandle()
        {
            _listeners = new List<DriverHandleListener>();
        }

        public DriverHandle(string name, int value)
        {
            _listeners = new List<DriverHandleListener>();

            this.driverName = name;
            this.driverValue = value;
        }

        public void AddListener(DriverHandleListener listener)
        {
            if (_listeners == null)
            {
                _listeners = new List<DriverHandleListener>();
            }
        }
    }
}