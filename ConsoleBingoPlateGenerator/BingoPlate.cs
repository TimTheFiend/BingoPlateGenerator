using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleBingoPlateGenerator
{
    public class BingoPlate
    {
        private readonly int _PositionLength = 9;
        private readonly int _RowLength = 5;

        public string Position { get; private set; }
        public string TopRow { get; private set; }
        public string MidRow { get; private set; }
        public string BottomRow { get; private set; }

        public int[,] Card;

        public BingoPlate(string id)
        {
            Position = id.Substring(0, _PositionLength);
            TopRow = id.Substring(_PositionLength, _RowLength);
            MidRow = id.Substring(_PositionLength + _RowLength, _RowLength);
            BottomRow = id.Substring(_PositionLength + _RowLength + _RowLength, _RowLength);

            Card = new int[3, 9];
            CreateBingoCard();
        }
        

        private void CreateBingoCard()
        {

            int top = 0;
            int mid = 0;
            int bot = 0;
            int currentPos = 0;

            foreach (char pos in Position.ToCharArray())
            {
                int value = pos - '0';
                switch (value)
                {
                    case 1:
                        Card[0, currentPos] = int.Parse(currentPos + TopRow.Substring(top, 1));
                        top++;
                        break;
                    case 2:
                        Card[1, currentPos] = int.Parse(currentPos + MidRow.Substring(mid, 1));
                        mid++;
                        break;
                    case 3:
                        Card[0, currentPos] = int.Parse(currentPos + TopRow.Substring(top, 1));
                        top++;
                        Card[1, currentPos] = int.Parse(currentPos + MidRow.Substring(mid, 1));
                        mid++;
                        break;
                    case 4:
                        Card[2, currentPos] = int.Parse(currentPos + BottomRow.Substring(bot, 1));
                        bot++;
                        break;
                    case 5:
                        Card[2, currentPos] = int.Parse(currentPos + BottomRow.Substring(bot, 1));
                        bot++;
                        Card[0, currentPos] = int.Parse(currentPos + TopRow.Substring(top, 1));
                        top++;
                        break;
                    case 6:
                        Card[2, currentPos] = int.Parse(currentPos + BottomRow.Substring(bot, 1));
                        bot++;
                        Card[1, currentPos] = int.Parse(currentPos + MidRow.Substring(mid, 1));
                        mid++;
                        break;
                    default:

                        break;
                }

                currentPos++;
            }
        }

        public bool EnsureCorrectValues
        {
            get
            {
                return this.values.CheckLength();
            }
        }

        private List<string> values => new List<string>() { Position, TopRow, MidRow, BottomRow };
    }
}
