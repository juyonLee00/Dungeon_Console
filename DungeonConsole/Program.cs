using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonConsole
{
    class MainClass
    {
        private static Character player;
        private static List<Item> itemList = new List<Item>();
        static bool isEquipEvent = false;
        static int firstPlayerAtk;
        static int firstPlayerDef;
        static bool changeAtk = false;
        static bool changeDef = false;

        public static void Main(string[] args)
        {
            GameDataSetting();
            DisplayGameIntro();
        }

        static void GameDataSetting()
        {
            //player info setting
            player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);
            firstPlayerAtk = player.Atk;
            firstPlayerDef = player.Def;

            //item info setting
            itemList.Add(new Item("무쇠갑옷", false, 'd', 5, "무쇠로 만들어져 튼튼한 갑옷입니다."));
            itemList.Add(new Item("낡은 검", false, 'a', 2, "쉽게 볼 수 있는 낡은 검입니다."));
            itemList.Add(new Item("나무 몽둥이", false, 'a', 3, "주위에서 많이 보이는 몽둥이입니다."));

        }

        static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("1. 상태보기");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("2. 인벤토리");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;
                case 2:
                    DisplayInventory();
                    break;
            }
        }

        static void DisplayMyInfo()
        {
            Console.Clear();

            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level}");
            Console.WriteLine($"{player.Name}({player.Job})");
            Console.WriteLine($"공격력 :{player.Atk} {ShowDifferenceAtkResult()}");
            Console.WriteLine($"방어력 : {player.Def} {ShowDifferenceDefResult()}");
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
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            Console.WriteLine("[아이템 목록]");

            itemList = itemList.OrderByDescending(item => item.Name.Length).ToList();

            for (int i = 0; i < itemList.Count; i++)
            {
                string itemName = itemList[i].Name;
                string itemType = "";
                if (itemList[i].IsEquip)
                {
                    itemName = "[E]" + itemName;
                }

                if (itemList[i].Type == 'a')
                {
                    itemType = "공격력";
                }
                else if (itemList[i].Type == 'd')
                {
                    itemType = "방어력";
                }

                Console.WriteLine($"- {itemName}  {itemType}  +{itemList[i].TypeEffect}  {itemList[i].Content}");
            }

            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 장착 관리");


            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    ManageEquipItem();
                    break;
            }
        }

        //아이템 장착 여부에 따른 playerDisplay창 변경
        static string ShowDifferenceAtkResult()
        {
            string result = "";
            int abstractAtk = player.Atk - firstPlayerAtk;
            if (abstractAtk != 0)
            {
                if (abstractAtk > 0)
                    result = " (+" + abstractAtk.ToString() + ")";
                else
                    result = " (-" + (Math.Abs(player.Atk - firstPlayerAtk)).ToString() + ")";

            }
            return result;
        }


        static string ShowDifferenceDefResult()
        {
            string result = "";
            int abstractDef = player.Def - firstPlayerDef;
            if (abstractDef != 0)
            { 
                if (abstractDef > 0)
                    result = " (+" + abstractDef.ToString() + ")";
                else
                    result = " (-" + (Math.Abs(abstractDef)).ToString() + ")";

            }
            return result;
        }

        static void ManageEquipItem()
        {
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < itemList.Count; i++)
            {
                string itemName = itemList[i].Name;
                string itemType = "";
                if (itemList[i].IsEquip)
                {
                    itemName = "[E]" + itemName;
                }

                if (itemList[i].Type == 'a')
                {
                    itemType = "공격력";
                }
                else if (itemList[i].Type == 'd')
                {
                    itemType = "방어력";
                }

                Console.WriteLine($"- {i + 1} {itemName}  {itemType}  +{itemList[i].TypeEffect}  {itemList[i].Content}");
            }

            Console.WriteLine("장착하거나 해제할 아이템 번호를 선택해주세요.");
            int input = int.Parse(Console.ReadLine());

            //장착하지 않은 아이템을 장착할 경우 
            if (!itemList[input - 1].IsEquip)
            {
                itemList[input - 1].IsEquip = true;
                itemList[input - 1].Name.Replace("[E]", "");
                switch (itemList[input - 1].Type)
                {
                    case 'a':
                        player.Atk += itemList[input - 1].TypeEffect;
                        break;
                    case 'd':
                        player.Def += itemList[input - 1].TypeEffect;
                        break;
                 }

            }
            //장착한 아이템을 장착하지 않을 경우 
            else
            {
                itemList[input - 1].IsEquip = false;
                itemList[input - 1].Name.Replace("[E]", "");
                switch (itemList[input - 1].Type)
                {
                    case 'a':
                        player.Atk -= itemList[input - 1].TypeEffect;
                        break;
                    case 'd':
                        player.Def -= itemList[input - 1].TypeEffect;
                        break;
                 }

            }
            DisplayMyInfo();
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
            public bool IsEquip { get; set; } //기본 false로 설정 .
            public char Type { get; }
            public int TypeEffect { get; }
            public string Content { get; }

            public Item(string name, bool isEquip, char type, int typeEffect, string content)
            {
                Name = name;
                IsEquip = isEquip;
                Type = type;
                TypeEffect = typeEffect;
                Content = content;
            }


        }

    public class Character
        {
            public string Name { get; }
            public string Job { get; }
            public int Level { get; }
            public int Atk { get; set; }
            public int Def { get; set; }
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
