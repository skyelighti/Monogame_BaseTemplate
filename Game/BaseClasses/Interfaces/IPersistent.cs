using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase;

public interface IPersistent
{
    void CaptureState(GameObjectSaveData data);
    void RestoreState(GameObjectSaveData data);
}