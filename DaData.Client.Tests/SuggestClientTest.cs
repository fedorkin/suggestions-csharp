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
            var token = "b6dd2fbec849b949ba1702261294853289a1e106";// Environment.GetEnvironmentVariable("DADATA_API_KEY");
            var url = "https://suggestions.dadata.ru/suggestions/api/4_1/rs";
            Api = new SuggestClient(token, url);
        }

        [Fact]
        public async Task SuggestAddressTest()
        {
            var query = "москва турчанинов 6с2";

            var response = await Api.QueryAddress(query);
            var address_data = response.suggestions[0].data;

            Assert.Equal("119034", address_data.postal_code);
            Assert.Equal("7704", address_data.tax_office);
        }

        [Fact]
        public async Task SuggestAddressLocationsKladrTest()
        {
            var query = new AddressSuggestQuery("ватутина");
            var location = new AddressData { kladr_id = "65" };
            query.locations = new AddressData[] { location };

            var response = await Api.QueryAddress(query);

            Assert.Equal("693022", response.suggestions[0].data.postal_code);
        }

        [Fact]
        public async Task SuggestAddressLocationsFiasCityTest()
        {
            var query = new AddressSuggestQuery("ватутина");
            // Южно-Сахалинск 
            var location = new AddressData { city_fias_id = "44388ad0-06aa-49b0-bbf9-1704629d1d68" };
            query.locations = new AddressData[] { location };

            var response = await Api.QueryAddress(query);

            Assert.Equal("693022", response.suggestions[0].data.postal_code);
        }

        [Fact]
        public async Task SuggestAddressBoundsTest()
        {
            var query = new AddressSuggestQuery("ново")
            {
                from_bound = new AddressBound("city"),
                to_bound = new AddressBound("city")
            };

            var response = await Api.QueryAddress(query);

            Assert.Equal("Новосибирск", response.suggestions[0].data.city);
        }

        [Fact]
        public async Task SuggestAddressHistoryTest()
        {
            var query = "москва хабар";

            var response = await Api.QueryAddress(query);
            var address_data = response.suggestions[0].data;

            Assert.Equal("ул Черненко", address_data.history_values[0]);
        }

        [Fact]
        public async Task SuggestBankTest()
        {
            var query = "сбербанк";

            var response = await Api.QueryBank(query);

            Assert.Equal("044525225", response.suggestions[0].data.bic);
            Assert.Equal("Москва", response.suggestions[0].data.address.data.city);
        }

        [Fact]
        public async Task SuggestBankStatusTest()
        {
            var query = new BankSuggestQuery("витас")
            {
                status = new PartyStatus[] { PartyStatus.LIQUIDATED }
            };

            var response = await Api.QueryBank(query);

            Assert.Equal("044585398", response.suggestions[0].data.bic);
        }

        [Fact]
        public async Task SuggestBankTypeTest()
        {
            var query = new BankSuggestQuery("я")
            {
                type = new BankType[] { BankType.NKO }
            };

            var response = await Api.QueryBank(query);

            Assert.Equal("044525444", response.suggestions[0].data.bic);
        }

        [Fact]
        public async Task SuggestEmailTest()
        {
            var query = "anton@m";

            var response = await Api.QueryEmail(query);

            Assert.Equal("anton@mail.ru", response.suggestions[0].value);
        }

        [Fact]
        public async Task SuggestFioTest()
        {
            var query = "викт";

            var response = await Api.QueryFio(query);

            Assert.Equal("Виктор", response.suggestions[0].data.name);
        }

        [Fact]
        public async Task SuggestFioPartsTest()
        {
            var query = new FioSuggestQuery("викт")
            {
                parts = new FioPart[] { FioPart.SURNAME }
            };

            var response = await Api.QueryFio(query);

            Assert.Equal("Викторова", response.suggestions[0].data.surname);
        }

        [Fact]
        public async Task SuggestPartyTest()
        {
            var query = "7707083893";

            var response = await Api.QueryParty(query);
            var party = response.suggestions[0];
            var address = response.suggestions[0].data.address;

            Assert.Equal("7707083893", party.data.inn);
            Assert.Equal("г Москва, ул Вавилова, д 19", address.value);
            Assert.Equal("117997, ГОРОД МОСКВА, УЛИЦА ВАВИЛОВА, 19", address.data.source);
            Assert.Equal("117312", address.data.postal_code);
        }

        [Fact]
        public async Task SuggestPartyStatusTest()
        {
            var query = new PartySuggestQuery("витас")
            {
                status = new PartyStatus[] { PartyStatus.LIQUIDATED }
            };

            var response = await Api.QueryParty(query);

            Assert.Equal("4713008497", response.suggestions[0].data.inn);
        }

        [Fact]
        public async Task SuggestPartyTypeTest()
        {
            var query = new PartySuggestQuery("витас")
            {
                type = PartyType.INDIVIDUAL
            };

            var response = await Api.QueryParty(query);

            Assert.Equal("470411980055", response.suggestions[0].data.inn);
        }
    }
}