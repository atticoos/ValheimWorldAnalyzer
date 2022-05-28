using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValheimWorldAnalyzer.Analyzers {
  internal abstract class Analyzer {
    protected ZdoManLite zdoMan;

    public Analyzer(ZdoManLite zdoMan) {
      this.zdoMan = zdoMan;
    }

    public abstract void Run();
  }
}
