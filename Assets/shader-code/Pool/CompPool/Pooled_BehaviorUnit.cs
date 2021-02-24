using AndrewBox.Comp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndrewBox.Pool
{
    public abstract class Pooled_BehaviorUnit : BaseBehavior, Pool_Unit
    {
        //单元状态对象
        protected Pool_UnitState m_unitState = new Pool_UnitState();
        //父列表对象
        Pool_UnitList<Pooled_BehaviorUnit> m_parentList;
        /// <summary>
        /// 返回一个单元状态，用于控制当前单元的闲置、工作状态
        /// </summary>
        /// <returns>单元状态</returns>
        public virtual Pool_UnitState state()
        {
            return m_unitState;
        }
        /// <summary>
        /// 接受父列表对象的设置
        /// </summary>
        /// <param name="parentList">父列表对象</param>
        public virtual void setParentList(object parentList)
        {
            m_parentList = parentList as Pool_UnitList<Pooled_BehaviorUnit>;
        }
        /// <summary>
        /// 归还自己，即将自己回收以便再利用
        /// </summary>
        public virtual void restore()
        {
            if (m_parentList != null)
            {
                m_parentList.restoreUnit(this);
            }
        }

    }
}
