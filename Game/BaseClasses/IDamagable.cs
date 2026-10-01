using BaseTemplate.Game.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IDamagable
{
    public (int curr, int max) health { get; }
    //have  this be protected set
    public void takeDamage(int dmg);

    //class implements how damage works
    // maybe also add a ui thing, where it should have a method to update UI?
    public event Action DmgTaken;
}
