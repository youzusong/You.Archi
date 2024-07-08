namespace You.Archi.Moment
{
    public class Moment : IMoment
    {
        public DateTime Now => DateTime.UtcNow.AddHours(8);

    }
}
