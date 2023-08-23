using System;
namespace DungeonConsole
{
    [Serializable]
    public class Item
    {
        public string Name { get; }
        public bool IsEquip { get; set; }
        public char Type { get; }
        public int TypeEffect { get; }
        public string Content { get; }
        public int Price { get; set; }
        public bool IsSoldOut { get; set; }

        public Item(string name, bool isEquip, char type, int typeEffect, string content, int price, bool isSoldOut)
        {
            Name = name;
            IsEquip = isEquip;
            Type = type;
            TypeEffect = typeEffect;
            Content = content;
            Price = price;
            IsSoldOut = isSoldOut;
        }
    }
}