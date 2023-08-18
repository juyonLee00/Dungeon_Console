using System;
using System.Collections.Generic;

namespace DungeonConsole
{
    class MainClass
    {
        private static Character player;
        private static List<Item> itemList = new List<Item>();

        public static void Main(string[] args)
        {
            GameDataSetting();
            DisplayGameIntro();
        }

        static void GameDataSetting()
        {
            //player info setting
            player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);

            //item info setting
            itemList.Add(new Item("무쇠갑옷", false, 'd', 5, "무쇠로 만들어져 튼튼한 갑옷입니다."));
            itemList.Add(new Item("낡은 검", false, 'a', 2, "쉽게 볼 수 있는 낡은 검입니다."));
            
        }

        static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;
            }

            static void DisplayMyInfo()
            {
                Console.Clear();

                Console.WriteLine("상태보기");
                Console.WriteLine("캐릭터의 정보를 표시합니다.");
                Console.WriteLine();
                Console.WriteLine($"Lv.{player.Level}");
                Console.WriteLine($"{player.Name}({player.Job})");
                Console.WriteLine($"공격력 :{player.Atk}");
                Console.WriteLine($"방어력 : {player.Def}");
                Console.WriteLine($"체력 : {player.Hp}");
                Console.WriteLine($"Gold : {player.Gold} G");
                Console.WriteLine();
                Console.WriteLine("0. 나가기");

                int input = CheckValidInput(0, 0);
                switch (input)
                {
                    case 0:
                        DisplayGameIntro();
                        break;
                }
            }

            static void DisplayInventory()
            {

            }

            static int CheckValidInput(int min, int max)
            {
                while (true)
                {
                    string input = Console.ReadLine();

                    bool parseSuccess = int.TryParse(input, out var ret);

                    if (parseSuccess)
                    {
                        if (ret >= min && ret <= max)
                            return ret;
                    }

                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }

        public class Item
        {
            public string Name { get; }
            public bool IsEquip { get; } //기본 false로 설정 .
            public char Type { get; }
            public int TypeEffect { get; }
            public string Content { get; }

            public Item(string name, bool isEquip, char type, int typeEffect, string content)
            {
                Name = name;
                IsEquip = isEquip;
                Type = Type;
                TypeEffect = TypeEffect;
                Content = content;
            }

            //해당 isEquip이 null일 경우 장비 장착 및 효과 반영 .
            public void EquipItem(Character player, int typeEffect, char type)
            {
                switch(type)
                {
                    case 'a':
                        player.Atk += typeEffect;
                        break;
                    case 'd':
                        player.Def += typeEffect;
                        break;
                }
            }

        }


        /*
        public class ItemList
        {
            private List<Item> items;
        }
        */

        public class Character
        {
            public string Name { get; }
            public string Job { get; }
            public int Level { get; }
            public int Atk { get; }
            public int Def { get; }
            public int Hp { get; }
            public int Gold { get; }

            public Character(string name, string job, int level, int atk, int def, int hp, int gold)
            {
                Name = name;
                Job = job;
                Level = level;
                Atk = atk;
                Def = def;
                Hp = hp;
                Gold = gold;
            }
        }
    }
}