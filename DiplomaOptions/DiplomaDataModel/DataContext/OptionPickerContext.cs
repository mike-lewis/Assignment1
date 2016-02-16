﻿using DiplomaDataModel.OptionPicker;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaDataModel.DataContext
{
    public class OptionPickerContext : DbContext
    {
        public OptionPickerContext() : base("DefaultConnection") { }

        public DbSet<Option> Options { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<YearTerm> YearTerms { get; set; }
    }
}
