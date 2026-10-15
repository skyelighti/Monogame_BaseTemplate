using System;
using System.Collections.Generic;
using System.IO;
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
        var ids = new HashSet<string>();
        XElement s = root.Element("Spawns");
        if (s == null)
            return spawns;

        foreach (XElement spawn in s.Elements("Spawn"))
        {
            string type = ((string)spawn.Attribute("type"))?.Trim();
            string definition = ((string)spawn.Attribute("definition"))?.Trim();
            string id = ((string)spawn.Attribute("id"))?.Trim();
            if (type != "Player" && type != "NPC" && type != "Pickup")
                throw new InvalidDataException($"Unknown spawn type '{type}'.");
            if (!int.TryParse((string)spawn.Attribute("column"), out int column) ||
                !int.TryParse((string)spawn.Attribute("row"), out int row) ||
                column < 0 || row < 0 || column >= columns || row >= rows)
                throw new InvalidDataException($"Spawn '{id}' is outside the map or has invalid coordinates.");
            if (string.IsNullOrWhiteSpace(id) || !ids.Add(id))
                throw new InvalidDataException($"Spawn ID '{id}' is missing or duplicated.");
            if (type != "Player" && string.IsNullOrWhiteSpace(definition))
                throw new InvalidDataException($"Spawn '{id}' requires a definition.");

            spawns.Add(new SpawnInfo(type, column, row, definition, id));
        }

        return spawns;
    }
}
