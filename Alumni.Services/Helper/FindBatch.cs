using Alumni.Domain.Enums;

namespace Alumni.Services.Helper
{
    public static class FindBatch
    {
        public static BatchEnum BatchNumber(string batch) => batch switch
        {
            "09" => BatchEnum.First,
            "10" => BatchEnum.Second,
            "11" => BatchEnum.Third,
            "12" => BatchEnum.Fourth,
            "13" => BatchEnum.Fifth,
            "14" => BatchEnum.Sixth,
            "15" => BatchEnum.Seventh,
            "16" => BatchEnum.Eighth,
            "17" => BatchEnum.Ninth,
            "18" => BatchEnum.Tenth,
            "19" => BatchEnum.Eleventh,
            "20" => BatchEnum.Twelth,
            "21" => BatchEnum.Thirteenth,
            "22" => BatchEnum.Fourteenth,
            "23" => BatchEnum.Fifteenth,
                _=> BatchEnum.None
        };
    }
}
