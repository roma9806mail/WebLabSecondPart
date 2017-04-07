using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLabSecondPart.Models
{
    public static class Game
    {
        public const int BoardSize = 10;
        public static char[,] GameBoard = new char[BoardSize, BoardSize];
        public static bool SbWin = false;

        public static void Iniziallize()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    GameBoard[i, j] = '1';
                }
            }
            using (var db = new Entityes.WebLabDb())
            {
                db.Games.Add(new Entityes.Game() { DateStarted = DateTime.Now });
                db.SaveChanges();
            }
        }

        public static string GamerTurn(int iRow, int iCol)
        {

            if (iRow <= 10 && iRow >= 1 && iCol <= 10 && iCol >= 1 && GameBoard[iRow - 1, iCol - 1] == '1')
            {
                GameBoard[iRow - 1, iCol - 1] = 'X';
                return "Next turn";
            }
            else
            {
                return "Try one more time";
            }
        }

        public static void RandomPcTurn(int randoms = 3)
        {
            Random random = new Random();
            int i = random.Next(0, 9);
            int j = random.Next(0, 9);
            if (GameBoard[i, j] == '1')
            {
                GameBoard[i, j] = '0';
                return;
            }
            else if (randoms <= 0)
            {
                for (int q = 0; q < 10; q++)
                {
                    for (int w = 0; w < 10; w++)
                    {
                        if (GameBoard[q, w] == '1')
                        {
                            GameBoard[q, w] = '0';
                            return;
                        }
                    }
                }
            }
            else
            {
                RandomPcTurn(--randoms);
            }

        }

        public static bool GameEnd(char needed)
        {
            int j, i, k;
            int CountOfKrests1 = 0, CountOfKrests2 = 0;

            for (j = 0; j < 10; j++)
            {
                CountOfKrests1 = CountOfKrests2 = 0;
                for (i = 0; i < 10; i++)
                {

                    if (GameBoard[j, i] == needed)
                    {
                        CountOfKrests1++;
                    }
                    else
                    {
                        CountOfKrests1 = 0;
                    }
                    if (GameBoard[j, i] == needed)
                    {
                        CountOfKrests2++;
                    }
                    else
                    {
                        CountOfKrests2 = 0;
                    }
                    if (CountOfKrests1 == 5 || CountOfKrests2 == 5)
                    {
                        SbWin = true;
                        return SbWin;
                    }
                }

            }

            CountOfKrests2 = CountOfKrests1 = 0;
            for (i = 0; i < 10; i++)
            {
                if (GameBoard[i, i] == needed)
                {
                    CountOfKrests1++;
                }
                else
                {
                    CountOfKrests1 = 0;
                }
                if (GameBoard[i, 9 - i] == needed)
                {
                    CountOfKrests2++;
                }
                else
                {
                    CountOfKrests2 = 0;
                }
                if (CountOfKrests1 == 5 || CountOfKrests2 == 5)
                {
                    SbWin = true;
                    return SbWin;
                }
            }

            for (i = 1; i < 6; i++)
            {
                CountOfKrests2 = CountOfKrests1 = 0;
                for (j = i, k = 0; j < 10; j++, k++)
                {
                    if (GameBoard[j, k] == needed)
                    {
                        CountOfKrests1++;
                    }
                    else
                    {
                        CountOfKrests1 = 0;
                    }
                    if (GameBoard[k, j] == needed)
                    {
                        CountOfKrests2++;
                    }
                    else
                    {
                        CountOfKrests2 = 0;
                    }
                    if (CountOfKrests1 == 5 || CountOfKrests2 == 5)
                    {
                        SbWin = true;
                        return SbWin;
                    }
                }
            }
            CountOfKrests2 = CountOfKrests1 = 0;


            for (i = 4; i < 10; i++)
            {
                CountOfKrests2 = CountOfKrests1 = 0;
                for (j = i, k = 0; j >= 0; j--, k++)
                {
                    if (GameBoard[k, j] == needed)
                    {
                        CountOfKrests1++;
                    }
                    else
                    {
                        CountOfKrests1 = 0;
                    }
                    if (GameBoard[9 - j, 9 - k] == needed)
                    {
                        CountOfKrests2++;
                    }
                    else
                    {
                        CountOfKrests2 = 0;
                    }
                    if (CountOfKrests1 == 5 || CountOfKrests2 == 5)
                    {
                        SbWin = true;
                        return SbWin;
                    }
                }

            }

            return SbWin;
        }

        public static void PCTurn()
        {

            int j, i, k;
            int CountOfKrests1 = 0, CountOfKrests2 = 0;

            for (j = 0; j < 10; j++)
            {
                CountOfKrests1 = CountOfKrests2 = 0;
                for (i = 0; i < 10; i++)
                {
                    if (GameBoard[j, i] == 'X')
                    {
                        CountOfKrests1++;
                        if (CountOfKrests1 >= 3)
                        {
                            if (i != 9 && GameBoard[j, i + 1] == '1')
                            {
                                GameBoard[j, i + 1] = '0';
                                return;
                            }
                            else if ((i - 3) >= 0 && GameBoard[j, i - 3] == '1')
                            {
                                GameBoard[j, i - 3] = '0';
                                return;
                            }
                        }
                    }
                    else
                    {
                        CountOfKrests1 = 0;
                    }
                    if (GameBoard[i, j] == 'X')
                    {
                        CountOfKrests2++;
                        if (CountOfKrests2 >= 3)
                        {
                            if (i != 9 && GameBoard[i + 1, j] == '1')
                            {
                                GameBoard[i + 1, j] = '0';
                                return;
                            }
                            else if ((i - 3) >= 0 && GameBoard[i - 3, j] == '1')
                            {
                                GameBoard[i - 3, j] = '0';
                                return;
                            }
                        }
                    }
                    else
                    {
                        CountOfKrests2 = 0;
                    }

                }

            }

            CountOfKrests2 = CountOfKrests1 = 0;
            for (i = 0; i < 10; i++)
            {
                if (GameBoard[i, i] == 'X')
                {
                    CountOfKrests1++;

                    if (CountOfKrests1 >= 3)
                    {
                        if (i != 9 && GameBoard[i + 1, i + 1] == '1')
                        {
                            GameBoard[i + 1, i + 1] = '0';
                            return;
                        }
                        else if ((i - 3) >= 0 && GameBoard[i - 3, i - 3] == '1')
                        {
                            GameBoard[i - 3, i - 3] = '0';
                            return;
                        }
                    }
                }
                else
                {
                    CountOfKrests1 = 0;
                }
                if (GameBoard[i, 9 - i] == 'X')
                {
                    CountOfKrests2++;

                    if (CountOfKrests2 >= 3)
                    {
                        if (i != 9 && GameBoard[i + 1, 9 - i + 1] == '1')
                        {
                            GameBoard[i + 1, 10 - i + 1] = '0';
                            return;
                        }
                        else if ((i - 3) >= 0 && GameBoard[i - 3, 9 - i + 3] == '1')
                        {
                            GameBoard[i - 3, 9 - i + 3] = '0';
                            return;
                        }
                    }
                }
                else
                {
                    CountOfKrests2 = 0;
                }

            }


            for (i = 1; i < 6; i++)
            {
                CountOfKrests2 = CountOfKrests1 = 0;
                for (j = i, k = 0; j < 10; j++, k++)
                {
                    if (GameBoard[j, k] == 'X')
                    {
                        CountOfKrests1++;

                        if (CountOfKrests1 >= 3)
                        {
                            if (j != 9 && k != 9 && GameBoard[j + 1, k + 1] == '1')
                            {
                                GameBoard[j + 1, k + 1] = '0';
                                return;
                            }
                            else if ((j - 3) >= 0 && (k - 3) >= 0 && GameBoard[j - 3, k - 3] == '1')
                            {
                                GameBoard[j - 3, k - 3] = '0';
                                return;
                            }
                        }
                    }
                    else
                    {
                        CountOfKrests1 = 0;
                    }
                    if (GameBoard[k, j] == 'X')
                    {
                        CountOfKrests2++;

                        if (CountOfKrests2 >= 3)
                        {
                            if (j != 9 && k != 9 && GameBoard[k + 1, j + 1] == '1')
                            {
                                GameBoard[k + 1, j + 1] = '0';
                                return;
                            }
                            else if ((j - 3) >= 0 && (k - 3) >= 0 && GameBoard[k - 3, j - 3] == '1')
                            {
                                GameBoard[k - 3, j - 3] = '0';
                                return;
                            }
                        }
                    }
                    else
                    {
                        CountOfKrests2 = 0;
                    }

                }
            }

            for (i = 4; i < 10; i++)
            {
                CountOfKrests2 = CountOfKrests1 = 0;
                for (j = i, k = 0; j >= 0; j--, k++)
                {
                    if (GameBoard[k, j] == 'X')
                    {
                        CountOfKrests1++;
                        if (CountOfKrests1 >= 3)
                        {
                            if (j >= 1 && k != 9 && GameBoard[k + 1, j - 1] == '1')
                            {
                                GameBoard[k + 1, j - 1] = '0';
                                return;
                            }
                            else if ((j + 3) <= 9 && (k - 3) >= 0 && GameBoard[k - 3, j + 3] == '1')
                            {
                                GameBoard[k - 3, j + 3] = '0';
                                return;
                            }
                        }
                    }
                    else
                    {
                        CountOfKrests1 = 0;
                    }
                    if (GameBoard[9 - j, 9 - k] == 'X')
                    {
                        CountOfKrests2++;

                        if (CountOfKrests2 >= 3)
                        {
                            if (j != 9 && k != 9 && GameBoard[9 - (j - 1), 9 - (k + 1)] == '1')
                            {
                                GameBoard[9 - (j - 1), 9 - (k + 1)] = '0';
                                return;
                            }
                            else if ((j - 3) >= 0 && (k - 3) >= 0 && GameBoard[9 - (j + 3), 9 - (k - 3)] == '1')
                            {
                                GameBoard[9 - (j + 3), 9 - (k - 3)] = '0';
                                return;
                            }
                        }
                    }
                    else
                    {
                        CountOfKrests2 = 0;
                    }
                }

            }

            int CountOfNulls1 = 0, CountOfNulls2 = 0;
            for (j = 0; j < 10; j++)
            {
                CountOfNulls1 = CountOfNulls2 = 0;
                for (i = 0; i < 10; i++)
                {
                    if (GameBoard[i, j] == '0')
                    {
                        CountOfNulls1++;
                        if (CountOfNulls1 >= 4)
                        {
                            if (i < 9 && GameBoard[i + 1, j] == '1')
                            {
                                GameBoard[i + 1, j] = '0';
                                return;
                            }
                            else if ((i - 3) > 0 && GameBoard[i - 4, j] == '1')
                            {
                                GameBoard[i - 4, j] = '0';
                                return;
                            }
                        }

                    }
                    else
                    {
                        CountOfNulls1 = 0;
                    }
                    if (GameBoard[j, i] == '0')
                    {
                        CountOfNulls2++;
                        if (CountOfNulls2 >= 4)
                        {
                            if (i < 9 && GameBoard[j, i + 1] == '1')
                            {
                                GameBoard[j, i + 1] = '0';
                                return;
                            }
                            else if ((i - 3) > 0 && GameBoard[j, i - 4] == '1')
                            {
                                GameBoard[j, i - 4] = '0';
                                return;
                            }
                        }
                    }
                    else
                    {
                        CountOfNulls2 = 0;
                    }


                }

            }

            CountOfNulls1 = CountOfNulls2 = 0;
            for (i = 0; i < 10; i++)
            {
                if (GameBoard[i, i] == '0')
                {
                    CountOfNulls1++;
                    if (CountOfNulls1 >= 4)
                    {
                        if (i < 9 && GameBoard[i + 1, i + 1] == '1')
                        {
                            GameBoard[i + 1, i + 1] = '0';
                            return;
                        }
                        else if ((i - 3) > 0 && GameBoard[i - 4, i - 4] == '1')
                        {
                            GameBoard[i - 4, i - 4] = '0';
                            return;
                        }
                    }
                }
                else
                {
                    CountOfNulls1 = 0;
                }
                if (GameBoard[i, 9 - i] == '0')
                {
                    CountOfNulls2++;
                    if (CountOfNulls1 >= 4)
                    {
                        if (i < 9 && i > 0 && GameBoard[i + 1, 9 - (i + 1)] == '1')
                        {
                            GameBoard[i + 1, 9 - (i + 1)] = '0';
                            return;
                        }
                        else if ((i - 3) > 0 && GameBoard[i - 4, 9 - (i - 4)] == '1')
                        {
                            GameBoard[i - 4, 9 - (i - 4)] = '0';
                            return;
                        }
                    }
                }
                else
                {
                    CountOfNulls2 = 0;
                }

            }

            for (i = 1; i < 6; i++)
            {
                CountOfNulls1 = CountOfNulls2 = 0;
                for (j = i, k = 0; j < 10; j++, k++)
                {
                    if (GameBoard[j, k] == 'X')
                    {
                        CountOfNulls1++;
                        if (CountOfNulls1 >= 4)
                        {
                            if (j != 9 && k != 9 && GameBoard[j + 1, k + 1] == '1')
                            {
                                GameBoard[j + 1, k + 1] = '0';
                                return;
                            }
                            else if ((j - 4) >= 0 && (k - 4) >= 0 && GameBoard[j - 4, k - 4] == '1')
                            {
                                GameBoard[j - 4, k - 4] = '0';
                                return;
                            }
                        }
                    }
                    else
                    {
                        CountOfNulls1 = 0;
                    }
                    if (GameBoard[k, j] == 'X')
                    {
                        CountOfNulls2++;
                        if (CountOfNulls2 >= 4)
                        {
                            if (j != 9 && k != 9 && GameBoard[k + 1, j + 1] == '1')
                            {
                                GameBoard[k + 1, j + 1] = '0';
                                return;
                            }
                            else if ((j - 4) >= 0 && (k - 4) >= 0 && GameBoard[k - 4, j - 4] == '1')
                            {
                                GameBoard[k - 4, j - 4] = '0';
                                return;
                            }
                        }
                    }
                    else
                    {
                        CountOfNulls2 = 0;
                    }

                }
            }

            for (i = 4; i < 10; i++)
            {
                CountOfNulls2 = CountOfNulls1 = 0;
                for (j = i, k = 0; j >= 0; j--, k++)
                {
                    if (GameBoard[k, j] == 'X')
                    {
                        CountOfNulls1++;
                        if (CountOfNulls1 >= 4)
                        {
                            if (j >= 1 && k != 9 && GameBoard[k + 1, j - 1] == '1')
                            {
                                GameBoard[k + 1, j - 1] = '0';
                                return;
                            }
                            else if ((j + 4) <= 9 && (k - 4) >= 0 && GameBoard[k - 4, j + 4] == '1')
                            {
                                GameBoard[k - 4, j + 4] = '0';
                                return;
                            }
                        }
                    }
                    else
                    {
                        CountOfNulls1 = 0;
                    }
                    if (GameBoard[9 - j, 9 - k] == 'X')
                    {
                        CountOfNulls2++;
                        if (CountOfNulls2 >= 4)
                        {
                            if (j != 9 && k != 9 && GameBoard[9 - (j - 1), 9 - (k + 1)] == '1')
                            {
                                GameBoard[9 - (j - 1), 9 - (k + 1)] = '0';
                                return;
                            }
                            else if ((j - 4) >= 0 && (k - 4) >= 0 && GameBoard[9 - (j + 4), 9 - (k - 4)] == '1')
                            {
                                GameBoard[9 - (j + 4), 9 - (k - 4)] = '0';
                                return;
                            }
                        }
                    }
                    else
                    {
                        CountOfNulls2 = 0;
                    }
                }

            }

            RandomPcTurn();

        }
    }
}