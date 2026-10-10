using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameBase.Tiles;

public class LevelData
{
    public Tilemap Map { get; }
    public IReadOnlyList<SpawnInfo> spawns { get; }

    public LevelData(Tilemap map, IEnumerable<SpawnInfo> spawn)
    {
        Map = map;
        spawns = new List<SpawnInfo>(spawn).AsReadOnly();
    }

    public static List<SpawnInfo> ReadSpawns(XElement root, int columns, int rows)
    {
        var spawns = new List<SpawnInfo>();
        XElement s = root.Element("Spawns");
        if (s == null)
            return spawns;

        foreach (XElement spawn in s.Elements("Spawn"))
        {
            string type = ((string)spawn.Attribute("type"))?.Trim();

            int column = int.Parse((string)spawn.Attribute("column"));
            int row = int.Parse((string)spawn.Attribute("row"));


            spawns.Add(new SpawnInfo(type, column, row));
        }

        return spawns;
    }
}
