using System;
using System.Collections.Generic;

namespace dotnet_infantsSchool.Entitys
{
    public partial class ActivityPicture
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int? ActivityId { get; set; }

        public virtual Activity Activity { get; set; }
    }
}
