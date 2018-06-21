using System;
using System.Net;
using System.Threading.Tasks;
using RestSharp;

namespace suggestionscsharp
{
    public class SuggestClient
    {
        private const string SUGGESTIONS_URL = "{0}/suggest";
        private const string ADDRESS_RESOURCE = "address";
        private const string PARTY_RESOURCE = "party";
        private const string BANK_RESOURCE = "bank";
        private const string FIO_RESOURCE = "fio";
        private const string EMAIL_RESOURCE = "email";

        private RestClient client;
        private string token;
        private ContentType contentType = ContentType.JSON;

        public IWebProxy Proxy
        {
            get { return client.Proxy; }
            set { client.Proxy = value; }
        }

        public SuggestClient(string token, string baseUrl)
        {
            this.token = token;
            this.client = new RestClient(String.Format(SUGGESTIONS_URL, baseUrl));
        }

        public Task<SuggestAddressResponse> QueryAddress(string address)
        {
            return QueryAddress(new AddressSuggestQuery(address));
        }

        public Task<SuggestAddressResponse> QueryAddress(AddressSuggestQuery query)
        {
            var request = new RestRequest(ADDRESS_RESOURCE, Method.POST);
            return Execute<SuggestAddressResponse>(request, query);
        }

        public Task<SuggestBankResponse> QueryBank(string bank)
        {
            return QueryBank(new BankSuggestQuery(bank));
        }

        public Task<SuggestBankResponse> QueryBank(BankSuggestQuery query)
        {
            var request = new RestRequest(BANK_RESOURCE, Method.POST);
            return Execute<SuggestBankResponse>(request, query);
        }

        public Task<SuggestEmailResponse> QueryEmail(string email)
        {
            var request = new RestRequest(EMAIL_RESOURCE, Method.POST);
            var query = new SuggestQuery(email);
            return Execute<SuggestEmailResponse>(request, query);
        }

        public Task<SuggestFioResponse> QueryFio(string fio)
        {
            return QueryFio(new FioSuggestQuery(fio));
        }

        public Task<SuggestFioResponse> QueryFio(FioSuggestQuery query)
        {
            var request = new RestRequest(FIO_RESOURCE, Method.POST);
            return Execute<SuggestFioResponse>(request, query);
        }

        public Task<SuggestPartyResponse> QueryParty(string party)
        {
            return QueryParty(new PartySuggestQuery(party));
        }

        public Task<SuggestPartyResponse> QueryParty(PartySuggestQuery query)
        {
            var request = new RestRequest(PARTY_RESOURCE, Method.POST);
            return Execute<SuggestPartyResponse>(request, query);
        }

        private async Task<T> Execute<T>(RestRequest request, SuggestQuery query) where T : new()
        {
            request.AddHeader("Authorization", "Token " + this.token);
            request.AddHeader("Content-Type", contentType.Name);
            request.AddHeader("Accept", contentType.Name);
            request.RequestFormat = contentType.Format;
            request.XmlSerializer.ContentType = contentType.Name;
            request.AddBody(query);
            var response = await client.ExecuteTaskAsync<T>(request);

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

            return response.Data;
        }
    }
}