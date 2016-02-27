using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clever.Service
{
    public class ViewModel<T>
    {
        public T Model;
    }

    public class ListViewModel<T>
    {
        public IEnumerable<T> Models;
    }
}
