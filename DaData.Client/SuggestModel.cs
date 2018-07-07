using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace DaData.Client
{
    public sealed class ContentType
    {
        public static readonly ContentType JSON = new ContentType("application/json", DataFormat.Json);

        private ContentType(string name, DataFormat format)
        {
            Name = name;
            Format = format;
        }

        public string Name { get; }

        public DataFormat Format { get; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class SuggestQuery
    {
        public SuggestQuery(string query)
        {
            Query = query;
            Count = 5;
        }

        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }

    public class AddressSuggestQuery : SuggestQuery
    {
        public AddressSuggestQuery(string query) : base(query) { }

        [JsonProperty("locations")]
        public AddressData[] Locations { get; set; }

        [JsonProperty("locations_boost")]
        public AddressData[] LocationsBoost { get; set; }

        [JsonProperty("from_bound")]
        public AddressBound FromBound { get; set; }

        [JsonProperty("to_bound")]
        public AddressBound ToBound { get; set; }

        [JsonProperty("restrict_value")]
        public bool RestrictValue { get; set; }
    }

    public class BankSuggestQuery : SuggestQuery
    {
        public BankSuggestQuery(string query) : base(query) { }

        [JsonProperty("status")]
        public PartyStatus[] Status { get; set; }

        [JsonProperty("type")]
        public BankType[] Type { get; set; }
    }

    public class FioSuggestQuery : SuggestQuery
    {
        public FioSuggestQuery(string query) : base(query) { }

        [JsonProperty("parts")]
        public FioPart[] Parts { get; set; }
    }

    public class PartySuggestQuery : SuggestQuery
    {
        public PartySuggestQuery(string query) : base(query) { }

        [JsonProperty("locations")]
        public AddressData[] Locations { get; set; }

        [JsonProperty("locations_boost")]
        public AddressData[] LocationsBoost { get; set; }

        [JsonProperty("status")]
        public PartyStatus[] Status { get; set; }

        [JsonProperty("type")]
        public PartyType? Type { get; set; }
    }

    public class AddressData
    {
        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("postal_code")]
        public string PostalCode { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }


        [JsonProperty("region_fias_id")]
        public string RegionFiasId { get; set; }

        [JsonProperty("region_kladr_id")]
        public string RegionKladrId { get; set; }

        [JsonProperty("region_with_type")]
        public string RegionWithType { get; set; }

        [JsonProperty("region_type")]
        public string RegionType { get; set; }

        [JsonProperty("region_type_full")]
        public string RegionTypeFull { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }


        [JsonProperty("area_fias_id")]
        public string AreaFiasId { get; set; }

        [JsonProperty("area_kladr_id")]
        public string AreaKladrId { get; set; }

        [JsonProperty("area_with_type")]
        public string AreaWithType { get; set; }

        [JsonProperty("area_type")]
        public string AreaType { get; set; }

        [JsonProperty("area_type_full")]
        public string AreaTypeFull { get; set; }

        [JsonProperty("area")]
        public string Area { get; set; }


        [JsonProperty("city_fias_id")]
        public string CityFiasId { get; set; }

        [JsonProperty("city_kladr_id")]
        public string CityKladrId { get; set; }

        [JsonProperty("city_with_type")]
        public string CityWithType { get; set; }

        [JsonProperty("city_type")]
        public string CityType { get; set; }

        [JsonProperty("city_type_full")]
        public string CityTypeFull { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("city_area")]
        public string CityArea { get; set; }


        [JsonProperty("city_district_fias_id")]
        public string CityDistrictFiasId { get; set; }

        [JsonProperty("city_district_kladr_id")]
        public string CityDistrictKladrId { get; set; }

        [JsonProperty("city_district_with_type")]
        public string CityDistrictWithType { get; set; }

        [JsonProperty("city_district_type")]
        public string CityDistrictType { get; set; }

        [JsonProperty("city_district_type_full")]
        public string CityDistrictTypeFull { get; set; }

        [JsonProperty("city_district")]
        public string CityDistrict { get; set; }


        [JsonProperty("settlement_fias_id")]
        public string SettlementFiasId { get; set; }

        [JsonProperty("settlement_kladr_id")]
        public string SettlementKladrId { get; set; }

        [JsonProperty("settlement_with_type")]
        public string SettlementWithType { get; set; }

        [JsonProperty("settlement_type")]
        public string SettlementType { get; set; }

        [JsonProperty("settlement_type_full")]
        public string SettlementTypeFull { get; set; }

        [JsonProperty("settlement")]
        public string Settlement { get; set; }


        [JsonProperty("street_fias_id")]
        public string StreetFiasId { get; set; }

        [JsonProperty("street_kladr_id")]
        public string StreetKladrId { get; set; }

        [JsonProperty("street_with_type")]
        public string StreetWithType { get; set; }

        [JsonProperty("street_type")]
        public string StreetType { get; set; }

        [JsonProperty("street_type_full")]
        public string StreetTypeFull { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }


        [JsonProperty("house_fias_id")]
        public string HouseFiasId { get; set; }

        [JsonProperty("house_kladr_id")]
        public string HouseKladrId { get; set; }

        [JsonProperty("house_type")]
        public string HouseType { get; set; }

        [JsonProperty("house_type_full")]
        public string HouseTypeFull { get; set; }

        [JsonProperty("house")]
        public string House { get; set; }


        [JsonProperty("block_type")]
        public string BlockType { get; set; }

        [JsonProperty("block_type_full")]
        public string BlockTypeFull { get; set; }

        [JsonProperty("block")]
        public string Block { get; set; }


        [JsonProperty("flat_type")]
        public string FlatType { get; set; }

        [JsonProperty("flat_type_full")]
        public string FlatTypeFull { get; set; }

        [JsonProperty("flat")]
        public string Flat { get; set; }

        [JsonProperty("flat_area")]
        public string FlatArea { get; set; }

        [JsonProperty("square_meter_price")]
        public string SquareMeterPrice { get; set; }

        [JsonProperty("flat_price")]
        public string FlatPrice { get; set; }


        [JsonProperty("postal_box")]
        public string PostalBox { get; set; }

        [JsonProperty("fias_id")]
        public string FiasId { get; set; }

        [JsonProperty("fias_level")]
        public string FiasLevel { get; set; }

        [JsonProperty("kladr_id")]
        public string KladrId { get; set; }

        [JsonProperty("capital_marker")]
        public string CapitalMarker { get; set; }


        [JsonProperty("okato")]
        public string Okato { get; set; }

        [JsonProperty("oktmo")]
        public string Oktmo { get; set; }

        [JsonProperty("tax_office")]
        public string TaxOffice { get; set; }

        [JsonProperty("tax_office_legal")]
        public string TaxOfficeLegal { get; set; }


        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("geo_lat")]
        public string GeoLat { get; set; }

        [JsonProperty("geo_lon")]
        public string GeoLon { get; set; }

        [JsonProperty("qc_geo")]
        public string QcGeo { get; set; }


        [JsonProperty("beltway_hit")]
        public string BeltwayHit { get; set; }

        [JsonProperty("beltway_distance")]
        public string BeltwayDistance { get; set; }


        [JsonProperty("history_values")]
        public List<string> HistoryValues { get; set; }


        [JsonProperty("metro")]
        public List<MetroData> Metro { get; set; }
    }

    public class MetroData
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("line")]
        public string Line { get; set; }

        [JsonProperty("distance")]
        public decimal Distance { get; set; }
    }

    public class AddressBound
    {
        public AddressBound(string name)
        {
            Value = name;
        }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class BankData
    {
        [JsonProperty("address")]
        public Suggestion<AddressData> Address { get; set; }


        [JsonProperty("bic")]
        public string Bic { get; set; }

        [JsonProperty("swift")]
        public string Swift { get; set; }

        [JsonProperty("registration_number")]
        public string RegistrationNumber { get; set; }

        [JsonProperty("correspondent_account")]
        public string CorrespondentAccount { get; set; }


        [JsonProperty("name")]
        public BankNameData Name { get; set; }

        [JsonProperty("okpo")]
        public string Okpo { get; set; }

        [JsonProperty("opf")]
        public BankOpfData Opf { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("rkc")]
        public string Rkc { get; set; }

        [JsonProperty("state")]
        public PartyStateData State { get; set; }

    }

    public class BankNameData
    {
        [JsonProperty("payment")]
        public string Payment { get; set; }

        [JsonProperty("full")]
        public string Full { get; set; }

        [JsonProperty("short")]
        public string Short { get; set; }
    }

    public class BankOpfData
    {
        [JsonProperty("type")]
        public BankType Type { get; set; }

        [JsonProperty("full")]
        public string Full { get; set; }

        [JsonProperty("short")]
        public string Short { get; set; }
    }

    public enum BankType
    {
        BANK,
        NKO,
        BANK_BRANCH,
        NKO_BRANCH,
        RKC,
        OTHER
    }

    public class EmailData
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("local")]
        public string Local { get; set; }

        [JsonProperty("domain")]
        public string Domain { get; set; }
    }

    public class FioData
    {
        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("patronymic")]
        public string Patronymic { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }
    }

    public enum FioPart
    {
        SURNAME,
        NAME,
        PATRONYMIC
    }

    public class PartyData
    {
        [JsonProperty("address")]
        public Suggestion<AddressData> Address { get; set; }


        [JsonProperty("branch_count")]
        public string BranchCount { get; set; }

        [JsonProperty("branch_type")]
        public PartyBranchType BranchType { get; set; }


        [JsonProperty("inn")]
        public string Inn { get; set; }

        [JsonProperty("kpp")]
        public string Kpp { get; set; }

        [JsonProperty("ogrn")]
        public string Ogrn { get; set; }

        [JsonProperty("ogrn_date")]
        public string OgrnDate { get; set; }

        [JsonProperty("hid")]
        public string Hid { get; set; }


        [JsonProperty("management")]
        public PartyManagementData Management { get; set; }

        [JsonProperty("name")]
        public PartyNameData Name { get; set; }


        [JsonProperty("okpo")]
        public string Okpo { get; set; }

        [JsonProperty("okved")]
        public string Okved { get; set; }

        [JsonProperty("okved_type")]
        public string OkvedType { get; set; }


        [JsonProperty("opf")]
        public PartyOpfData Opf { get; set; }

        [JsonProperty("state")]
        public PartyStateData State { get; set; }

        [JsonProperty("type")]
        public PartyType Type { get; set; }
    }

    public enum PartyBranchType
    {
        MAIN,
        BRANCH
    }

    public class PartyManagementData
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("post")]
        public string Post { get; set; }
    }

    public class PartyNameData
    {
        [JsonProperty("full_with_opf")]
        public string FullWithOpf { get; set; }

        [JsonProperty("short_with_opf")]
        public string ShortWithOpf { get; set; }

        [JsonProperty("latin")]
        public string Latin { get; set; }

        [JsonProperty("full")]
        public string Full { get; set; }

        [JsonProperty("short")]
        public string Short { get; set; }
    }

    public class PartyOpfData
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("full")]
        public string Full { get; set; }

        [JsonProperty("short")]
        public string Short { get; set; }
    }

    public class PartyStateData
    {
        [JsonProperty("actuality_date")]
        public string ActualityDate { get; set; }

        [JsonProperty("registration_date")]
        public string RegistrationDate { get; set; }

        [JsonProperty("liquidation_date")]
        public string LiquidationDate { get; set; }

        [JsonProperty("status")]
        public PartyStatus Status { get; set; }
    }

    public enum PartyStatus
    {
        ACTIVE,
        LIQUIDATING,
        LIQUIDATED
    }

    public enum PartyType
    {
        LEGAL,
        INDIVIDUAL
    }

    public class Suggestion<T>
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("unrestricted_value")]
        public string UnrestrictedValue { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }

    public class SuggestAddressResponse
    {
        [JsonProperty("suggestions")]
        public List<Suggestion<AddressData>> Suggestions { get; set; }
    }

    public class SuggestBankResponse
    {
        [JsonProperty("suggestions")]
        public List<Suggestion<BankData>> Suggestions { get; set; }
    }

    public class SuggestEmailResponse
    {
        [JsonProperty("suggestions")]
        public List<Suggestion<EmailData>> Suggestions { get; set; }
    }

    public class SuggestFioResponse
    {
        [JsonProperty("suggestions")]
        public List<Suggestion<FioData>> Suggestions { get; set; }
    }

    public class SuggestPartyResponse
    {
        [JsonProperty("suggestions")]
        public List<Suggestion<PartyData>> Suggestions { get; set; }
    }
}