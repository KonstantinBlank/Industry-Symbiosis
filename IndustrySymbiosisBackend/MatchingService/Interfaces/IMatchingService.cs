namespace MatchingService.Interfaces
{
    public interface IMatchingService
    {

        string GetAvailableInputStreams(string enterpriseId);
        string GetAvailableOutputStreams(string enterpriseId);
        string GetMatchingInputStreams(int outputStreamId);
        string GetMatchingOutputStreams(int inputStreamId);
        int create(string PersonalStreamId, string PartnerStreamId);


    }
}
