using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6cello
{
    class MiniGame
    {
        public Bord miniGameBord = new Bord();

        public MiniGame(GameMaster gm)
        {
            miniGameBord.bordInit(8, Config.MAP);

            for (int py = 0; py < miniGameBord.bordNum + 2; py++)
                for (int px = 0; px < miniGameBord.bordNum + 2; px++)
                    miniGameBord.bord[px, py] = gm.gameBord.bord[px, py];

            miniTurn = gm.turn;
            miniPlayer = gm.player;
        }

        public int miniTurn;
        public int miniPlayer;

        public int blackCornerNum()
        {
            int count = 0;

            if (miniGameBord.bord[1, 1] == 1)
                count++;
            if (miniGameBord.bord[1, miniGameBord.bordNum] == 1)
                count++;
            if (miniGameBord.bord[miniGameBord.bordNum, 1] == 1)
                count++;
            if (miniGameBord.bord[miniGameBord.bordNum, miniGameBord.bordNum] == 1)
                count++;

            return count;
        }

        public int whiteCornerNum()
        {
            int count = 0;

            if (miniGameBord.bord[1, 1] == 2)
                count++;
            if (miniGameBord.bord[1, miniGameBord.bordNum] == 2)
                count++;
            if (miniGameBord.bord[miniGameBord.bordNum, 1] == 2)
                count++;
            if (miniGameBord.bord[miniGameBord.bordNum, miniGameBord.bordNum] == 2)
                count++;

            return count;
        }

        public int miniSetPlayer()
        {
            int p = miniTurn % 2 + 1;

            miniPlayer = p;

            return p;
        }

    }
}
