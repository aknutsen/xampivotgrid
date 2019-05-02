using Infragistics.Olap.FlatData;

namespace XamPivotGridTest
{
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

            hierarchyDescriptor.LevelDescriptors.Add(new HierarchyLevelDescriptor
            {
                LevelExpressionPath = propertyPath,
                LevelName = displayName,
                LevelDisplayName = displayName,
            });
            return hierarchyDescriptor;
        }
    }
}