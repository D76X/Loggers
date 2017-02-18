using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

/// <summary>
/// http://blogs.msmvps.com/bsonnino/2013/08/31/developing-modular-applications-with-mef/
/// </summary>
namespace WpfMef.Interfaces
{
    public interface IModule
    {
        string Title { get; }
        UIElement Content { get; }
    }
}
