using DeviceTreeModule.ViewModels;
using LogXtreme.WinDsk.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceTreeModule.DesignTimeViewModels {
    public class DesignTimeDeviceTreeViewModel {

        public TreeItemViewModel<DeviceViewModel> DeviceTree {

            get {

                var devices = new List<DeviceModel>() {

                    new DeviceModel(){
                        Name =@"Device1",
                        Address=@"add1",
                        Port=@"port1",
                        Staus=@"active",
                        Type="USB"
                    },

                    new DeviceModel(){
                        Name =@"Device2",
                        Address=@"add2",
                        Port=@"port2",
                        Staus=@"active",
                        Type="ETHERNET"
                    }
                };


                var master = new DeviceModel() { Name = @"master" };
                var root = new DeviceViewModel(master);
                var deviceViewModels = devices.Select(d => new DeviceViewModel(d));

                var rootNode = new Node<DeviceViewModel>(root, null, null);
                var childNodes = deviceViewModels.Select(dvm => new Node<DeviceViewModel>(dvm, rootNode, null));
                childNodes.ToList().ForEach(cn => rootNode.Add(cn));

                var deviceTree = new TreeItemViewModel<DeviceViewModel>(rootNode, null);

                return deviceTree;
            }
        }
    }
}
