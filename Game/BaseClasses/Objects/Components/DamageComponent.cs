using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase;

public class DamageComponent : Component, IDamagable
{
    public (int curr, int max) health { get; private set; }
    public event Action DmgTaken;
    public event Action Death;
    public event Action Heal;
    public DamageComponent(int hp, GameObject o) : base(o)
    {
        health = (hp, hp);
    }

    public void takeDamage(int dmg)
    {
        if (health.curr <= 0 || dmg < 0 || dmg == 0) { return; }
        if (health.curr - dmg <= 0)
        {
            health = (0, health.max);
            Death?.Invoke();
        }
        else {
            health = (health.curr - dmg, health.max);
        }
        DmgTaken?.Invoke();
    }
    public void healHP(int hp)
    {
        if (health.curr <= 0 || hp < 0|| hp == 0) { return; }
        if (health.curr + hp >= health.max)
        {
            health = (health.max, health.max);
        }
        else
        {
            health = (health.curr + hp, health.max);
        }
        Heal?.Invoke();
    }
}
