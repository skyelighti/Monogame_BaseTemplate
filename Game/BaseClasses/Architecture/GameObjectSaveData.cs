using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase;

public class GameObjectSaveData
{
    public string InstanceId { get; set; }
    public string PrefabId { get; set; }

    public bool IsActive { get; set; } = true;
    public bool ActiveOnSceneEnter { get; set; } = true;

    public float X { get; set; }
    public float Y { get; set; }
    public float Rotation { get; set; }
    public float ScaleX { get; set; }
    public float ScaleY { get; set; }
    public int? Health { get; set; }
    //npc and item dont have hp
    public float? RemainingLifetime { get; set; }
    //bullet

}
