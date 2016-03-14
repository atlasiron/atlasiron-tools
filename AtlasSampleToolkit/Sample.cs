using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtlasSampleToolkit
{
    public class Sample
    {
        public Sample()
        {
        }

        public DateTime? SampledOn { get; set; }
        public String Shift { get; set; }
        public String Pit { get; set; }
        public String Bench { get; set; }
        public String ShotNo { get; set; }
        public String HoleNo { get; set; }
        public String SampleId { get; set; }
        public Boolean Sizings { get; set; }
        public Boolean LumpSizings { get; set; }
        public Boolean Assays { get; set; }
        public Boolean Moisture { get; set; }

        public Double Distance { get; set; }
        public Double East { get; set; }
        public Double North { get; set; }
        public Double Elevation { get; set; }

        public DateTime? DespatchedFromMine { get; set; }
        public DateTime? ArrivedForPreparation { get; set; }
        public DateTime? DespatchedFromPreparation { get; set; }
        public DateTime? ArrivedForAnalysis { get; set; }
    }
}
