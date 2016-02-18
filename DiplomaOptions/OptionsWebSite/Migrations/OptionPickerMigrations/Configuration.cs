namespace OptionsWebSite.Migrations.OptionPickerMigrations
{
    using DiplomaDataModel.OptionPicker;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DiplomaDataModel.DataContext.OptionPickerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\OptionPickerMigrations";
        }

        protected override void Seed(DiplomaDataModel.DataContext.OptionPickerContext context)
        {
            context.Options.AddOrUpdate(
                o => o.Title,
                getOptions().ToArray());
            context.SaveChanges();

            context.YearTerms.AddOrUpdate(
                y => new { y.Year, y.Term },
                getYearTerm().ToArray());
            context.SaveChanges();
        }

        private List<Option> getOptions()
        {
            List<Option> options = new List<Option>()
            {
                new Option
                {
                    Title = "Web and Mobile",
                    IsActive = true,
                },
                new Option
                {
                    Title = "Client Server",
                    IsActive = true,
                },
                new Option
                {
                    Title = "Digital Processing",
                    IsActive = true,
                },
                new Option
                {
                    Title = "Tech Pro",
                    IsActive = false,
                },
                new Option
                {
                    Title = "Data Communications",
                    IsActive = true,
                },
                new Option
                {
                    Title = "Information Systems",
                    IsActive = true,
                },
                new Option
                {
                    Title = "Database",
                    IsActive = false,
                },
            };
            return options;
        }

        private List<YearTerm> getYearTerm()
        {
            List<YearTerm> yearTerm = new List<YearTerm>()
            {
                new YearTerm
                {
                    Year = 2015,
                    Term = 20,
                    IsDefault = false,
                },
                new YearTerm
                {
                    Year = 2015,
                    Term = 30,
                    IsDefault = false,
                },
                new YearTerm
                {
                    Year = 2016,
                    Term = 10,
                    IsDefault = false,
                },
                new YearTerm
                {
                    Year = 2016,
                    Term = 30,
                    IsDefault = true,
                },
            };
            return yearTerm;
        }

    }
}
