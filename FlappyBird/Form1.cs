using System.Drawing.Drawing2D;

namespace FlappyBird;

public partial class Form1 : Form
{
    private readonly Game game;
    private readonly System.Windows.Forms.Timer gameTimer = new System.Windows.Forms.Timer();
    private readonly Button restartButton = new Button();

    public Form1()
    {
        InitializeComponent();

        Text = "Flappy Bird - YMH219";
        ClientSize = new Size(480, 640);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.SkyBlue;
        DoubleBuffered = true;
        KeyPreview = true;

        game = new Game(ClientSize.Width, ClientSize.Height);

        restartButton.Text = "Yeniden Baþlat";
        restartButton.Size = new Size(120, 30);
        restartButton.Location = new Point(ClientSize.Width / 2 - 60, ClientSize.Height - 45);
        restartButton.TabStop = false;
        restartButton.Click += RestartButton_Click;
        Controls.Add(restartButton);

        gameTimer.Interval = 20;
        gameTimer.Tick += GameTimer_Tick;
        gameTimer.Start();

        KeyDown += Form1_KeyDown;
        Paint += Form1_Paint;
    }

    private void GameTimer_Tick(object? sender, EventArgs e)
    {
        game.Update();
        Invalidate();
    }

    private void Form1_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Space)
        {
            game.Flap();
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.R)
        {
            game.Reset();
        }
    }

    private void RestartButton_Click(object? sender, EventArgs e)
    {
        game.Reset();
        ActiveControl = null;
        Focus();
    }

    private void Form1_Paint(object? sender, PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        using var pipeBrush = new SolidBrush(Color.ForestGreen);
        using var groundBrush = new SolidBrush(Color.SaddleBrown);
        using var infoFont = new Font("Segoe UI", 14, FontStyle.Bold);
        using var bigFont = new Font("Segoe UI", 28, FontStyle.Bold);

        foreach (var pipe in game.Pipes)
        {
            g.FillRectangle(pipeBrush, pipe.TopBounds);
            g.FillRectangle(pipeBrush, pipe.BottomBounds);
        }

        g.FillRectangle(groundBrush, 0, ClientSize.Height - Game.GroundHeight, ClientSize.Width, Game.GroundHeight);

        DrawBird(g, game.Bird);

        g.DrawString($"Skor: {game.Score}   Seviye: {game.Level}   En Yüksek: {game.HighScore}",
            infoFont, Brushes.White, 10, 10);

        if (!game.IsStarted)
            DrawCentered(g, "Baþlamak için SPACE", infoFont, 250);

        if (game.IsGameOver)
        {
            DrawCentered(g, "GAME OVER", bigFont, 230);
            DrawCentered(g, $"Skor: {game.Score}  -  R veya butonla yeniden baþla", infoFont, 290);
        }
    }

    private void DrawCentered(Graphics g, string text, Font font, int y)
    {
        var size = g.MeasureString(text, font);
        g.DrawString(text, font, Brushes.White, (ClientSize.Width - size.Width) / 2, y);
    }
    private void DrawBird(Graphics g, Bird bird)
    {
        var state = g.Save();

        float r = bird.Size / 2f;
        float cx = bird.X + r;
        float cy = bird.Y + r;

        // Hýza göre eðim: yukarý çýkarken burun yukarý, düþerken aþaðý
        float angle = Math.Clamp(bird.Velocity * 4f, -25f, 70f);
        g.TranslateTransform(cx, cy);
        g.RotateTransform(angle);

        using var bodyBrush = new SolidBrush(Color.DeepSkyBlue);
        using var wingBrush = new SolidBrush(Color.DodgerBlue);
        using var bellyBrush = new SolidBrush(Color.WhiteSmoke);
        using var beakBrush = new SolidBrush(Color.Orange);
        using var outline = new Pen(Color.Black, 1.5f);

        // Kuyruk
        PointF[] tail =
        {
        new PointF(-r + 3, -2),
        new PointF(-r - 10, -9),
        new PointF(-r - 10, 7)
    };
        g.FillPolygon(wingBrush, tail);
        g.DrawPolygon(outline, tail);

        // Gövde
        g.FillEllipse(bodyBrush, -r, -r, bird.Size, bird.Size);
        g.DrawEllipse(outline, -r, -r, bird.Size, bird.Size);

        // Karýn
        g.FillEllipse(bellyBrush, -r * 0.2f, r * 0.2f, r * 1.1f, r * 0.65f);

        // Kanat: yükselirken yukarýda, düþerken aþaðýda
        float wingY = bird.Velocity < 0 ? -r * 0.35f : r * 0.05f;
        g.FillEllipse(wingBrush, -r * 0.9f, wingY, r * 1.1f, r * 0.7f);
        g.DrawEllipse(outline, -r * 0.9f, wingY, r * 1.1f, r * 0.7f);

        // Gaga
        PointF[] beak =
        {
        new PointF(r - 2, -4),
        new PointF(r + 10, 1),
        new PointF(r - 2, 6)
    };
        g.FillPolygon(beakBrush, beak);
        g.DrawPolygon(outline, beak);

        // Göz
        g.FillEllipse(Brushes.White, r * 0.15f, -r * 0.65f, r * 0.6f, r * 0.6f);
        g.DrawEllipse(outline, r * 0.15f, -r * 0.65f, r * 0.6f, r * 0.6f);
        g.FillEllipse(Brushes.Black, r * 0.4f, -r * 0.5f, r * 0.25f, r * 0.25f);

        g.Restore(state);
    }
}