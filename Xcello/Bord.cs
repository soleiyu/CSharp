using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6cello
{
    class Bord
    {
        /* ----- RULE -----
         -1 NOT AVAILABLE
          0 NO USE
          1 BLACK STONE : P1
          2 WHITE STONE : P2 
          ----- ----- -----*/

        public int[,] bord;
        public int bordNum;

        public void bordInit(int num, int map)
        {
            if (map == 1)
                bordInit(num);
            else if (map == 2)
                bordInitMap2(num);
            else if (map == 3)
                bordInitMap3(num);
        }

        public void bordInit(int num)
        {
            bordNum = num;

            bord = new int[bordNum + 2, bordNum + 2];

            for (int y = 0; y < bordNum + 2; y++)
                for (int x = 0; x < bordNum + 2; x++)
                {
                    if (x == 0)
                        bord[x, y] = -1;
                    else if (x == bordNum + 1)
                        bord[x, y] = -1;
                    else if (y == 0)
                        bord[x, y] = -1;
                    else if (y == bordNum + 1)
                        bord[x, y] = -1;
                    else
                        bord[x, y] = 0;
                }

            //DEFAULT SET
            bord[bordNum / 2, bordNum / 2] = 2;
            bord[bordNum / 2 + 1, bordNum / 2] = 1;
            bord[bordNum / 2, bordNum / 2 + 1] = 1;
            bord[bordNum / 2 + 1, bordNum / 2 + 1] = 2;
        }
        
        public void bordInitMap2(int num)
        {
            bordNum = num;

            bord = new int[bordNum + 2, bordNum + 2];

            for (int y = 0; y < bordNum + 2; y++)
                for (int x = 0; x < bordNum + 2; x++)
                {
                    if (x == 0)
                        bord[x, y] = -1;
                    else if (x == bordNum + 1)
                        bord[x, y] = -1;
                    else if (y == 0)
                        bord[x, y] = -1;
                    else if (y == bordNum + 1)
                        bord[x, y] = -1;
                    else if (x == 1 && y == 1)
                        bord[x, y] = -1;
                    else if (x == 1 && y == bordNum)
                        bord[x, y] = -1;
                    else if (x == bordNum && y == 1)
                        bord[x, y] = -1;
                    else if (x == bordNum && y == bordNum)
                        bord[x, y] = -1;
                    else
                        bord[x, y] = 0;
                }

            //DEFAULT SET
            bord[bordNum / 2, bordNum / 2] = 2;
            bord[bordNum / 2 + 1, bordNum / 2] = 1;
            bord[bordNum / 2, bordNum / 2 + 1] = 1;
            bord[bordNum / 2 + 1, bordNum / 2 + 1] = 2;
        }
        
        public void bordInitMap3(int num)
        {
            bordNum = num;

            bord = new int[bordNum + 2, bordNum + 2];

            for (int y = 0; y < bordNum + 2; y++)
                for (int x = 0; x < bordNum + 2; x++)
                {
                    if (x == 0)
                        bord[x, y] = -1;
                    else if (x == bordNum + 1)
                        bord[x, y] = -1;
                    else if (y == 0)
                        bord[x, y] = -1;
                    else if (y == bordNum + 1)
                        bord[x, y] = -1;
                    else if (x == y)
                        bord[x, y] = -1;
                    else if (x == bordNum + 1 - y)
                        bord[x, y] = -1;
                    else
                        bord[x, y] = 0;
                }

            //DEFAULT SET
            bord[bordNum / 2, bordNum / 2] = 2;
            bord[bordNum / 2 + 1, bordNum / 2] = 1;
            bord[bordNum / 2, bordNum / 2 + 1] = 1;
            bord[bordNum / 2 + 1, bordNum / 2 + 1] = 2;
        }

        public int[] mkHash()
        {
            int[] count = new int[bordNum + 2];

            for(int y = 0; y < bordNum + 2; y++)
            {
                count[y] = 0;

                for (int x = 0; x < bordNum + 2; x++)
                {
                    int val = bord[x, y] + 1;
                    count[y] *= 4;
                    count[y] += val;
                }
            }

            return count;
        }

        public Coord[] enablePlace(int player)
        {
            List<Coord> res = new List<Coord>();

            for(int y = 0; y < bordNum + 2; y++)
            {
                for(int x = 0; x < bordNum + 2; x++)
                {
                    if (bord[x, y] != 0)
                        continue;

                    Coord cache = new Coord(x, y);
                    
                    if (check(cache, player) != 0)
                        res.Add(cache);
                }
            }

            return res.ToArray();
        }

        public string bordId()
        {
            string res = "";

            for(int py = 1; py < bordNum + 1; py++)
                for (int px = 1; px < bordNum + 1; px++)
                    res += bord[px, py].ToString();

            return res;
        }

        public int check(Coord pos, int player)
        {
            return
                check_LFT(pos, player)
                + check_LUP(pos, player)
                + check_UPP(pos, player)
                + check_RUP(pos, player)
                + check_RGT(pos, player)
                + check_RDW(pos, player)
                + check_DWN(pos, player)
                + check_LDW(pos, player);
        }

        public int checkAll(int player)
        {
            int count = 0;

            for (int y = 0; y < bordNum + 2; y++)
                for (int x = 0; x < bordNum + 2; x++)
                    if (bord[x, y] == 0)
                        count += check(new Coord(x, y), player);

            return count;
        }

        public Coord checkMax(int player)
        {
            Coord maxPos = new Coord();
            int maxCount = -1;

            for (int y = 0; y < bordNum + 2; y++)
                for (int x = 0; x < bordNum + 2; x++)
                    if (bord[x, y] == 0)
                    {
                        int count = check(new Coord(x, y), player);

                        if (maxCount < count)
                        {
                            maxCount = count;
                            maxPos = new Coord(x, y);
                        }
                    }

            return maxPos;
        }

        public void bordChange(Coord pos, int player)
        {
            change_LFT(pos, player);
            change_LUP(pos, player);
            change_UPP(pos, player);
            change_RUP(pos, player);

            change_RGT(pos, player);
            change_RDW(pos, player);
            change_DWN(pos, player);
            change_LDW(pos, player);

            bord[pos.x, pos.y] = player;
        }

        public int blackCount()
        {
            int count = 0;

            for (int py = 0; py < bordNum + 2; py++)
                for (int px = 0; px < bordNum + 2; px++)
                    if (bord[px, py] == 1)
                        count++;

            return count;
        }
        
        public int whiteCount()
        {
            int count = 0;

            for (int py = 0; py < bordNum + 2; py++)
                for (int px = 0; px < bordNum + 2; px++)
                    if (bord[px, py] == 2)
                        count++;

            return count;
        }

        #region CHANGE
        void change_LFT(Coord pos, int player)
        {
            if (check_LFT(pos, player) == 0)
                return;

            int ofs = 1;
            while (true)
            {
                if (bord[pos.x - ofs, pos.y] == player)
                    break;

                bord[pos.x - ofs, pos.y] = player;
                ofs++;
            }
        }
        
        void change_LUP(Coord pos, int player)
        {
            if (check_LUP(pos, player) == 0)
                return;

            int ofs = 1;
            while (true)
            {
                if (bord[pos.x - ofs, pos.y - ofs] == player)
                    break;

                bord[pos.x - ofs, pos.y - ofs] = player;
                ofs++;
            }
        }
        void change_UPP(Coord pos, int player)
        {
            if (check_UPP(pos, player) == 0)
                return;

            int ofs = 1;
            while (true)
            {
                if (bord[pos.x, pos.y - ofs] == player)
                    break;

                bord[pos.x, pos.y - ofs] = player;
                ofs++;
            }
        }
        void change_RUP(Coord pos, int player)
        {
            if (check_RUP(pos, player) == 0)
                return;

            int ofs = 1;
            while (true)
            {
                if (bord[pos.x + ofs, pos.y - ofs] == player)
                    break;

                bord[pos.x + ofs, pos.y - ofs] = player;
                ofs++;
            }
        }

        void change_RGT(Coord pos, int player)
        {
            if (check_RGT(pos, player) == 0)
                return;

            int ofs = 1;
            while (true)
            {
                if (bord[pos.x + ofs, pos.y] == player)
                    break;

                bord[pos.x + ofs, pos.y] = player;
                ofs++;
            }
        }
        
        void change_RDW(Coord pos, int player)
        {
            if (check_RDW(pos, player) == 0)
                return;

            int ofs = 1;
            while (true)
            {
                if (bord[pos.x + ofs, pos.y + ofs] == player)
                    break;

                bord[pos.x + ofs, pos.y + ofs] = player;
                ofs++;
            }
        }
        void change_DWN(Coord pos, int player)
        {
            if (check_DWN(pos, player) == 0)
                return;

            int ofs = 1;
            while (true)
            {
                if (bord[pos.x, pos.y + ofs] == player)
                    break;

                bord[pos.x, pos.y + ofs] = player;
                ofs++;
            }
        }
        
        void change_LDW(Coord pos, int player)
        {
            if (check_LDW(pos, player) == 0)
                return;

            int ofs = 1;
            while (true)
            {
                if (bord[pos.x - ofs, pos.y + ofs] == player)
                    break;

                bord[pos.x - ofs, pos.y + ofs] = player;
                ofs++;
            }
        }
        #endregion

        #region CHECK
        int check_LFT(Coord pos, int player)
        {
            int ofs = 1;

            while (true)
            {
                if (bord[pos.x - ofs, pos.y] < 1)
                {
                    ofs = 1; // init
                    break;
                }
                if (bord[pos.x - ofs, pos.y] == player)
                    break;

                ofs++;
            }

            return ofs - 1;
        }
        
        int check_LUP(Coord pos, int player)
        {
            int ofs = 1;

            while (true)
            {
                if (bord[pos.x - ofs, pos.y - ofs] < 1)
                {
                    ofs = 1; // init
                    break;
                }
                if (bord[pos.x - ofs, pos.y - ofs] == player)
                    break;

                ofs++;
            }

            return ofs - 1;
        }
        
        int check_UPP(Coord pos, int player)
        {
            int ofs = 1;

            while (true)
            {
                if (bord[pos.x, pos.y - ofs] < 1)
                {
                    ofs = 1; // init
                    break;
                }
                if (bord[pos.x, pos.y - ofs] == player)
                    break;

                ofs++;
            }

            return ofs - 1;
        }
        
        int check_RUP(Coord pos, int player)
        {
            int ofs = 1;

            while (true)
            {
                if (bord[pos.x + ofs, pos.y - ofs] < 1)
                {
                    ofs = 1; // init
                    break;
                }
                if (bord[pos.x + ofs, pos.y - ofs] == player)
                    break;

                ofs++;
            }

            return ofs - 1;
        }

        int check_RGT(Coord pos, int player)
        {
            int ofs = 1;

            while (true)
            {
                if (bord[pos.x + ofs, pos.y] < 1)
                {
                    ofs = 1; // init
                    break;
                }
                if (bord[pos.x + ofs, pos.y] == player)
                    break;

                ofs++;
            }

            return ofs - 1;
        }
        
        int check_RDW(Coord pos, int player)
        {
            int ofs = 1;

            while (true)
            {
                if (bord[pos.x + ofs, pos.y + ofs] < 1)
                {
                    ofs = 1; // init
                    break;
                }
                if (bord[pos.x + ofs, pos.y + ofs] == player)
                    break;

                ofs++;
            }

            return ofs - 1;
        }
        
        int check_DWN(Coord pos, int player)
        {
            int ofs = 1;

            while (true)
            {
                if (bord[pos.x, pos.y + ofs] < 1)
                {
                    ofs = 1; // init
                    break;
                }
                if (bord[pos.x, pos.y + ofs] == player)
                    break;

                ofs++;
            }

            return ofs - 1;
        }
        
        int check_LDW(Coord pos, int player)
        {
            int ofs = 1;

            while (true)
            {
                if (bord[pos.x - ofs, pos.y + ofs] < 1)
                {
                    ofs = 1; // init
                    break;
                }
                if (bord[pos.x - ofs, pos.y + ofs] == player)
                    break;

                ofs++;
            }

            return ofs - 1;
        }
        #endregion

    }
}
