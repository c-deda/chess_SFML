namespace Chess
{
    class Program
    {
        public static void Main(string[] args)
        {
            GameClient.Init();
            GameClient.Instance().Run();
        }
    }
}
