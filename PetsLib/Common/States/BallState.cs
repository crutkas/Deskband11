namespace PetsLib.Common.States
{
    /// <summary>
    /// Represents the state of a ball that pets can chase.
    /// </summary>
    public class BallState(double cx, double cy, double vx, double vy)
    {
        public double Cx { get; set; } = cx;
        public double Cy { get; set; } = cy;
        public double Vx { get; set; } = vx;
        public double Vy { get; set; } = vy;
        public bool Paused { get; set; } = false;
    }
}