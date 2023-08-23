using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DungeonConsole
{
    class MainClass
    {
        private static Character player;
        private static List<Item> itemList = new List<Item>();
        private static string path = "/Users/juyon/Desktop/DungeonConsole/DungeonConsole/userData/";

        static bool isEquipEvent = false;
        static float firstPlayerAtk;
        static int firstPlayerDef;
        static bool changeAtk = false;
        static bool changeDef = false;
        static int DGclearNum = 0;

        public static void Main(string[] args)
        {
            DisplayGameStart();
        }

        static void DisplayGameStart()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전\n\n\n");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 새 게임");
            Console.WriteLine("2. 이전 데이터 불러오기");

            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    DisplayGameStart();
                    break;
                case 1:
                    GameDataSetting();
                    DisplayGameIntro();
                    break;
                case 2:
                    LoadSaveData();
                    break;
            }

        }

        static void LoadSaveData()
        {
            Console.Clear();
            DirectoryInfo di = new DirectoryInfo(path);
            FileInfo[] files = di.GetFiles("*.txt");

            if (files.Length == 0)
            {
                Console.WriteLine("저장된 데이터가 없습니다.\n\n");
                Console.WriteLine("0. 나가기");

                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");

                int input = CheckValidInput(0, 0);
                switch (input)
                {
                    case 0:
                        DisplayGameStart();
                        break;
                }

            }

            else
            {
                Console.WriteLine("[세이브 데이터]");

                
                for(int i=0; i < files.Length; i++)
                {
                    Console.Write(i + 1);
                    Console.WriteLine(". " + files[i].Name);
                }
                Console.WriteLine("\n불러올 데이터를 선택해주세요.");
                int input = CheckValidInput(1, files.Length + 1);

                BinaryFormatter bf = new BinaryFormatter();

                FileStream fs = new FileStream(path + files[input - 1].Name, FileMode.Open);

                player = bf.Deserialize(fs) as Character;
                fs.Close();

                Console.Clear();
                Console.WriteLine("저장된 데이터를 불러왔습니다! 게임을 시작해주세요.");
                Console.WriteLine("0. 게임 시작\n");

                input = CheckValidInput(0, 0);
                switch(input)
                {
                    case 0:
                        DisplayGameIntro();
                        break;
                }

            }
            
        }



        static void GameDataSetting()
        {
            PlayerDataSetting();
            ItemDataSetting();
        }

        static void PlayerDataSetting()
        {
            Console.Clear();
            Console.WriteLine("플레이어 데이터 설정\n\n");
            Console.WriteLine("플레이어의 이름을 입력해주세요.");

            string input = Console.ReadLine();

            player = new Character(input, "전사", 1, 10, 5, 100, 1500, false, false);
            firstPlayerAtk = player.Atk;
            firstPlayerDef = player.Def;
        }

        static void ItemDataSetting()
        {
            itemList.Add(new Item("무쇠갑옷", false, 'd', 5, "무쇠로 만들어져 튼튼한 갑옷입니다.", 500, true));
            itemList.Add(new Item("낡은 검", false, 'a', 2, "쉽게 볼 수 있는 낡은 검입니다.", 600, false));
            itemList.Add(new Item("나무 몽둥이", false, 'a', 3, "주위에서 많이 보이는 몽둥이입니다.", 100, true));
            itemList.Add(new Item("스파르타의 갑옷", false, 'd', 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, false));
            itemList.Add(new Item("수련자 갑옷", false, 'd', 5, "수련에 도움을 주는 갑옷입니다.", 1000, false));
            itemList.Add(new Item("청동 도끼", false, 'a', 5, "어디선가 사용됐던거 같은 도끼입니다.", 1500, false));
            itemList.Add(new Item("스파르타의 창", false, 'a', 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3000, false));
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
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("3. 상점");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("4. 던전");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("5. 휴식");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("6. 저장");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 6);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;
                case 2:
                    DisplayInventory();
                    break;
                case 3:
                    DisplayMarket();
                    break;
                case 4:
                    DisplayDungeon();
                    break;
                case 5:
                    DisplayRest();
                    break;
                case 6:
                    DisplayCheckSaveData();
                    break;
            }
        }

        static void DisplayCheckSaveData()
        {
            Console.Clear();
            Console.WriteLine("현재 데이터를 저장하시겠습니까?\n\n");
            Console.WriteLine("0. 저장 안 함");
            Console.WriteLine("1. 저장");

            Console.WriteLine("\n선택지를 입력해주세요");

            int input = CheckValidInput(0, 1);
            switch(input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    SaveUserData();
                    break;
            }
        }

        static void SaveUserData()
        {
            string userStatusName = player.Name + ".txt";
            string userItemName = player.Name + "_Item.txt";

            string userStatusDataPath = path + userStatusName;
            string userItemDataPath = path + userItemName;

            FileInfo fileInfo = new FileInfo(userStatusDataPath);

            if(fileInfo.Exists)
            {
                File.Delete(userStatusDataPath);
                File.Delete(userItemDataPath);
            }

            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(userStatusDataPath, FileMode.Create);
            bf.Serialize(fs, player);
            fs.Close();


            fs = new FileStream(userItemDataPath, FileMode.Create);
            bf.Serialize(fs, itemList);
            fs.Close();


        }

        static void DisplayRest()
        {
            Console.WriteLine("휴식하기");
            Console.WriteLine("500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : " + player.Gold + " G");

            Console.WriteLine("\n1. 휴식하기");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 1);
            switch(input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    RecoveryHp();
                    break;
            }
        }

        static void RecoveryHp()
        {
            if(player.Gold < 500)
            {
                Console.WriteLine("Gold가 부족합니다.");
            }

            else
            {
                player.Hp = 100;
                Console.WriteLine("휴식을 완료했습니다."); 
            }

            Console.WriteLine("0. 나가기");

            Console.WriteLine("\n원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }

        static void DisplayDungeon()
        {
            Console.WriteLine("던전입장");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine();

            Console.WriteLine("1. 쉬운 던전    | 방어력 5 이상 권장");
            Console.WriteLine("2. 일반 던전    | 방어력 11 이상 권장");
            Console.WriteLine("3. 어려운 던전   | 방어력 17 이상 권장");
            Console.WriteLine("0. 나가기");

            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            int input = CheckValidInput(0, 3);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                default:
                    EntryDungeon(input);
                    break;
            }
        }

        static void EntryDungeon(int level)
        {

            //권장 방어력보다 낮다면
            int recommendedDef = 5 + 6 * (level - 1);
            if (player.Def < recommendedDef)
            {
                Random rand = new Random();
                int playerDGNum = rand.Next(1, 11);

                if(playerDGNum < 5)
                {
                    FailDungeon();
                }

                else
                {
                    ClearDungeon(level);
                }
            }

            //권장 방어력보다 높거나 같다면
            else
            {
                ClearDungeon(level);
            }

            Console.WriteLine("\n0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }


        static void FailDungeon()
        {
            Console.WriteLine("던전 클리어를 실패하셨습니다.");
            Console.Write("Hp : " + player.Hp);

            player.Hp /= 2;

            Console.Write(" -> " + player.Hp);
        }

        static void ClearDungeon(int input)
        {
            Console.WriteLine("던전 클리어");
            Console.WriteLine("축하합니다!! \n쉬운 던전을 클리어 하였습니다.\n");

            Console.WriteLine("[탐험 결과]");
            Console.Write("체력 " + player.Hp);

            int recommendedDef = 5 + 6 * (input - 1);
            int AbsDef = recommendedDef - player.Def;

            Random rand = new Random();
            int lossHp = rand.Next(20 + AbsDef, 35 + AbsDef + 1);
            player.Hp -= lossHp;

            Console.Write(" -> " + player.Hp);
            Console.WriteLine();
            Console.Write("Gold " + player.Gold + " G -> ");

            int basicCompensation = 0;

            switch(input)
            {
                case 1:
                    basicCompensation = 1000;
                    break;
                case 2:
                    basicCompensation = 1700;
                    break;
                case 3:
                    basicCompensation = 2500;
                    break;
            }

            int extraCompensation = basicCompensation * (rand.Next((int)player.Atk, (int)player.Atk * 2 + 1)) / 100;
            int Compensation = basicCompensation + extraCompensation;

            player.Gold += Compensation;

            Console.Write(player.Gold + " G ");

            DGclearNum += 1;
            switch(DGclearNum)
            {
                case 1:
                    PlayerLevelUp();
                    break;
                case 3:
                    PlayerLevelUp();
                    break;
                case 6:
                    PlayerLevelUp();
                    break;
                case 10:
                    PlayerLevelUp();
                    break;
            }

        }

        static void PlayerLevelUp()
        {
            player.Level += 1;
            player.Atk += (float)0.5;
            firstPlayerAtk = player.Atk;

            player.Def += 1;
            firstPlayerDef = player.Def;
        }

        static void DisplayMarket()
        {
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[아이템 목록]");


            for (int i = 0; i < itemList.Count; i++)
            {
                Console.Write("- ");
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

                Console.Write($"{itemName}");

                string blanks = "";
                int blankNum = 0;
                blankNum = 10 - itemName.Length;
                for (int j = 0; j < blankNum; j++)
                {
                    blanks += " ";
                }
                Console.Write(blanks);
                blanks = "";

                string itemEffect = itemType + " + " + itemList[i].TypeEffect.ToString();
                blankNum = 10 - itemEffect.Length;
                for (int j = 0; j < blankNum; j++)
                {
                    blanks += " ";
                }
                Console.Write("|" + itemEffect + blanks);

                blanks = "";
                blankNum = 30 - itemList[i].Content.Length;

                for (int j = 0; j < blankNum; j++)
                {
                    blanks += " ";
                }
                Console.Write($"| {itemList[i].Content}" + blanks + " | ");

                if (itemList[i].IsSoldOut)
                {
                    Console.Write("구매완료");
                }
                else
                {
                    Console.Write($"{itemList[i].Price}");
                }

                Console.WriteLine();

            }

            Console.WriteLine();
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("0. 나가기");


            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    PurchaseItem();
                    break;
            }
        }



        static void PurchaseItem()
        {
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[아이템 목록]");


            for (int i = 0; i < itemList.Count; i++)
            {
                Console.Write($"- {i + 1}  ");
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

                Console.Write($"{itemName}");

                string blanks = "";
                int blankNum = 0;
                blankNum = 10 - itemName.Length;
                for (int j = 0; j < blankNum; j++)
                {
                    blanks += " ";
                }
                Console.Write(blanks);
                blanks = "";

                string itemEffect = itemType + " + " + itemList[i].TypeEffect.ToString();
                blankNum = 10 - itemEffect.Length;
                for (int j = 0; j < blankNum; j++)
                {
                    blanks += " ";
                }
                Console.Write("|" + itemEffect + blanks);

                blanks = "";
                blankNum = 30 - itemList[i].Content.Length;

                for (int j = 0; j < blankNum; j++)
                {
                    blanks += " ";
                }
                Console.Write($"| {itemList[i].Content}" + blanks + " | ");

                if (itemList[i].IsSoldOut)
                {
                    Console.Write("구매완료");
                }
                else
                {
                    Console.Write($"{itemList[i].Price}");
                }

                Console.WriteLine();

            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");


            int input = CheckValidInput(0, 7);

            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                default:
                    IsPurchasedItem(input - 1);
                    break;
            }

        }



        static void IsPurchasedItem(int itemIdx)
        {
            if (itemList[itemIdx].IsSoldOut)
                Console.WriteLine("이미 구매한 아이템입니다.");

            else
            {
                if (player.Gold >= itemList[itemIdx].Price)
                {
                    Console.WriteLine("구매를 완료했습니다.");
                    player.Gold -= itemList[itemIdx].Price;
                    itemList[itemIdx].IsSoldOut = true;
                }

                else
                {
                    Console.WriteLine("Gold가 부족합니다.");
                }
            }

            Console.WriteLine();
            Console.WriteLine("1. 상점으로 돌아가기");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");


            int input = CheckValidInput(0, 1);

            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    DisplayMarket();
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
                if (!itemList[i].IsSoldOut)
                    continue;

                Console.Write("- ");
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

                Console.Write($"{itemName}");

                string blanks = "";
                int blankNum = 0;
                blankNum = 10 - itemName.Length;
                for (int j = 0; j < blankNum; j++)
                {
                    blanks += " ";
                }
                Console.Write(blanks);
                blanks = "";

                string itemEffect = itemType + " + " + itemList[i].TypeEffect.ToString();
                blankNum = 10 - itemEffect.Length;
                for (int j = 0; j < blankNum; j++)
                {
                    blanks += " ";
                }
                Console.Write("|" + itemEffect + blanks);

                blanks = "";
                blankNum = 30 - itemList[i].Content.Length;

                for (int j = 0; j < blankNum; j++)
                {
                    blanks += " ";
                }
                Console.Write($"| {itemList[i].Content}" + blanks);
                Console.WriteLine();

            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 장착 관리");


            int input = CheckValidInput(0, 1);
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
            float abstractAtk = player.Atk - firstPlayerAtk;
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

            int itemNum = 0;
            Dictionary<int, int> itemListPair = new Dictionary<int, int>();

            for (int i = 0; i < itemList.Count; i++)
            {
                if (!itemList[i].IsSoldOut)
                {
                    continue;
                }
                itemNum+=1;
                itemListPair.Add(itemNum, i);

                Console.Write("- " + (itemNum) + " ");
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

                Console.Write($"{itemName}");

                string blanks = "";
                int blankNum = 0;
                blankNum = 10 - itemName.Length;
                for (int j = 0; j < blankNum; j++)
                {
                    blanks += " ";
                }
                Console.Write(blanks);
                blanks = "";

                string itemEffect = itemType + " + " + itemList[i].TypeEffect.ToString();
                blankNum = 10 - itemEffect.Length;
                for (int j = 0; j < blankNum; j++)
                {
                    blanks += " ";
                }
                Console.Write("|" + itemEffect + blanks + $"|{itemList[i].Content}");
                Console.WriteLine();

            }

            Console.WriteLine("장착하거나 해제할 아이템 번호를 선택해주세요.");
            int input = CheckValidInput(1, itemNum);


            //장착하지 않은 아이템을 장착할 경우
            int inputItemIdx = itemListPair[input];
            if (!itemList[inputItemIdx].IsEquip)
            {
                char inputItemType = itemList[inputItemIdx].Type;

                if(inputItemType == 'a')
                {
                    //이미 어택형 아이템 장착할 경우 
                    if(player.IsEquipAtkItem)
                    {
                        int EquipAtkItemIdx = 0;
                        for(int j=0; j<itemList.Count; j++)
                        {
                            if(itemList[j].IsSoldOut && itemList[j].Type == 'a' && itemList[j].IsEquip)
                            {
                                EquipAtkItemIdx = j;
                                break;
                            }
                        }

                        itemList[EquipAtkItemIdx].IsEquip = false;
                        player.Atk -= itemList[EquipAtkItemIdx].TypeEffect;


                        itemList[inputItemIdx].IsEquip = true;
                        player.Atk += itemList[inputItemIdx].TypeEffect;
                    }

                    //어택형 아이템을 장착하고 있지 않을 경우
                    else
                    {
                        player.IsEquipAtkItem = true;
                        itemList[inputItemIdx].IsEquip = true;
                        player.Atk += itemList[inputItemIdx].TypeEffect;
                    }
                }

                else
                {
                    //디펜스형 아이템을 장착한 경우 
                    if(player.IsEquipDefItem)
                    {
                        int EquipDefItemIdx = 0;
                        for (int j = 0; j < itemList.Count; j++)
                        {
                            if (itemList[j].IsSoldOut && itemList[j].Type == 'd' && itemList[j].IsEquip)
                            {
                                EquipDefItemIdx = j;
                                break;
                            }
                        }

                        itemList[EquipDefItemIdx].IsEquip = false;
                        player.Def -= itemList[EquipDefItemIdx].TypeEffect;


                        itemList[inputItemIdx].IsEquip = true;
                        player.Def += itemList[inputItemIdx].TypeEffect;
                    }
                    //디펜스형 아이템을 장착하지 않은 경
                    else
                    {
                        player.IsEquipDefItem = true;
                        itemList[inputItemIdx].IsEquip = true;
                        player.Def += itemList[inputItemIdx].TypeEffect;
                    }
                }

            }
            //장착한 아이템을 장착하지 않을 경우 
            else
            {
                itemList[inputItemIdx].IsEquip = false;
                switch (itemList[inputItemIdx].Type)
                {
                    case 'a':
                        player.Atk -= itemList[inputItemIdx].TypeEffect;
                        break;
                    case 'd':
                        player.Def -= itemList[inputItemIdx].TypeEffect;
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
