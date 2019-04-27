using System;
using System.Collections.Generic;
using System.Linq;
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
            _data = new List<Result>();

            for (int i = 0; i < 1000; i++)
            {
                _data.Add(new Result{ Name = $"data {i}", StartYear = 2019, Unit = "Money", Values = Enumerable.Range(1, 20).Select(v => (double)v).ToArray()});
            }

            FlatDataSource = new FlatDataSource
            {
                ItemsSource = _data,
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
}
