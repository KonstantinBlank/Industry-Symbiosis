namespace MatchingService.Interfaces
{
    public interface IStreamMatchingService
    {
        /*
        string GetAvailableInputStreams(int enterpriseId);
        string GetAvailableOutputStreams(int enterpriseId);
        string GetMatchingInputStreams(int outputStreamId);
        string GetMatchingOutputStreams(int inputStreamId);
        */
        int propose(int enterpriseId, bool selectedIsInput, int selectedStreamId, int requestedStreamId, float amount, float priceProposal, string comment);
        public int accept(int matchID);

    }
}
