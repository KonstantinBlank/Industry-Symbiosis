namespace MatchingService.Interfaces
{
    public interface IStreamMatchingService
    {

        string GetAvailableInputStreams(int enterpriseId);
        string GetAvailableOutputStreams(int enterpriseId);
        string GetMatchingInputStreams(int outputStreamId);
        string GetMatchingOutputStreams(int inputStreamId);
        int create(int enterpriseId, bool selectedIsInput, string selectedStreamId, string requestedStreamId, float amount, float priceProposal, string comment);


    }
}
