﻿using System;
using System.ComponentModel.DataAnnotations;
using AC2C.Common.SerializationToolkit;
using AC2C.Entites.BaseEntites.Contracts;

namespace AC2C.Entites.BaseEntites
{
    [Serializable]
    public abstract class TitleShowTimeEntity : TitleShowTimeEntity<int>
    {
    }

    [Serializable]
    public abstract class TitleShowTimeEntity<TPrimaryKey> : Entity<TPrimaryKey>, IHasTitle, IHasShowTime
    {
        public DateTime? ShowStartDate { get; set; }
        public DateTime? ShowEndDate { get; set; }

        [MaxLength(75)]
        public string Name { get; set; }

        [MaxLength(150)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }
}