namespace ymdb
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // Clear the console to apply the background color to the entire window
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            string[] graph = {
                "██    ██ ███    ███ ██████  ██████ ",
                " ██  ██  ████  ████ ██   ██ ██   ██",
                "  ████   ██ ████ ██ ██   ██ ██████ ",
                "   ██    ██  ██  ██ ██   ██ ██   ██",
                "   ██    ██      ██ ██████  ██████ "
        };

            foreach (string line in graph)
            {
                Console.WriteLine(line);
            }
            Console.ForegroundColor = ConsoleColor.Magenta;
        }
    }
}
