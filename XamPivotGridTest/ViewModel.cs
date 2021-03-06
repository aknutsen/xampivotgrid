﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Infragistics.Olap;
using Infragistics.Olap.FlatData;

namespace XamPivotGridTest
{
    public class ViewModel
    {
        public FlatDataSource FlatDataSource { get; set; }

        public ViewModel()
        {            
            var years = Enumerable.Range(2019, 70).Select(v => v.ToString()).ToArray();
            var units = new[] {"dollar", "kroner", "euro", "yen"};
            var reportLevel1 = new[] {"Norway", "Sweden", "Denmark", "Germany", "Finland", "India"};
            var reportLevel2 = new[] { "City 1", "City 2", "City 3", "City 4", "City 5" };
            var metadataFactory = new DimensionMetadataFactory();
            var values = Enumerable.Range(1, 100).Select(v => RandomString(4)).ToArray();
            var numProperties = 100;
            var numValues = 750000;
            
            List<object> data = new List<object>();
            List<string> fields = Enumerable.Range(1, numProperties).Select(v => $"Item{v}").Concat(new []{"Value", "ReportLevel1", "ReportLevel2", "Unit", "Year"}).ToList();
            
            var myType = _typeBuilder.GenerateType(fields.Select(f => new DynamicTypePropertyInfo { PropertyName = f, PropertyType = f.Equals("Value") ? typeof(double) : typeof(object) }).ToList());

            for (int i = 0; i <numValues; i++)
            {
                var obj = Activator.CreateInstance(myType);
                myType.GetProperty("Unit").SetValue(obj, GetRandomFrom(units));
                myType.GetProperty("Year").SetValue(obj, GetRandomFrom(years));
                myType.GetProperty("ReportLevel1").SetValue(obj, GetRandomFrom(reportLevel1));
                myType.GetProperty("ReportLevel2").SetValue(obj, GetRandomFrom(reportLevel2));
                myType.GetProperty("Value").SetValue(obj, 1.0);
                data.Add(obj);
            }

            foreach (var prop in myType.GetProperties().Take(numProperties))
                for (int i = 0; i < numValues; i++)
                    prop.SetValue(data[i], GetRandomFrom(values), null);

            var cubeMetadata = new CubeMetadata {DataTypeFullName = _typeBuilder.DynamicTypeName, DisplayName = "Pivot"};

            fields.ForEach(field => cubeMetadata.DimensionSettings.Add(metadataFactory.Create(field, field, field, field.Equals("Value"))));

            FlatDataSource = new FlatDataSource
            {
                ItemsSource = data,
                CubesSettings = { cubeMetadata },
                DimensionsGenerationMode = DimensionsGenerationMode.Metadata,
                PreserveMembersOrder = false
            };
        }

        private string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        private string GetRandomFrom(string[] values)
        {
            return values[_random.Next(values.Length)];
        }

        private readonly Random _random = new Random();
        private readonly DynamicTypeBuilder _typeBuilder = new DynamicTypeBuilder
        {
            DynamicAssemblyName = "AppApp",
            DynamicTypeName = "MyDynamicType"
        };
    }
}