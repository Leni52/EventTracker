public class ItemDoesNotExistException : HttpException
{
    public ItemDoesNotExistException(string message = "Item does not exist.") : base(message)
    {
        StatusCode = HttpStatusCode.NotFound;
    }
}
}
