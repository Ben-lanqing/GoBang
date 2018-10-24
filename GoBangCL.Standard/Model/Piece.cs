using System;
using System.Collections.Generic;
using System.Text;

namespace GoBangCL.Standard.Model
{
    public class Piece
    {
        /// <summary>
        /// 点横向坐标（左0-右14）
        /// </summary>
        public int X { set; get; }
        /// <summary>
        /// 点纵向坐标（上0-下14）
        /// </summary>
        public int Y { set; get; }
        /// <summary>
        /// 1黑，2白, 空点0
        /// </summary>
        public ColourEnum Colour { set; get; }

        public Piece()
        {
            X = 0;
            Y = 0;
            Colour = ColourEnum.Empty;
        }
        public Piece(int x, int y, ColourEnum colour)
        {
            X = x;
            Y = y;
            Colour = colour;
        }

        public Piece Clone()
        {
            var item = new Piece();
            item.X = X;
            item.Y = Y;
            item.Colour = Colour;
            return item;
        }
        public static List<Piece> ClonePieceList(List<Piece> source)
        {
            var list = new List<Piece>();
            if (source == null || source.Count == 0) return list;
            foreach (var item in source)
            {
                list.Add(item.Clone());
            }
            return list;
        }
    }
}
