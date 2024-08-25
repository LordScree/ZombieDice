namespace LordScree.ZombieDice.Dice;
public enum ZombieDieFace
{
    Brain,
    Runner,
    Shotgun
}

public enum ZombieDieType
{
    Green,
    Yellow,
    Red
}

public interface IZombieDie
{
    ZombieDieFace[] GetFaces();

    ZombieDieType GetZombieDieType();

    ZombieDieFace LastFace { get; set; }
}