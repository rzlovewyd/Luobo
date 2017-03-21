using System.Collections.Generic;

public class StaticData : Singleton<StaticData> 
{
    Dictionary<int, MonsterInfo> m_Monsters = new Dictionary<int, MonsterInfo>();

    protected override void Awake()
    {
        base.Awake();

        InitMonster();
        InitTowers();
        InitBullets();
    }

    void InitMonster()
    {
        m_Monsters.Add(0, new MonsterInfo() { Hp = 1, MoveSpeed = 1.5f });
        m_Monsters.Add(1, new MonsterInfo() { Hp = 2, MoveSpeed = 1f });
        m_Monsters.Add(2, new MonsterInfo() { Hp = 5, MoveSpeed = 1f });
        m_Monsters.Add(3, new MonsterInfo() { Hp = 10, MoveSpeed = 1f });
        m_Monsters.Add(4, new MonsterInfo() { Hp = 10, MoveSpeed = 1f });
        m_Monsters.Add(5, new MonsterInfo() { Hp = 100, MoveSpeed = 0.5f });
    }

    public MonsterInfo GetMonsterInfo(int monsterType)
    {
        return m_Monsters[monsterType];
    }

    void InitTowers()
    {

    }

    void InitBullets()
    {

    }
}
