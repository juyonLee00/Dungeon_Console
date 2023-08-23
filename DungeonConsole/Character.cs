using System;

namespace DungeonConsole
{
    [Serializable]

    public class Character
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; set; }
        public float Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }
        public int Gold { get; set; }
        public bool IsEquipDefItem { get; set; }
        public bool IsEquipAtkItem { get; set; }

        public Character(string name, string job, int level, float atk, int def, int hp, int gold, bool isEquipDefItem, bool isEquipAtkItem)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
            IsEquipDefItem = isEquipDefItem;
            IsEquipAtkItem = isEquipAtkItem;
        }
    }
}