using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfEmf.Interfaces
{
    /// <summary>
    /// http://stackoverflow.com/questions/12204614/load-wpf-ui-into-mvvm-application-from-plug-in-assembly
    /// </summary>
    public interface IBasePlugin
    {
        WorkSpaceViewModel ViewModel { get; }
        ResourceDictionary View { get; }
    }
}
