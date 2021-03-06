﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhoneBook.Web.Models
{
    public class PhoneBookViewModel : BaseViewModel
    {
        public int TotalEntries { get; set; }
        public List<EntryViewModel> Entries { get; set; }
    }
}
