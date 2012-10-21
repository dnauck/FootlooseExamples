namespace FootlooseExamples.AsyncAwait.Shared
{
    public interface IServiceContract
    {
        ResponseDto HandleRequest(RequestDto request);
    }
}
