using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _6cello
{
    public partial class Form1 : Form
    {
        GameMaster GM = new GameMaster();

        public Form1()
        {
            InitializeComponent();

            colorInit();

            playerLabelSet();
        }

        void playerLabelSet()
        {
            if (GM.player == 1)
            {
                plabel.BackColor = Color.Black;
                plabel.ForeColor = Color.White;
                plabel.Text = "Player1";
            }
            else
            {
                plabel.BackColor = Color.White;
                plabel.ForeColor = Color.Black;
                plabel.Text = "Player2";
            }

            lbc.Text = GM.blackCount.ToString();
            lwc.Text = GM.whiteCount.ToString();

            lbwc.Text = GM.bwCount.ToString();
            lwwc.Text = GM.wwCount.ToString();
            lbwr.Text = ((int)(100.0f * (float)GM.bwCount / (float)(GM.bwCount + GM.wwCount))).ToString() + "%";
            lwwr.Text = ((int)(100.0f * (float)GM.wwCount / (float)(GM.bwCount + GM.wwCount))).ToString() + "%";
        }

        void colorInit()
        {
            #region BT11
            if (GM.gameBord.bord[1, 1] == 0)
                bt11.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[1, 1] == 1)
                bt11.BackColor = Color.Black;
            else if (GM.gameBord.bord[1, 1] == 2)
                bt11.BackColor = Color.White;
            else
                bt11.BackColor = Color.Gray;
            #endregion
            #region BT12
            if (GM.gameBord.bord[1, 2] == 0)
                bt12.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[1, 2] == 1)
                bt12.BackColor = Color.Black;
            else if (GM.gameBord.bord[1, 2] == 2)
                bt12.BackColor = Color.White;
            else
                bt12.BackColor = Color.Gray;
            #endregion
            #region BT13
            if (GM.gameBord.bord[1, 3] == 0)
                bt13.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[1, 3] == 1)
                bt13.BackColor = Color.Black;
            else if (GM.gameBord.bord[1, 3] == 2)
                bt13.BackColor = Color.White;
            else
                bt13.BackColor = Color.Gray;
            #endregion
            #region BT14
            if (GM.gameBord.bord[1, 4] == 0)
                bt14.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[1, 4] == 1)
                bt14.BackColor = Color.Black;
            else if (GM.gameBord.bord[1, 4] == 2)
                bt14.BackColor = Color.White;
            else
                bt14.BackColor = Color.Gray;
            #endregion
            #region BT15
            if (GM.gameBord.bord[1, 5] == 0)
                bt15.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[1, 5] == 1)
                bt15.BackColor = Color.Black;
            else if (GM.gameBord.bord[1, 5] == 2)
                bt15.BackColor = Color.White;
            else
                bt15.BackColor = Color.Gray;
            #endregion
            #region BT16
            if (GM.gameBord.bord[1, 6] == 0)
                bt16.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[1, 6] == 1)
                bt16.BackColor = Color.Black;
            else if (GM.gameBord.bord[1, 6] == 2)
                bt16.BackColor = Color.White;
            else
                bt16.BackColor = Color.Gray;
            #endregion
            #region BT17
            if (GM.gameBord.bord[1, 7] == 0)
                bt17.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[1, 7] == 1)
                bt17.BackColor = Color.Black;
            else if (GM.gameBord.bord[1, 7] == 2)
                bt17.BackColor = Color.White;
            else
                bt17.BackColor = Color.Gray;
            #endregion
            #region BT18
            if (GM.gameBord.bord[1, 8] == 0)
                bt18.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[1, 8] == 1)
                bt18.BackColor = Color.Black;
            else if (GM.gameBord.bord[1, 8] == 2)
                bt18.BackColor = Color.White;
            else
                bt18.BackColor = Color.Gray;
            #endregion

            #region BT21
            if (GM.gameBord.bord[2, 1] == 0)
                bt21.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[2, 1] == 1)
                bt21.BackColor = Color.Black;
            else if (GM.gameBord.bord[2, 1] == 2)
                bt21.BackColor = Color.White;
            else
                bt21.BackColor = Color.Gray;
            #endregion
            #region BT22
            if (GM.gameBord.bord[2, 2] == 0)
                bt22.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[2, 2] == 1)
                bt22.BackColor = Color.Black;
            else if (GM.gameBord.bord[2, 2] == 2)
                bt22.BackColor = Color.White;
            else
                bt22.BackColor = Color.Gray;
            #endregion
            #region BT23
            if (GM.gameBord.bord[2, 3] == 0)
                bt23.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[2, 3] == 1)
                bt23.BackColor = Color.Black;
            else if (GM.gameBord.bord[2, 3] == 2)
                bt23.BackColor = Color.White;
            else
                bt23.BackColor = Color.Gray;
            #endregion
            #region BT24
            if (GM.gameBord.bord[2, 4] == 0)
                bt24.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[2, 4] == 1)
                bt24.BackColor = Color.Black;
            else if (GM.gameBord.bord[2, 4] == 2)
                bt24.BackColor = Color.White;
            else
                bt24.BackColor = Color.Gray;
            #endregion
            #region BT25
            if (GM.gameBord.bord[2, 5] == 0)
                bt25.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[2, 5] == 1)
                bt25.BackColor = Color.Black;
            else if (GM.gameBord.bord[2, 5] == 2)
                bt25.BackColor = Color.White;
            else
                bt25.BackColor = Color.Gray;
            #endregion
            #region BT26
            if (GM.gameBord.bord[2, 6] == 0)
                bt26.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[2, 6] == 1)
                bt26.BackColor = Color.Black;
            else if (GM.gameBord.bord[2, 6] == 2)
                bt26.BackColor = Color.White;
            else
                bt26.BackColor = Color.Gray;
            #endregion   
            #region BT27
            if (GM.gameBord.bord[2, 7] == 0)
                bt27.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[2, 7] == 1)
                bt27.BackColor = Color.Black;
            else if (GM.gameBord.bord[2, 7] == 2)
                bt27.BackColor = Color.White;
            else
                bt27.BackColor = Color.Gray;
            #endregion
            #region BT28
            if (GM.gameBord.bord[2, 8] == 0)
                bt28.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[2, 8] == 1)
                bt28.BackColor = Color.Black;
            else if (GM.gameBord.bord[2, 8] == 2)
                bt28.BackColor = Color.White;
            else
                bt28.BackColor = Color.Gray;
            #endregion

            #region BT31
            if (GM.gameBord.bord[3, 1] == 0)
                bt31.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[3, 1] == 1)
                bt31.BackColor = Color.Black;
            else if (GM.gameBord.bord[3, 1] == 2)
                bt31.BackColor = Color.White;
            else
                bt31.BackColor = Color.Gray;
            #endregion
            #region BT32
            if (GM.gameBord.bord[3, 2] == 0)
                bt32.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[3, 2] == 1)
                bt32.BackColor = Color.Black;
            else if (GM.gameBord.bord[3, 2] == 2)
                bt32.BackColor = Color.White;
            else
                bt32.BackColor = Color.Gray;
            #endregion
            #region BT33
            if (GM.gameBord.bord[3, 3] == 0)
                bt33.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[3, 3] == 1)
                bt33.BackColor = Color.Black;
            else if (GM.gameBord.bord[3, 3] == 2)
                bt33.BackColor = Color.White;
            else
                bt33.BackColor = Color.Gray;
            #endregion
            #region BT34
            if (GM.gameBord.bord[3, 4] == 0)
                bt34.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[3, 4] == 1)
                bt34.BackColor = Color.Black;
            else if (GM.gameBord.bord[3, 4] == 2)
                bt34.BackColor = Color.White;
            else
                bt34.BackColor = Color.Gray;
            #endregion
            #region BT35
            if (GM.gameBord.bord[3, 5] == 0)
                bt35.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[3, 5] == 1)
                bt35.BackColor = Color.Black;
            else if (GM.gameBord.bord[3, 5] == 2)
                bt35.BackColor = Color.White;
            else
                bt35.BackColor = Color.Gray;
            #endregion
            #region BT36
            if (GM.gameBord.bord[3, 6] == 0)
                bt36.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[3, 6] == 1)
                bt36.BackColor = Color.Black;
            else if (GM.gameBord.bord[3, 6] == 2)
                bt36.BackColor = Color.White;
            else
                bt36.BackColor = Color.Gray;
            #endregion     
            #region BT37
            if (GM.gameBord.bord[3, 7] == 0)
                bt37.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[3, 7] == 1)
                bt37.BackColor = Color.Black;
            else if (GM.gameBord.bord[3, 7] == 2)
                bt37.BackColor = Color.White;
            else
                bt37.BackColor = Color.Gray;
            #endregion
            #region BT38
            if (GM.gameBord.bord[3, 8] == 0)
                bt38.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[3, 8] == 1)
                bt38.BackColor = Color.Black;
            else if (GM.gameBord.bord[3, 8] == 2)
                bt38.BackColor = Color.White;
            else
                bt38.BackColor = Color.Gray;
            #endregion

            #region BT41
            if (GM.gameBord.bord[4, 1] == 0)
                bt41.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[4, 1] == 1)
                bt41.BackColor = Color.Black;
            else if (GM.gameBord.bord[4, 1] == 2)
                bt41.BackColor = Color.White;
            else
                bt41.BackColor = Color.Gray;
            #endregion
            #region BT42
            if (GM.gameBord.bord[4, 2] == 0)
                bt42.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[4, 2] == 1)
                bt42.BackColor = Color.Black;
            else if (GM.gameBord.bord[4, 2] == 2)
                bt42.BackColor = Color.White;
            else
                bt42.BackColor = Color.Gray;
            #endregion
            #region BT43
            if (GM.gameBord.bord[4, 3] == 0)
                bt43.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[4, 3] == 1)
                bt43.BackColor = Color.Black;
            else if (GM.gameBord.bord[4, 3] == 2)
                bt43.BackColor = Color.White;
            else
                bt43.BackColor = Color.Gray;
            #endregion
            #region BT44
            if (GM.gameBord.bord[4, 4] == 0)
                bt44.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[4, 4] == 1)
                bt44.BackColor = Color.Black;
            else if (GM.gameBord.bord[4, 4] == 2)
                bt44.BackColor = Color.White;
            else
                bt44.BackColor = Color.Gray;
            #endregion
            #region BT45
            if (GM.gameBord.bord[4, 5] == 0)
                bt45.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[4, 5] == 1)
                bt45.BackColor = Color.Black;
            else if (GM.gameBord.bord[4, 5] == 2)
                bt45.BackColor = Color.White;
            else
                bt45.BackColor = Color.Gray;
            #endregion
            #region BT46
            if (GM.gameBord.bord[4, 6] == 0)
                bt46.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[4, 6] == 1)
                bt46.BackColor = Color.Black;
            else if (GM.gameBord.bord[4, 6] == 2)
                bt46.BackColor = Color.White;
            else
                bt46.BackColor = Color.Gray;
            #endregion
            #region BT47
            if (GM.gameBord.bord[4, 7] == 0)
                bt47.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[4, 7] == 1)
                bt47.BackColor = Color.Black;
            else if (GM.gameBord.bord[4, 7] == 2)
                bt47.BackColor = Color.White;
            else
                bt47.BackColor = Color.Gray;
            #endregion
            #region BT48
            if (GM.gameBord.bord[4, 8] == 0)
                bt48.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[4, 8] == 1)
                bt48.BackColor = Color.Black;
            else if (GM.gameBord.bord[4, 8] == 2)
                bt48.BackColor = Color.White;
            else
                bt48.BackColor = Color.Gray;
            #endregion

            #region BT51
            if (GM.gameBord.bord[5, 1] == 0)
                bt51.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[5, 1] == 1)
                bt51.BackColor = Color.Black;
            else if (GM.gameBord.bord[5, 1] == 2)
                bt51.BackColor = Color.White;
            else
                bt51.BackColor = Color.Gray;
            #endregion
            #region BT52
            if (GM.gameBord.bord[5, 2] == 0)
                bt52.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[5, 2] == 1)
                bt52.BackColor = Color.Black;
            else if (GM.gameBord.bord[5, 2] == 2)
                bt52.BackColor = Color.White;
            else
                bt52.BackColor = Color.Gray;
            #endregion
            #region BT53
            if (GM.gameBord.bord[5, 3] == 0)
                bt53.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[5, 3] == 1)
                bt53.BackColor = Color.Black;
            else if (GM.gameBord.bord[5, 3] == 2)
                bt53.BackColor = Color.White;
            else
                bt53.BackColor = Color.Gray;
            #endregion
            #region BT54
            if (GM.gameBord.bord[5, 4] == 0)
                bt54.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[5, 4] == 1)
                bt54.BackColor = Color.Black;
            else if (GM.gameBord.bord[5, 4] == 2)
                bt54.BackColor = Color.White;
            else
                bt54.BackColor = Color.Gray;
            #endregion
            #region BT55
            if (GM.gameBord.bord[5, 5] == 0)
                bt55.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[5, 5] == 1)
                bt55.BackColor = Color.Black;
            else if (GM.gameBord.bord[5, 5] == 2)
                bt55.BackColor = Color.White;
            else
                bt55.BackColor = Color.Gray;
            #endregion
            #region BT56
            if (GM.gameBord.bord[5, 6] == 0)
                bt56.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[5, 6] == 1)
                bt56.BackColor = Color.Black;
            else if (GM.gameBord.bord[5, 6] == 2)
                bt56.BackColor = Color.White;
            else
                bt56.BackColor = Color.Gray;
            #endregion
            #region BT57
            if (GM.gameBord.bord[5, 7] == 0)
                bt57.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[5, 7] == 1)
                bt57.BackColor = Color.Black;
            else if (GM.gameBord.bord[5, 7] == 2)
                bt57.BackColor = Color.White;
            else
                bt57.BackColor = Color.Gray;
            #endregion
            #region BT58
            if (GM.gameBord.bord[5, 8] == 0)
                bt58.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[5, 8] == 1)
                bt58.BackColor = Color.Black;
            else if (GM.gameBord.bord[5, 8] == 2)
                bt58.BackColor = Color.White;
            else
                bt58.BackColor = Color.Gray;
            #endregion

            #region BT61
            if (GM.gameBord.bord[6, 1] == 0)
                bt61.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[6, 1] == 1)
                bt61.BackColor = Color.Black;
            else if (GM.gameBord.bord[6, 1] == 2)
                bt61.BackColor = Color.White;
            else
                bt61.BackColor = Color.Gray;
            #endregion
            #region BT62
            if (GM.gameBord.bord[6, 2] == 0)
                bt62.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[6, 2] == 1)
                bt62.BackColor = Color.Black;
            else if (GM.gameBord.bord[6, 2] == 2)
                bt62.BackColor = Color.White;
            else
                bt62.BackColor = Color.Gray;
            #endregion
            #region BT63
            if (GM.gameBord.bord[6, 3] == 0)
                bt63.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[6, 3] == 1)
                bt63.BackColor = Color.Black;
            else if (GM.gameBord.bord[6, 3] == 2)
                bt63.BackColor = Color.White;
            else
                bt63.BackColor = Color.Gray;
            #endregion
            #region BT64
            if (GM.gameBord.bord[6, 4] == 0)
                bt64.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[6, 4] == 1)
                bt64.BackColor = Color.Black;
            else if (GM.gameBord.bord[6, 4] == 2)
                bt64.BackColor = Color.White;
            else
                bt64.BackColor = Color.Gray;
            #endregion
            #region BT65
            if (GM.gameBord.bord[6, 5] == 0)
                bt65.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[6, 5] == 1)
                bt65.BackColor = Color.Black;
            else if (GM.gameBord.bord[6, 5] == 2)
                bt65.BackColor = Color.White;
            else
                bt65.BackColor = Color.Gray;
            #endregion
            #region BT66
            if (GM.gameBord.bord[6, 6] == 0)
                bt66.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[6, 6] == 1)
                bt66.BackColor = Color.Black;
            else if (GM.gameBord.bord[6, 6] == 2)
                bt66.BackColor = Color.White;
            else
                bt66.BackColor = Color.Gray;
            #endregion
            #region BT67
            if (GM.gameBord.bord[6, 7] == 0)
                bt67.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[6, 7] == 1)
                bt67.BackColor = Color.Black;
            else if (GM.gameBord.bord[6, 7] == 2)
                bt67.BackColor = Color.White;
            else
                bt67.BackColor = Color.Gray;
            #endregion
            #region BT68
            if (GM.gameBord.bord[6, 8] == 0)
                bt68.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[6, 8] == 1)
                bt68.BackColor = Color.Black;
            else if (GM.gameBord.bord[6, 8] == 2)
                bt68.BackColor = Color.White;
            else
                bt68.BackColor = Color.Gray;
            #endregion

            #region BT71
            if (GM.gameBord.bord[7, 1] == 0)
                bt71.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[7, 1] == 1)
                bt71.BackColor = Color.Black;
            else if (GM.gameBord.bord[7, 1] == 2)
                bt71.BackColor = Color.White;
            else
                bt71.BackColor = Color.Gray;
            #endregion
            #region BT72
            if (GM.gameBord.bord[7, 2] == 0)
                bt72.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[7, 2] == 1)
                bt72.BackColor = Color.Black;
            else if (GM.gameBord.bord[7, 2] == 2)
                bt72.BackColor = Color.White;
            else
                bt72.BackColor = Color.Gray;
            #endregion
            #region BT73
            if (GM.gameBord.bord[7, 3] == 0)
                bt73.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[7, 3] == 1)
                bt73.BackColor = Color.Black;
            else if (GM.gameBord.bord[7, 3] == 2)
                bt73.BackColor = Color.White;
            else
                bt73.BackColor = Color.Gray;
            #endregion
            #region BT74
            if (GM.gameBord.bord[7, 4] == 0)
                bt74.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[7, 4] == 1)
                bt74.BackColor = Color.Black;
            else if (GM.gameBord.bord[7, 4] == 2)
                bt74.BackColor = Color.White;
            else
                bt74.BackColor = Color.Gray;
            #endregion
            #region BT75
            if (GM.gameBord.bord[7, 5] == 0)
                bt75.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[7, 5] == 1)
                bt75.BackColor = Color.Black;
            else if (GM.gameBord.bord[7, 5] == 2)
                bt75.BackColor = Color.White;
            else
                bt75.BackColor = Color.Gray;
            #endregion
            #region BT76
            if (GM.gameBord.bord[7, 6] == 0)
                bt76.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[7, 6] == 1)
                bt76.BackColor = Color.Black;
            else if (GM.gameBord.bord[7, 6] == 2)
                bt76.BackColor = Color.White;
            else
                bt76.BackColor = Color.Gray;
            #endregion
            #region BT77
            if (GM.gameBord.bord[7, 7] == 0)
                bt77.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[7, 7] == 1)
                bt77.BackColor = Color.Black;
            else if (GM.gameBord.bord[7, 7] == 2)
                bt77.BackColor = Color.White;
            else
                bt77.BackColor = Color.Gray;
            #endregion
            #region BT78
            if (GM.gameBord.bord[7, 8] == 0)
                bt78.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[7, 8] == 1)
                bt78.BackColor = Color.Black;
            else if (GM.gameBord.bord[7, 8] == 2)
                bt78.BackColor = Color.White;
            else
                bt78.BackColor = Color.Gray;
            #endregion

            #region BT81
            if (GM.gameBord.bord[8, 1] == 0)
                bt81.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[8, 1] == 1)
                bt81.BackColor = Color.Black;
            else if (GM.gameBord.bord[8, 1] == 2)
                bt81.BackColor = Color.White;
            else
                bt81.BackColor = Color.Gray;
            #endregion
            #region BT82
            if (GM.gameBord.bord[8, 2] == 0)
                bt82.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[8, 2] == 1)
                bt82.BackColor = Color.Black;
            else if (GM.gameBord.bord[8, 2] == 2)
                bt82.BackColor = Color.White;
            else
                bt82.BackColor = Color.Gray;
            #endregion
            #region BT83
            if (GM.gameBord.bord[8, 3] == 0)
                bt83.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[8, 3] == 1)
                bt83.BackColor = Color.Black;
            else if (GM.gameBord.bord[8, 3] == 2)
                bt83.BackColor = Color.White;
            else
                bt83.BackColor = Color.Gray;
            #endregion
            #region BT84
            if (GM.gameBord.bord[8, 4] == 0)
                bt84.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[8, 4] == 1)
                bt84.BackColor = Color.Black;
            else if (GM.gameBord.bord[8, 4] == 2)
                bt84.BackColor = Color.White;
            else
                bt84.BackColor = Color.Gray;
            #endregion
            #region BT85
            if (GM.gameBord.bord[8, 5] == 0)
                bt85.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[8, 5] == 1)
                bt85.BackColor = Color.Black;
            else if (GM.gameBord.bord[8, 5] == 2)
                bt85.BackColor = Color.White;
            else
                bt85.BackColor = Color.Gray;
            #endregion
            #region BT86
            if (GM.gameBord.bord[8, 6] == 0)
                bt86.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[8, 6] == 1)
                bt86.BackColor = Color.Black;
            else if (GM.gameBord.bord[8, 6] == 2)
                bt86.BackColor = Color.White;
            else
                bt86.BackColor = Color.Gray;
            #endregion
            #region BT87
            if (GM.gameBord.bord[8, 7] == 0)
                bt87.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[8, 7] == 1)
                bt87.BackColor = Color.Black;
            else if (GM.gameBord.bord[8, 7] == 2)
                bt87.BackColor = Color.White;
            else
                bt87.BackColor = Color.Gray;
            #endregion
            #region BT88
            if (GM.gameBord.bord[8, 8] == 0)
                bt88.BackColor = Color.DarkGreen;
            else if (GM.gameBord.bord[8, 8] == 1)
                bt88.BackColor = Color.Black;
            else if (GM.gameBord.bord[8, 8] == 2)
                bt88.BackColor = Color.White;
            else
                bt88.BackColor = Color.Gray;
            #endregion
        }

        void btClick(Coord pos)
        {
            GM.putStone(pos);

            colorInit();

            playerLabelSet();
        }

        #region BUTTONCLICK
        private void bt11_Click(object sender, EventArgs e)
        {
            btClick(new Coord(1, 1));
        }
        private void bt21_Click(object sender, EventArgs e)
        {
            btClick(new Coord(2, 1));
        }
        private void bt31_Click(object sender, EventArgs e)
        {
            btClick(new Coord(3, 1));
        }
        private void bt41_Click(object sender, EventArgs e)
        {
            btClick(new Coord(4, 1));
        }
        private void bt51_Click(object sender, EventArgs e)
        {
            btClick(new Coord(5, 1));
        }
        private void bt61_Click(object sender, EventArgs e)
        {
            btClick(new Coord(6, 1));
        }
        private void bt71_Click(object sender, EventArgs e)
        {
            btClick(new Coord(7, 1));
        }
        private void bt81_Click(object sender, EventArgs e)
        {
            btClick(new Coord(8, 1));
        }

        private void bt12_Click(object sender, EventArgs e)
        {
            btClick(new Coord(1, 2));
        }
        private void bt22_Click(object sender, EventArgs e)
        {
            btClick(new Coord(2, 2));
        }
        private void bt32_Click(object sender, EventArgs e)
        {
            btClick(new Coord(3, 2));
        }
        private void bt42_Click(object sender, EventArgs e)
        {
            btClick(new Coord(4, 2));
        }
        private void bt52_Click(object sender, EventArgs e)
        {
            btClick(new Coord(5, 2));
        }
        private void bt62_Click(object sender, EventArgs e)
        {
            btClick(new Coord(6, 2));
        }
        private void bt72_Click(object sender, EventArgs e)
        {
            btClick(new Coord(7, 2));
        }
        private void bt82_Click(object sender, EventArgs e)
        {
            btClick(new Coord(8, 2));
        }

        private void bt13_Click(object sender, EventArgs e)
        {
            btClick(new Coord(1, 3));
        }
        private void bt23_Click(object sender, EventArgs e)
        {
            btClick(new Coord(2, 3));
        }
        private void bt33_Click(object sender, EventArgs e)
        {
            btClick(new Coord(3, 3));
        }
        private void bt43_Click(object sender, EventArgs e)
        {
            btClick(new Coord(4, 3));
        }
        private void bt53_Click(object sender, EventArgs e)
        {
            btClick(new Coord(5, 3));
        }
        private void bt63_Click(object sender, EventArgs e)
        {
            btClick(new Coord(6, 3));
        }
        private void bt73_Click(object sender, EventArgs e)
        {
            btClick(new Coord(7, 3));
        }
        private void bt83_Click(object sender, EventArgs e)
        {
            btClick(new Coord(8, 3));
        }

        private void bt14_Click(object sender, EventArgs e)
        {
            btClick(new Coord(1, 4));
        }
        private void bt24_Click(object sender, EventArgs e)
        {
            btClick(new Coord(2, 4));
        }
        private void bt34_Click(object sender, EventArgs e)
        {
            btClick(new Coord(3, 4));
        }
        private void bt44_Click(object sender, EventArgs e)
        {
            btClick(new Coord(4, 4));
        }
        private void bt54_Click(object sender, EventArgs e)
        {
            btClick(new Coord(5, 4));
        }
        private void bt64_Click(object sender, EventArgs e)
        {
            btClick(new Coord(6, 4));
        }
        private void bt74_Click(object sender, EventArgs e)
        {
            btClick(new Coord(7, 4));
        }
        private void bt84_Click(object sender, EventArgs e)
        {
            btClick(new Coord(8, 4));
        }

        private void bt15_Click(object sender, EventArgs e)
        {
            btClick(new Coord(1, 5));
        }
        private void bt25_Click(object sender, EventArgs e)
        {
            btClick(new Coord(2, 5));
        }
        private void bt35_Click(object sender, EventArgs e)
        {
            btClick(new Coord(3, 5));
        }
        private void bt45_Click(object sender, EventArgs e)
        {
            btClick(new Coord(4, 5));
        }
        private void bt55_Click(object sender, EventArgs e)
        {
            btClick(new Coord(5, 5));
        }
        private void bt65_Click(object sender, EventArgs e)
        {
            btClick(new Coord(6, 5));
        }
        private void bt75_Click(object sender, EventArgs e)
        {
            btClick(new Coord(7, 5));
        }
        private void bt85_Click(object sender, EventArgs e)
        {
            btClick(new Coord(8, 5));
        }

        private void bt16_Click(object sender, EventArgs e)
        {
            btClick(new Coord(1, 6));
        }
        private void bt26_Click(object sender, EventArgs e)
        {
            btClick(new Coord(2, 6));
        }
        private void bt36_Click(object sender, EventArgs e)
        {
            btClick(new Coord(3, 6));
        }
        private void bt46_Click(object sender, EventArgs e)
        {
            btClick(new Coord(4, 6));
        }
        private void bt56_Click(object sender, EventArgs e)
        {
            btClick(new Coord(5, 6));
        }
        private void bt66_Click(object sender, EventArgs e)
        {
            btClick(new Coord(6, 6));
        }
        private void bt76_Click(object sender, EventArgs e)
        {
            btClick(new Coord(7, 6));
        }
        private void bt86_Click(object sender, EventArgs e)
        {
            btClick(new Coord(8, 6));
        }

        private void bt17_Click(object sender, EventArgs e)
        {
            btClick(new Coord(1, 7));
        }
        private void bt27_Click(object sender, EventArgs e)
        {
            btClick(new Coord(2, 7));
        }
        private void bt37_Click(object sender, EventArgs e)
        {
            btClick(new Coord(3, 7));
        }
        private void bt47_Click(object sender, EventArgs e)
        {
            btClick(new Coord(4, 7));
        }
        private void bt57_Click(object sender, EventArgs e)
        {
            btClick(new Coord(5, 7));
        }
        private void bt67_Click(object sender, EventArgs e)
        {
            btClick(new Coord(6, 7));
        }
        private void bt77_Click(object sender, EventArgs e)
        {
            btClick(new Coord(7, 7));
        }
        private void bt87_Click(object sender, EventArgs e)
        {
            btClick(new Coord(8, 7));
        }

        private void bt18_Click(object sender, EventArgs e)
        {
            btClick(new Coord(1, 8));
        }
        private void bt28_Click(object sender, EventArgs e)
        {
            btClick(new Coord(2, 8));
        }
        private void bt38_Click(object sender, EventArgs e)
        {
            btClick(new Coord(3, 8));
        }
        private void bt48_Click(object sender, EventArgs e)
        {
            btClick(new Coord(4, 8));
        }
        private void bt58_Click(object sender, EventArgs e)
        {
            btClick(new Coord(5, 8));
        }
        private void bt68_Click(object sender, EventArgs e)
        {
            btClick(new Coord(6, 8));
        }
        private void bt78_Click(object sender, EventArgs e)
        {
            btClick(new Coord(7, 8));
        }
        private void bt88_Click(object sender, EventArgs e)
        {
            btClick(new Coord(8, 8));
        }
        #endregion

        private void brst_Click(object sender, EventArgs e)
        {
            GM.GameInit();

            if (cb1pcom.Checked)
                GM.putComStone(2);

            playerLabelSet();
            colorInit();
        }

        private void cb2pcom_CheckedChanged(object sender, EventArgs e)
        {
            Config.com2p = cb2pcom.Checked;

            if (cb2pcom.Checked)
                if (GM.player == 2)
                {
                    GM.putComStone(Config.COMLV2);
                    playerLabelSet();
                    colorInit();
                }
        }

        private void cb1pcom_CheckedChanged(object sender, EventArgs e)
        {
            Config.com1p = cb1pcom.Checked;

            if (cb1pcom.Checked)
                if (GM.player == 1)
                {
                    GM.putComStone(Config.COMLV1);
                    playerLabelSet();
                    colorInit();
                }
        }

        private void bai_Click(object sender, EventArgs e)
        {
            int num;

            try { num = int.Parse(tbAinum.Text); }
            catch { num = 2048; }

            GM.ai2(num);
            playerLabelSet();
            colorInit();
        }

        private void tb1pComLv_Scroll(object sender, EventArgs e)
        {
            Config.COMLV1 = tb1pComLv.Value;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Config.COMLV2 = tb2pComLv.Value;
        }

        private void btAuto_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < 100; i++)
            {
                GM.GameInit();

                if (cb1pcom.Checked)
                    GM.putComStone(2);

                playerLabelSet();
                colorInit();

                this.Refresh();
            }
        }
    }

}
