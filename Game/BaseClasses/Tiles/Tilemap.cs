using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using GameBase.Assets;

namespace GameBase.Tiles;

public class Tilemap
{
    private readonly Dictionary<string, Tileset> _tileset = new();
    public const int EmptyTile = -1;
    private readonly TileReference[] _tiles;

    /// <summary>
    /// Gets the total number of rows in this tilemap.
    /// </summary>
    public int Rows { get; }

    /// <summary>
    /// Gets the total number of columns in this tilemap.
    /// </summary>
    public int Columns { get; }

    /// <summary>
    /// Gets the total number of tiles in this tilemap.
    /// </summary>
    public int Count { get; }

    /// <summary>
    /// Gets or Sets the scale factor to draw each tile at.
    /// </summary>
    public Vector2 Scale { get; set; }

    public int CellWidth { get; }
    public int CellHeight { get; }

    public float TileWidth => CellWidth * Scale.X;
    public float TileHeight => CellHeight * Scale.Y;

    /// <summary>
    /// Creates a new tilemap.
    /// </summary>
    /// <param name="tileset">The tileset used by this tilemap.</param>
    /// <param name="columns">The total number of columns in this tilemap.</param>
    /// <param name="rows">The total number of rows in this tilemap.</param>
    /// <param name="cellWidth">The width of each tile in pixels.</param>
    /// <param name="cellHeight">The height of each tile in pixels.</param>
    public Tilemap(Dictionary<string, Tileset> tileset, int columns, int rows, int cellWidth, int cellHeight)
    {
        _tileset = tileset;
        Rows = rows;
        Columns = columns;
        Count = Columns * Rows;
        Scale = Vector2.One;
        CellWidth = cellWidth;
        CellHeight = cellHeight;
        _tiles = new TileReference[Count];
        Array.Fill(_tiles, null);
    }

    /// <summary>
    /// Sets the tile at the given index in this tilemap to use the tile from
    /// the tileset at the specified tileset id.
    /// </summary>
    /// <param name="index">The index of the tile in this tilemap.</param>
    /// <param name="tileReference">The tile reference containing the tileset name and ID.</param>
    public void SetTile(int index, TileReference tileReference)
    {
        _tiles[index] = tileReference;
    }

    /// <summary>
    /// Sets the tile at the given column and row in this tilemap to use the tile
    /// from the tileset at the specified tileset id.
    /// </summary>
    /// <param name="column">The column of the tile in this tilemap.</param>
    /// <param name="row">The row of the tile in this tilemap.</param>
    /// <param name="tilesetName">The name of the tileset containing the tile.</param>
    /// <param name="tilesetID">The tileset id of the tile from the tileset to use.</param>
    public void SetTile(int column, int row, string tilesetName, int tilesetID)
    {
        int index = row * Columns + column;
        SetTile(index, new TileReference { TilesetName = tilesetName, TileID = tilesetID });
    }

    /// <summary>
    /// Gets the texture region of the tile from this tilemap at the specified index.
    /// </summary>
    /// <param name="index">The index of the tile in this tilemap.</param>
    /// <returns>The texture region of the tile from this tilemap at the specified index.</returns>
    public TileInfo GetTile(int index)
    {
        TileReference tileReference = _tiles[index];

        if (tileReference == null)
            return null;
        return _tileset[tileReference.TilesetName].GetTile(tileReference.TileID);
    }

    /// <summary>
    /// Gets the texture region of the tile from this tilemap at the specified
    /// column and row.
    /// </summary>
    /// <param name="column">The column of the tile in this tilemap.</param>
    /// <param name="row">The row of the tile in this tilemap.</param>
    /// <returns>The texture region of the tile from this tilemap at the specified column and row.</returns>
    public TileInfo GetTile(int column, int row)
    {
        int index = row * Columns + column;
        return GetTile(index);
    }

    /// <summary>
    /// Draws this tilemap using the given sprite batch.
    /// </summary>
    /// <param name="spriteBatch">The sprite batch used to draw this tilemap.</param>
    public void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < Count; i++)
        {
            TileReference tileReference = _tiles[i];
            if (tileReference == null)
                continue;
            TextureRegion tile = _tileset[tileReference.TilesetName].GetTile(tileReference.TileID).Region;

            int x = i % Columns;
            int y = i / Columns;

            Vector2 position = new Vector2(x * TileWidth, y * TileHeight);
            tile.Draw(spriteBatch, position, Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 1.0f);
        }
    }

    public IEnumerable<TileCollider> CreateColliders()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                TileInfo tile = GetTile(column, row);
                if (tile == null || (!tile.IsSolid && !tile.IsTrigger))
                    continue;

                var position = new Vector2(column * TileWidth, row * TileHeight);
                yield return new TileCollider(tile, new Transform(position, 0f, Scale));
            }
        }
    }

    public bool IsSolid(int column, int row)
    {
        if (column < 0 || column >= Columns || row < 0 || row >= Rows)
        {
            return false;
        }

        int index = row * Columns + column;
        TileReference tileReference = _tiles[index];
        if (tileReference == null)
            return false;
        return _tileset[tileReference.TilesetName].IsSolid(tileReference.TileID);
    }


    private static Tilemap ReadLayer(Tileset tileset, XElement element)
    {
        if (element == null || string.IsNullOrWhiteSpace(element.Value))
            throw new InvalidDataException("The level requires a nonempty Tiles element.");

        string[] rows = element.Value.Trim().Split(
            '\n',
            StringSplitOptions.RemoveEmptyEntries |
            StringSplitOptions.TrimEntries);

        char[] separators = { ' ', '\t' };

        int columnCount = rows[0].Split(
            separators,
            StringSplitOptions.RemoveEmptyEntries).Length;

        Tilemap map = new Tilemap(new Dictionary<string, Tileset> { { tileset.Name, tileset } }, columnCount, rows.Length, tileset.TileWidth, tileset.TileHeight);

        for (int row = 0; row < rows.Length; row++)
        {
            string[] cells = rows[row].Split(
                separators,
                StringSplitOptions.RemoveEmptyEntries);

            if (cells.Length != columnCount)
            {
                throw new InvalidDataException(
                    $"Row {row} has the wrong number of tiles.");
            }

            for (int column = 0; column < columnCount; column++)
            {
                if (!int.TryParse(cells[column], out int tileID))
                    throw new InvalidDataException($"Invalid tile ID at column {column}, row {row}.");
                if (tileID == EmptyTile)
                    continue;

                if (tileID < 0 || tileID >= tileset.Count)
                    throw new InvalidDataException($"Tile ID {tileID} at ({column}, {row}) is outside the tileset.");

                map.SetTile(column, row, tileset.Name, tileID);
            }
        }

        return map;
    }

    /// <summary>
    /// Creates a new tilemap based on a tilemap xml configuration file.
    /// </summary>
    /// <param name="content">The content manager used to load the texture for the tileset.</param>
    /// <param name="filename">The path to the xml file, relative to the content root directory.</param>
    /// <returns>The tilemap created by this method.</returns>
    public static Tilemap FromFile(ContentManager content, string filename)
    {
        return LoadLevel(content, filename).Map;
    }

    public static LevelData LoadLevel(ContentManager content, string filename)
    {
        string filePath = Path.Combine(content.RootDirectory, filename);

        using (Stream stream = TitleContainer.OpenStream(filePath))
        {
            using (XmlReader reader = XmlReader.Create(stream))
            {
                XDocument doc = XDocument.Load(reader);
                XElement root = doc.Root;
                if (root == null || root.Name != "Tilemap")
                    throw new InvalidDataException("The level root must be Tilemap.");

                // The <Tileset> element contains the information about the tileset
                // used by the tilemap.
                //
                // Example
                // <Tileset region="0 0 100 100" tileWidth="10" tileHeight="10">contentPath</Tileset>
                //
                // The region attribute represents the x, y, width, and height
                // components of the boundary for the texture region within the
                // texture at the contentPath specified.
                //
                // the tileWidth and tileHeight attributes specify the width and
                // height of each tile in the tileset.
                //
                // the contentPath value is the contentPath to the texture to
                // load that contains the tileset
                XElement tilesetElement = root.Element("Tileset")
                    ?? throw new InvalidDataException("The level requires a Tileset element.");

                string name = (string)tilesetElement.Attribute("name");
                if (string.IsNullOrWhiteSpace(name))
                    throw new InvalidDataException("Tileset requires a name attribute.");

                string regionAttribute = (string)tilesetElement.Attribute("region")
                    ?? throw new InvalidDataException("Tileset requires a region attribute.");
                string[] split = regionAttribute.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (split.Length != 4)
                    throw new InvalidDataException("Tileset region must contain x, y, width, and height.");
                int x = int.Parse(split[0]);
                int y = int.Parse(split[1]);
                int width = int.Parse(split[2]);
                int height = int.Parse(split[3]);

                int tileWidth = int.Parse(tilesetElement.Attribute("tileWidth").Value);
                int tileHeight = int.Parse(tilesetElement.Attribute("tileHeight").Value);
                string contentPath = tilesetElement.Value.Trim();

                // Load the texture 2d at the content path
                Texture2D texture = content.Load<Texture2D>(contentPath);

                // Create the texture region from the texture
                TextureRegion textureRegion = new TextureRegion(texture, x, y, width, height);

                // Create the tileset using the texture region
                Tileset tileset = new Tileset(name, textureRegion, tileWidth, tileHeight);

                XElement solidElement = root.Element("SolidTiles");

                if (solidElement != null)
                {
                    foreach (int tileID in ReadTileIds(solidElement, tileset.Count))
                    {
                        tileset.SetSolid(tileID, true);
                    }
                }

                XElement layersElement = root.Element("TileLayers");
                if (layersElement != null)
                {
                    foreach (XElement layerElement in layersElement.Elements("Layer"))
                    {
                        string layerName = (string)layerElement.Attribute("name");
                        if (!Enum.TryParse(layerName, out Layer layer) ||
                            !Enum.IsDefined(typeof(Layer), layer) || layerName != layer.ToString())
                            throw new InvalidDataException($"Unknown layer name '{layerName}'.");

                        foreach (int tileID in ReadTileIds(layerElement, tileset.Count))
                            tileset.SetLayer(tileID, layer);
                    }
                }

                XElement triggerElement = root.Element("TriggerTiles");
                if (triggerElement != null)
                {
                    foreach (int tileID in ReadTileIds(triggerElement, tileset.Count))
                        tileset.SetTrigger(tileID, true);
                }

                // The <Tiles> element contains lines of strings where each line
                // represents a row in the tilemap.  Each line is a space
                // separated string where each element represents a column in that
                // row.  The value of the column is the id of the tile in the
                // tileset to draw for that location.
                //
                // Example:
                // <Tiles>
                //      00 01 01 02
                //      03 04 04 05
                //      03 04 04 05
                //      06 07 07 08
                // </Tiles>
                Tilemap ground = ReadLayer(tileset, root.Element("Tiles"));


                return new LevelData(ground, LevelData.ReadSpawns(root, ground.Columns, ground.Rows));
            }
        }


    }


    private static IEnumerable<int> ReadTileIds(XElement element, int tileCount)
    {
        string[] values = element.Value.Split(
            new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string value in values)
        {
            if (!int.TryParse(value, out int tileID) || tileID < 0 || tileID >= tileCount)
                throw new InvalidDataException($"Invalid tile ID '{value}' in {element.Name}.");
            yield return tileID;
        }
    }

}
