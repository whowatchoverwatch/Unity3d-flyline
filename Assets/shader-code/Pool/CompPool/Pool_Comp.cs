using AndrewBox.Comp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AndrewBox.Pool
{

    public class Pool_Comp:Pool_Base<Pooled_BehaviorUnit,Pool_UnitList_Comp>
    {
        [SerializeField][Tooltip("运行父节点")]
        protected Transform m_work;
        [SerializeField][Tooltip("闲置父节点")]
        protected Transform m_idle;

        protected override void OnInitFirst()
        {
            if (m_work == null)
            {
                m_work = CompUtil.Create(m_transform, "work");
            }
            if (m_idle == null)
            {
                m_idle = CompUtil.Create(m_transform, "idle");
                m_idle.gameObject.SetActive(false);
            }
        }

        public void OnUnitChangePool(Pooled_BehaviorUnit unit)
        {
            if (unit != null)
            {
                var inPool=unit.state().InPool;
                if (inPool == Pool_Type.Idle)
                {
                    unit.m_transform.SetParent(m_idle);
                }
                else if (inPool == Pool_Type.Work)
                {
                    unit.m_transform.SetParent(m_work);
                }
            }
        }
        protected override Pool_UnitList_Comp createNewUnitList<UT>()
        {
            Pool_UnitList_Comp list = new Pool_UnitList_Comp();
            list.setPool(this);
            return list;
        }


    }
}
