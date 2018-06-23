using System;
using System.Threading.Tasks;
using Xunit;

namespace DaData.Client.Tests
{
    public class SuggestionsClientTest
    {
        public SuggestClient Api { get; set; }

        public SuggestionsClientTest()
        {
            var token = Environment.GetEnvironmentVariable("DADATA_API_KEY");
            var url = "https://suggestions.dadata.ru/suggestions/api/4_1/rs";
            Api = new SuggestClient(token, url);
        }

        [Fact]
        public async Task SuggestAddressTest()
        {
            var query = "москва турчанинов 6с2";

            var response = await Api.QueryAddress(query);
            var addressData = response.Suggestions[0].Data;

            Assert.Equal("119034", addressData.PostalCode);
            Assert.Equal("7704", addressData.TaxOffice);
        }

        [Fact]
        public async Task SuggestAddressLocationsKladrTest()
        {
            var query = new AddressSuggestQuery("ватутина");
            var location = new AddressData { KladrId = "65" };
            query.Locations = new AddressData[] { location };

            var response = await Api.QueryAddress(query);

            Assert.Equal("693022", response.Suggestions[0].Data.PostalCode);
        }

        [Fact]
        public async Task SuggestAddressLocationsFiasCityTest()
        {
            var query = new AddressSuggestQuery("ватутина");
            // Южно-Сахалинск 
            var location = new AddressData { CityFiasId = "44388ad0-06aa-49b0-bbf9-1704629d1d68" };
            query.Locations = new AddressData[] { location };

            var response = await Api.QueryAddress(query);

            Assert.Equal("693022", response.Suggestions[0].Data.PostalCode);
        }

        [Fact]
        public async Task SuggestAddressBoundsTest()
        {
            var query = new AddressSuggestQuery("ново")
            {
                FromBound = new AddressBound("city"),
                ToBound = new AddressBound("city")
            };

            var response = await Api.QueryAddress(query);

            Assert.Equal("Новосибирск", response.Suggestions[0].Data.City);
        }

        [Fact]
        public async Task SuggestAddressHistoryTest()
        {
            var query = "москва хабар";

            var response = await Api.QueryAddress(query);
            var addressData = response.Suggestions[0].Data;

            Assert.Equal("ул Черненко", addressData.HistoryValues[0]);
        }

        [Fact]
        public async Task SuggestBankTest()
        {
            var query = "сбербанк";

            var response = await Api.QueryBank(query);

            Assert.Equal("044525225", response.Suggestions[0].Data.Bic);
            Assert.Equal("Москва", response.Suggestions[0].Data.Address.Data.City);
        }

        [Fact]
        public async Task SuggestBankStatusTest()
        {
            var query = new BankSuggestQuery("витас")
            {
                Status = new PartyStatus[] { PartyStatus.LIQUIDATED }
            };

            var response = await Api.QueryBank(query);

            Assert.Equal("044585398", response.Suggestions[0].Data.Bic);
        }

        [Fact]
        public async Task SuggestBankTypeTest()
        {
            var query = new BankSuggestQuery("я")
            {
                Type = new BankType[] { BankType.NKO }
            };

            var response = await Api.QueryBank(query);

            Assert.Equal("044525444", response.Suggestions[0].Data.Bic);
        }

        [Fact]
        public async Task SuggestEmailTest()
        {
            var query = "anton@m";

            var response = await Api.QueryEmail(query);

            Assert.Equal("anton@mail.ru", response.Suggestions[0].Value);
        }

        [Fact]
        public async Task SuggestFioTest()
        {
            var query = "викт";

            var response = await Api.QueryFio(query);

            Assert.Equal("Виктор", response.Suggestions[0].Data.Name);
        }

        [Fact]
        public async Task SuggestFioPartsTest()
        {
            var query = new FioSuggestQuery("викт")
            {
                Parts = new FioPart[] { FioPart.SURNAME }
            };

            var response = await Api.QueryFio(query);

            Assert.Equal("Викторова", response.Suggestions[0].Data.Surname);
        }

        [Fact]
        public async Task SuggestPartyTest()
        {
            var query = "7707083893";

            var response = await Api.QueryParty(query);
            var party = response.Suggestions[0];
            var address = response.Suggestions[0].Data.Address;

            Assert.Equal("7707083893", party.Data.Inn);
            Assert.Equal("г Москва, ул Вавилова, д 19", address.Value);
            Assert.Equal("117997, ГОРОД МОСКВА, УЛИЦА ВАВИЛОВА, 19", address.Data.Source);
            Assert.Equal("117312", address.Data.PostalCode);
        }

        [Fact]
        public async Task SuggestPartyStatusTest()
        {
            var query = new PartySuggestQuery("витас")
            {
                Status = new PartyStatus[] { PartyStatus.LIQUIDATED }
            };

            var response = await Api.QueryParty(query);

            Assert.Equal("4713008497", response.Suggestions[0].Data.Inn);
        }

        [Fact]
        public async Task SuggestPartyTypeTest()
        {
            var query = new PartySuggestQuery("витас")
            {
                Type = PartyType.INDIVIDUAL
            };

            var response = await Api.QueryParty(query);

            Assert.Equal("470411980055", response.Suggestions[0].Data.Inn);
        }
    }
}