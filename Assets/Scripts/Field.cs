using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field
{
    public FieldType fieldType;
    public EntityType entityType;

    public Field(FieldType field, EntityType entity = EntityType.None)
    {
        this.fieldType = field;
        this.entityType = entity;
    }
}

public enum FieldType
{
    Wall,
    Floor,
    Target,
}
public enum EntityType
{
    Player,
    Chest,
    None,
}
