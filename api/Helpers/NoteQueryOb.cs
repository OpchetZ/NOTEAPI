using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class NoteQueryOb
    {
        public bool IsDecsending { get; set; } = true;
        public int PageNum { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}