namespace FlappyBird;

public class Pipe
{
    private static readonly Random rnd = new Random();

    public float X { get; private set; }
    public int Width { get; } = 60;
    public int GapY { get; private set; }
    public int GapHeight { get; } = 150;
    public bool Passed { get; set; }

    public Pipe(float x, int playAreaHeight)
    {
        Reposition(x, playAreaHeight);
    }

    public void Move(float speed) => X -= speed;

    public void Reposition(float x, int playAreaHeight)
    {
        X = x;
        GapY = rnd.Next(60, playAreaHeight - GapHeight - 60);
        Passed = false;
    }

    public Rectangle TopBounds => new Rectangle((int)X, 0, Width, GapY);
    public Rectangle BottomBounds => new Rectangle((int)X, GapY + GapHeight, Width, 2000);
}