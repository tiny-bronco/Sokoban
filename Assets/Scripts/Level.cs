using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public List<List<Field>> levelLayout;

    public int width = 7;
    public int height = 5;
    public Level()
    {
        levelLayout = new List<List<Field>>();

        string demoLevel = 
@"#######
#....##
#@.$._#
#....##
#######";

        CreateFromString(demoLevel);
    }
    public Level(string s)
    {
        levelLayout = new List<List<Field>>();

        CreateFromString(s);
    }
    private void CreateFromString(string s)
    {
        // # sciana
        // . podloga
        // @ gracz
        // $ skrzynka
        // _ miejsce docelowe
        
        char[] charArr = s.ToCharArray();

        List<Field> fieldList = new List<Field>();

        foreach ( char c in charArr)
        {

             switch(c)
            {
                case '#':
                    fieldList.Add(new Field(FieldType.Wall));
                    break;
                case '.':
                    fieldList.Add(new Field(FieldType.Floor));
                    break;
                case '_':
                    fieldList.Add(new Field(FieldType.Target));
                    break;
                case '@':
                    fieldList.Add(new Field(FieldType.Floor, EntityType.Player));
                    break;
                case '$':
                    fieldList.Add(new Field(FieldType.Floor, EntityType.Chest));
                    break;
                case '\n':
                    levelLayout.Add(new List<Field>(fieldList));
                    fieldList.Clear();
                    break;
            }
        }
        levelLayout.Add(new List<Field>(fieldList));
    }
}
