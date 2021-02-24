using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AndrewBox.Pool
{
    public class Pool_UnitList_Comp : Pool_UnitList<Pooled_BehaviorUnit>
    {
        protected Pool_Comp m_pool;
        public void setPool(Pool_Comp pool)
        {
            m_pool = pool;
        }
        protected override Pooled_BehaviorUnit createNewUnit<UT>() 
        {
            GameObject result_go = null;
            if (m_template != null && m_template is GameObject)
            {
                result_go = GameObject.Instantiate((GameObject)m_template);
            }
            else
            {
                result_go = new GameObject();
                result_go.name = typeof(UT).Name;
            }
            result_go.name =result_go.name + "_"+m_createdNum;
            UT comp = result_go.GetComponent<UT>();
            if (comp == null)
            {
                comp = result_go.AddComponent<UT>();
            }
            comp.DoInit();
            return comp;
        }

        protected override void OnUnitChangePool(Pooled_BehaviorUnit unit)
        {
            if (m_pool != null)
            {
                m_pool.OnUnitChangePool(unit);
            }
        }
    }
}
