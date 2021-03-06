﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GoBangCL.Standard.Model
{
    public class Board
    {
        /// <summary>
        /// 二维点集（0-14）
        /// </summary>
        public Piece[,] Table { private set; get; }
        public int Step { private set; get; }
        public List<Piece> DownPieces { private set; get; }
        public List<Piece> RelatedPieces { private set; get; }

        public Board()
        {
            for (int x = 0; x < 15; x++)
            {
                for (int y = 0; y < 15; y++)
                {
                    this.Table[x, y] = new Piece(x, y, ColourEnum.Empty);
                }
            }
            Step = 0;
            DownPieces = new List<Piece>();
        }


        public Board Clone()
        {
            var item = new Board();
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    item.Table[i, j] = Table[i, j].Clone();
                }
            }
            item.Step = Step;
            item.DownPieces = Piece.ClonePieceList(DownPieces);
            return item;

        }
        public Board ChangeBoard(int x, int y, int step)
        {
            var newBoard = Clone();
            newBoard.Step = step;

            bool isBlack = step % 2 == 1;
            var colour = isBlack ? ColourEnum.Black : ColourEnum.White;
            var p = newBoard.Table[x, y];
            p.Colour = colour;
            DownPieces.Add(p);
            UpdateRelatedPieces(x, y);
            return newBoard;
        }

        public ColourEnum[] GetLinePieces(int x, int y, int direction)
        {
            ColourEnum[] line = new ColourEnum[9];
            int index = 0; ColourEnum colour = ColourEnum.Empty;
            for (int i = -4; i <= 4; i++)
            {
                index = 4 + i;
                if (i == 0) continue;
                if (direction == 0)
                {
                    if (x + i >= 0 && x + i < 15) // -向
                    {
                        colour = Table[x + i, y].Colour;
                    }
                    else
                    {
                        colour = ColourEnum.Out;
                    }

                }
                if (direction == 1)
                {
                    if (y + i >= 0 && y + i < 15) // |向
                    {
                        colour = Table[x, y + i].Colour;
                    }
                    else
                    {
                        colour = ColourEnum.Out;
                    }

                }
                if (direction == 2)
                {
                    if (x - i >= 0 && x - i < 15 && y + i >= 0 && y + i < 15) // /向
                    {
                        colour = Table[x - i, y + i].Colour;
                    }
                    else
                    {
                        colour = ColourEnum.Out;
                    }

                }
                if (direction == 3)
                {
                    if (x + i >= 0 && x + i < 15 && y + i >= 0 && y + i < 15) // \向
                    {
                        colour = Table[x + i, y + i].Colour;
                    }
                    else
                    {
                        colour = ColourEnum.Out;
                    }

                }
                line[index] = colour;
            }
            return line;
        }

        private void UpdateRelatedPieces(int x, int y)
        {
            RelatedPieces.RemoveAll(a => a.X == x && a.Y == y);
            for (int i = -5; i <= 5; i++)
            {
                if (i == 0) continue;
                //相关点不超出棋盘范围
                if (x + i >= 0 && x + i < 15) // -向
                {
                    var item = Table[x + i, y];
                    if (!RelatedPieces.Exists(a => a.X == item.X && a.Y == item.Y))
                        RelatedPieces.Add(item);
                }
                if (y + i >= 0 && y + i < 15) // |向
                {
                    var item = Table[x, y + i];
                    if (!RelatedPieces.Exists(a => a.X == item.X && a.Y == item.Y))
                        RelatedPieces.Add(item);

                }
                if (x + i >= 0 && x + i < 15 && y + i >= 0 && y + i < 15) // \向
                {
                    var item = Table[x + i, y + i];
                    if (!RelatedPieces.Exists(a => a.X == item.X && a.Y == item.Y))
                        RelatedPieces.Add(item);

                }
                if (x - i >= 0 && x - i < 15 && y + i >= 0 && y + i < 15) // /向
                {
                    var item = Table[x - i, y + i];
                    if (!RelatedPieces.Exists(a => a.X == item.X && a.Y == item.Y))
                        RelatedPieces.Add(item);

                }
            }
        }


    }
}
