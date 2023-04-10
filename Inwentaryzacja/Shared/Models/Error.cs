using System;
using System.Collections.Generic;

namespace Inwentaryzacja.Shared.Models
{
    public partial class Error
    {
        public int Id { get; set; }
        public string ErrorText { get; set; } = null!;
        public DateTime Time { get; set; }
    }
}
