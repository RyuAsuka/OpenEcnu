namespace OpenEcnuSDK.Entities
{
    public class AccessToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string user_id { get; set; }
    }
}
