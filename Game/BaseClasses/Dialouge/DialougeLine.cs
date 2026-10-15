using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase.Dialogue;

public class DialogueLine
{
    public string Name;
    public string dialogue;
    public Sprite sprite;
    // should this contain a string to the location of the sprite instaead?
    public Sprite background;
    public DialogueLine(string n, string d, Sprite s, Sprite b)
    {
        Name = n;
        dialogue = d;
        sprite = s;
        background = b;
    }
}
