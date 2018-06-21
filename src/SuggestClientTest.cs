using System;
using Xunit;

namespace suggestionscsharp
{
    public class SuggestionsClientTest
    {
        public SuggestClient api { get; set; }

        public SuggestionsClientTest()
        {
            var token = Environment.GetEnvironmentVariable("DADATA_API_KEY");
            var url = "https://suggestions.dadata.ru/suggestions/api/4_1/rs";
            this.api = new SuggestClient(token, url);
        }

        [Fact]
        public async void SuggestAddressTest()
        {
            var query = "москва турчанинов 6с2";
            var response = await api.QueryAddress(query);
            var address_data = response.suggestions[0].data;
            Assert.Equal("119034", address_data.postal_code);
            Assert.Equal("7704", address_data.tax_office);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async void SuggestAddressLocationsKladrTest()
        {
            var query = new AddressSuggestQuery("ватутина");
            var location = new AddressData();
            location.kladr_id = "65";
            query.locations = new AddressData[] { location };
            var response = await api.QueryAddress(query);
            Assert.Equal("693022", response.suggestions[0].data.postal_code);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async void SuggestAddressLocationsFiasCityTest()
        {
            var query = new AddressSuggestQuery("ватутина");
            var location = new AddressData();
            location.city_fias_id = "44388ad0-06aa-49b0-bbf9-1704629d1d68"; // Южно-Сахалинск
            query.locations = new AddressData[] { location };
            var response = await api.QueryAddress(query);
            Assert.Equal("693022", response.suggestions[0].data.postal_code);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async void SuggestAddressBoundsTest()
        {
            var query = new AddressSuggestQuery("ново");
            query.from_bound = new AddressBound("city");
            query.to_bound = new AddressBound("city");
            var response = await api.QueryAddress(query);
            Assert.Equal("Новосибирск", response.suggestions[0].data.city);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async void SuggestAddressHistoryTest()
        {
            var query = "москва хабар";
            var response = await api.QueryAddress(query);
            var address_data = response.suggestions[0].data;
            Assert.Equal("ул Черненко", address_data.history_values[0]);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async void SuggestBankTest()
        {
            var query = "сбербанк";
            var response = await api.QueryBank(query);
            Assert.Equal("044525225", response.suggestions[0].data.bic);
            Assert.Equal("Москва", response.suggestions[0].data.address.data.city);
            Console.WriteLine(response.suggestions[0].data.opf.type);
            Console.WriteLine(response.suggestions[0].data.state.status);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async void SuggestBankStatusTest()
        {
            var query = new BankSuggestQuery("витас");
            query.status = new PartyStatus[] { PartyStatus.LIQUIDATED };
            var response = await api.QueryBank(query);
            Assert.Equal("044585398", response.suggestions[0].data.bic);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async void SuggestBankTypeTest()
        {
            var query = new BankSuggestQuery("я");
            query.type = new BankType[] { BankType.NKO };
            var response = await api.QueryBank(query);
            Assert.Equal("044525444", response.suggestions[0].data.bic);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async void SuggestEmailTest()
        {
            var query = "anton@m";
            var response = await api.QueryEmail(query);
            Assert.Equal("anton@mail.ru", response.suggestions[0].value);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async void SuggestFioTest()
        {
            var query = "викт";
            var response = await api.QueryFio(query);
            Assert.Equal("Виктор", response.suggestions[0].data.name);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async void SuggestFioPartsTest()
        {
            var query = new FioSuggestQuery("викт");
            query.parts = new FioPart[] { FioPart.SURNAME };
            var response = await api.QueryFio(query);
            Assert.Equal("Викторова", response.suggestions[0].data.surname);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async void SuggestPartyTest()
        {
            var query = "7707083893";
            var response = await api.QueryParty(query);
            var party = response.suggestions[0];
            var address = response.suggestions[0].data.address;
            Assert.Equal("7707083893", party.data.inn);
            Assert.Equal("г Москва, ул Вавилова, д 19", address.value);
            Assert.Equal("117997, ГОРОД МОСКВА, УЛИЦА ВАВИЛОВА, 19", address.data.source);
            Assert.Equal("117312", address.data.postal_code);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async void SuggestPartyStatusTest()
        {
            var query = new PartySuggestQuery("витас");
            query.status = new PartyStatus[] { PartyStatus.LIQUIDATED };
            var response = await api.QueryParty(query);
            Assert.Equal("4713008497", response.suggestions[0].data.inn);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Fact]
        public async void SuggestPartyTypeTest()
        {
            var query = new PartySuggestQuery("витас");
            query.type = PartyType.INDIVIDUAL;
            var response = await api.QueryParty(query);
            Assert.Equal("470411980055", response.suggestions[0].data.inn);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }
    }
}