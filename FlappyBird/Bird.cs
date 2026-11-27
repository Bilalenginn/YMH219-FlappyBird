namespace FlappyBird;

public class Bird
{
    public float X { get; }
    public float Y { get; private set; }
    public float Velocity { get; private set; }
    public int Size { get; } = 30;
    public float Gravity { get; } = 0.6f;
    public float FlapStrength { get; } = -8f;

    public Bird(float x, float y)
    {
        X = x;
        Y = y;
    }

    public void Update()
    {
        Velocity += Gravity;
        Y += Velocity;
    }

    public void Flap() => Velocity = FlapStrength;

    public void Reset(float y)
    {
        Y = y;
        Velocity = 0;
    }

    public Rectangle Bounds => new Rectangle((int)X, (int)Y, Size, Size);
}