using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase.Tiles;

public class SpawnInfo
{
    public string Type { get; }
    public int Column { get; }
    public int Row { get; }

    public SpawnInfo(string type, int column, int row)
    {
        Type = type;
        Column = column;
        Row = row;
    }
}
