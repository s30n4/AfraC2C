using System;

namespace AC2C.Entites.BaseEntites.Contracts
{
    public interface IHasShowTime
    {
        DateTime? ShowStartDate { get; set; }

        DateTime? ShowEndDate { get; set; }
    }
}