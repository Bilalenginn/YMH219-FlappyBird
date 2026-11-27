namespace FlappyBird;

public class Game
{
    public const int GroundHeight = 60;
    private const int PipeSpacing = 220;
    private const int PipeCount = 3;

    private readonly int width;
    private readonly int height;

    public Bird Bird { get; }
    public List<Pipe> Pipes { get; } = new List<Pipe>();
    public int Score { get; private set; }
    public int HighScore { get; private set; }
    public int Level => Score / 5 + 1;
    public float Speed => 3f + (Level - 1) * 0.5f;
    public bool IsStarted { get; private set; }
    public bool IsGameOver { get; private set; }

    public Game(int width, int height)
    {
        this.width = width;
        this.height = height;
        Bird = new Bird(100, height / 2f);
        Reset();
    }

    public void Reset()
    {
        Bird.Reset(height / 2f);
        Pipes.Clear();
        for (int i = 0; i < PipeCount; i++)
            Pipes.Add(new Pipe(width + 100 + i * PipeSpacing, height - GroundHeight));

        Score = 0;
        IsStarted = false;
        IsGameOver = false;
    }

    public void Flap()
    {
        if (IsGameOver) return;
        IsStarted = true;
        Bird.Flap();
    }

    public void Update()
    {
        if (!IsStarted || IsGameOver) return;

        Bird.Update();

        foreach (var pipe in Pipes)
        {
            pipe.Move(Speed);

            if (!pipe.Passed && pipe.X + pipe.Width < Bird.X)
            {
                pipe.Passed = true;
                Score++;
                HighScore = Math.Max(HighScore, Score);
            }

            if (pipe.X + pipe.Width < 0)
            {
                float maxX = Pipes.Max(p => p.X);
                pipe.Reposition(maxX + PipeSpacing, height - GroundHeight);
            }
        }

        CheckCollision();
    }

    private void CheckCollision()
    {
        var b = Bird.Bounds;
        bool hitPipe = Pipes.Any(p => b.IntersectsWith(p.TopBounds) || b.IntersectsWith(p.BottomBounds));
        bool hitGround = Bird.Y + Bird.Size >= height - GroundHeight;
        bool hitCeiling = Bird.Y < 0;

        if (hitPipe || hitGround || hitCeiling)
            IsGameOver = true;
    }
}