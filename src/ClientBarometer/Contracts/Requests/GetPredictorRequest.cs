namespace ClientBarometer.Contracts.Requests
{
    public class GetPredictorRequest
    {
        public double PrevBaro { get; set; }
        public string CustomerInitMessage { get; set; }
        public string SellerAnswer { get; set; }
        public string CustomerFollowingMessage { get; set; }
    }
}