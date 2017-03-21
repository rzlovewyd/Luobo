using UnityEngine;
using System.Collections;
using System;

public class Role : ReusableObject 
{
    #region 常量

    #endregion

    #region 事件
    public event Action<int, int> OnHpChanged;    //血量变化
    public event Action<Role> OnDead;             //死亡事件 
    #endregion

    #region 字段
    int m_Hp;
    int m_MaxHp;
    #endregion

    #region 属性
    public int Hp
    {
        get { return m_Hp; }

        set
        {
            //范围检测
            value = Mathf.Clamp(value, 0, MaxHp);

            //减少重复
            if (value == m_Hp)
                return;

            //赋值
            m_Hp = value;

            if(OnHpChanged != null)
            {
                OnHpChanged(m_Hp, MaxHp);
            }

            //死亡事件
            if (m_Hp <= 0 && OnDead != null)
                OnDead(this);
        }
    }

    public int MaxHp
    {
        get { return m_MaxHp; }

        set { m_MaxHp = value; }
    }

    public bool IsDead
    {
        get { return m_Hp == 0; }
    }
    #endregion

    #region 方法
    public virtual void Damage(int hit)
    {
        if (IsDead) return;
            
        Hp -= hit;
    }

    protected virtual void Die(Role role)
    {

    }
    #endregion

    #region Unity回调
    #endregion

    #region 事件回调
    public override void OnSpawn()
    {
        OnDead += Die;
    }

    public override void OnUnSpawn()
    {
        Hp = 0;
        MaxHp = 0;

        while (OnHpChanged != null)
            OnHpChanged -= OnHpChanged;

        while (OnDead != null)
            OnDead -= OnDead;
    }
    #endregion

    #region 帮助方法
    #endregion
}
