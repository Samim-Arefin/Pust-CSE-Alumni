﻿namespace Alumni.Services.Helper
{
    public static class FindSession
    {
        public static string Session(string session) => session switch
        {
            "09" => "2008-2009",
            "10" => "2009-2010",
            "11" => "2010-2011",
            "12" => "2011-2012",
            "13" => "2012-2013",
            "14" => "2013-2014",
            "15" => "2014-2015",
            "16" => "2015-2016",
            "17" => "2016-2017",
            "18" => "2017-2018",
            "19" => "2018-2019",
            "20" => "2019-2020",
            "21" => "2020-2021",
            "22" => "2021-2022",
            "23" => "2022-2023",
            _ => string.Empty
        };
    }
}
