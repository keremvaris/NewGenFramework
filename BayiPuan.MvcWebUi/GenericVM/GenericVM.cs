using System.Collections.Generic;

namespace BayiPuan.MvcWebUi.GenericVM
{
    public class GenericVM
    {
        public TableMeta Meta { get; private set; }

        public GenericVM(TableMeta meta)
        {
            this.Meta = meta;
        }

        public IDictionary<string, object> Data { get; set; }

    }
}