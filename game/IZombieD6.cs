namespace ZombieDice;
public enum DieFace
{
    Brain,
    Runner,
    Shotgun
}

public enum DieType
{
    Green,
    Yellow,
    Red
}

public interface IZombieD6
{
    DieFace[] GetFaces();

    DieType GetDieType();
}