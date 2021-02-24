using AndrewBox.Comp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndrewBox.Pool
{
    public interface Pool_Unit
    {
        Pool_UnitState state();
        void setParentList(object parentList);
        void restore();
    }
    public enum Pool_Type
    {
        Idle,
        Work
    }
    public class Pool_UnitState
    {
        public Pool_Type InPool
        {
            get;
            set;
        }
    }
}
