using Escape_Board.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Escape_Board
{
    public partial class WoodsEscape : Form
    {
        public enum enDiceFaces { One = 1, Two, Three, Four, Five, Six,  Default = 0 };
        public enum enModes { Lion, Bear, Eagle, Crocodile, Owl, Nothing }

        public enum enTurns { Player1 = 1, Player2, Player3, Player4 }


        public stPlayer[] arrPlayers = new stPlayer[4];
        public stGameStats GameStats = new stGameStats();
        public stAvatarChoices Avatars = new stAvatarChoices();

        string[] Quizes = new string[] {
 
            "Is Liverpool FC located in Manchester?",                                 // No (0)
            "Is the River Nile the longest river in the world?",                      // Yes (1)
            "Did the United States gain independence in 1790?",                       // No (2)
            "Has Real Madrid won La Liga more than 30 times?",                        // Yes (3)
            "Is Mount Everest located in the Alps?",                                  // No (4)
            "Is FC Barcelona based in Spain?",                                        // Yes (5)
            "Is the Sahara Desert the smallest desert in the world?",                 // No (6)
            "Has Manchester City won the English Premier League?",                     // Yes (7)
            "Did World War II end in 1944?",                                          // No (8)
            "Is Greenland considered part of North America?",                         // Yes (9)
            "Is Mount Fuji located in South America?",                                // No (10)
            "Is the FIFA World Cup held every four years?",                           // Yes (11)
            "Was Cleopatra a queen of the Persian Empire?",                           // No (12)
            "Is the Great Barrier Reef off the coast of Australia?",                  // Yes (13)
            "Is Juventus based in Germany?",                                          // No (14)
            "Is the Gobi Desert located in Asia?",                                    // Yes (15)
            "Did the Berlin Wall fall in 1989?",                                      // No (16)
            "Does Real Madrid play in La Liga?",                                      // Yes (17)
            "Is Australia both a country and a continent located entirely in the Northern Hemisphere?", // No (18)
            "Is Lake Superior the smallest Great Lake?",                              // Yes (19)
            "Did the Magna Carta sign in 1215?",                                      // No (20)
            "Is the Arctic Circle north of the Equator?",                             // Yes (21)
            "Is Paris Saint-Germain an Italian club?",                                // No (22)
            "Has Barcelona ever won a treble (league, cup, Champions League) in one season?", // Yes (23)
            "Is the Dead Sea the highest point on Earth?",                            // No (24)
            "Has Manchester United won the Champions League?",                         // Yes (25)
            "Is Chelsea FC located in Manchester?",                                   // No (26)
            "Was Winston Churchill Prime Minister of the UK during World War II?",     // Yes (27)
            "Is the Mediterranean Sea a freshwater lake?",                            // No (28)
            "Is the Amazon River located in Africa?",                                 // Yes (29)
            "Is AC Milan based in Rome?",                                             // No (30)
            "Has Juventus reached the Champions League final since 2017?",            // Yes (31)
            "Did Christopher Columbus reach North America in 1600?",                  // No (32)
            "Is the Ural Mountains considered the boundary between Europe and Asia?", // Yes (33)
            "Is Borussia Dortmund a Spanish club?",                                   // No (34)
            "Has Real Madrid won the UEFA Champions League more than 10 times?",       // Yes (35)
            "Did the Ottoman Empire collapse in the 18th century?",                   // No (36)
            "Is Mount Kilimanjaro in Tanzania?",                                      // Yes (37)
            "Is the Super Bowl the final match of the NBA season?",                   // No (38)
            "Is Africa a continent?",                                                 // Yes (39)
            "Was the Battle of Hastings in 1066?",                                    // No (40)
            "Is Lionel Messi a Ballon d'Or winner?",                                  // Yes (41)
            "Is golf a team sport played with a ball and clubs?",                     // No (42)
            "Is the Rhine River in Europe?",                                          // Yes (43)
            "Is the World Cup held every two years?",                                 // No (44)
            "Was the United Nations founded in 1945?",                                // Yes (45)
            "Is Boca Juniors an Argentine football club?",                            // No (46)
            "Is Mount Rushmore located in the United States?",                        // Yes (47)
            "Is tennis a type of football?",                                          // No (48)
            "Is Wembley Stadium in London?",                                          // Yes (49)
            "Is Manchester City based in Italy?",                                     // No (50)
            "Is the Black Sea bordered by Turkey?",                                   // Yes (51)
            "Is the Taj Mahal built in the 16th century BC?",                        // No (52)
            "Was the Battle of Trafalgar the last naval battle of the Napoleonic Wars?", // Yes (53)
            "Is Manchester United's home stadium called Anfield?",                    // No (54)
            "Has Liverpool ever won the Premier League title?",                       // Yes (55)
            "Is the Eiffel Tower located in Rome?",                                   // No (56)
            "Is the Amazon Rainforest primarily in Brazil?",                          // Yes (57)
            "Did Pelé play for Barcelona?",                                           // No (58)
            "Has Manchester City ever won the UEFA Champions League?",                // Yes (59)
            "Is the Dead Sea located below sea level?",                               // No (60)
            "Is the Great Wall of China visible from space with the naked eye?",      // Yes (61)
            "Is the Louvre Museum located in Geneva?",                                // No (62)
            "Is the Colosseum in Rome an ancient amphitheater?",                      // Yes (63)
            "Is the Danube River in South America?",                                  // No (64)
            "Is Machu Picchu located in Peru?",                                       // Yes (65)
            "Did the Renaissance begin in Northern Europe?",                          // No (66)
            "Is the Parthenon located in Athens?",                                    // Yes (67)
            "Is the Kremlin located in Paris?",                                       // No (68)
            "Is the Vatican City the smallest independent state in the world?"       // Yes (69)
        
    };

        PictureBox[] Avatar1Steps;
        PictureBox[] Avatar2Steps;
        PictureBox[] Avatar3Steps;
        PictureBox[] Avatar4Steps;
        public struct stPlayer
        {

            public string Name;
            public byte Position;
            public byte Wins;
            public bool shield;
            public Image Avatar;
            public TextBox txtPlayerName;
            public Panel PlayerAvatarPanel;
            public Label PlayerNameLabel;
            public PictureBox AvatarPosition;
            public int AvatarIndex;

        };

        public struct stGameStats
        {
            public enTurns PlayerTurn;
            public byte PlayersCount;
            public byte Rounds;
            public byte steps;
            public bool Win;
            public enDiceFaces DiceFace;
            
        }

        public struct stAvatarChoices
        {
            public Image Player1Avatar;
            public int Avatar1Index;
            public Image Player2Avatar;
            public int Avatar2Index;
            public Image Player3Avatar;
            public int Avatar3Index;
            public Image Player4Avatar;
            public int Avatar4Index;
        }

        public WoodsEscape()
        {
            InitializeComponent();
            Avatar1Steps = new PictureBox[]{
        picbPlayer1,
        Avatar1Step1, Avatar1Step2, Avatar1Step3, Avatar1Step4, Avatar1Step5,
        Avatar1Step6, Avatar1Step7, Avatar1Step8, Avatar1Step9, Avatar1Step10, Avatar1Step11,
        Avatar1Step12, Avatar1Step13, Avatar1Step14, Avatar1Step15, Avatar1Step16,
        Avatar1Step17, Avatar1Step18, Avatar1Step19, Avatar1Step20, Avatar1Step21,
        Avatar1Step22, Avatar1Step23, Avatar1Step24, Avatar1Step25,
        Avatar1Step26, Avatar1Step27, Avatar1Step28, Avatar1Step29,
        Avatar1Step30,Avatar1Step31

     };
            Avatar2Steps = new PictureBox[]
            {
        picbPlayer2,
        Avatar2Step1, Avatar2Step2, Avatar2Step3, Avatar2Step4, Avatar2Step5,
        Avatar2Step6, Avatar2Step7, Avatar2Step8, Avatar2Step9, Avatar2Step10,
        Avatar2Step11, Avatar2Step12, Avatar2Step13, Avatar2Step14, Avatar2Step15,
        Avatar2Step16,Avatar2Step17, Avatar2Step18, Avatar2Step19, Avatar2Step20,
        Avatar2Step21, Avatar2Step22, Avatar2Step23, Avatar2Step24, Avatar2Step25,
        Avatar2Step26, Avatar2Step27, Avatar2Step28, Avatar2Step29,Avatar2Step30, Avatar2Step31
            };
            Avatar3Steps = new PictureBox[]
           {
        picbPlayer3,
        Avatar3Step1, Avatar3Step2, Avatar3Step3, Avatar3Step4, Avatar3Step5,
        Avatar3Step6, Avatar3Step7, Avatar3Step8, Avatar3Step9, Avatar3Step10,
        Avatar3Step11, Avatar3Step12, Avatar3Step13, Avatar3Step14, Avatar3Step15,
        Avatar3Step16,Avatar3Step17, Avatar3Step18, Avatar3Step19, Avatar3Step20,
        Avatar3Step21, Avatar3Step22, Avatar3Step23, Avatar3Step24, Avatar3Step25,
        Avatar3Step26, Avatar3Step27, Avatar3Step28, Avatar3Step29,Avatar3Step30, Avatar3Step31
           };
            Avatar4Steps = new PictureBox[]
         {
        picbPlayer4,
        Avatar4Step1, Avatar4Step2,   Avatar4Step3,  Avatar4Step4,  Avatar4Step5,
        Avatar4Step6, Avatar4Step7,   Avatar4Step8,  Avatar4Step9,  Avatar4Step10,
        Avatar4Step11, Avatar4Step12, Avatar4Step13, Avatar4Step14, Avatar4Step15,
        Avatar4Step16, Avatar4Step17, Avatar4Step18, Avatar4Step19, Avatar4Step20,
        Avatar4Step21, Avatar4Step22, Avatar4Step23, Avatar4Step24, Avatar4Step25,
        Avatar4Step26, Avatar4Step27, Avatar4Step28, Avatar4Step29, Avatar4Step30, Avatar4Step31
         };

        }

        public void CheckConfirmedPlayerCount()
        {

            if (butConfirmPlayersCount.Enabled)
            {
                if (MessageBox.Show("Please Choose Players Count First", "", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    comboBox1.Focus();
                }
            }
        }

        public void GiveDefaultForPlayers()
        {
            arrPlayers[0].txtPlayerName = txtbPlayer1;
            arrPlayers[0].PlayerAvatarPanel = panel1;
            arrPlayers[0].PlayerNameLabel = lbPlayerName1;
            arrPlayers[0].AvatarPosition = Avatar1Step1;
            arrPlayers[0].Position = 1;
            arrPlayers[0].Wins = 0;


            arrPlayers[1].txtPlayerName = txtbPlayer2;
            arrPlayers[1].PlayerAvatarPanel = panel2;
            arrPlayers[1].PlayerNameLabel = lbPlayerName2;
            arrPlayers[1].AvatarPosition = Avatar2Step1;
            arrPlayers[1].Position = 1;
            arrPlayers[1].Wins = 0;


            arrPlayers[2].txtPlayerName = txtbPlayer3;
            arrPlayers[2].PlayerAvatarPanel = panel3;
            arrPlayers[2].PlayerNameLabel = lbPlayerName3;
            arrPlayers[2].AvatarPosition = Avatar3Step1;
            arrPlayers[2].Position = 1;
            arrPlayers[2].Wins = 0;


            arrPlayers[3].txtPlayerName = txtbPlayer4;
            arrPlayers[3].PlayerAvatarPanel = panel4;
            arrPlayers[3].PlayerNameLabel = lbPlayerName4;
            arrPlayers[3].AvatarPosition = Avatar4Step1;
            arrPlayers[3].Position = 1;
            arrPlayers[3].Wins = 0;

            GameStats.Win = false;



        }

        void ResetAvatarPlaces(int PlayerNumber)
        {
            switch (PlayerNumber)
            {
                case 0:
                    arrPlayers[0].AvatarPosition.Visible = false;
                    arrPlayers[0].AvatarPosition = Avatar1Step1;
                    arrPlayers[0].AvatarPosition.Image = arrPlayers[0].Avatar;
                    Avatar1Step1.Visible = true;
                    break;
                case 1:
                    arrPlayers[1].AvatarPosition.Visible = false;
                    arrPlayers[1].AvatarPosition = Avatar2Step1;
                    arrPlayers[1].AvatarPosition.Image = arrPlayers[1].Avatar;
                    Avatar2Step1.Visible = true;
                    break;
                case 2:
                    arrPlayers[2].AvatarPosition.Visible = false;
                    arrPlayers[2].AvatarPosition = Avatar3Step1;
                    arrPlayers[2].AvatarPosition.Image = arrPlayers[2].Avatar;
                    Avatar3Step1.Visible = true;
                    break;
                case 3:
                    arrPlayers[3].AvatarPosition.Visible = false;
                    arrPlayers[3].AvatarPosition = Avatar4Step1;
                    arrPlayers[3].AvatarPosition.Image = arrPlayers[3].Avatar;
                    Avatar4Step1.Visible = true;
                    break;

            }
        }

        void ResetGame()
        {
            for (int i = 0; i < GameStats.PlayersCount; i++)
            {
                arrPlayers[i].Position = 0;
                arrPlayers[i].shield = false;
                ResetAvatarPlaces(i);

            }
        }

        private void WoodsEscape_Load(object sender, EventArgs e)
        {
            tabPage2.Focus();
            butConfirmPlayersCount.Focus();
            listView1.View = View.Details;
            GameStats.Rounds = 0;
            GiveDefaultForPlayers();

        }

        public void UnlockPlayer(byte PlayerNumber)
        {
            arrPlayers[PlayerNumber].txtPlayerName.Visible = true ;
            arrPlayers[PlayerNumber].PlayerAvatarPanel.Visible = true;
        }

        public void LockPlayer(byte PlayerNumber)
        {
            arrPlayers[PlayerNumber].txtPlayerName.Visible = false;
            arrPlayers[PlayerNumber].PlayerAvatarPanel.Visible = false;
        }

        public void UnlockPlayers()
        {
            for (byte i = 0; i < 4; i++)
            {
                UnlockPlayer(i);
            }
            
            lbPlayerName1.BackColor = Color.White;
        }

        public void LockUnusedPlayers()
        {
            for (byte i = GameStats.PlayersCount; i < 4; i++)
            {
                LockPlayer(i);
            }
            GameStats.PlayerTurn = enTurns.Player1;
            lbPlayerName1.BackColor = Color.Orange;
        }

        void AddGameStatistics(string content, byte PlayerNumber)
        {
            ListViewItem item = new ListViewItem(Convert.ToString(GameStats.Rounds));
            item.SubItems.Add(content + arrPlayers[PlayerNumber].Name);
            listView2.Items.Add(item);

        }

        void FillStatisticsListIndexes()
        {

            for (int i = 1; i <= GameStats.PlayersCount; i++)
            {
                ListViewItem player = new ListViewItem(Convert.ToString(i));
                listView1.Items.Add(player);
            }
        }

        void FillStatisticsListNames()
        {
            for (int i = 0; i < GameStats.PlayersCount; i++)
            {

                listView1.Items[i].SubItems.Add(arrPlayers[i].Name);
                listView1.Items[i].SubItems.Add("0");

            }
        }

        void FillStatisticsListAvatars()
        {
            for (int i = 0; i < GameStats.PlayersCount; i++)
            {

                listView1.Items[i].ImageIndex = arrPlayers[i].AvatarIndex;

            }
        }

        public void AssignPlayersAvatars1(RadioButton rdbAvatar)
        {

            switch (Convert.ToByte(rdbAvatar.Tag))
            {
                case 1:
                    Avatars.Player1Avatar = Resources.face1;
                    Avatars.Avatar1Index = 0;
                    break;
                case 2:
                    Avatars.Player1Avatar = Resources.face2;
                    Avatars.Avatar1Index = 1;
                    break;
                case 3:
                    Avatars.Player1Avatar = Resources.face3;
                    Avatars.Avatar1Index = 2;
                    break;
                case 4:
                    Avatars.Player1Avatar = Resources.face4;
                    Avatars.Avatar1Index = 3;
                    break;
                case 5:
                    Avatars.Player1Avatar = Resources.face5;
                    Avatars.Avatar1Index = 4;
                    break;
                case 6:
                    Avatars.Player1Avatar = Resources.face6;
                    Avatars.Avatar1Index = 5;
                    break;
            }
        }

        public void AssignPlayersAvatars2(RadioButton rdbAvatar)
        {

            switch (Convert.ToByte(rdbAvatar.Tag))
            {
                case 1:
                    Avatars.Player2Avatar = Resources.face1;
                    Avatars.Avatar2Index = 0;
                    break;
                case 2:
                    Avatars.Player2Avatar = Resources.face2;
                    Avatars.Avatar2Index = 1;
                    break;
                case 3:
                    Avatars.Player2Avatar = Resources.face3;
                    Avatars.Avatar2Index = 2;
                    break;
                case 4:
                    Avatars.Player2Avatar = Resources.face4;
                    Avatars.Avatar2Index = 3;
                    break;
                case 5:
                    Avatars.Player2Avatar = Resources.face5;
                    Avatars.Avatar2Index = 4;
                    break;
                case 6:
                    Avatars.Player2Avatar = Resources.face6;
                    Avatars.Avatar2Index = 5;
                    break;
            }
        }

        public void AssignPlayersAvatars3(RadioButton rdbAvatar)
        {

            switch (Convert.ToByte(rdbAvatar.Tag))
            {
                case 1:
                    Avatars.Player3Avatar = Resources.face1;
                    Avatars.Avatar3Index = 0;
                    break;
                case 2:
                    Avatars.Player3Avatar = Resources.face2;
                    Avatars.Avatar3Index = 1;
                    break;
                case 3:
                    Avatars.Player3Avatar = Resources.face3;
                    Avatars.Avatar3Index = 2;
                    break;
                case 4:
                    Avatars.Player3Avatar = Resources.face4;
                    Avatars.Avatar3Index = 3;
                    break;
                case 5:
                    Avatars.Player3Avatar = Resources.face5;
                    Avatars.Avatar3Index = 4;
                    break;
                case 6:
                    Avatars.Player3Avatar = Resources.face6;
                    Avatars.Avatar3Index = 5;
                    break;
            }
        }

        public void AssignPlayersAvatars4(RadioButton rdbAvatar)
        {

            switch (Convert.ToByte(rdbAvatar.Tag))
            {
                case 1:
                    Avatars.Player4Avatar = Resources.face1;
                    Avatars.Avatar4Index = 0;
                    break;
                case 2:
                    Avatars.Player4Avatar = Resources.face2;
                    Avatars.Avatar4Index = 1;
                    break;
                case 3:
                    Avatars.Player4Avatar = Resources.face3;
                    Avatars.Avatar4Index = 2;
                    break;
                case 4:
                    Avatars.Player4Avatar = Resources.face4;
                    Avatars.Avatar4Index = 3;
                    break;
                case 5:
                    Avatars.Player4Avatar = Resources.face5;
                    Avatars.Avatar4Index = 4;
                    break;
                case 6:
                    Avatars.Player4Avatar = Resources.face6;
                    Avatars.Avatar4Index = 5;
                    break;
            }
        }

        void MoveAvatar1Picture(byte PlayerNumber, byte PreviousPosition)
        {

            arrPlayers[0].AvatarPosition.Visible = false;
            Avatar1Steps[arrPlayers[PlayerNumber].Position].Image = picbPlayer1.Image;
            arrPlayers[0].AvatarPosition = Avatar1Steps[arrPlayers[PlayerNumber].Position];
            arrPlayers[0].AvatarPosition.Visible = true;
        }

        void MoveAvatar2Picture(byte PlayerNumber, byte PreviousPosition)
        {

            arrPlayers[1].AvatarPosition.Visible = false;
            Avatar2Steps[arrPlayers[PlayerNumber].Position].Image = picbPlayer2.Image;
            arrPlayers[1].AvatarPosition = Avatar2Steps[arrPlayers[PlayerNumber].Position];
            arrPlayers[1].AvatarPosition.Visible = true;
        }

        void MoveAvatar3Picture(byte PlayerNumber, byte PreviousPosition)
        {
            arrPlayers[2].AvatarPosition.Visible = false;
            Avatar3Steps[arrPlayers[PlayerNumber].Position].Image = picbPlayer3.Image;
            arrPlayers[2].AvatarPosition = Avatar3Steps[arrPlayers[PlayerNumber].Position];
            arrPlayers[2].AvatarPosition.Visible = true;
        }

        void MoveAvatar4Picture(byte PlayerNumber, byte PreviousPosition)
        {
            arrPlayers[3].AvatarPosition.Visible = false;
            Avatar4Steps[arrPlayers[PlayerNumber].Position].Image = picbPlayer4.Image;
            arrPlayers[3].AvatarPosition = Avatar4Steps[arrPlayers[PlayerNumber].Position];
            arrPlayers[3].AvatarPosition.Visible = true;
        }

        private void butConfirmPlayersCount_Click(object sender, EventArgs e)

        {
            byte SelctedPlayersCount = Convert.ToByte(comboBox1.SelectedItem);

            if (SelctedPlayersCount <= 0)

            {
                MessageBox.Show("Please choose a players count", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            GameStats.PlayersCount = SelctedPlayersCount;
            comboBox1.Enabled = false;
            butConfirmPlayersCount.Enabled = false;

            FillStatisticsListIndexes();
            LockUnusedPlayers();

        }

        private void txtbPlayer1_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckConfirmedPlayerCount();

        }

        private void button2_Click(object sender, EventArgs e)
        {

            CheckConfirmedPlayerCount();

            arrPlayers[0].Name = txtbPlayer1.Text;
            lbPlayerName1.Text = txtbPlayer1.Text;



            arrPlayers[1].Name = txtbPlayer2.Text;
            lbPlayerName2.Text = txtbPlayer2.Text;

            arrPlayers[2].Name = txtbPlayer3.Text;
            lbPlayerName3.Text = txtbPlayer3.Text;

            arrPlayers[3].Name = txtbPlayer4.Text;
            lbPlayerName4.Text = txtbPlayer4.Text;

            FillStatisticsListNames();

        }

        private void butConfirmAvatars_Click(object sender, EventArgs e)
        {
            CheckConfirmedPlayerCount();

            arrPlayers[0].Avatar = Avatars.Player1Avatar;
            picbPlayer1.Image = Avatars.Player1Avatar;
            Avatar1Step1.Image = arrPlayers[0].Avatar;
            arrPlayers[0].AvatarIndex = Avatars.Avatar1Index;



            arrPlayers[1].Avatar = Avatars.Player2Avatar;
            picbPlayer2.Image = Avatars.Player2Avatar;
            Avatar2Step1.Image = Avatars.Player2Avatar;
            arrPlayers[1].AvatarIndex = Avatars.Avatar2Index;


            arrPlayers[2].Avatar = Avatars.Player3Avatar;
            picbPlayer3.Image = Avatars.Player3Avatar;
            Avatar3Step1.Image = Avatars.Player3Avatar;
            arrPlayers[2].AvatarIndex = Avatars.Avatar3Index;


            arrPlayers[3].Avatar = Avatars.Player4Avatar;
            picbPlayer4.Image = Avatars.Player4Avatar;
            Avatar4Step1.Image = Avatars.Player4Avatar;
            arrPlayers[3].AvatarIndex = Avatars.Avatar4Index;

            FillStatisticsListAvatars();

        }

        private void rdbAvatar1Player1_Click(object sender, EventArgs e)
        {
            AssignPlayersAvatars1((RadioButton)sender);
        }

        private void rdbAvatar2Player2_Click(object sender, EventArgs e)
        {
            AssignPlayersAvatars2((RadioButton)sender);
        }

        private void rdbAvatar3Player3_Click(object sender, EventArgs e)
        {
            AssignPlayersAvatars3((RadioButton)sender);
        }

        private void rdbAvatar4Player4_Click(object sender, EventArgs e)
        {
            AssignPlayersAvatars4((RadioButton)sender);
        }

        bool CheckWinner()
        {
            if ((arrPlayers[Convert.ToByte(GameStats.PlayerTurn - 1)].Position + GameStats.steps) >= 32)
            {

                MessageBox.Show($"{arrPlayers[Convert.ToByte(GameStats.PlayerTurn - 1)].Name} has Won", "", MessageBoxButtons.OK);
                arrPlayers[Convert.ToByte(GameStats.PlayerTurn - 1)].Wins++;
                GameStats.Win = true;
                listView1.Items[Convert.ToByte(GameStats.PlayerTurn - 1)].SubItems[2].Text = Convert.ToString(arrPlayers[Convert.ToByte(GameStats.PlayerTurn - 1)].Wins);
                AddGameStatistics("Won the Game ", Convert.ToByte(GameStats.PlayerTurn - 1));
                ResetGame();
                return true;
            }
            else
                return false;
        }

        public void GetRandomDiceFace()
        {
            Random Rand = new Random();
            GameStats.DiceFace = (enDiceFaces)Convert.ToByte(Rand.Next(1, 6));
            GameStats.steps = Convert.ToByte(GameStats.DiceFace);
            GetDiceFace();
        }

        public void GetDiceFace()
        {
            switch (GameStats.DiceFace)
            {
                case enDiceFaces.One:
                    picbDice.Image = Resources.One;
                    break;
                case enDiceFaces.Two:
                    picbDice.Image = Resources.Two;
                    break;
                case enDiceFaces.Three:
                    picbDice.Image = Resources.Three;
                    break;
                case enDiceFaces.Four:
                    picbDice.Image = Resources.Four;
                    break;
                case enDiceFaces.Five:
                    picbDice.Image = Resources.Five;
                    break;
                case enDiceFaces.Six:
                    picbDice.Image = Resources.Six;
                    break;


            }
        }

        bool OwlMode()
        {
            Random rand = new Random();
            int QuizIndex = rand.Next(0, 69);


            if (MessageBox.Show(Quizes[QuizIndex], "Quiz", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                if (QuizIndex % 2 == 0)
                    return true;
                else
                    return false;
            }
            else
            {
                if (QuizIndex % 2 != 0)
                    return true;
                else
                    return false;
            }
        }

        bool LionMode()
        {
            Random rand = new Random();
            if (rand.Next(1, 10) % 2 == 0)
                return true;
            else
                return false;
        }

        bool HasShield(byte PlayerNumber)
        {
            return arrPlayers[PlayerNumber].shield;
        }

        void BreakShield(byte PlayerNumber)
        {
            arrPlayers[PlayerNumber].shield = false;
        }

        void MoveForward(byte PlayerNumber, byte steps)
        {

            if (CheckWinner())
                return;

            arrPlayers[PlayerNumber].Position += steps;
            AddGameStatistics($"Moved {steps} Forward ", Convert.ToByte(GameStats.PlayerTurn - 1));
        }

        void MoveBackward(byte PlayerNumber, byte steps)
        {
            if (HasShield(PlayerNumber))
            {

                if (MessageBox.Show("Lucky, you have a shield", "Shield Protection", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                
                {
                    BreakShield(PlayerNumber);
                    AddGameStatistics("Defended Attack with Shield ", Convert.ToByte(GameStats.PlayerTurn - 1));
                    return;
                }
               
            }

            arrPlayers[PlayerNumber].Position -=  steps ;
            AddGameStatistics($"Moved {steps} Backward ", Convert.ToByte(GameStats.PlayerTurn - 1));


        }

       

        void CheckMode(PictureBox AvatarPosition)
        {
            switch (Convert.ToString(AvatarPosition.Tag))
            {
                case "Bear":
                    
                   if( MessageBox.Show("Bears will attack you","",MessageBoxButtons.OK,MessageBoxIcon.Warning)
                        == DialogResult.OK)
                    {
                        MovePlayer(Convert.ToByte(GameStats.PlayerTurn - 1), 3, 'B');
                    }
                   
                    break;


                case "Eagle":
                    if (MessageBox.Show("An Eagle will carry you", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        == DialogResult.OK)
                    {
                        MovePlayer(Convert.ToByte(GameStats.PlayerTurn - 1), 3, 'F');
                    }
                    AddGameStatistics("An Eagle carried ", Convert.ToByte(GameStats.PlayerTurn - 1));

                    break;


                case "Crocodile":
                    if (MessageBox.Show("Crocodile will attack you", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        == DialogResult.OK)
                    {
                        AddGameStatistics("Crocodile Attacked ", Convert.ToByte(GameStats.PlayerTurn - 1));
                        MovePlayer(Convert.ToByte(GameStats.PlayerTurn - 1), 1, 'B');
                    }
                    
                    break;


                case "Owl":
                    if (MessageBox.Show("Owl will ask you quiz", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        == DialogResult.OK)
                    {
                        AddGameStatistics("Owl Quized ", Convert.ToByte(GameStats.PlayerTurn - 1));
                        if (!OwlMode())
                        MovePlayer(Convert.ToByte(GameStats.PlayerTurn - 1), 6, 'B');
                    }
                    
                    break;


                case "Lion":
                    if (LionMode())
                    {
                        if (MessageBox.Show("Lions will Protect you", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            == DialogResult.OK)
                        {
                            arrPlayers[Convert.ToByte(GameStats.PlayerTurn - 1)].shield = true;
                        }
                        AddGameStatistics("Lions Protected ", Convert.ToByte(GameStats.PlayerTurn - 1));
                    }
                    else
                    {
                        if (MessageBox.Show("Lions will Attack", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                    == DialogResult.OK)
                        {
                            MovePlayer(Convert.ToByte(GameStats.PlayerTurn - 1), 2,'B');
                            AddGameStatistics("Lions  Attacked ", Convert.ToByte(GameStats.PlayerTurn - 1));
                        }
                    }

                    break;
                
            }
        }
        
        public void MovePlayer(byte PlayerNumber, byte steps, char Direction)
        {
            
           byte PreviousPosition = arrPlayers[PlayerNumber].Position;

            
           if(Direction == 'F') 

                MoveForward(PlayerNumber, steps);
           else
                MoveBackward(PlayerNumber, steps);


            if (PreviousPosition == arrPlayers[PlayerNumber].Position)
                return;


            if (!GameStats.Win)

            {
                switch (PlayerNumber)
                {
                    case 0:
                        MoveAvatar1Picture(PlayerNumber, PreviousPosition);
                        break;
                    case 1:
                        MoveAvatar2Picture(PlayerNumber, PreviousPosition);
                        break;
                    case 2:
                        MoveAvatar3Picture(PlayerNumber, PreviousPosition);
                        break;
                    case 3:
                        MoveAvatar4Picture(PlayerNumber, PreviousPosition);
                        break;

                }



            }
            else
            {
                GameStats.Win = false;
            }
        }

        public void UpdatePlayer(byte PlayerNumber, byte steps)
        {
            
            MovePlayer(PlayerNumber, steps, 'F');           
            
            CheckMode(arrPlayers[PlayerNumber].AvatarPosition);

        }

        public void UpdateTurn()
        {
            arrPlayers[Convert.ToByte(GameStats.PlayerTurn - 1)].PlayerNameLabel.BackColor = Color.White;

            if (Convert.ToByte(GameStats.PlayerTurn) != GameStats.PlayersCount)

            {
                GameStats.PlayerTurn++;
                arrPlayers[Convert.ToByte(GameStats.PlayerTurn - 1)].PlayerNameLabel.BackColor = Color.Orange;

            }

            else

            {
                GameStats.PlayerTurn = enTurns.Player1;
                arrPlayers[0].PlayerNameLabel.BackColor = Color.Orange;
            }
        }

        private void btRollDice_Click(object sender, EventArgs e)
        {

            if (butConfirmPlayersCount.Enabled)

            {
                MessageBox.Show("Please Enter Players Data First", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            button3.Enabled = false;

            GameStats.Rounds++;

            GetRandomDiceFace();
            UpdatePlayer(Convert.ToByte(GameStats.PlayerTurn - 1), GameStats.steps);
            UpdateTurn();


        }

      
        private void button2_Click_1(object sender, EventArgs e)
        {
            listView2.Items.Clear();
        }

        private void button3_Click(object sender, EventArgs e)

        {
            GameStats.PlayersCount = 4;
            comboBox1.Enabled = true;
            butConfirmPlayersCount.Enabled = true;

            listView1.Items.Clear();
            UnlockPlayers();
        }

     
    }
}
