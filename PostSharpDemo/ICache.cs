using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSharpDemo
{
    public interface ICache
    {
        object this[string key] { get; set; }
    }
}