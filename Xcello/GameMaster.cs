using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace _6cello
{
    class GameMaster
    {
        public Bord gameBord = new Bord();

        public GameMaster()
        {
            gameBord.bordInit(8, Config.MAP);
            turn = 0;
            setPlayer();
            blackCount = gameBord.blackCount();
            whiteCount = gameBord.whiteCount();
        }
        public GameMaster(int num)
        {
            gameBord.bordInit(num, Config.MAP);
            turn = 0;
            setPlayer();
            blackCount = gameBord.blackCount();
            whiteCount = gameBord.whiteCount();
        }

        public void GameInit()
        {
            gameBord.bordInit(8, Config.MAP);
            turn = 0;
            setPlayer();
            blackCount = gameBord.blackCount();
            whiteCount = gameBord.whiteCount();
        }

        public int turn;
        public int player;

        public int blackCount;
        public int whiteCount;

        public int bwCount = 0;
        public int wwCount = 0;

        static int noMemo = 0;

        public void putStone(Coord pos)
        {
            setPlayer();

            //Ceck Stone Place is True
            if (gameBord.bord[pos.x, pos.y] != 0)
                return;
            if (gameBord.check(pos, player) == 0)
                return;

            gameBord.bordChange(pos, player);

            blackCount = gameBord.blackCount();
            whiteCount = gameBord.whiteCount();

            turn++;
            setPlayer();

            //PASS
            if (gameBord.checkAll(player) == 0)
            {
                //Console.WriteLine("PASS : PLAYER{0}", player);
                turn++;
                setPlayer();

                //GAME OVER
                int ca = gameBord.checkAll(player);
                if (ca == 0)
                    gameEnd();
            }

            if (player == 1)
            {
                if (Config.com1p)
                    putComStone(Config.COMLV1);
            }
            if (player == 2)
            {
                if (Config.com2p)
                    putComStone(Config.COMLV2);
            }
        }

        public void putComStone(int lv)
        {
            #region MONTE
            if (lv == 1)
            {
                if (gameBord.checkAll(player) == 0)
                    return;

                System.Random r = new Random();
                while (true)
                {
                    Coord rpos = new Coord(r.Next(gameBord.bordNum + 2), r.Next(gameBord.bordNum + 2));
                    if (gameBord.bord[rpos.x, rpos.y] == 0)
                        if (gameBord.check(rpos, player) != 0)
                        {
                            putStone(rpos);
                            return;
                        }
                }

            }
            #endregion

            #region MAX
            if (lv == 2)
            {
                if (gameBord.checkAll(player) == 0)
                    return;
                Coord maxPos = gameBord.checkMax(player);

                putStone(maxPos);
            }
            #endregion

            #region AI
            if (lv == 3)
            {
                int myTurn = (turn - turn % 2) / 2;

                float[] grl;

                if (player == 1)
                    grl = g1Ratio[myTurn];
                else
                    grl = g2Ratio[myTurn];

                //AI
                #region
                while (true)
                {
                    int pos = 0;
                    float val = -1;

                    for (int i = 0; i < grl.Length; i++)
                    {
                        if (val < grl[i])
                        {
                            pos = i;
                            val = grl[i];
                        }
                    }

                    if (val == 0)
                        break;

                    Coord posc = new Coord(pos % gameBord.bordNum, pos / gameBord.bordNum);

                    if (gameBord.bord[posc.x, posc.y] == 0)
                        if (gameBord.check(posc, player) != 0)
                        {
                            putStone(posc);
                            return;
                        }

                    grl[pos] = 0;
                }
                #endregion

                //MAX
                if (gameBord.checkAll(player) == 0)
                    return;
                Coord maxPos = gameBord.checkMax(player);

                putStone(maxPos);
            }
            #endregion

            #region AI2
            if (lv == 4)
            {
                string bordId = gameBord.bordId();

                int pos = -1;

                if (player == 1)
                {
                    StreamReader sr = new StreamReader("bp.AI");

                    while (sr.Peek() >= 0)
                    {
                        string stBuffer = sr.ReadLine();

                        if (stBuffer == bordId)
                        {
                            pos = int.Parse(sr.ReadLine());
                            break;
                        }
                    }
                }
                else
                {
                    StreamReader sr = new StreamReader("wp.AI");

                    while (sr.Peek() >= 0)
                    {
                        string stBuffer = sr.ReadLine();

                        if (stBuffer == bordId)
                        {
                            pos = int.Parse(sr.ReadLine());
                            break;
                        }
                    }
                }

                if (pos == -1)
                {
                    //MAX
                    if (gameBord.checkAll(player) == 0)
                        return;
                    Coord maxPos = gameBord.checkMax(player);
                    noMemo++;
                    Console.WriteLine("No Memo {0}", noMemo);
                    putStone(maxPos);
                }
                else
                    putStone(new Coord(pos % gameBord.bordNum, pos / gameBord.bordNum));
            }
            #endregion

            #region AI3
            if (lv == 5)
            {
                setPlayer();

                System.Random r = new Random();
                Coord[] ep = gameBord.enablePlace(player);
                if (ep.Length == 0)
                    return;

                //GoodGameCount
                int[] ggc = new int[ep.Length]; //
                int[] bgc = new int[ep.Length]; //BadGameCount
                float[] wr = new float[ep.Length]; //WonRatio
                int gc = 0;

                //init
                for (int i = 0; i < ep.Length; i++)
                {
                    ggc[i] = 0;
                    bgc[i] = 0;
                    wr[i] = -1;
                }

                DateTime startDt = DateTime.Now;

                //Learning
                #region Lean
                while (true)
                {
                    MiniGame gmp = new MiniGame(this);
                    int firstPos = r.Next(ep.Length);

                    gmp.miniGameBord.bordChange(ep[firstPos], gmp.miniPlayer);
                    gmp.miniTurn++;
                    gmp.miniSetPlayer();

                    //PASS
                    #region PASS
                    if (gmp.miniGameBord.checkAll(gmp.miniPlayer) == 0)
                    {
                        gmp.miniTurn++;
                        gmp.miniSetPlayer();

                        if (gmp.miniGameBord.checkAll(gmp.miniPlayer) == 0)
                        {
                            if (gmp.miniGameBord.blackCount() < gmp.miniGameBord.whiteCount())
                            {
                                //my won
                                if (player == 2)
                                    ggc[firstPos]++;
                                else
                                    bgc[firstPos]++;
                            }
                            else
                            {
                                if (player == 1)
                                    ggc[firstPos]++;
                                else
                                    bgc[firstPos]++;
                            }

                            DateTime endDtl = DateTime.Now;
                            TimeSpan tsl = endDtl - startDt;

                            if (Config.CALCUTIME < tsl.TotalMilliseconds)
                            {
                                Console.WriteLine("GameCount : {0}", gc);
                                break;
                            }

                            continue;
                        }
                    }
                    #endregion

                    //GAME
                    #region GAME
                    while (true)
                    {
                        gmp.miniSetPlayer();

                        Coord[] eps = gmp.miniGameBord.enablePlace(gmp.miniPlayer);

                        if (eps.Length == 0)
                        {
                            //pass
                            gmp.miniTurn++;
                            gmp.miniSetPlayer();
                            eps = gmp.miniGameBord.enablePlace(gmp.miniPlayer);

                            if (eps.Length == 0)
                            {
                                if (gmp.miniGameBord.blackCount() < gmp.miniGameBord.whiteCount())
                                {
                                    //my won
                                    if (player == 2)
                                        ggc[firstPos]++;
                                    else
                                        bgc[firstPos]++;
                                }
                                else
                                {
                                    if (player == 1)
                                        ggc[firstPos]++;
                                    else
                                        bgc[firstPos]++;
                                }
                            }

                            break;
                        }

                        gmp.miniGameBord.bordChange(eps[r.Next(eps.Length)], gmp.miniPlayer);

                        gmp.miniTurn++;
                        gmp.miniSetPlayer();
                    }
                    #endregion

                    gc++;

                    DateTime endDt = DateTime.Now;
                    TimeSpan ts = endDt - startDt;

                    if (Config.CALCUTIME < ts.TotalMilliseconds)
                    {
                        Console.WriteLine("GameCount : {0}", gc);
                        break;
                    }
                }
                #endregion

                for (int i = 0; i < ep.Length; i++)
                {
                    bgc[i] += ggc[i];

                    if (ggc[i] == 0)
                        wr[i] = -1;
                    else
                        wr[i] = (float)ggc[i] / (float)bgc[i];
                }

                float maxRatio = -1;
                int maxPos = -1;

                for (int i = 0; i < ep.Length; i++)
                {
                    if (maxRatio < wr[i])
                    {
                        maxRatio = wr[i];
                        maxPos = i;
                    }
                }

                if (maxPos == -1)
                {
                    //MAX
                    if (gameBord.checkAll(player) == 0)
                        return;
                    Coord maxPosc = gameBord.checkMax(player);
                    putStone(maxPosc);
                }
                else
                {
                    Console.WriteLine("Wining Ratio : {0}", maxRatio);
                    putStone(ep[maxPos]);
                }

            }
            #endregion

            #region AI4
            if (lv == 6)
            {
                setPlayer();

                System.Random r = new Random();
                Coord[] ep = gameBord.enablePlace(player);
                if (ep.Length == 0)
                    return;

                //GoodGameCount
                int[] ggc = new int[ep.Length]; //
                int[] bgc = new int[ep.Length]; //BadGameCount
                float[] wr = new float[ep.Length]; //WonRatio
                int gc = 0;

                //init
                for (int i = 0; i < ep.Length; i++)
                {
                    ggc[i] = 0;
                    bgc[i] = 0;
                    wr[i] = -1;
                }

                DateTime startDt = DateTime.Now;

                //Learning
                #region Lean
                while (true)
                {
                    MiniGame gmp = new MiniGame(this);
                    int firstPos = r.Next(ep.Length);

                    gmp.miniGameBord.bordChange(ep[firstPos], gmp.miniPlayer);
                    gmp.miniTurn++;
                    gmp.miniSetPlayer();

                    //PASS
                    #region PASS
                    if (gmp.miniGameBord.checkAll(gmp.miniPlayer) == 0)
                    {
                        gmp.miniTurn++;
                        gmp.miniSetPlayer();

                        if (gmp.miniGameBord.checkAll(gmp.miniPlayer) == 0)
                        {
                            if (gmp.miniGameBord.blackCount() < gmp.miniGameBord.whiteCount())
                            {
                                //my won
                                if (player == 2)
                                {
                                    ggc[firstPos]++;
                                    ggc[firstPos] += gmp.whiteCornerNum();
                                    bgc[firstPos] += gmp.blackCornerNum();
                                }
                                else
                                {
                                    bgc[firstPos]++;
                                    bgc[firstPos] += gmp.blackCornerNum();

                                }
                            }
                            else
                            {
                                if (player == 1)
                                {
                                    ggc[firstPos]++;
                                    bgc[firstPos] += gmp.whiteCornerNum();
                                    ggc[firstPos] += gmp.blackCornerNum();

                                }
                                else
                                {
                                    bgc[firstPos]++;
                                    bgc[firstPos] += gmp.whiteCornerNum();
                                }
                            }

                            DateTime endDtl = DateTime.Now;
                            TimeSpan tsl = endDtl - startDt;

                            if (Config.CALCUTIME < tsl.TotalMilliseconds)
                            {
                                Console.WriteLine("GameCount : {0}", gc);
                                break;
                            }

                            continue;
                        }
                    }
                    #endregion

                    //GAME
                    #region GAME
                    while (true)
                    {
                        gmp.miniSetPlayer();

                        Coord[] eps = gmp.miniGameBord.enablePlace(gmp.miniPlayer);

                        if (eps.Length == 0)
                        {
                            //pass
                            gmp.miniTurn++;
                            gmp.miniSetPlayer();
                            eps = gmp.miniGameBord.enablePlace(gmp.miniPlayer);

                            if (eps.Length == 0)
                            {
                                if (gmp.miniGameBord.blackCount() < gmp.miniGameBord.whiteCount())
                                {
                                    //my won
                                    if (player == 2)
                                        ggc[firstPos]++;
                                    else
                                        bgc[firstPos]++;
                                }
                                else
                                {
                                    if (player == 1)
                                        ggc[firstPos]++;
                                    else
                                        bgc[firstPos]++;
                                }
                            }

                            break;
                        }

                        gmp.miniGameBord.bordChange(eps[r.Next(eps.Length)], gmp.miniPlayer);

                        gmp.miniTurn++;
                        gmp.miniSetPlayer();
                    }
                    #endregion

                    gc++;

                    DateTime endDt = DateTime.Now;
                    TimeSpan ts = endDt - startDt;

                    if (Config.CALCUTIME < ts.TotalMilliseconds)
                    {
                        Console.WriteLine("GameCount : {0}", gc);
                        break;
                    }
                }
                #endregion

                for (int i = 0; i < ep.Length; i++)
                {
                    bgc[i] += ggc[i];

                    if (ggc[i] == 0)
                        wr[i] = -1;
                    else
                        wr[i] = (float)ggc[i] / (float)bgc[i];
                }

                float maxRatio = -1;
                int maxPos = -1;

                for (int i = 0; i < ep.Length; i++)
                {
                    if (maxRatio < wr[i])
                    {
                        maxRatio = wr[i];
                        maxPos = i;
                    }
                }

                if (maxPos == -1)
                {
                    //MAX
                    if (gameBord.checkAll(player) == 0)
                        return;
                    Coord maxPosc = gameBord.checkMax(player);
                    putStone(maxPosc);
                }
                else
                {
                    Console.WriteLine("Wining Ratio : {0}", maxRatio);
                    putStone(ep[maxPos]);
                }

            }
            #endregion
        }

        public void gameEnd()
        {
            //Ending Process
            Console.WriteLine("GAME OVER : {0}", gameBord.mkHash()[1]);

            if (whiteCount < blackCount)
                bwCount++;
            else
                wwCount++;
        }


        public int setPlayer()
        {
            int p = turn % 2 + 1;

            player = p;

            return p;
        }

        //AI
        public void aiTest(int num)
        {
            aiCore(num);
            aiReadTest1(num);
            aiReadTest2(num);
        }

        void aiCore(int num)
        {
            for (int i = 0; i < num; i++)
            {
                Console.Clear();
                Console.WriteLine("{0}", i);

                GameInit();

                StreamWriter swb = new StreamWriter("testAIb" + i);
                StreamWriter sww = new StreamWriter("testAIw" + i);
                System.Random r = new Random();

                while (true)
                {
                    Coord[] cbox = gameBord.enablePlace(player);

                    Coord selected = cbox[r.Next(cbox.Length)];

                    int bno = gameBord.bordNum * selected.y + selected.x;

                    if (player == 1)
                        swb.WriteLine(bno);
                    else
                        sww.WriteLine(bno);


                    gameBord.bordChange(selected, player);

                    blackCount = gameBord.blackCount();
                    whiteCount = gameBord.whiteCount();

                    turn++;
                    setPlayer();

                    //PASS
                    if (gameBord.checkAll(player) == 0)
                    {
                        //Console.WriteLine("PASS : PLAYER{0}", player);
                        turn++;
                        setPlayer();

                        //GAME OVER
                        if (gameBord.checkAll(player) == 0)
                            break;
                    }
                }

                if (blackCount < whiteCount)
                {
                    sww.WriteLine("-1");
                    swb.WriteLine("-2");
                }
                else
                {
                    sww.WriteLine("-2");
                    swb.WriteLine("-1");
                }

                swb.Close();
                sww.Close();
            }

        }

        public void aiTest()
        {
            StreamWriter swb = new StreamWriter("testAIb.txt");
            StreamWriter sww = new StreamWriter("testAIw.txt");
            System.Random r = new Random();

            while (true)
            {
                Coord[] cbox = gameBord.enablePlace(player);

                Coord selected = cbox[r.Next(cbox.Length)];

                int bno = gameBord.bordNum * selected.y + selected.x;

                if (player == 1)
                    swb.WriteLine(bno);
                else
                    sww.WriteLine(bno);


                gameBord.bordChange(selected, player);

                blackCount = gameBord.blackCount();
                whiteCount = gameBord.whiteCount();

                turn++;
                setPlayer();

                //PASS
                if (gameBord.checkAll(player) == 0)
                {
                    Console.WriteLine("PASS : PLAYER{0}", player);
                    turn++;
                    setPlayer();

                    //GAME OVER
                    if (gameBord.checkAll(player) == 0)
                        break;
                }
            }

            if (blackCount < whiteCount)
            {
                sww.WriteLine("-1");
                swb.WriteLine("-2");
            }
            else
            {
                sww.WriteLine("-2");
                swb.WriteLine("-1");
            }

            swb.Close();
            sww.Close();
        }

        void aiReadTest1(int num)
        {
            int[][] godPos = new int[HandleMax][];
            int[][] badPos = new int[HandleMax][];

            for (int i = 0; i < HandleMax; i++)
            {
                godPos[i] = new int[81];
                badPos[i] = new int[81];
                g1Ratio[i] = new float[81];
            }

            for (int i = 0; i < num; i++)
            {
                StreamReader sr = new StreamReader("testAIb" + i);

                List<int> cache = new List<int>();
                int flag = 0;

                while (true)
                {
                    int c = int.Parse(sr.ReadLine());

                    if (0 < c)
                        cache.Add(c);
                    else
                    {
                        flag = c;
                        break;
                    }
                }

                if (flag == -1)
                    for (int n = 0; n < cache.Count; n++)
                        godPos[n][cache[n]]++;
                else
                    for (int n = 0; n < cache.Count; n++)
                        badPos[n][cache[n]]++;
            }

            for (int i = 0; i < HandleMax; i++)
                for (int n = 0; n < 81; n++)
                    g1Ratio[i][n] = (float)godPos[i][n] / (float)(godPos[i][n] + badPos[i][n]);
        }

        void aiReadTest2(int num)
        {
            int[][] godPos = new int[HandleMax][];
            int[][] badPos = new int[HandleMax][];

            for (int i = 0; i < HandleMax; i++)
            {
                godPos[i] = new int[81];
                badPos[i] = new int[81];
                g2Ratio[i] = new float[81];
            }

            for (int i = 0; i < num; i++)
            {
                StreamReader sr = new StreamReader("testAIw" + i);

                List<int> cache = new List<int>();
                int flag = 0;

                while (true)
                {
                    int c = int.Parse(sr.ReadLine());

                    if (0 < c)
                        cache.Add(c);
                    else
                    {
                        flag = c;
                        break;
                    }
                }

                if (flag == -1)
                    for (int n = 0; n < cache.Count; n++)
                        godPos[n][cache[n]]++;
                else
                    for (int n = 0; n < cache.Count; n++)
                        badPos[n][cache[n]]++;
            }

            for (int i = 0; i < HandleMax; i++)
                for (int n = 0; n < 64; n++)
                    g2Ratio[i][n] = (float)godPos[i][n] / (float)(godPos[i][n] + badPos[i][n]);
        }

        static int HandleMax = 60;
        float[][] g1Ratio = new float[HandleMax][];
        float[][] g2Ratio = new float[HandleMax][];
        public void ai2(int num)
        {
            int[][] gp1 = new int[HandleMax][];
            int[][] gp2 = new int[HandleMax][];
            int[][] bp1 = new int[HandleMax][];
            int[][] bp2 = new int[HandleMax][];
            for (int i = 0; i < HandleMax; i++)
            {
                gp1[i] = new int[81];
                gp2[i] = new int[81];
                bp1[i] = new int[81];
                bp2[i] = new int[81];
                g1Ratio[i] = new float[81];
                g2Ratio[i] = new float[81];
            }
            System.Random r = new Random();

            for (int i = 0; i < num; i++)
            {
                List<int> bpos = new List<int>();
                List<int> wpos = new List<int>();

                Console.WriteLine("{0}", i);
                GameInit();

                while (true)
                {
                    Coord[] cbox = gameBord.enablePlace(player);
                    Coord selected = cbox[r.Next(cbox.Length)];

                    int bno = gameBord.bordNum * selected.y + selected.x;

                    if (player == 1)
                        bpos.Add(bno);
                    else
                        wpos.Add(bno);

                    gameBord.bordChange(selected, player);

                    blackCount = gameBord.blackCount();
                    whiteCount = gameBord.whiteCount();

                    turn++;
                    setPlayer();

                    //PASS
                    if (gameBord.checkAll(player) == 0)
                    {
                        turn++;
                        setPlayer();

                        //GAME OVER
                        if (gameBord.checkAll(player) == 0)
                            break;
                    }
                }

                if (blackCount < whiteCount)
                {
                    //White Won

                    for (int c = 0; c < bpos.Count; c++)
                        bp1[c][bpos[c]]++;
                    for (int c = 0; c < wpos.Count; c++)
                        gp2[c][wpos[c]]++;
                }
                else
                {
                    //Black Won

                    for (int c = 0; c < bpos.Count; c++)
                        gp1[c][bpos[c]]++;
                    for (int c = 0; c < wpos.Count; c++)
                        bp2[c][wpos[c]]++;
                }
            }

            for (int c1 = 0; c1 < HandleMax; c1++)
                for (int c2 = 0; c2 < 81; c2++)
                {
                    if (gp1[c1][c2] == 0)
                        g1Ratio[c1][c2] = 0;
                    else
                        g1Ratio[c1][c2] = (float)gp1[c1][c2] / (float)(gp1[c1][c2] + bp1[c1][c2]);
                    if (gp2[c1][c2] == 0)
                        g2Ratio[c1][c2] = 0;
                    else
                        g2Ratio[c1][c2] = (float)gp2[c1][c2] / (float)(gp2[c1][c2] + bp2[c1][c2]);
                }

        }

        float[,,] g13Ratio = new float[100, 100, 100];
        float[,,] g23Ratio = new float[100, 100, 100];

        public void ai3(int num)
        {
            System.Random r = new Random();

            for (int i = 0; i < num; i++)
            {
                List<string> blackBordId = new List<string>();
                List<string> whiteBordId = new List<string>();

                List<int> bpos = new List<int>();
                List<int> wpos = new List<int>();

                Console.WriteLine("{0}", i);
                GameInit();

                while (true)
                {
                    //Bord Info Make
                    Coord[] cbox = gameBord.enablePlace(player);
                    Coord selected = cbox[r.Next(cbox.Length)];

                    int bno = gameBord.bordNum * selected.y + selected.x;

                    if (player == 1)
                    {
                        bpos.Add(bno);
                        blackBordId.Add(gameBord.bordId());
                    }
                    else
                    {
                        wpos.Add(bno);
                        whiteBordId.Add(gameBord.bordId());
                    }

                    gameBord.bordChange(selected, player);

                    blackCount = gameBord.blackCount();
                    whiteCount = gameBord.whiteCount();

                    turn++;
                    setPlayer();

                    //PASS
                    if (gameBord.checkAll(player) == 0)
                    {
                        turn++;
                        setPlayer();

                        //GAME OVER
                        if (gameBord.checkAll(player) == 0)
                            break;
                    }
                }

                if (blackCount < whiteCount)
                {
                    //White Won
                    for (int wc = 0; wc < whiteBordId.Count; wc++)
                    {
                        StreamWriter sww = new StreamWriter("2" + whiteBordId[wc], true);
                        sww.WriteLine(wpos[wc]);
                        sww.Close();
                    }
                    for (int bc = 0; bc < blackBordId.Count; bc++)
                    {
                        StreamWriter swb = new StreamWriter("1" + blackBordId[bc], true);
                        swb.WriteLine(-bpos[bc]);
                        swb.Close();
                    }
                }
                else
                {
                    //Black Won
                    for (int wc = 0; wc < whiteBordId.Count; wc++)
                    {
                        StreamWriter sww = new StreamWriter("2" + whiteBordId[wc], true);
                        sww.WriteLine(-wpos[wc]);
                        sww.Close();
                    }
                    for (int bc = 0; bc < blackBordId.Count; bc++)
                    {
                        StreamWriter swb = new StreamWriter("1" + blackBordId[bc], true);
                        swb.WriteLine(bpos[bc]);
                        swb.Close();
                    }
                }
            }

            Console.WriteLine(gameBord.bordId());


        }
    }
}
