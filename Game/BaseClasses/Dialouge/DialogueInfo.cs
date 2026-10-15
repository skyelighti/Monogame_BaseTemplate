
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameBase.Dialogue;

public class DialogueInfo
{
    public Character character;
    List<DialogueLine> dialogue = new();
    public string ID;
    public bool advancesProgress;
    int indx = 0;

    public DialogueInfo(List<DialogueLine> d, string id, bool advanceafter, Character c)
    {
        dialogue = d;
        ID = id;
        advancesProgress = advanceafter;
        character = c;

    }
    public DialogueLine GetDialogueLine()
    {
        return dialogue[indx];
    }
    public string GetDialogue()
    {
        string dia = indx < dialogue.Count ? dialogue[indx].dialogue : null;
        return dia;
    }
    public string GetName()
    {
        string name = indx < dialogue.Count ? dialogue[indx].Name : null;
        return name;
    }
    public Sprite GetIcon()
    {
        Sprite icon = indx < dialogue.Count ? dialogue[indx].sprite : null;
        return icon;
    }
    public Sprite GetBackground()
    {
        Sprite bg = indx < dialogue.Count ? dialogue[indx].background : null;
        return bg;
    }
    public void IncrementIndex()
    {
        indx++;
    }
    public void ResetIndex()
    {
        indx = 0;
    }

    public static List<DialogueInfo> FromFile(ContentManager content, string filename)
    {
        List<DialogueInfo> dialogueDefs = new();
        string filePath = Path.Combine(content.RootDirectory, filename);
        using (Stream stream = TitleContainer.OpenStream(filePath))
        {
            XDocument document = XDocument.Load(stream);
            XElement root = document.Root;
            foreach (XElement dInfoElement in root.Elements("DialogueInfo"))
            {
                string character = (string)dInfoElement.Element("Character");
                string a = (string)dInfoElement.Element("Advance");
                string ID = (string)dInfoElement.Element("ID");
                List<DialogueLine> dialogueLine = new();
                foreach (XElement dLine in dInfoElement.Element("DialogueLines").Elements("Line"))
                {
                    string n = (string)dLine.Element("Name");
                    string d = (string)dLine.Element("Dialogue");
                    string s = (string)dLine.Element("Sprite");
                    string b = (string)dLine.Element("Background");

                    Sprite sp = string.IsNullOrWhiteSpace(s) ? null : Services.Sprites.CreateSprite(s);
                    Sprite bg = string.IsNullOrWhiteSpace(b) ? null : Services.Sprites.CreateSprite(b);
                    dialogueLine.Add(new DialogueLine(n, d, sp, bg));
                }
                Character c = Enum.Parse<Character>(character);
                bool advance = bool.Parse(a);
                dialogueDefs.Add(new DialogueInfo(dialogueLine, ID, advance, c));
            }
        }

        return dialogueDefs;
    }
}
