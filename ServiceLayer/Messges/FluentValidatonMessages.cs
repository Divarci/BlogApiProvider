namespace ServiceLayer.Messges
{
    public static class FluentValidatonMessages
    {
        public static string EmptyNullMessage(string title) { return $"{title} is required."; }
        public static string MaximumLength(int value) { return $"You are allowed to use maximum {value} character."; }

        
    }
}
