using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndrewBox.Pool
{
    public abstract class Pool_UnitList<T> where T:class,Pool_Unit
    {
        protected object m_template;
        protected List<T> m_idleList;
        protected List<T> m_workList;
        protected int m_createdNum = 0;
        public Pool_UnitList()
        {
            m_idleList = new List<T>();
            m_workList = new List<T>();
        }



        /// <summary>
        /// 获取一个闲置的单元，如果不存在则创建一个新的
        /// </summary>
        /// <returns>闲置单元</returns>
        public virtual T takeUnit<UT>() where UT:T
        {
            T unit;
            if (m_idleList.Count > 0)
            {
                unit = m_idleList[0];
                m_idleList.RemoveAt(0);
            }
            else
            {
                unit = createNewUnit<UT>();
                unit.setParentList(this);
                m_createdNum++;
            }
            m_workList.Add(unit);
            unit.state().InPool = Pool_Type.Work;
            OnUnitChangePool(unit);
            return unit;
        }
        /// <summary>
        /// 归还某个单元
        /// </summary>
        /// <param name="unit">单元</param>
        public virtual void restoreUnit(T unit)
        {
            if (unit!=null && unit.state().InPool == Pool_Type.Work)
            {
                m_workList.Remove(unit);
                m_idleList.Add(unit);
                unit.state().InPool = Pool_Type.Idle;
                OnUnitChangePool(unit);
            }
        }
        /// <summary>
        /// 设置模板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="template"></param>
        public void setTemplate(object template)
        {
            m_template = template;
        }
        protected abstract void OnUnitChangePool(T unit);
        protected abstract T createNewUnit<UT>() where UT : T;
    }
}
