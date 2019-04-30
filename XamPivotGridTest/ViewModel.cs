using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Infragistics.Olap.FlatData;

namespace XamPivotGridTest
{
    public class ViewModel
    {
        private List<Result> _data;

        public FlatDataSource FlatDataSource { get; set; }

        public ViewModel()
        {
            var metadataFactory = new DimensionMetadataFactory();
            _data = new List<Result>();

            for (int i = 0; i < 1000; i++)
            {
                _data.Add(new Result{ Name = $"data {i}", StartYear = 2019, Unit = "Money", Values = Enumerable.Range(1, 20).Select(v => (double)v).ToArray()});
            }

            var cubeMetadata = new CubeMetadata {DataTypeFullName = typeof(Result).FullName, DisplayName = "Pivot"};
            string[] propertyNames = {"Unit", "Value", "Name"};
            foreach (var propName in propertyNames)
            {
                var property = metadataFactory.Create(propName, propName, propName, false);
                cubeMetadata.DimensionSettings.Add(property);
            }
            FlatDataSource = new FlatDataSource
            {
                ItemsSource = _data,
                CubesSettings = { cubeMetadata },
                DimensionsGenerationMode = DimensionsGenerationMode.Metadata,
                PreserveMembersOrder = false
            };
        }
    }

    public class Result
    {
        public int StartYear { get; set; }

        public double[] Values { get; set; }

        public string Name { get; set; }

        public string Unit { get; set; }
    }

    public class DimensionMetadataFactory
    {
        public DimensionMetadata Create(string propertyName, string propertyPath, string displayName, bool isMeasure)
        {
            return new DimensionMetadata
            {
                DimensionType = isMeasure ? DimensionType.Measure : DimensionType.Dimension,
                SourcePropertyName = propertyName,
                DisplayName = displayName,
                DisplayFormat = isMeasure ? "{0:#,##0.00}" : null,
                HierarchyDescriptors = { CreateHierarchyDescriptor(propertyName, propertyPath, displayName) },
            };
        }

        private HierarchyDescriptor CreateHierarchyDescriptor(string propertyName, string propertyPath, string displayName)
        {
            var hierarchyDescriptor = new HierarchyDescriptor
            {
                HierarchyName = displayName,
                HierarchyDisplayName = displayName,
                SourcePropertyName = propertyName
            };

            var level = new HierarchyLevelDescriptor
            {
                LevelExpressionPath = propertyPath,
                LevelName = displayName,
                LevelDisplayName = displayName,
            };

            //if (_sortMappings.ContainsKey(displayName))
            //{
            //    var mapping = _sortMappings[displayName];
            //    level.OrderByKeyExpression = (Expression<Func<object, string>>)(p => GetSortName(p, propertyName, mapping));
            //}

            hierarchyDescriptor.LevelDescriptors.Add(level);

            return hierarchyDescriptor;
        }
    }
}
