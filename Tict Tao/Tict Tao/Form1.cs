using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics.SymbolStore;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tict_Tao.Properties;

namespace Tict_Tao
{

          
    public partial class Form1 : Form
    {

        public enum enGameMode { enPvp = 0, enNpc = 1 };
        enGameMode _GamMode;

        private string player01 = "Player 01";
        private string player02;

        private short totalCells;
        static private sbyte row = 3;
        static private sbyte col = 3;

        private short tries;

        private bool gameProgress = true;
        private bool pastNpcTurn = true;

        public Form1(enGameMode GameMode)
        {
            InitializeComponent();
            initiatePictureBoxArray();
            _GamMode = GameMode;
            if (_GamMode == enGameMode.enNpc){
                player02 = "Computer";
            } else player02 = "Player 02";

            totalCells = (short)(row * col); 
            randomaizeTurn();
        }
        enum enPlayer { enP1 = 0, enP2 = 1};
        enum enSymbol { O = -1, X = 1 };

        enum enEndGame { enWon = 0 , enTie = 1};

        enPlayer Turn;

        Dictionary<enPlayer, enSymbol> symbolPlayerMap = new Dictionary<enPlayer, enSymbol>();

        SortedDictionary<float, sbyte> TicTacToeList = new SortedDictionary<float, sbyte>();

        PictureBox[] pictureBoxArray = new PictureBox[col * row];

        List<PictureBox> pictureBoxList = new List<PictureBox>();

        List<int> emptyCells = new List<int>(row * col);

        private void initiatePictureBoxArray()
        {
            for (int i = 0; i < row * col; i++)
            {
                string pictureBoxName = "pb" + (i + 1); 
                FieldInfo fieldInfo = this.GetType().GetField(pictureBoxName, BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    PictureBox pictureBox = (PictureBox)fieldInfo.GetValue(this);
                    pictureBoxArray[i] = pictureBox;
                    pictureBox.Click += PictureBox_Click;
                }
            }

         }

        private void randomaizeTurn()
        {

            Random rand = new Random();
            int randomNumber = rand.Next(2);

             enPlayer chosen = (enPlayer)randomNumber;

             Turn = chosen;
            

            symbolPlayerMap[Turn] = enSymbol.X;
            symbolPlayerMap[(enPlayer)((int)Turn ^ 1)] = enSymbol.O;
           
            intiateTicTacTao();
            TurnIdentifier(Turn);
        }

        private void TurnIdentifier(enPlayer Player)
        {
            switch (Player)
            {
                case enPlayer.enP1:
                    playerTurn.Text = player01;
                    if (_GamMode == enGameMode.enNpc) pastNpcTurn = true;
                    break;
                case enPlayer.enP2:
                    playerTurn.Text = player02;
                    
                    if (_GamMode == enGameMode.enNpc)
                    {
                        pastNpcTurn = false;
                        npcPatternPicker(); 
                    }
                    break;

            }
        }

        private void intiateTicTacTao()
        {
            emptyCells.Clear();
            for (int i = 0; i < totalCells; i++)
            {
                TicTacToeList[i] = 0;
                emptyCells.Add(i);
                pictureBoxArray[i].Image = Properties.Resources.question_mark_96;
            }
            HighlightWinningCells(false);
            GameReset();
        }

        private void GameReset()
        {
            tries = (short)(totalCells - 1);
            gameProgress = true;
            gameStatus.Text = "In Progress";
        }

        private bool getGameStutes()
        {
            if (tries + row <= totalCells)
            {
                if (horizontalAuthonticator(row, col)) return true;

                if (verticalAuthonticator(row, col)) return true;

                if (diagonalAuthonticator(row, col)) return true;

            }

            if (tries-- == 0)
            {
                EndGameStatus(enEndGame.enTie);
                return true;
            }

            return false;

        }

        private bool horizontalAuthonticator(sbyte row, sbyte col)
        {
            
            for (int i = 0; i < row; i++)
            {
                sbyte sum = 0;
                bool allow = true;
                for (int j = row * i; j < (row * i) + row; j++)
                {
                    sbyte result = TicTacToeList[j];
                    if (result == 0)
                    {
                        allow = false;
                        break;
                    }
                    pictureBoxList.Add(pictureBoxArray[j]);
                    sum += result;
                }
                if (allow && (sum == row || sum == col || sum == (row * -1) || sum == (col * -1))) return winnerDecider(sum);
                else pictureBoxList.Clear();
            }

            return false;
        }

        private bool verticalAuthonticator(sbyte row, sbyte col)
        {
            for (int i = 0; i < col; i++)
            {
                int index = i;
                sbyte sum = 0;
                bool allow = true;
                for (int j = i; j <= (row * row) - row + i; j += col)
                {
                    sbyte result = TicTacToeList[j];
                    if (result == 0) { 
                        allow = false;
                        break;
                    }
                    pictureBoxList.Add(pictureBoxArray[j]);
                    sum += result;
                    index++;
                }
                if (allow && (sum == row || sum == col || sum == (row * -1) || sum == (col * -1))) return winnerDecider(sum);
                else pictureBoxList.Clear();
            }
            return false;
        }

        private bool diagonalAuthonticator(sbyte row, sbyte col)
        {
            
            int sum = 0;
            int index = 0;
            for (int i = 0; i < totalCells; i = (row * index) + index)
            {
                bool allow = true;
                sbyte result = TicTacToeList[i];
                if (result == 0)
                {
                    allow = false;
                    break;
                }
                pictureBoxList.Add(pictureBoxArray[i]);
                sum += result;
                index++;

                if (allow && (sum == row || sum == col || sum == (row * -1) || sum == (col * -1))) return winnerDecider(sum);
            }
            pictureBoxList.Clear();
            sum = 0;
            index = row - 1;
            for (int i = index; i <= totalCells - col; i += index )
            {
                bool allow = true;
                sbyte result = TicTacToeList[i];
                if (result == 0)
                {
                    allow = false;
                    break;
                }
                pictureBoxList.Add(pictureBoxArray[i]);
                sum += result;

                if (allow && (sum == row || sum == col || sum == (row * -1) || sum == (col * -1))) return winnerDecider(sum);
            }
            pictureBoxList.Clear();
            return false;
        }

        private void HighlightWinningCells(bool switcher)
        {
            if(switcher) {
                for (int i = 0; i < pictureBoxList.Count; i++)
                {
                    pictureBoxList[i].BackColor = Color.Lime;
                }
                return;
            }

            for (int i = 0; i < pictureBoxList.Count; i++)
            {
                pictureBoxList[i].BackColor = Color.Black;
            }
            pictureBoxList.Clear();
        }

        private bool winnerDecider(int sum)
        {

            if (sum == row || sum == col)
            {
                enPlayer winner = symbolPlayerMap.FirstOrDefault(x => x.Value == enSymbol.X).Key;
                EndGameStatus(enEndGame.enWon, winner);
                return true;
            }

            if (sum == (row * -1) || sum == (col * -1) ) 
            {
                enPlayer winner = symbolPlayerMap.FirstOrDefault(x => x.Value == enSymbol.O).Key;
                EndGameStatus(enEndGame.enWon, winner);
                return true;
            }

            return false;
        }

        private void NextTurn()
        {
            Turn = (enPlayer)((int)Turn ^ 1);
            TurnIdentifier(Turn);
        }

        private void EndGameStatus(enEndGame result, enPlayer? winner = null)
        {
            switch (result)
            {
                case enEndGame.enTie:
                    gameStatus.Text = "   Tie!";
                    break;
                case enEndGame.enWon:
                    if(winner == enPlayer.enP1) gameStatus.Text = player01;
                    else gameStatus.Text = player02;
                    break;
            }

            playerTurn.Text = "Game Over";
            gameProgress = false;
            HighlightWinningCells(true);
        }
            
        private void SymbolIdentifier(PictureBox clickedPictureBox, enSymbol symbol)
        {

            if(pastNpcTurn && patternOrganaizor(clickedPictureBox) == 0) return;

            switch (symbol)
            {
                case enSymbol.X:
                    clickedPictureBox.Image = Properties.Resources.X;
                    break;
                case enSymbol.O:
                    clickedPictureBox.Image = Properties.Resources.O;
                    break;
            }

            if (getGameStutes()) return;

            NextTurn();
        }

        private short patternOrganaizor(PictureBox clickedPictureBox)
        {
            float index = Convert.ToSingle(clickedPictureBox.Tag);

            if (TicTacToeList[index] != 0) return 0;
            TicTacToeList[index] = (sbyte)symbolPlayerMap[Turn];

            int cell = emptyCells.IndexOf((int)index);
            emptyCells.RemoveAt(cell);

            return -1;
        }

        private void npcPatternPicker()
        {
            Random rand = new Random();
            int index;

            if(emptyCells.Count == 2) index = rand.Next(2);
            else if(emptyCells.Count == 1) index = 0;
            else index = rand.Next(emptyCells.Count);

            TicTacToeList[emptyCells[index]] = (sbyte)symbolPlayerMap[Turn];
            int chosenCell = emptyCells[index];
            emptyCells.RemoveAt(index);

            SymbolIdentifier(pictureBoxArray[chosenCell], symbolPlayerMap[Turn]);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color white = Color.Snow;

            Pen pen = new Pen(white);
            pen.Width = 10;

            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            e.Graphics.DrawLine(pen, 400, 100, 400, 400);

            e.Graphics.DrawLine(pen, 300, 200, 650, 200);

            e.Graphics.DrawLine(pen, 550, 100, 550, 400);

            e.Graphics.DrawLine(pen, 300, 310, 650, 310);

        }   

        private void PictureBox_Click(object sender, EventArgs e)
        {
            if (pastNpcTurn && gameProgress) SymbolIdentifier((PictureBox)sender, symbolPlayerMap[Turn]);
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            randomaizeTurn();
        }
    }
}


