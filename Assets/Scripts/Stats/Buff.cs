using System;
using UnityEngine;

namespace Manapotion.Stats
{
    public class Buff : IModifier
    {
        public Stat stat;
        public int value;

        public int AddValue()
        {
            return value;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {   
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                Buff b = (Buff)obj;
                return (b.stat == this.stat) && (b.value == this.value);
            }
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Tuple.Create(stat, value).GetHashCode();
        }
    }
}